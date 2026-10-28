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

            grid.DataSource = lista;
            grid.ClearSelection();

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
    }
}
