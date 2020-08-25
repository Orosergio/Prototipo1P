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
    public partial class frmCobroClientes : Form
    {
        clsConexion cn = new clsConexion();
        public frmCobroClientes()
        {
            InitializeComponent();
            funcCargarDatos();

        }

        private void frmCobroClientes_Load(object sender, EventArgs e)
        {

        }

        void funcCargarDatos()
        {
            try
            {
                string cadena = "SELECT C.nitCliente, C.nombres, C.apellidos,FACENCCRED.idFacturaEncabezadoCredito, FACENCCRED.serie, FACENCCRED.fecha, TC.nombre, TC.cantidadDias, FACENCCRED.total FROM CLIENTE C, FACTURAENCABEZADOCREDITO FACENCCRED, TIPOCREDITO TC WHERE FACENCCRED.nitCliente = C.nitCliente AND FACENCCRED.idTipoCredito = TC.idTipoCredito AND FACENCCRED.estatus = false; ";
                OdbcCommand cma = new OdbcCommand(cadena, cn.nuevaConexion());
                OdbcDataReader reader = cma.ExecuteReader();
                while (reader.Read())
                {
                    cmbCliente.Items.Add(reader.GetString(1) + " " + reader.GetString(2));
                    cmbCode.Items.Add(reader.GetInt32(0).ToString());
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR AL MOSTRAR DATOS AL DATAGRIDVIEW " + e);
            }
            //CARGAR PELICULAS
            try
            {
                string cadena = "SELECT C.nitCliente, C.nombres, C.apellidos,FACENCCRED.idFacturaEncabezadoCredito, FACENCCRED.serie, FACENCCRED.fecha, TC.nombre, TC.cantidadDias, FACENCCRED.total FROM CLIENTE C, FACTURAENCABEZADOCREDITO FACENCCRED, TIPOCREDITO TC WHERE FACENCCRED.nitCliente = C.nitCliente AND FACENCCRED.idTipoCredito = TC.idTipoCredito AND FACENCCRED.estatus = false; ";
                OdbcCommand cma = new OdbcCommand(cadena, cn.nuevaConexion());
                OdbcDataReader reader = cma.ExecuteReader();
                while (reader.Read())
                {
                    dgvCliente.Rows.Add(reader.GetString(0), reader.GetString(1) + " " + reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR AL MOSTRAR DATOS AL DATAGRIDVIEW " + e);
            }

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

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidacion validar = new clsValidacion();
            validar.funcSueldo(e);
        }

        
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            double precioOriginal = 0;
            double precioActualizado = 0;
            
            if (txtPrice.Text=="")
            {
                MessageBox.Show("INGRESAR UN VALOR PARA CANCELAR DEUDA");
            }
            else
            {
                double aux = double.Parse(txtPrice.Text.ToString());
                try
                {
                    string cadena = "SELECT C.nitCliente, C.nombres, C.apellidos,FACENCCRED.idFacturaEncabezadoCredito, FACENCCRED.serie, FACENCCRED.fecha, TC.nombre, TC.cantidadDias, FACENCCRED.total FROM CLIENTE C, FACTURAENCABEZADOCREDITO FACENCCRED, TIPOCREDITO TC WHERE FACENCCRED.nitCliente = C.nitCliente AND FACENCCRED.idTipoCredito = TC.idTipoCredito AND FACENCCRED.estatus = false AND C.nitCliente = " + int.Parse(cmbCode.Text) + " AND FACENCCRED.idFacturaEncabezadoCredito = "+int.Parse(textBox1.Text.ToString())+"; ";
                    OdbcCommand cma = new OdbcCommand(cadena, cn.nuevaConexion());
                    OdbcDataReader reader = cma.ExecuteReader();
                    while (reader.Read())
                    {
                        precioOriginal = reader.GetDouble(8);
                        //MessageBox.Show("ORIGINAL: " + reader.GetDouble(8));
                    }
                    
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR AL MOSTRAR DATOS AL DATAGRIDVIEW ORIGINALL " + ex);
                }

                
                precioActualizado = precioOriginal - aux;
                if (precioActualizado == 0)
                {
                    try
                    {

                        string Modificar = "UPDATE FACTURAENCABEZADOCREDITO SET estatus = true  WHERE idFacturaEncabezadoCredito=" + Int32.Parse(textBox1.Text);
                        OdbcCommand Consulta = new OdbcCommand(Modificar, cn.nuevaConexion());
                        OdbcDataReader leer = Consulta.ExecuteReader();
                        MessageBox.Show("Ha realizado todos los pagos correspondientes a esta factura");
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudieron mostrar los registros en este momento intente mas tarde" + ex);
                    }
                }
               // MessageBox.Show("ACTUALIZADO: " + precioActualizado);

                try
                {

                    string Modificar = "UPDATE FACTURAENCABEZADOCREDITO SET total = '" + precioActualizado+ "'  WHERE idFacturaEncabezadoCredito=" + Int32.Parse(textBox1.Text);
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

        private void cmbCode_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCode.SelectedIndex = cmbCliente.SelectedIndex;
            dgvCliente.Rows.Clear();
            try
            {
                string cadena = "SELECT C.nitCliente, C.nombres, C.apellidos,FACENCCRED.idFacturaEncabezadoCredito, FACENCCRED.serie, FACENCCRED.fecha, TC.nombre, TC.cantidadDias, FACENCCRED.total FROM CLIENTE C, FACTURAENCABEZADOCREDITO FACENCCRED, TIPOCREDITO TC WHERE FACENCCRED.nitCliente = C.nitCliente AND FACENCCRED.idTipoCredito = TC.idTipoCredito AND FACENCCRED.estatus = false AND C.nitCliente = "+int.Parse(cmbCode.Text)+"; ";
                OdbcCommand cma = new OdbcCommand(cadena, cn.nuevaConexion());
                OdbcDataReader reader = cma.ExecuteReader();
                while (reader.Read())
                {
                    dgvCliente.Rows.Add(reader.GetString(0), reader.GetString(1) + " " + reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL MOSTRAR DATOS AL DATAGRIDVIEW " + e);
            }
        }

        void actualizar()
        {
            dgvCliente.Rows.Clear();
            try
            {
                string cadena = "SELECT C.nitCliente, C.nombres, C.apellidos,FACENCCRED.idFacturaEncabezadoCredito, FACENCCRED.serie, FACENCCRED.fecha, TC.nombre, TC.cantidadDias, FACENCCRED.total FROM CLIENTE C, FACTURAENCABEZADOCREDITO FACENCCRED, TIPOCREDITO TC WHERE FACENCCRED.nitCliente = C.nitCliente AND FACENCCRED.idTipoCredito = TC.idTipoCredito AND FACENCCRED.estatus = false AND C.nitCliente = " + int.Parse(cmbCode.Text) + "; ";
                OdbcCommand cma = new OdbcCommand(cadena, cn.nuevaConexion());
                OdbcDataReader reader = cma.ExecuteReader();
                while (reader.Read())
                {
                    dgvCliente.Rows.Add(reader.GetString(0), reader.GetString(1) + " " + reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL MOSTRAR DATOS AL DATAGRIDVIEW actualizar " + ex);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsValidacion validar = new clsValidacion();
            validar.funcSoloNumeros(e);
        }
    }
}
