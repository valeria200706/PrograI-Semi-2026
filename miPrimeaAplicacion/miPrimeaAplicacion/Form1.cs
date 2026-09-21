using System.Windows.Forms;

namespace miPrimeaAplicacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {

        }

        private GroupBox grbDatos;
        private GroupBox grbNavegacion;
        private Button btnPrimeroAlumno;
        private GroupBox grbEdicion;
        private Label lblCodigoAlumno;
        private TextBox txtCodigoAlumno;
        private TextBox txt;
        private Label lblNombreAlumno;
        private TextBox textBox2;
        private Label lblDireccionAlumno;
        private TextBox textBox1;
        private Label lbltelefonoAlumno;
        private TextBox textBox3;
        private Label lblEmailAlumno;

        private void InitializeComponent()
        {
            this.grbDatos = new System.Windows.Forms.GroupBox();
            this.grbNavegacion = new System.Windows.Forms.GroupBox();
            this.grbEdicion = new System.Windows.Forms.GroupBox();
            this.lblCodigoAlumno = new System.Windows.Forms.Label();
            this.txtCodigoAlumno = new System.Windows.Forms.TextBox();
            this.btnPrimeroAlumno = new System.Windows.Forms.Button();
            this.txt = new System.Windows.Forms.TextBox();
            this.lblNombreAlumno = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblDireccionAlumno = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lbltelefonoAlumno = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.lblEmailAlumno = new System.Windows.Forms.Label();
            this.btnUltimoAlumno = new System.Windows.Forms.Button();
            this.btnAnteriorAlumno = new System.Windows.Forms.Button();
            this.btnSiguienteAlumno = new System.Windows.Forms.Button();
            this.btnModificarAlumno = new System.Windows.Forms.Button();
            this.btnAgregarAlumno = new System.Windows.Forms.Button();
            this.grbDatos.SuspendLayout();
            this.grbNavegacion.SuspendLayout();
            this.grbEdicion.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbDatos
            // 
            this.grbDatos.Controls.Add(this.textBox3);
            this.grbDatos.Controls.Add(this.lblEmailAlumno);
            this.grbDatos.Controls.Add(this.textBox1);
            this.grbDatos.Controls.Add(this.lbltelefonoAlumno);
            this.grbDatos.Controls.Add(this.textBox2);
            this.grbDatos.Controls.Add(this.lblDireccionAlumno);
            this.grbDatos.Controls.Add(this.txt);
            this.grbDatos.Controls.Add(this.lblNombreAlumno);
            this.grbDatos.Controls.Add(this.txtCodigoAlumno);
            this.grbDatos.Controls.Add(this.lblCodigoAlumno);
            this.grbDatos.Location = new System.Drawing.Point(36, 61);
            this.grbDatos.Name = "grbDatos";
            this.grbDatos.Size = new System.Drawing.Size(808, 293);
            this.grbDatos.TabIndex = 0;
            this.grbDatos.TabStop = false;
            this.grbDatos.Text = "DATOS";
            // 
            // grbNavegacion
            // 
            this.grbNavegacion.Controls.Add(this.btnAnteriorAlumno);
            this.grbNavegacion.Controls.Add(this.btnSiguienteAlumno);
            this.grbNavegacion.Controls.Add(this.btnUltimoAlumno);
            this.grbNavegacion.Controls.Add(this.btnPrimeroAlumno);
            this.grbNavegacion.Location = new System.Drawing.Point(36, 389);
            this.grbNavegacion.Name = "grbNavegacion";
            this.grbNavegacion.Size = new System.Drawing.Size(384, 152);
            this.grbNavegacion.TabIndex = 1;
            this.grbNavegacion.TabStop = false;
            this.grbNavegacion.Text = "Navegacion";
            this.grbNavegacion.Enter += new System.EventHandler(this.grbNavegacion_Enter);
            // 
            // grbEdicion
            // 
            this.grbEdicion.Controls.Add(this.btnAgregarAlumno);
            this.grbEdicion.Controls.Add(this.btnModificarAlumno);
            this.grbEdicion.Location = new System.Drawing.Point(426, 389);
            this.grbEdicion.Name = "grbEdicion";
            this.grbEdicion.Size = new System.Drawing.Size(418, 152);
            this.grbEdicion.TabIndex = 2;
            this.grbEdicion.TabStop = false;
            this.grbEdicion.Text = "Edicion";
            this.grbEdicion.Enter += new System.EventHandler(this.grbEdicion_Enter);
            // 
            // lblCodigoAlumno
            // 
            this.lblCodigoAlumno.AutoSize = true;
            this.lblCodigoAlumno.Location = new System.Drawing.Point(12, 38);
            this.lblCodigoAlumno.Name = "lblCodigoAlumno";
            this.lblCodigoAlumno.Size = new System.Drawing.Size(74, 20);
            this.lblCodigoAlumno.TabIndex = 0;
            this.lblCodigoAlumno.Text = "CODIGO";
            // 
            // txtCodigoAlumno
            // 
            this.txtCodigoAlumno.Location = new System.Drawing.Point(103, 35);
            this.txtCodigoAlumno.Name = "txtCodigoAlumno";
            this.txtCodigoAlumno.Size = new System.Drawing.Size(191, 26);
            this.txtCodigoAlumno.TabIndex = 1;
            // 
            // btnPrimeroAlumno
            // 
            this.btnPrimeroAlumno.Location = new System.Drawing.Point(16, 64);
            this.btnPrimeroAlumno.Name = "btnPrimeroAlumno";
            this.btnPrimeroAlumno.Size = new System.Drawing.Size(79, 37);
            this.btnPrimeroAlumno.TabIndex = 0;
            this.btnPrimeroAlumno.Text = "l<";
            this.btnPrimeroAlumno.UseVisualStyleBackColor = true;
            // 
            // txt
            // 
            this.txt.Location = new System.Drawing.Point(103, 78);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(428, 26);
            this.txt.TabIndex = 3;
            // 
            // lblNombreAlumno
            // 
            this.lblNombreAlumno.AutoSize = true;
            this.lblNombreAlumno.Location = new System.Drawing.Point(12, 81);
            this.lblNombreAlumno.Name = "lblNombreAlumno";
            this.lblNombreAlumno.Size = new System.Drawing.Size(79, 20);
            this.lblNombreAlumno.TabIndex = 2;
            this.lblNombreAlumno.Text = "NOMBRE";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(97, 117);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(454, 59);
            this.textBox2.TabIndex = 5;
            // 
            // lblDireccionAlumno
            // 
            this.lblDireccionAlumno.AutoSize = true;
            this.lblDireccionAlumno.Location = new System.Drawing.Point(6, 120);
            this.lblDireccionAlumno.Name = "lblDireccionAlumno";
            this.lblDireccionAlumno.Size = new System.Drawing.Size(75, 20);
            this.lblDireccionAlumno.TabIndex = 4;
            this.lblDireccionAlumno.Text = "Direccion";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(98, 197);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(191, 26);
            this.textBox1.TabIndex = 7;
            // 
            // lbltelefonoAlumno
            // 
            this.lbltelefonoAlumno.AutoSize = true;
            this.lbltelefonoAlumno.Location = new System.Drawing.Point(7, 200);
            this.lbltelefonoAlumno.Name = "lbltelefonoAlumno";
            this.lbltelefonoAlumno.Size = new System.Drawing.Size(94, 20);
            this.lbltelefonoAlumno.TabIndex = 6;
            this.lbltelefonoAlumno.Text = "TELEFONO";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(103, 247);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(293, 26);
            this.textBox3.TabIndex = 9;
            // 
            // lblEmailAlumno
            // 
            this.lblEmailAlumno.AutoSize = true;
            this.lblEmailAlumno.Location = new System.Drawing.Point(12, 250);
            this.lblEmailAlumno.Name = "lblEmailAlumno";
            this.lblEmailAlumno.Size = new System.Drawing.Size(58, 20);
            this.lblEmailAlumno.TabIndex = 8;
            this.lblEmailAlumno.Text = "EMAIL";
            // 
            // btnUltimoAlumno
            // 
            this.btnUltimoAlumno.Location = new System.Drawing.Point(101, 64);
            this.btnUltimoAlumno.Name = "btnUltimoAlumno";
            this.btnUltimoAlumno.Size = new System.Drawing.Size(79, 37);
            this.btnUltimoAlumno.TabIndex = 1;
            this.btnUltimoAlumno.Text = "<";
            this.btnUltimoAlumno.UseVisualStyleBackColor = true;
            // 
            // btnAnteriorAlumno
            // 
            this.btnAnteriorAlumno.Location = new System.Drawing.Point(286, 64);
            this.btnAnteriorAlumno.Name = "btnAnteriorAlumno";
            this.btnAnteriorAlumno.Size = new System.Drawing.Size(73, 37);
            this.btnAnteriorAlumno.TabIndex = 3;
            this.btnAnteriorAlumno.Text = ">|";
            this.btnAnteriorAlumno.UseVisualStyleBackColor = true;
            // 
            // btnSiguienteAlumno
            // 
            this.btnSiguienteAlumno.Location = new System.Drawing.Point(207, 64);
            this.btnSiguienteAlumno.Name = "btnSiguienteAlumno";
            this.btnSiguienteAlumno.Size = new System.Drawing.Size(73, 37);
            this.btnSiguienteAlumno.TabIndex = 2;
            this.btnSiguienteAlumno.Text = ">";
            this.btnSiguienteAlumno.UseVisualStyleBackColor = true;
            this.btnSiguienteAlumno.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnModificarAlumno
            // 
            this.btnModificarAlumno.Location = new System.Drawing.Point(207, 62);
            this.btnModificarAlumno.Name = "btnModificarAlumno";
            this.btnModificarAlumno.Size = new System.Drawing.Size(107, 40);
            this.btnModificarAlumno.TabIndex = 0;
            this.btnModificarAlumno.Text = "Modificar";
            this.btnModificarAlumno.UseVisualStyleBackColor = true;
            // 
            // btnAgregarAlumno
            // 
            this.btnAgregarAlumno.Location = new System.Drawing.Point(36, 61);
            this.btnAgregarAlumno.Name = "btnAgregarAlumno";
            this.btnAgregarAlumno.Size = new System.Drawing.Size(107, 40);
            this.btnAgregarAlumno.TabIndex = 1;
            this.btnAgregarAlumno.Text = "Agregar";
            this.btnAgregarAlumno.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(882, 618);
            this.Controls.Add(this.grbEdicion);
            this.Controls.Add(this.grbNavegacion);
            this.Controls.Add(this.grbDatos);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grbDatos.ResumeLayout(false);
            this.grbDatos.PerformLayout();
            this.grbNavegacion.ResumeLayout(false);
            this.grbEdicion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void grbNavegacion_Enter(object sender, System.EventArgs e)
        {

        }

        private Button btnUltimoAlumno;

        private void grbEdicion_Enter(object sender, System.EventArgs e)
        {

        }

        private Button btnAnteriorAlumno;
        private Button btnSiguienteAlumno;

        private void button2_Click(object sender, System.EventArgs e)
        {

        }

        private Button btnAgregarAlumno;
        private Button btnModificarAlumno;
    }

}


