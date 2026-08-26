using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clinica_progra
{
    public partial class Form1 : Form
    {
        private int numeroTurno = 1;
        private Timer temporizador;

        public Form1()
        {
            InitializeComponent();

            cmbCondicion.Items.Clear();

            cmbCondicion.Items.Add("Gripe");
            cmbCondicion.Items.Add("Resfriado");
            cmbCondicion.Items.Add("Dolor leve");
            cmbCondicion.Items.Add("Fiebre");
            cmbCondicion.Items.Add("Lesión");
            cmbCondicion.Items.Add("Infección");
            cmbCondicion.Items.Add("Dolor intenso");
            cmbCondicion.Items.Add("Embarazo avanzado");
            cmbCondicion.Items.Add("Dificultad respiratoria");
            cmbCondicion.Items.Add("Estado crítico");
            cmbCondicion.Items.Add("Otro");

            cmbCondicion.SelectedIndex = 0;

            nudEdad.Minimum = 0;
            nudEdad.Maximum = 110;
            nudEdad.Value = 0;

            dgvPacientes.Columns.Clear();

            dgvPacientes.Columns.Add("Turno", "Turno");
            dgvPacientes.Columns.Add("Dui", "DUI / Documento");
            dgvPacientes.Columns.Add("Prioridad", "Prioridad");
            dgvPacientes.Columns.Add("Nombre", "Nombre");
            dgvPacientes.Columns.Add("Edad", "Edad");
            dgvPacientes.Columns.Add("Condicion", "Situación");
            dgvPacientes.Columns.Add("Especificacion", "Especificación");
            dgvPacientes.Columns.Add("FechaHora", "Fecha y hora de llegada");
            dgvPacientes.Columns.Add("Espera", "Tiempo de espera");

            dgvPacientes.Columns["FechaHora"].ReadOnly = true;
            dgvPacientes.Columns["Espera"].ReadOnly = true;

            txtNombre.KeyPress += txtNombre_KeyPress;

            temporizador = new Timer();
            temporizador.Interval = 1000;
            temporizador.Tick += Temporizador_Tick;
            temporizador.Start();

            ActualizarContadores();
        }

        private void Temporizador_Tick(object sender, EventArgs e)
        {
            ActualizarTiempoEspera();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) &&
                !char.IsControl(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private int ObtenerPrioridad(string condicion)
        {
            switch (condicion)
            {
                case "Gripe":
                case "Resfriado":
                case "Dolor leve":
                    return 1;

                case "Fiebre":
                case "Lesión":
                case "Infección":
                    return 2;

                case "Dolor intenso":
                    return 3;

                case "Embarazo avanzado":
                case "Dificultad respiratoria":
                case "Estado crítico":
                    return 4;

                default:
                    return 1;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre del paciente.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDui.Text))
            {
                MessageBox.Show(
                    "Ingrese el DUI o documento de identidad.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtDui.Focus();
                return;
            }

            if (cmbCondicion.SelectedItem == null)
            {
                MessageBox.Show(
                    "Seleccione la situación del paciente.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string nombre = txtNombre.Text.Trim();
            string dui = txtDui.Text.Trim();
            int edad = (int)nudEdad.Value;
            string condicion = cmbCondicion.SelectedItem.ToString();
            string especificacion = txtEspecificacion.Text.Trim();

            if (DuiYaExiste(dui))
            {
                MessageBox.Show(
                    "Ese DUI o documento ya está registrado.",
                    "Paciente duplicado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtDui.Focus();
                return;
            }

            int prioridad = ObtenerPrioridad(condicion);

            DateTime fechaHoraLlegada = DateTime.Now;

            string turno = "A-" + numeroTurno.ToString("000");
            numeroTurno++;

            dgvPacientes.Rows.Add(
                turno,
                dui,
                prioridad,
                nombre,
                edad,
                condicion,
                especificacion,
                fechaHoraLlegada.ToString("dd/MM/yyyy HH:mm:ss"),
                "00:00:00"
            );

            OrdenarPacientes();
            ActualizarTiempoEspera();
            ActualizarContadores();

            LimpiarCampos();

            txtNombre.Focus();
        }

        private bool DuiYaExiste(string dui)
        {
            foreach (DataGridViewRow fila in dgvPacientes.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                string duiRegistrado =
                    fila.Cells["Dui"].Value?.ToString() ?? "";

                if (duiRegistrado.Equals(
                    dui,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private void OrdenarPacientes()
        {
            for (int i = 0; i < dgvPacientes.Rows.Count - 1; i++)
            {
                for (int j = 0;
                     j < dgvPacientes.Rows.Count - 1 - i;
                     j++)
                {
                    int prioridadActual =
                        Convert.ToInt32(
                            dgvPacientes.Rows[j]
                            .Cells["Prioridad"].Value);

                    int prioridadSiguiente =
                        Convert.ToInt32(
                            dgvPacientes.Rows[j + 1]
                            .Cells["Prioridad"].Value);

                    DateTime horaActual =
                        DateTime.Parse(
                            dgvPacientes.Rows[j]
                            .Cells["FechaHora"].Value.ToString());

                    DateTime horaSiguiente =
                        DateTime.Parse(
                            dgvPacientes.Rows[j + 1]
                            .Cells["FechaHora"].Value.ToString());

                    bool debeCambiar =
                        prioridadActual < prioridadSiguiente ||
                        (
                            prioridadActual == prioridadSiguiente &&
                            horaActual > horaSiguiente
                        );

                    if (debeCambiar)
                    {
                        DataGridViewRow fila =
                            dgvPacientes.Rows[j];

                        dgvPacientes.Rows.RemoveAt(j);
                        dgvPacientes.Rows.Insert(j + 1, fila);
                    }
                }
            }

            ColorearPrioridades();
        }

        private void ColorearPrioridades()
        {
            foreach (DataGridViewRow fila in dgvPacientes.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                int prioridad =
                    Convert.ToInt32(
                        fila.Cells["Prioridad"].Value);

                if (prioridad == 4)
                {
                    fila.DefaultCellStyle.BackColor = Color.Red;
                    fila.DefaultCellStyle.ForeColor = Color.White;
                }
                else if (prioridad == 3)
                {
                    fila.DefaultCellStyle.BackColor = Color.Orange;
                    fila.DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (prioridad == 2)
                {
                    fila.DefaultCellStyle.BackColor = Color.Yellow;
                    fila.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    fila.DefaultCellStyle.BackColor = Color.LightGreen;
                    fila.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void ActualizarTiempoEspera()
        {
            foreach (DataGridViewRow fila in dgvPacientes.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                if (fila.Cells["FechaHora"].Value == null)
                    continue;

                DateTime llegada;

                if (DateTime.TryParse(
                    fila.Cells["FechaHora"].Value.ToString(),
                    out llegada))
                {
                    TimeSpan espera =
                        DateTime.Now - llegada;

                    if (espera.TotalSeconds < 0)
                        espera = TimeSpan.Zero;

                    fila.Cells["Espera"].Value =
                        espera.ToString(@"hh\:mm\:ss");
                }
            }
        }

        private void btnAtender_Click(object sender, EventArgs e)
        {
            if (dgvPacientes.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay pacientes pendientes.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            OrdenarPacientes();

            DataGridViewRow paciente =
                dgvPacientes.Rows[0];

            string turno =
                paciente.Cells["Turno"].Value.ToString();

            string dui =
                paciente.Cells["Dui"].Value.ToString();

            string nombre =
                paciente.Cells["Nombre"].Value.ToString();

            string condicion =
                paciente.Cells["Condicion"].Value.ToString();

            string especificacion =
                paciente.Cells["Especificacion"].Value.ToString();

            int edad =
                Convert.ToInt32(
                    paciente.Cells["Edad"].Value);

            int prioridad =
                Convert.ToInt32(
                    paciente.Cells["Prioridad"].Value);

            string fechaHora =
                paciente.Cells["FechaHora"].Value.ToString();

            string espera =
                paciente.Cells["Espera"].Value.ToString();

            DialogResult respuesta = MessageBox.Show(
                "Turno: " + turno +
                "\nDUI / Documento: " + dui +
                "\nPaciente: " + nombre +
                "\nEdad: " + edad +
                "\nSituación: " + condicion +
                "\nEspecificación: " + especificacion +
                "\nPrioridad: " + prioridad +
                "\nLlegada: " + fechaHora +
                "\nTiempo de espera: " + espera +
                "\n\n¿Desea atender a este paciente?",
                "Atender paciente",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                dgvPacientes.Rows.RemoveAt(0);

                ActualizarContadores();

                MessageBox.Show(
                    "El paciente " + nombre +
                    " ha sido atendido correctamente.",
                    "Paciente atendido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPacientes.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un paciente de la lista.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DataGridViewRow paciente =
                dgvPacientes.SelectedRows[0];

            string nombre =
                paciente.Cells["Nombre"].Value.ToString();

            string dui =
                paciente.Cells["Dui"].Value.ToString();

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de eliminar al paciente " +
                nombre +
                "\nDUI / Documento: " +
                dui +
                "?",
                "Eliminar paciente",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (respuesta == DialogResult.Yes)
            {
                dgvPacientes.Rows.RemoveAt(
                    paciente.Index);

                ActualizarContadores();

                MessageBox.Show(
                    "El paciente " + nombre +
                    " ha sido eliminado.",
                    "Paciente eliminado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string texto =
                txtBuscar.Text.Trim().ToLower();

            foreach (DataGridViewRow fila in dgvPacientes.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                string nombre =
                    fila.Cells["Nombre"]
                    .Value?.ToString()
                    .ToLower() ?? "";

                string dui =
                    fila.Cells["Dui"]
                    .Value?.ToString()
                    .ToLower() ?? "";

                bool encontrado =
                    nombre.Contains(texto) ||
                    dui.Contains(texto);

                fila.Visible = encontrado;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDui.Clear();
            txtEspecificacion.Clear();

            nudEdad.Value = 0;

            if (cmbCondicion.Items.Count > 0)
                cmbCondicion.SelectedIndex = 0;

            txtBuscar.Clear();

            foreach (DataGridViewRow fila in dgvPacientes.Rows)
            {
                if (!fila.IsNewRow)
                    fila.Visible = true;
            }

            txtNombre.Focus();
        }

        private void ActualizarContadores()
        {
            int total = dgvPacientes.Rows.Count;

            lblTotal.Text =
                "Pacientes registrados: " + total;

            lblPendientes.Text =
                "Pacientes en espera: " + total;
        }

        private void lblcondicion_Click(object sender, EventArgs e)
        {
        }
    }
}