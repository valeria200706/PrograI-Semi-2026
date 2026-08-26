namespace Clinica_progra
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDui;
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.Label lblcondicion;
        private System.Windows.Forms.Label lblEspecificacion;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblPendientes;
        private System.Windows.Forms.Label lblBuscar;

        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDui;
        private System.Windows.Forms.TextBox txtEspecificacion;
        private System.Windows.Forms.TextBox txtBuscar;

        private System.Windows.Forms.NumericUpDown nudEdad;
        private System.Windows.Forms.ComboBox cmbCondicion;

        private System.Windows.Forms.DataGridView dgvPacientes;

        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnAtender;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDui = new System.Windows.Forms.Label();
            this.lblEdad = new System.Windows.Forms.Label();
            this.lblcondicion = new System.Windows.Forms.Label();
            this.lblEspecificacion = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblPendientes = new System.Windows.Forms.Label();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDui = new System.Windows.Forms.TextBox();
            this.txtEspecificacion = new System.Windows.Forms.TextBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.nudEdad = new System.Windows.Forms.NumericUpDown();
            this.cmbCondicion = new System.Windows.Forms.ComboBox();
            this.dgvPacientes = new System.Windows.Forms.DataGridView();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnAtender = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudEdad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacientes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(32, 27);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(406, 54);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "CLÍNICA - REGISTRO";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNombre.Location = new System.Drawing.Point(39, 113);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(216, 28);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre del paciente:";
            // 
            // lblDui
            // 
            this.lblDui.AutoSize = true;
            this.lblDui.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDui.Location = new System.Drawing.Point(334, 113);
            this.lblDui.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDui.Name = "lblDui";
            this.lblDui.Size = new System.Drawing.Size(183, 28);
            this.lblDui.TabIndex = 3;
            this.lblDui.Text = "DUI / Documento:";
            // 
            // lblEdad
            // 
            this.lblEdad.AutoSize = true;
            this.lblEdad.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEdad.Location = new System.Drawing.Point(591, 113);
            this.lblEdad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEdad.Name = "lblEdad";
            this.lblEdad.Size = new System.Drawing.Size(63, 28);
            this.lblEdad.TabIndex = 5;
            this.lblEdad.Text = "Edad:";
            // 
            // lblcondicion
            // 
            this.lblcondicion.AutoSize = true;
            this.lblcondicion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblcondicion.Location = new System.Drawing.Point(39, 200);
            this.lblcondicion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblcondicion.Name = "lblcondicion";
            this.lblcondicion.Size = new System.Drawing.Size(105, 28);
            this.lblcondicion.TabIndex = 7;
            this.lblcondicion.Text = "Situación:";
            // 
            // lblEspecificacion
            // 
            this.lblEspecificacion.AutoSize = true;
            this.lblEspecificacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEspecificacion.Location = new System.Drawing.Point(334, 200);
            this.lblEspecificacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEspecificacion.Name = "lblEspecificacion";
            this.lblEspecificacion.Size = new System.Drawing.Size(151, 28);
            this.lblEspecificacion.TabIndex = 9;
            this.lblEspecificacion.Text = "Especificación:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(39, 753);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(237, 28);
            this.lblTotal.TabIndex = 18;
            this.lblTotal.Text = "Pacientes registrados: 0";
            // 
            // lblPendientes
            // 
            this.lblPendientes.AutoSize = true;
            this.lblPendientes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPendientes.Location = new System.Drawing.Point(386, 753);
            this.lblPendientes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPendientes.Name = "lblPendientes";
            this.lblPendientes.Size = new System.Drawing.Size(222, 28);
            this.lblPendientes.TabIndex = 19;
            this.lblPendientes.Text = "Pacientes en espera: 0";
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBuscar.Location = new System.Drawing.Point(733, 113);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(167, 28);
            this.lblBuscar.TabIndex = 11;
            this.lblBuscar.Text = "Buscar paciente:";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.Location = new System.Drawing.Point(39, 147);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(269, 34);
            this.txtNombre.TabIndex = 2;
            // 
            // txtDui
            // 
            this.txtDui.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDui.Location = new System.Drawing.Point(334, 147);
            this.txtDui.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDui.MaxLength = 20;
            this.txtDui.Name = "txtDui";
            this.txtDui.Size = new System.Drawing.Size(230, 34);
            this.txtDui.TabIndex = 4;
            // 
            // txtEspecificacion
            // 
            this.txtEspecificacion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEspecificacion.Location = new System.Drawing.Point(334, 233);
            this.txtEspecificacion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEspecificacion.Name = "txtEspecificacion";
            this.txtEspecificacion.Size = new System.Drawing.Size(359, 34);
            this.txtEspecificacion.TabIndex = 10;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(733, 147);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(295, 34);
            this.txtBuscar.TabIndex = 12;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // nudEdad
            // 
            this.nudEdad.Location = new System.Drawing.Point(591, 147);
            this.nudEdad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudEdad.Maximum = new decimal(new int[] {
            110,
            0,
            0,
            0});
            this.nudEdad.Name = "nudEdad";
            this.nudEdad.Size = new System.Drawing.Size(103, 26);
            this.nudEdad.TabIndex = 6;
            // 
            // cmbCondicion
            // 
            this.cmbCondicion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCondicion.FormattingEnabled = true;
            this.cmbCondicion.Location = new System.Drawing.Point(39, 233);
            this.cmbCondicion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbCondicion.Name = "cmbCondicion";
            this.cmbCondicion.Size = new System.Drawing.Size(269, 28);
            this.cmbCondicion.TabIndex = 8;
            // 
            // dgvPacientes
            // 
            this.dgvPacientes.AllowUserToAddRows = false;
            this.dgvPacientes.AllowUserToDeleteRows = false;
            this.dgvPacientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPacientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPacientes.Location = new System.Drawing.Point(39, 360);
            this.dgvPacientes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvPacientes.MultiSelect = false;
            this.dgvPacientes.Name = "dgvPacientes";
            this.dgvPacientes.ReadOnly = true;
            this.dgvPacientes.RowHeadersVisible = false;
            this.dgvPacientes.RowHeadersWidth = 62;
            this.dgvPacientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPacientes.Size = new System.Drawing.Size(990, 373);
            this.dgvPacientes.TabIndex = 17;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(733, 213);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(141, 47);
            this.btnAgregar.TabIndex = 13;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnAtender
            // 
            this.btnAtender.Location = new System.Drawing.Point(887, 213);
            this.btnAtender.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAtender.Name = "btnAtender";
            this.btnAtender.Size = new System.Drawing.Size(141, 47);
            this.btnAtender.TabIndex = 14;
            this.btnAtender.Text = "Atender";
            this.btnAtender.UseVisualStyleBackColor = true;
            this.btnAtender.Click += new System.EventHandler(this.btnAtender_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(733, 273);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(141, 47);
            this.btnEliminar.TabIndex = 15;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(887, 273);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(141, 47);
            this.btnLimpiar.TabIndex = 16;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1080, 813);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblDui);
            this.Controls.Add(this.txtDui);
            this.Controls.Add(this.lblEdad);
            this.Controls.Add(this.nudEdad);
            this.Controls.Add(this.lblcondicion);
            this.Controls.Add(this.cmbCondicion);
            this.Controls.Add(this.lblEspecificacion);
            this.Controls.Add(this.txtEspecificacion);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnAtender);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.dgvPacientes);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblPendientes);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Clínica";
            ((System.ComponentModel.ISupportInitialize)(this.nudEdad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}