using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototipo1P
{
    public partial class frmPagoProveedor : Form
    {
        clsConexion cn = new clsConexion();
        public frmPagoProveedor()
        {
            InitializeComponent();
            funcCargarDatos();

        }

        void funcCargarDatos()
        {
            try
            {
                string cadena = "SELECT CPENC.idCompraProveedorEncabezado, CPENC.serie, CPENC.fecha, CPENC.nit, CPENC.total, P.nombre, P.idProveedor FROM COMPRAPROVEEDORENCABEZADO CPENC, PROVEEDOR P WHERE CPENC.idProveedor = P.idProveedor AND CPENC.pago = 0; ";
                OdbcCommand cma = new OdbcCommand(cadena, cn.nuevaConexion());
                OdbcDataReader reader = cma.ExecuteReader();
                while (reader.Read())
                {
                    cmbCliente.Items.Add(reader.GetString(5));
                    cmbCode.Items.Add(reader.GetInt32(6).ToString());
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR AL MOSTRAR DATOS AL DATAGRIDVIEW " + e);
            }
            //CARGAR PELICULAS
            try
            {
                string cadena = "SELECT CPENC.idCompraProveedorEncabezado, CPENC.serie, CPENC.fecha, CPENC.nit, CPENC.total, P.nombre FROM COMPRAPROVEEDORENCABEZADO CPENC, PROVEEDOR P WHERE CPENC.idProveedor = P.idProveedor AND CPENC.pago = 0;";
                OdbcCommand cma = new OdbcCommand(cadena, cn.nuevaConexion());
                OdbcDataReader reader = cma.ExecuteReader();
                while (reader.Read())
                {
                    dgvCliente.Rows.Add(reader.GetString(0), reader.GetString(1),reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR AL MOSTRAR DATOS AL DATAGRIDVIEW " + e);
            }

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

        private void frmPagoProveedor_Load(object sender, EventArgs e)
        {

        }

        private void chkPagar_CheckedChanged(object sender, EventArgs e)
        {

            if (chkPagar.Checked == true)
            {
                lblQ.Visible = true;
                txtPrice.Visible = true;
                label1.Visible = true;
                textBox1.Visible = true;

            }
            else
            {
                lblQ.Visible = false;
                txtPrice.Visible = false;
                label1.Visible = false;
                textBox1.Visible = false;

            }
        }

        void actualizar()
        {
            dgvCliente.Rows.Clear();
            try
            {
                string cadena = "SELECT CPENC.idCompraProveedorEncabezado, CPENC.serie, CPENC.fecha, CPENC.nit, CPENC.total, P.nombre, P.idProveedor FROM COMPRAPROVEEDORENCABEZADO CPENC, PROVEEDOR P WHERE CPENC.idProveedor = P.idProveedor AND CPENC.pago = 0 AND P.idProveedor = " + int.Parse(cmbCode.Text.ToString()) + ";";
                OdbcCommand cma = new OdbcCommand(cadena, cn.nuevaConexion());
                OdbcDataReader reader = cma.ExecuteReader();
                while (reader.Read())
                {
                    dgvCliente.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL MOSTRAR DATOS AL DATAGRIDVIEW actualizar " + ex);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            double precioOriginal = 0;
            double precioActualizado = 0;

            if (txtPrice.Text == "")
            {
                MessageBox.Show("INGRESAR UN VALOR PARA CANCELAR DEUDA");
            }
            else
            {
                double aux = double.Parse(txtPrice.Text.ToString());
                try
                {
                    string cadena = "SELECT CPENC.idCompraProveedorEncabezado, CPENC.serie, CPENC.fecha, CPENC.nit, CPENC.total, P.nombre, P.idProveedor FROM COMPRAPROVEEDORENCABEZADO CPENC, PROVEEDOR P WHERE CPENC.idProveedor = P.idProveedor AND CPENC.pago = 0 AND P.idProveedor = "+int.Parse(textBox1.Text.ToString())+ " AND CPENC.idCompraProveedorEncabezado = " + int.Parse(cmbCode.Text.ToString()) + "; ";
                    OdbcCommand cma = new OdbcCommand(cadena, cn.nuevaConexion());
                    OdbcDataReader reader = cma.ExecuteReader();
                    while (reader.Read())
                    {
                        precioOriginal = reader.GetDouble(4);
                        MessageBox.Show("ORIGINAL UNO: " + reader.GetDouble(4));
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR AL MOSTRAR DATOS AL DATAGRIDVIEW ORIGINALL " + ex);
                }


                precioActualizado = precioOriginal - aux;
                MessageBox.Show("ACTUALIZADO UNO: " + precioActualizado);
                if (precioActualizado == 0)
                {
                    try
                    {

                        string Modificar = "UPDATE COMPRAPROVEEDORENCABEZADO SET pago = true  WHERE idCompraProveedorEncabezado=" + Int32.Parse(textBox1.Text);
                        OdbcCommand Consulta = new OdbcCommand(Modificar, cn.nuevaConexion());
                        OdbcDataReader leer = Consulta.ExecuteReader();
                        MessageBox.Show("Ha realizado todos los pagos correspondientes a esta factura");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudieron mostrar los registros en este momento intente mas tarde" + ex);
                    }
                }
                

                try
                {

                    string Modificar = "UPDATE COMPRAPROVEEDORENCABEZADO SET total = '" + precioActualizado + "'  WHERE idCompraProveedorEncabezado=" + Int32.Parse(textBox1.Text);
                    OdbcCommand Consulta = new OdbcCommand(Modificar, cn.nuevaConexion());
                    OdbcDataReader leer = Consulta.ExecuteReader();
                    MessageBox.Show("Los datos fueron actualizados correctamente ");
                    actualizar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudieron mostrar los registros en este momento intente mas tarde" + ex);
                }
            }
        }

        private void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCode.SelectedIndex = cmbCliente.SelectedIndex;
            dgvCliente.Rows.Clear();
            try
            {
                string cadena = "SELECT CPENC.idCompraProveedorEncabezado, CPENC.serie, CPENC.fecha, CPENC.nit, CPENC.total, P.nombre, P.idProveedor FROM COMPRAPROVEEDORENCABEZADO CPENC, PROVEEDOR P WHERE CPENC.idProveedor = P.idProveedor AND CPENC.pago = 0 AND P.idProveedor = "+int.Parse(cmbCode.Text.ToString())+"; ";
                OdbcCommand cma = new OdbcCommand(cadena, cn.nuevaConexion());
                OdbcDataReader reader = cma.ExecuteReader();
                while (reader.Read())
                {
                    dgvCliente.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL MOSTRAR DATOS AL DATAGRIDVIEW " + e);
            }
        }
    }
}
