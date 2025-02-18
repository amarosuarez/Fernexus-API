using ENT;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class clsListadoProovedoresDAL
    {
        /// <summary>
        /// Metodo para obtener el listado de proovedores completo de la base de datos
        /// Pre: None
        /// Post: Listado de proovedores puede ser null si la tabla está vacía
        /// </summary>
        /// <returns></returns>
        public static List<clsProovedor> obtenerListadoProovedoresCompletoDAL()
        {
            List<clsProovedor> listaProovedores = new List<clsProovedor>();

            clsConexion miConexion = new clsConexion();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            clsProovedor oProovedor;

            try
            {


                miComando.CommandText = "SELECT * FROM Proovedores";

                miComando.Connection = miConexion.ObtenerConexion(); ;

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        oProovedor = new clsProovedor();

                        oProovedor.IdProovedor = (int)miLector["IdProveedor"];

                        oProovedor.Nombre = (string)miLector["Nombre"];

                        oProovedor.Correo = (string)miLector["Correo"];

                        oProovedor.Telefono = (string)miLector["Telefono"];

                        oProovedor.Direccion = (string)miLector["Direccion"];

                        oProovedor.Pais = (string)miLector["Pais"];
                        

                        listaProovedores.Add(oProovedor);
                    }
                }
                miLector.Close();


            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                miConexion.Desconectar();
            }
            return listaProovedores;
        }

        public static List<clsProovedor> obtenerListadoProovedoresPorPaisDAL(string pais)
        {
            List<clsProovedor> listaProovedores = new List<clsProovedor>();

            clsConexion miConexion = new clsConexion();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            clsProovedor oProovedor;

            try
            {
                miComando.CommandText = "EXEC FiltrarProovedoresPorPais @Pais";
                miComando.Parameters.AddWithValue("@Pais", pais);

                miComando.Connection = miConexion.ObtenerConexion();

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        oProovedor = new clsProovedor();

                        oProovedor.IdProovedor = (int)miLector["IdProveedor"];

                        oProovedor.Nombre = (string)miLector["Nombre"];

                        oProovedor.Correo = (string)miLector["Correo"];

                        oProovedor.Telefono = (string)miLector["Telefono"];

                        oProovedor.Direccion = (string)miLector["Direccion"];

                        oProovedor.Pais = (string)miLector["Pais"];


                        listaProovedores.Add(oProovedor);
                    }
                }
                miLector.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                miConexion.Desconectar();
            }

            return listaProovedores;
        }
    }
}
