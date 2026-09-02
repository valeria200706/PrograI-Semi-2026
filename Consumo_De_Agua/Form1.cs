using System;
using System.Windows.Forms;

namespace Consumo_De_Agua
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtConsumo.Text, out double metros) || metros < 0)
            {
                MessageBox.Show("Ingrese una cantidad válida de metros cúbicos.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConsumo.Focus();
                return;
            }

            // Determinar Rango y Total a pagar
            string rango = "";
            double total = 0;

            if (metros <= 10)
            {
                rango = "0 - 10 m³";
                total = 5.00;
            }
            else if (metros <= 20)
            {
                rango = "11 - 20 m³";
                total = 5.00 + ((metros - 10) * 0.35);
            }
            else if (metros <= 30)
            {
                rango = "21 - 30 m³";
                total = 5.00 + (10 * 0.35) + ((metros - 20) * 0.50);
            }
            else if (metros <= 40)
            {
                rango = "31 - 40 m³";
                total = 5.00 + (10 * 0.35) + (10 * 0.50) + ((metros - 30) * 0.88);
            }
            else
            {
                rango = "Más de 40 m³";
                total = 5.00 + (10 * 0.35) + (10 * 0.50) + (10 * 0.88) + ((metros - 40) * 1.20);
            }

            txtRango.Text = rango;
            txtTotal.Text = $"${total:F2}";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtConsumo.Clear();
            txtRango.Clear();
            txtTotal.Clear();
            txtConsumo.Focus();
        }
    }
}