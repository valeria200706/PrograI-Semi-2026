using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace miPrimeaAplicacion
{
    public partial class login : Form
    {
        Conexion objConexion = new Conexion();

        TextBox txtUsuario = new TextBox();
        TextBox txtClave = new TextBox();

        // CREDENCIALES DE PRUEBA (SQL SERVER):
        // 1. Usuario: admin123   | Clave: Clave123
        // 2. Usuario: valeria07  | Clave: Valeria2026
        // 3. Usuario: usuario_03 | Clave: User03DB
        Button btnIngresar = new Button();
        Button btnCancelar = new Button();

        public login()
        {
            InitializeComponent();
            CrearInterfaz();
        }

        private void login_Load(object sender, EventArgs e)
        {
        }

        private void CrearInterfaz()
        {
            this.Text = "Inicio de Sesión - USSS004826";
            this.Size = new Size(350, 230);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label lblUsuario = new Label() { Text = "Usuario:", Location = new Point(30, 30), AutoSize = true };
            txtUsuario.Location = new Point(110, 27);
            txtUsuario.Size = new Size(180, 25);

            Label lblClave = new Label() { Text = "Clave:", Location = new Point(30, 70), AutoSize = true };
            txtClave.Location = new Point(110, 67);
            txtClave.Size = new Size(180, 25);
            txtClave.PasswordChar = '*';

            btnIngresar.Text = "Ingresar";
            btnIngresar.Location = new Point(50, 120);
            btnIngresar.Size = new Size(100, 35);
            btnIngresar.Click += btnIngresar_Click;

            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new Point(170, 120);
            btnCancelar.Size = new Size(100, 35);
            btnCancelar.Click += (s, e) => Application.Exit();

            this.Controls.Add(lblUsuario);
            this.Controls.Add(txtUsuario);
            this.Controls.Add(lblClave);
            this.Controls.Add(txtClave);
            this.Controls.Add(btnIngresar);
            this.Controls.Add(btnCancelar);
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuarioIngresado = txtUsuario.Text.Trim();
            string claveIngresada = txtClave.Text.Trim();

            if (string.IsNullOrEmpty(usuarioIngresado) || string.IsNullOrEmpty(claveIngresada))
            {
                MessageBox.Show("Por favor ingrese usuario y clave.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Consultar datos desde la base de datos a través de la clase Conexion
            DataSet ds = objConexion.obtenerDatos();
            DataTable dtUsuarios = ds.Tables["usuarios_usss004826"];

            bool accesoConcedido = false;

            if (dtUsuarios != null)
            {
                foreach (DataRow fila in dtUsuarios.Rows)
                {
                    if (fila["usuario"].ToString() == usuarioIngresado && fila["clave"].ToString() == claveIngresada)
                    {
                        accesoConcedido = true;
                        break;
                    }
                }
            }

            if (accesoConcedido)
            {
                MessageBox.Show("¡Acceso concedido! Bienvenido al sistema.", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();

                // Abre el formulario de mantenimiento de usuarios
                frm_usuario formularioUsuarios = new frm_usuario();
                formularioUsuarios.ShowDialog();

                this.Close();
            }
            else
            {
                MessageBox.Show("Acceso denegado: Usuario o clave incorrectos.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}