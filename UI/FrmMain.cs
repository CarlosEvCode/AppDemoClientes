using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BOL;

namespace UI
{
    public partial class FrmMain : Form
    {
        private readonly ClienteBol _clienteBol;
        private readonly ProveedorBol _proveedorBol;

        public FrmMain(ClienteBol clienteBol, ProveedorBol proveedorBol)
        {
            InitializeComponent();

            _clienteBol = clienteBol;
            _proveedorBol = proveedorBol;
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            using var frm = new FrmCliente(_clienteBol);
            frm.ShowDialog(this);
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            using var frm = new FrmProveedor(_proveedorBol);
            frm.ShowDialog(this);
        }
    }
}
