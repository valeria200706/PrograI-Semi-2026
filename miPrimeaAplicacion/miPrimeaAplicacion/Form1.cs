using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace miPrimeaAplicacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        Conexion objConexion = new Conexion();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        String accion = "nuevo";
        int posicion = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            obtenerDatos();
        }

        private void obtenerDatos()
        {
            ds.Clear();
            ds = objConexion.obtenerDatos();
            dt = ds.Tables["alumnos"];

            dt.PrimaryKey = new DataColumn[] { dt.Columns["idAlumno"] };
            dataGridView1.DataSource = dt.DefaultView;

            mostrarDatos();
        }

        private void mostrarDatos()
        {
            if (dt.Rows.Count > 0 && posicion >= 0 && posicion < dt.Rows.Count)
            {
                txtCodigoAlumno.Text = dt.Rows[posicion]["codigo"].ToString();
                txtNombreAlumno.Text = dt.Rows[posicion]["nombre"].ToString();
                txtDireccionAlumno.Text = dt.Rows[posicion]["direccion"].ToString();
                txtTelefonoAlumno.Text = dt.Rows[posicion]["telefono"].ToString();
                txtEmailAlumno.Text = dt.Rows[posicion]["email"].ToString();

                lblRegistrosAlumnos.Text = (posicion + 1) + " de " + dt.Rows.Count;
            }
            else
            {
                limpiarControles();
                lblRegistrosAlumnos.Text = "0 de 0";
            }
        }

        private void limpiarControles()
        {
            txtCodigoAlumno.Text = "";
            txtNombreAlumno.Text = "";
            txtDireccionAlumno.Text = "";
            txtTelefonoAlumno.Text = "";
            txtEmailAlumno.Text = "";
        }

        private void txtBuscarAlumno_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                filtrarDatos(txtBuscarAlumno.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void filtrarDatos(string valor)
        {
            try
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "codigo LIKE '%" + valor + "%' OR nombre LIKE '%" + valor + "%'";
                dataGridView1.DataSource = dv;

                seleccionarAlumnoDesdeGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void seleccionarAlumnoDesdeGrid()
        {
            try
            {
                if (dataGridView1.CurrentRow == null) return;

                string id = dataGridView1.CurrentRow.Cells["idAlumno"].Value.ToString();
                posicion = dt.Rows.IndexOf(dt.Rows.Find(id));

                mostrarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            seleccionarAlumnoDesdeGrid();
        }

        private void activarDesactivarCtrls(Boolean estado)
        {
            grbDatos.Enabled = estado;
            grbNavegacion.Enabled = !estado;
        }

        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            if (btnAgregarAlumno.Text == "Agregar")
            {
                btnAgregarAlumno.Text = "Guardar";
                btnModificarALumno.Text = "Cancelar";
                accion = "nuevo";
                limpiarControles();
                activarDesactivarCtrls(true);
            }
            else
            {
                string idAlumno = dt.Rows.Count > 0 && posicion >= 0 && posicion < dt.Rows.Count
                    ? dt.Rows[posicion]["idAlumno"].ToString()
                    : "0";

                string[] datosAlumno = {
                    idAlumno,
                    txtCodigoAlumno.Text,
                    txtNombreAlumno.Text,
                    txtDireccionAlumno.Text,
                    txtTelefonoAlumno.Text,
                    txtEmailAlumno.Text
                };

                string respuesta = objConexion.administrarDatosAlumnos(datosAlumno, accion);

                if (respuesta == "1")
                {
                    activarDesactivarCtrls(false);
                    btnAgregarAlumno.Text = "Agregar";
                    btnModificarALumno.Text = "Modificar";
                    obtenerDatos();
                }
                else
                {
                    MessageBox.Show(respuesta, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnModificarALumno_Click(object sender, EventArgs e)
        {
            if (btnModificarALumno.Text == "Modificar")
            {
                btnAgregarAlumno.Text = "Guardar";
                btnModificarALumno.Text = "Cancelar";
                accion = "modificar";
                activarDesactivarCtrls(true);
            }
            else
            {
                mostrarDatos();
                activarDesactivarCtrls(false);
                btnAgregarAlumno.Text = "Agregar";
                btnModificarALumno.Text = "Modificar";
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0 && posicion >= 0 && posicion < dt.Rows.Count)
            {
                if (MessageBox.Show("¿Está seguro de eliminar a " + txtNombreAlumno.Text + "?",
                    "Eliminar Alumno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string idAlumno = dt.Rows[posicion]["idAlumno"].ToString();
                    string[] datosAlumno = { idAlumno, "", "", "", "", "" };

                    string respuesta = objConexion.administrarDatosAlumnos(datosAlumno, "eliminar");

                    if (respuesta == "1")
                    {
                        posicion = 0;
                        obtenerDatos();
                    }
                    else
                    {
                        MessageBox.Show(respuesta, "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay registros para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSiguienteAlumno_Click(object sender, EventArgs e)
        {
            if (posicion < dt.Rows.Count - 1)
            {
                posicion++;
                mostrarDatos();
            }
        }

        private void btnAnteriorAlumno_Click(object sender, EventArgs e)
        {
            if (posicion > 0)
            {
                posicion--;
                mostrarDatos();
            }
        }

        private void btnUltimoAlumno_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                posicion = dt.Rows.Count - 1;
                mostrarDatos();
            }
        }

        private void btnPrimeroAlumno_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                posicion = 0;
                mostrarDatos();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
        }
    }
}