using System;
using System.Windows.Forms;

namespace Impuestos_Parcial1
{
    public partial class Form1 : Form
    {
        // Matriz tridimensional con los datos de la tabla del examen
        double[,] tablaImpuestos = {
            { 0.01, 500, 1.5, 0 },
            { 500.01, 1000, 1.5, 3 },
            { 1000.01, 2000, 3, 3 },
            { 2000.01, 3000, 6, 3 },
            { 3000.01, 6000, 9, 2 },
            { 6000.01, 18000, 15, 2 },
            { 18000.01, 30000, 39, 2 },
            { 30000.01, 60000, 63, 1 },
            { 60000.01, 100000, 93, 0.8 },
            { 100000.01, 200000, 125, 0.7 },
            { 200000.01, 300000, 195, 0.6 },
            { 300000.01, 400000, 255, 0.45 },
            { 400000.01, 500000, 300, 0.4 },
            { 500000.01, 1000000, 340, 0.30 },
            { 1000000.01, 99999999, 490, 0.18 }
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvTabla.Columns.Clear();
            dgvTabla.Rows.Clear();

            dgvTabla.Columns.Add("No", "No.");
            dgvTabla.Columns.Add("Desde", "DESDE");
            dgvTabla.Columns.Add("Hasta", "HASTA");
            dgvTabla.Columns.Add("Precio", "PRECIO");
            dgvTabla.Columns.Add("Adicional", "ADICIONAL");

            for (int i = 0; i < tablaImpuestos.GetLength(0); i++)
            {
                dgvTabla.Rows.Add(
                    i + 1,
                    $"$ {tablaImpuestos[i, 0]:N2}",
                    $"$ {tablaImpuestos[i, 1]:N2}",
                    $"$ {tablaImpuestos[i, 2]:N2}",
                    tablaImpuestos[i, 3]
                );
            }

            dgvTabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTabla.ReadOnly = true;
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtMonto.Text, out double monto) && monto > 0)
            {
                double impuestoTotal = 0;
                bool encontrado = false;

                for (int i = 0; i < tablaImpuestos.GetLength(0); i++)
                {
                    double desde = tablaImpuestos[i, 0];
                    double hasta = tablaImpuestos[i, 1];
                    double precio = tablaImpuestos[i, 2];
                    double adicional = tablaImpuestos[i, 3];

                    if (monto >= desde && monto <= hasta)
                    {
                        // Fórmula especificada en las indicaciones del examen
                        impuestoTotal = ((monto - desde) / 1000.0) * adicional + precio;
                        encontrado = true;

                        dgvTabla.ClearSelection();
                        dgvTabla.Rows[i].Selected = true;
                        break;
                    }
                }

                if (encontrado)
                {
                    lblResultado.Text = $"Impuesto a Pagar: ${impuestoTotal:F2}";
                }
                else
                {
                    MessageBox.Show("El monto ingresado no se encuentra dentro de los rangos de la tabla.", "Fuera de Rango", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un monto económico válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMonto.Focus();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtMonto.Clear();
            lblResultado.Text = "Impuesto a Pagar: $0.00";
            dgvTabla.ClearSelection();
            txtMonto.Focus();
        }
    }
}