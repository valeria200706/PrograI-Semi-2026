using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace miPrimeaAplicacion
{
    public partial class frm_usuario : Form
    {
        // Instancia de conexión y DataSet
        Conexion objConexion = new Conexion();
        DataSet ds = new DataSet();
        int posicion = 0;
        string modoAccion = ""; // Variable para saber si estamos guardando uno "nuevo" o "modificando"

        // Cajas de texto
        TextBox txtIdUsuario = new TextBox();
        TextBox txtUsuario = new TextBox();
        TextBox txtClave = new TextBox();
        TextBox txtConfirmarClave = new TextBox();
        TextBox txtNombre = new TextBox();
        TextBox txtDireccion = new TextBox();
        TextBox txtTelefono = new TextBox();

        // Botones de edición
        Button btnAgregar = new Button();
        Button btnModificar = new Button();
        Button btnEliminar = new Button();

        // Botones de navegación
        Button btnPrimero = new Button();
        Button btnAnterior = new Button();
        Button btnSiguiente = new Button();
        Button btnUltimo = new Button();

        public frm_usuario()
        {
            InitializeComponent();
            CrearInterfazGrafica();
            CargarDatos();
        }

        private void frm_usuario_Load(object sender, EventArgs e)
        {
        }

        private void CrearInterfazGrafica()
        {
            this.Text = "Mantenimiento de Usuarios - USSS004826";
            this.Size = new Size(480, 520);
            this.StartPosition = FormStartPosition.CenterScreen;

            string[] etiquetas = { "ID Usuario:", "Usuario:", "Clave:", "Confirmar Clave:", "Nombre:", "Dirección:", "Teléfono:" };
            TextBox[] cajas = { txtIdUsuario, txtUsuario, txtClave, txtConfirmarClave, txtNombre, txtDireccion, txtTelefono };

            txtIdUsuario.ReadOnly = true;
            txtClave.PasswordChar = '*';
            txtConfirmarClave.PasswordChar = '*';

            int top = 20;
            for (int i = 0; i < etiquetas.Length; i++)
            {
                Label lbl = new Label() { Text = etiquetas[i], Location = new Point(30, top), AutoSize = true };
                cajas[i].Location = new Point(160, top - 3);
                cajas[i].Size = new Size(250, 25);

                this.Controls.Add(lbl);
                this.Controls.Add(cajas[i]);
                top += 35;
            }

            // Configuración de Botones de Edición
            btnAgregar.Text = "Nuevo";
            btnAgregar.Location = new Point(30, top + 10);
            btnAgregar.Size = new Size(110, 35);
            btnAgregar.Click += btnAgregar_Click;

            btnModificar.Text = "Modificar";
            btnModificar.Location = new Point(150, top + 10);
            btnModificar.Size = new Size(110, 35);
            btnModificar.Click += btnModificar_Click;

            btnEliminar.Text = "Eliminar";
            btnEliminar.Location = new Point(270, top + 10);
            btnEliminar.Size = new Size(110, 35);
            btnEliminar.Click += btnEliminar_Click;

            // Configuración de Botones de Navegación
            btnPrimero.Text = "|<";
            btnPrimero.Location = new Point(30, top + 55);
            btnPrimero.Size = new Size(80, 30);
            btnPrimero.Click += (s, e) => Navegar(0);

            btnAnterior.Text = "<";
            btnAnterior.Location = new Point(120, top + 55);
            btnAnterior.Size = new Size(80, 30);
            btnAnterior.Click += (s, e) => Navegar(posicion - 1);

            btnSiguiente.Text = ">";
            btnSiguiente.Location = new Point(210, top + 55);
            btnSiguiente.Size = new Size(80, 30);
            btnSiguiente.Click += (s, e) => Navegar(posicion + 1);

            btnUltimo.Text = ">|";
            btnUltimo.Location = new Point(300, top + 55);
            btnUltimo.Size = new Size(80, 30);
            btnUltimo.Click += (s, e) => {
                if (ds.Tables["usuarios_usss004826"] != null && ds.Tables["usuarios_usss004826"].Rows.Count > 0)
                    Navegar(ds.Tables["usuarios_usss004826"].Rows.Count - 1);
            };

            this.Controls.Add(btnAgregar);
            this.Controls.Add(btnModificar);
            this.Controls.Add(btnEliminar);
            this.Controls.Add(btnPrimero);
            this.Controls.Add(btnAnterior);
            this.Controls.Add(btnSiguiente);
            this.Controls.Add(btnUltimo);

            // Bloquear los controles al inicio
            HabilitarControles(false);
        }

        // Método para bloquear o desbloquear las cajas y navegación
        private void HabilitarControles(bool editando)
        {
            txtUsuario.ReadOnly = !editando;
            txtClave.ReadOnly = !editando;
            txtConfirmarClave.ReadOnly = !editando;
            txtNombre.ReadOnly = !editando;
            txtDireccion.ReadOnly = !editando;
            txtTelefono.ReadOnly = !editando;

            // Si estamos editando, apagamos la navegación para evitar errores
            btnPrimero.Enabled = !editando;
            btnAnterior.Enabled = !editando;
            btnSiguiente.Enabled = !editando;
            btnUltimo.Enabled = !editando;
        }

        private void CargarDatos()
        {
            ds = objConexion.obtenerDatos();
            if (ds.Tables["usuarios_usss004826"] != null && ds.Tables["usuarios_usss004826"].Rows.Count > 0)
            {
                MostrarRegistro(posicion);
            }
        }

        private void MostrarRegistro(int index)
        {
            if (ds.Tables["usuarios_usss004826"] == null || ds.Tables["usuarios_usss004826"].Rows.Count == 0) return;

            posicion = index;
            DataRow row = ds.Tables["usuarios_usss004826"].Rows[posicion];

            txtIdUsuario.Text = row["idUsuario"].ToString();
            txtUsuario.Text = row["usuario"].ToString();
            txtClave.Text = row["clave"].ToString();
            txtConfirmarClave.Text = row["clave"].ToString();
            txtNombre.Text = row["nombre"].ToString();
            txtDireccion.Text = row["direccion"].ToString();
            txtTelefono.Text = row["telefono"].ToString();
        }

        private void Navegar(int nuevaPosicion)
        {
            if (ds.Tables["usuarios_usss004826"] == null) return;
            int total = ds.Tables["usuarios_usss004826"].Rows.Count;
            if (total == 0) return;

            if (nuevaPosicion >= 0 && nuevaPosicion < total)
            {
                MostrarRegistro(nuevaPosicion);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (btnAgregar.Text == "Nuevo")
            {
                // 1. Limpiar cajas y preparar para ingreso
                txtIdUsuario.Clear();
                txtUsuario.Clear();
                txtClave.Clear();
                txtConfirmarClave.Clear();
                txtNombre.Clear();
                txtDireccion.Clear();
                txtTelefono.Clear();

                modoAccion = "nuevo";
                HabilitarControles(true); // Desbloquea las cajas

                // Cambiar el texto de los botones
                btnAgregar.Text = "Guardar";
                btnModificar.Text = "Cancelar";
                btnEliminar.Enabled = false; // Ocultamos eliminar mientras agregamos
                txtUsuario.Focus();
            }
            else if (btnAgregar.Text == "Guardar")
            {
                // 2. Proceso de guardado (Validaciones y BD)
                string usuario = txtUsuario.Text.Trim();
                string clave = txtClave.Text.Trim();
                string confirmarClave = txtConfirmarClave.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string direccion = txtDireccion.Text.Trim();
                string telefono = txtTelefono.Text.Trim();

                if (usuario.Length < 6 || usuario.Length > 16)
                {
                    MessageBox.Show("El usuario debe tener entre 6 y 16 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (clave != confirmarClave)
                {
                    MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!clave.Any(char.IsLetter) || !clave.Any(char.IsDigit))
                {
                    MessageBox.Show("La clave debe contener al menos una letra y un número.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                String[] datos = { txtIdUsuario.Text, usuario, clave, nombre, direccion, telefono };
                string respuesta = objConexion.mantenimiento_usuarios_usss004826(datos, modoAccion);

                if (respuesta == "1")
                {
                    MessageBox.Show("Operación realizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Restaurar los botones y bloquear cajas
                    btnAgregar.Text = "Nuevo";
                    btnModificar.Text = "Modificar";
                    btnEliminar.Enabled = true;
                    HabilitarControles(false);

                    CargarDatos(); // Recargar datos de SQL

                    if (modoAccion == "nuevo")
                    {
                        // Si era nuevo, ir al último registro agregado
                        Navegar(ds.Tables["usuarios_usss004826"].Rows.Count - 1);
                    }
                }
                else
                {
                    MessageBox.Show("Error al guardar: " + respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (btnModificar.Text == "Modificar")
            {
                if (string.IsNullOrEmpty(txtIdUsuario.Text)) return;

                modoAccion = "modificar";
                HabilitarControles(true); // Desbloqueamos las cajas para que pueda editar

                // El botón Nuevo se vuelve Guardar y Modificar se vuelve Cancelar
                btnAgregar.Text = "Guardar";
                btnModificar.Text = "Cancelar";
                btnEliminar.Enabled = false;
            }
            else if (btnModificar.Text == "Cancelar")
            {
                // Si se arrepintió, restauramos el sistema
                btnAgregar.Text = "Nuevo";
                btnModificar.Text = "Modificar";
                btnEliminar.Enabled = true;
                HabilitarControles(false); // Volvemos a bloquear las cajas
                MostrarRegistro(posicion); // Recargamos el registro original para deshacer cambios visuales
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdUsuario.Text)) return;

            if (MessageBox.Show("¿Está seguro de eliminar este usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String[] datos = { txtIdUsuario.Text, "", "", "", "", "" };
                string respuesta = objConexion.mantenimiento_usuarios_usss004826(datos, "eliminar");

                if (respuesta == "1")
                {
                    MessageBox.Show("Usuario eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    Navegar(0); // Volver al primer registro tras eliminar
                }
                else
                {
                    MessageBox.Show("Error al eliminar: " + respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}