using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos
{
    public class Conexion
    {
        private string Base;
        private string Servidor;
        private string Usuario;
        private string Clave;
        private bool Seguridad;
        private static Conexion Con = null;
        private Conexion()
        {
            this.Base = "dbsistema";
            this.Servidor = "LAPTOP-QVNE9EO2";
            this.Usuario = "sa";
            this.Clave = "Mausql123";
            this.Seguridad = true;

        }
        public SqlConnection CrearConexion()
        {
            SqlConnection cadena = new SqlConnection();
            try
            {
                cadena.ConnectionString = "Server=" + this.Servidor + "; Database=" + this.Base + ";";
                //Si no funciona utilizar la cadena de conexion fijada en app.Config en la capa de presentacion
                if (Seguridad is true)
                {
                    cadena.ConnectionString= cadena.ConnectionString+"Integrated Security = SSPI";
                }
                else
                {
                    cadena.ConnectionString = cadena.ConnectionString +"User Id="+this.Usuario+";Password="+this.Clave;
                }
            }
            catch (Exception ex)
            {
                cadena = null;

                throw ex;
            }
            return cadena;
        }

        public static Conexion getInstancia()
        {
            if(Con == null)
            {
                Con = new Conexion();
            }
            return Con;
        }
    }
}
