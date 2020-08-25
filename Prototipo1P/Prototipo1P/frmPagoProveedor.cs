using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototipo1P
{
    public partial class frmPagoProveedor : Form
    {
        public frmPagoProveedor()
        {
            InitializeComponent();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidacion validar = new clsValidacion();
            validar.funcSueldo(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidacion validar = new clsValidacion();
            validar.funcSoloNumeros(e);
        }
    }
}
