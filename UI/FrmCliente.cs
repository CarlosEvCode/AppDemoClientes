using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BOL;
using DAL;
using Entidades;

namespace UI
{
    public partial class FrmCliente : Form
    {
        private readonly ClienteBol _bol;
        private int _idActual; //Si es 0 -> Nuevo registro, Si es > 0 -> Actualizar registro
        private bool _cargandoGrid; //Evita la seleccion mientras se renderiza el grid

        //Definir constantes
        Color colorActivo = Color.LightYellow;
        Color colorBase = Color.White;

        //Constructor
        public FrmCliente(ClienteBol bol)
        {

            InitializeComponent(); //Construir UI
            _bol = bol;

            //Enlace EVENTO
            Load += async (_, _) => await CargarAsync();
        }

        //Enviar la informacion obtenida del API REST al grid
        private async Task CargarListAsync()
        {
            var lista = await _bol.ListarAsync(txtBuscar.Text);

            _cargandoGrid = true;
            grid.DataSource = lista;
            grid.ClearSelection();
            _cargandoGrid = false;

            configurarGrid();
            LimpiarFormulario();

        }

        private async Task EjecutarAsync(Func<Task> accion)
        {
            Control? foco = null; //ReglaNegocioException -> Error -> Control

            try
            {
                UseWaitCursor = true;
                Enabled = true;
                await accion();
            }
            catch (ReglaNegocioException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //ReglaNegocio solo nos indica el nombre del campo: apellidos, nombres, dni, etc. (txtApellidos, txtNombres, txtDni, etc.)
                foco = ex.Campo switch
                {
                    nameof(Cliente.Apellidos) => txtApellidos,
                    nameof(Cliente.Nombres) => txtNombres,
                    nameof(Cliente.Dni) => txtDni,
                    nameof(Cliente.Direccion) => txtDireccion,
                    nameof(Cliente.Correo) => txtCorreo,
                    nameof(Cliente.Telefono) => txtTelefono,
                    _ => null
                };
            }
            catch (ApiException ex)
            {
                MessageBox.Show(ex.Message, " Error en el servidor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("El servidor está demasiado ocupado", "Tiempo agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "No hay conexión al servidor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                UseWaitCursor = false;
                Enabled = true;
            }

            foco?.Focus(); //Elemento activo
        }

        private Task CargarAsync() => EjecutarAsync(CargarListAsync);

        private async Task GuardarAsync()
        {
            var cliente = new Cliente
            {
                Id = _idActual,
                Apellidos = txtApellidos.Text,
                Nombres = txtNombres.Text,
                Dni = txtDni.Text,
                Telefono = txtTelefono.Text,
                Correo = txtCorreo.Text,
                Direccion = txtDireccion.Text
            };

            await EjecutarAsync(async () =>
            {
                await _bol.GuardarAsync(cliente);
                await CargarListAsync(); // Actualizar grid
                LimpiarFormulario();

                MessageBox.Show("Registro guardado correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        private void LimpiarFormulario()
        {
            _idActual = 0;
            txtApellidos.Clear();
            txtNombres.Clear();
            txtDni.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            grid.ClearSelection();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await GuardarAsync();
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloDigitos(sender, e);

        }

        /// <summary>
        /// Bloquea cualquier caracter ingresado que no sea un digito (0-9) en el TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SoloDigitos(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; //Bloquear el caracter ingresado
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloDigitos(sender, e);
        }

        private void configurarGrid()
        {
            // Configuración del grid
            //Ancho de las columnas
            grid.Columns[0].Visible = false; //Id
            grid.Columns[1].Width = 170; //Apellidos
            grid.Columns[2].Width = 170; //Nombres
            grid.Columns[5].Width = 200; //Correo
            grid.Columns[6].Width = 200; //Direccion

            //Filas cebreadas
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {

        }

        private void txtApellidos_Enter(object sender, EventArgs e)
        {
            txtApellidos.BackColor = colorActivo;
        }

        private void txtApellidos_Leave(object sender, EventArgs e)
        {
            txtApellidos.BackColor = colorBase;
        }

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            //Este evento se ejecuta cuando cambias la fila activa
            MostrarSeleccionado();
        }

        private void MostrarSeleccionado()
        {
            if (_cargandoGrid || grid.SelectedRows.Count == 0) return; //No hay fila seleccionada
            if (grid.SelectedRows[0].DataBoundItem is not Cliente c) return; //API

            _idActual = c.Id;
            txtApellidos.Text = c.Apellidos;
            txtNombres.Text = c.Nombres;
            txtDni.Text = c.Dni;
            txtTelefono.Text = c.Telefono;
            txtCorreo.Text = c.Correo;
            txtDireccion.Text = c.Direccion;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            await EliminarAsync();
        }

        private async Task EliminarAsync()
        {
            if (_idActual == 0) return; //No hay registro activo

            string datosCliente = $"{txtApellidos.Text} {txtNombres.Text}";
            DialogResult rpta = MessageBox.Show($"¿Desea eliminar al cliente {datosCliente}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rpta == DialogResult.No) return;

            var id = _idActual;
            //1. Eliminar > 2. Actualizar Grid > 3. Limpiar formulario > 4. Mensaje de confirmacion
            await EjecutarAsync(async () =>
            {
                await _bol.EliminarAsync(id);
                await CargarListAsync(); // Actualizar grid
                LimpiarFormulario();
                MessageBox.Show("Registro eliminado correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }


        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await CargarAsync();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            //Keypress (presiona, libera) vs Keydown (presiona)
            //Identficar el Enter
            if (e.KeyCode == Keys.Enter)
            {
               e.SuppressKeyPress = true; //Evita el sonido del Enter
                btnBuscar.PerformClick(); //Simula el click del boton
            }
        }
    }
}
