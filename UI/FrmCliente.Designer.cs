namespace UI
{
    partial class FrmCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnBuscar = new Button();
            label1 = new Label();
            grid = new DataGridView();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            txtApellidos = new TextBox();
            txtDni = new TextBox();
            txtNombres = new TextBox();
            txtTelefono = new TextBox();
            txtCorreo = new TextBox();
            txtDireccion = new TextBox();
            txtBuscar = new TextBox();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(442, 12);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(100, 30);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 9);
            label1.Name = "label1";
            label1.Size = new Size(52, 19);
            label1.TabIndex = 0;
            label1.Text = "Buscar:";
            // 
            // grid
            // 
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.Location = new Point(41, 62);
            grid.Name = "grid";
            grid.Size = new Size(914, 308);
            grid.TabIndex = 18;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(61, 408);
            label2.Name = "label2";
            label2.Size = new Size(67, 19);
            label2.TabIndex = 3;
            label2.Text = "Apellidos:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(281, 402);
            label3.Name = "label3";
            label3.Size = new Size(68, 19);
            label3.TabIndex = 5;
            label3.Text = "Nombres:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(57, 467);
            label4.Name = "label4";
            label4.Size = new Size(36, 19);
            label4.TabIndex = 9;
            label4.Text = "DNI:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(277, 468);
            label5.Name = "label5";
            label5.Size = new Size(63, 19);
            label5.TabIndex = 11;
            label5.Text = "Teléfono:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(472, 404);
            label6.Name = "label6";
            label6.Size = new Size(54, 19);
            label6.TabIndex = 7;
            label6.Text = "Correo:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(481, 474);
            label7.Name = "label7";
            label7.Size = new Size(68, 19);
            label7.TabIndex = 13;
            label7.Text = "Dirección:";
            // 
            // txtApellidos
            // 
            txtApellidos.Location = new Point(68, 436);
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(186, 25);
            txtApellidos.TabIndex = 4;
            // 
            // txtDni
            // 
            txtDni.Location = new Point(68, 489);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(186, 25);
            txtDni.TabIndex = 10;
            // 
            // txtNombres
            // 
            txtNombres.Location = new Point(277, 436);
            txtNombres.Name = "txtNombres";
            txtNombres.Size = new Size(186, 25);
            txtNombres.TabIndex = 6;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(277, 489);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(186, 25);
            txtTelefono.TabIndex = 12;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(481, 436);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(186, 25);
            txtCorreo.TabIndex = 8;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(481, 489);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(186, 25);
            txtDireccion.TabIndex = 14;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(86, 9);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(340, 25);
            txtBuscar.TabIndex = 1;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(77, 538);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(94, 34);
            btnNuevo.TabIndex = 15;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(288, 543);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 34);
            btnGuardar.TabIndex = 16;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(477, 546);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 34);
            btnEliminar.TabIndex = 17;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // FrmCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 611);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(btnNuevo);
            Controls.Add(txtBuscar);
            Controls.Add(txtDireccion);
            Controls.Add(txtTelefono);
            Controls.Add(txtCorreo);
            Controls.Add(txtNombres);
            Controls.Add(txtDni);
            Controls.Add(txtApellidos);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(grid);
            Controls.Add(label1);
            Controls.Add(btnBuscar);
            Font = new Font("Segoe UI", 10F);
            Name = "FrmCliente";
            Text = "FrmCliente";
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBuscar;
        private Label label1;
        private DataGridView grid;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox txtApellidos;
        private TextBox txtDni;
        private TextBox txtNombres;
        private TextBox txtTelefono;
        private TextBox txtCorreo;
        private TextBox txtDireccion;
        private TextBox txtBuscar;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEliminar;
    }
}