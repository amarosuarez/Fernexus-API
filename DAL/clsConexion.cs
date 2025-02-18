using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class clsConexion
    {
        /// <summary>
        /// Metodo para obtener la conexion a la base de datos de azure
        /// </summary>
        /// <returns>Devuelve la conexion a la base de datos</returns>
        public SqlConnection ObtenerConexion()
        {
            SqlConnection miConexion = new SqlConnection();

            try
            {

                miConexion.ConnectionString = "server=marco-holguin.database.windows.net;database=MarcoDB;uid=usuario;pwd=LaCampana123;trustServerCertificate = true;";

                miConexion.Open();

            }
            catch (Exception ex)
            {
                throw;
            }

            return miConexion;
        }

        /// <summary>
        /// Metodo para desconectar de la base de datos de azure
        /// </summary>
        /// <returns>Devuelve la conexion a la base de datos</returns>
        public SqlConnection Desconectar()
        {
            SqlConnection miConexion = new SqlConnection();

            try
            {

                miConexion.ConnectionString = "server=marco-holguin.database.windows.net;database=MarcoDB;uid=usuario;pwd=LaCampana123;trustServerCertificate = true;";

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                miConexion.Close();
            }

            return miConexion;
        }
    }
}
