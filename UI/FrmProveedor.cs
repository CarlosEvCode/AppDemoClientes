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
    public partial class FrmProveedor : Form
    {
        private readonly ProveedorBol _bol;
        private int _idActual; //0 = nuevo, > 0 = actualizar
        private bool _cargandoGrid;

        Color colorActivo = Color.LightYellow;
        Color colorBase = Color.White;

        public FrmProveedor(ProveedorBol bol)
        {
            InitializeComponent();
            _bol = bol;

            Load += async (_, _) => await CargarAsync();
        }

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
            Control? foco = null;

            try
            {
                UseWaitCursor = true;
                await accion();
            }
            catch (ReglaNegocioException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                foco = ex.Campo switch
                {
                    nameof(Proveedor.RazonSocial) => txtRazonSocial,
                    nameof(Proveedor.Ruc) => txtRuc,
                    nameof(Proveedor.Telefono) => txtTelefono,
                    nameof(Proveedor.Direccion) => txtDireccion,
                    nameof(Proveedor.Correo) => txtCorreo,
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
            }

            foco?.Focus();
        }

        private Task CargarAsync() => EjecutarAsync(CargarListAsync);

        private async Task GuardarAsync()
        {
            var proveedor = new Proveedor
            {
                Id = _idActual,
                RazonSocial = txtRazonSocial.Text,
                Ruc = txtRuc.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text,
                Correo = txtCorreo.Text
            };

            await EjecutarAsync(async () =>
            {
                await _bol.GuardarAsync(proveedor);
                await CargarListAsync();
                LimpiarFormulario();

                MessageBox.Show("Registro guardado correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        private void LimpiarFormulario()
        {
            _idActual = 0;
            txtRazonSocial.Clear();
            txtRuc.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtCorreo.Clear();
            grid.ClearSelection();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtRazonSocial.Focus();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await GuardarAsync();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            await EliminarAsync();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await CargarAsync();
        }

        private async Task EliminarAsync()
        {
            if (_idActual == 0) return;

            string datosProveedor = txtRazonSocial.Text;
            DialogResult rpta = MessageBox.Show($"¿Desea eliminar al proveedor {datosProveedor}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rpta == DialogResult.No) return;

            var id = _idActual;
            await EjecutarAsync(async () =>
            {
                await _bol.EliminarAsync(id);
                await CargarListAsync();
                LimpiarFormulario();
                MessageBox.Show("Registro eliminado correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloDigitos(sender, e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloDigitos(sender, e);
        }

        private static void SoloDigitos(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBox_Enter(object? sender, EventArgs e)
        {
            if (sender is TextBox tb) tb.BackColor = colorActivo;
        }

        private void TextBox_Leave(object? sender, EventArgs e)
        {
            if (sender is TextBox tb) tb.BackColor = colorBase;
        }

        private void configurarGrid()
        {
            if (grid.Columns.Count < 6) return;

            grid.Columns[0].Visible = false; //Id
            grid.Columns[1].Width = 250; //RazonSocial
            grid.Columns[2].Width = 120; //Ruc
            grid.Columns[3].Width = 110; //Telefono
            grid.Columns[4].Width = 200; //Direccion
            grid.Columns[5].Width = 200; //Correo

            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            MostrarSeleccionado();
        }

        private void MostrarSeleccionado()
        {
            if (_cargandoGrid || grid.SelectedRows.Count == 0) return;
            if (grid.SelectedRows[0].DataBoundItem is not Proveedor p) return;

            _idActual = p.Id;
            txtRazonSocial.Text = p.RazonSocial;
            txtRuc.Text = p.Ruc;
            txtTelefono.Text = p.Telefono;
            txtDireccion.Text = p.Direccion;
            txtCorreo.Text = p.Correo;
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnBuscar.PerformClick();
            }
        }
    }
}
