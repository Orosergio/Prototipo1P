using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototipo1P
{
    class clsConexion
    {
        //Clase de conexion --------------

        public OdbcConnection nuevaConexion()
        {
            //creacion de la conexion via ODBC
            OdbcConnection conn = new OdbcConnection("DSN=parcial1");
            try
            {
                conn.Open();
            }
            catch (OdbcException er)
            {
                Console.WriteLine("No Conectó" + er);
            }
            return conn;
        }

        //metodo para cerrar la conexion
        public void desconexion(OdbcConnection conn)
        {
            try
            {
                conn.Close();
            }
            catch (OdbcException)
            {
                Console.WriteLine("No Conectó");
            }
        }
    }
}
