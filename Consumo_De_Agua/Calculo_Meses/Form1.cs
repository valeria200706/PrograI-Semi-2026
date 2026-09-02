using System;
using System.Windows.Forms;

namespace Calculo_Meses
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMeses.Text, out int nMeses) || nMeses <= 0)
            {
                MessageBox.Show("Por favor, ingrese un número entero mayor a 0 para los meses.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMeses.Focus();
                return;
            }

            double.TryParse(txtMontoMensual.Text, out double montoMensual);

            dgvTabla.Rows.Clear();
            double acumulado = 0;

            for (int i = 1; i <= nMeses; i++)
            {
                acumulado += montoMensual;
                dgvTabla.Rows.Add(i, $"${montoMensual:F2}", $"${acumulado:F2}");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtMeses.Clear();
            txtMontoMensual.Clear();
            dgvTabla.Rows.Clear();
            txtMeses.Focus();
        }
    }
}