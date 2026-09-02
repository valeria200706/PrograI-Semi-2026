namespace miPrimeraAplicacion
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblPais;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.Label lblOrigen;
        private System.Windows.Forms.Label lblDestino;
        private System.Windows.Forms.Label lblResultado;

        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.TextBox txtResultado;

        private System.Windows.Forms.ComboBox cmbOrigen;
        private System.Windows.Forms.ComboBox cmbDestino;

        private System.Windows.Forms.Button btnConvertir;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnSalir;

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
            this.components = new System.ComponentModel.Container();

            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblPais = new System.Windows.Forms.Label();
            this.lblValor = new System.Windows.Forms.Label();
            this.lblOrigen = new System.Windows.Forms.Label();
            this.lblDestino = new System.Windows.Forms.Label();
            this.lblResultado = new System.Windows.Forms.Label();

            this.txtValor = new System.Windows.Forms.TextBox();
            this.txtResultado = new System.Windows.Forms.TextBox();

            this.cmbOrigen = new System.Windows.Forms.ComboBox();
            this.cmbDestino = new System.Windows.Forms.ComboBox();

            this.btnConvertir = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();

            this.SuspendLayout();

            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font(
                "Segoe UI",
                20F,
                System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(0, 70, 140);
            this.lblTitulo.Location = new System.Drawing.Point(90, 25);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(420, 37);
            this.lblTitulo.Text = "CONVERSOR DE ÁREA";

            this.lblPais.AutoSize = true;
            this.lblPais.Font = new System.Drawing.Font(
                "Segoe UI",
                11F,
                System.Drawing.FontStyle.Italic);
            this.lblPais.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblPais.Location = new System.Drawing.Point(140, 70);
            this.lblPais.Name = "lblPais";
            this.lblPais.Size = new System.Drawing.Size(320, 20);
            this.lblPais.Text = "Medidas utilizadas en El Salvador";

            this.lblValor.AutoSize = true;
            this.lblValor.Font = new System.Drawing.Font(
                "Segoe UI",
                11F,
                System.Drawing.FontStyle.Bold);
            this.lblValor.Location = new System.Drawing.Point(70, 125);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(51, 20);
            this.lblValor.Text = "Valor:";

            this.txtValor.Font = new System.Drawing.Font(
                "Segoe UI",
                11F);
            this.txtValor.Location = new System.Drawing.Point(180, 122);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(320, 27);

            this.lblOrigen.AutoSize = true;
            this.lblOrigen.Font = new System.Drawing.Font(
                "Segoe UI",
                11F,
                System.Drawing.FontStyle.Bold);
            this.lblOrigen.Location = new System.Drawing.Point(70, 180);
            this.lblOrigen.Name = "lblOrigen";
            this.lblOrigen.Size = new System.Drawing.Size(64, 20);
            this.lblOrigen.Text = "Origen:";

            this.cmbOrigen.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cmbOrigen.Font = new System.Drawing.Font(
                "Segoe UI",
                11F);

            this.cmbOrigen.FormattingEnabled = true;

            this.cmbOrigen.Items.AddRange(new object[]
            {
                "Pie Cuadrado",
                "Vara Cuadrada",
                "Yarda Cuadrada",
                "Metro Cuadrado",
                "Tarea",
                "Manzana",
                "Hectárea"
            });

            this.cmbOrigen.Location = new System.Drawing.Point(180, 177);
            this.cmbOrigen.Name = "cmbOrigen";
            this.cmbOrigen.Size = new System.Drawing.Size(320, 28);

            this.lblDestino.AutoSize = true;
            this.lblDestino.Font = new System.Drawing.Font(
                "Segoe UI",
                11F,
                System.Drawing.FontStyle.Bold);
            this.lblDestino.Location = new System.Drawing.Point(70, 235);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(71, 20);
            this.lblDestino.Text = "Destino:";

            this.cmbDestino.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cmbDestino.Font = new System.Drawing.Font(
                "Segoe UI",
                11F);

            this.cmbDestino.FormattingEnabled = true;

            this.cmbDestino.Items.AddRange(new object[]
            {
                "Pie Cuadrado",
                "Vara Cuadrada",
                "Yarda Cuadrada",
                "Metro Cuadrado",
                "Tarea",
                "Manzana",
                "Hectárea"
            });

            this.cmbDestino.Location = new System.Drawing.Point(180, 232);
            this.cmbDestino.Name = "cmbDestino";
            this.cmbDestino.Size = new System.Drawing.Size(320, 28);

            this.btnConvertir.BackColor =
                System.Drawing.Color.FromArgb(0, 120, 215);

            this.btnConvertir.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnConvertir.Font = new System.Drawing.Font(
                "Segoe UI",
                10F,
                System.Drawing.FontStyle.Bold);

            this.btnConvertir.ForeColor =
                System.Drawing.Color.White;

            this.btnConvertir.Location =
                new System.Drawing.Point(70, 295);

            this.btnConvertir.Name = "btnConvertir";
            this.btnConvertir.Size =
                new System.Drawing.Size(130, 40);

            this.btnConvertir.Text = "CONVERTIR";
            this.btnConvertir.UseVisualStyleBackColor = false;

            this.btnConvertir.Click +=
                new System.EventHandler(this.btnConvertir_Click);

            this.btnLimpiar.Font = new System.Drawing.Font(
                "Segoe UI",
                10F,
                System.Drawing.FontStyle.Bold);

            this.btnLimpiar.Location =
                new System.Drawing.Point(220, 295);

            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size =
                new System.Drawing.Size(130, 40);

            this.btnLimpiar.Text = "LIMPIAR";
            this.btnLimpiar.UseVisualStyleBackColor = true;

            this.btnLimpiar.Click +=
                new System.EventHandler(this.btnLimpiar_Click);

            this.btnSalir.BackColor =
                System.Drawing.Color.Firebrick;

            this.btnSalir.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnSalir.Font = new System.Drawing.Font(
                "Segoe UI",
                10F,
                System.Drawing.FontStyle.Bold);

            this.btnSalir.ForeColor =
                System.Drawing.Color.White;

            this.btnSalir.Location =
                new System.Drawing.Point(370, 295);

            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size =
                new System.Drawing.Size(130, 40);

            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;

            this.btnSalir.Click +=
                new System.EventHandler(this.btnSalir_Click);

            this.lblResultado.AutoSize = true;
            this.lblResultado.Font = new System.Drawing.Font(
                "Segoe UI",
                11F,
                System.Drawing.FontStyle.Bold);

            this.lblResultado.Location =
                new System.Drawing.Point(70, 370);

            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size =
                new System.Drawing.Size(89, 20);

            this.lblResultado.Text = "Resultado:";

            this.txtResultado.BackColor =
                System.Drawing.Color.FromArgb(235, 245, 255);

            this.txtResultado.Font = new System.Drawing.Font(
                "Segoe UI",
                13F,
                System.Drawing.FontStyle.Bold);

            this.txtResultado.Location =
                new System.Drawing.Point(180, 365);

            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ReadOnly = true;

            this.txtResultado.Size =
                new System.Drawing.Size(320, 31);

            this.txtResultado.TextAlign =
                System.Windows.Forms.HorizontalAlignment.Right;

            this.AutoScaleDimensions =
                new System.Drawing.SizeF(7F, 15F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.BackColor =
                System.Drawing.Color.WhiteSmoke;

            this.ClientSize =
                new System.Drawing.Size(570, 440);

            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnConvertir);
            this.Controls.Add(this.cmbDestino);
            this.Controls.Add(this.lblDestino);
            this.Controls.Add(this.cmbOrigen);
            this.Controls.Add(this.lblOrigen);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.lblPais);
            this.Controls.Add(this.lblTitulo);

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;
            this.Name = "Form1";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text = "Conversor de Área - El Salvador";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}