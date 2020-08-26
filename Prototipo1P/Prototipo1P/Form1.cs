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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void aYUDAToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cobroAClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCobroClientes frm = new frmCobroClientes();
            frm.MdiParent = this;
            frm.Show();
        }

        private void pagoAProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPagoProveedor frm = new frmPagoProveedor();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
