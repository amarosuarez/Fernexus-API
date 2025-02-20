using ENT;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class clsListadoProveedoresDAL
    {
        /// <summary>
        /// Metodo para obtener el listado de Proveedores completo de la base de datos
        /// Pre: None
        /// Post: Listado de Proveedores puede ser null si la tabla está vacía
        /// </summary>
        /// <returns></returns>
        public static List<clsProveedor> obtenerListadoProveedoresCompletoDAL()
        {
            List<clsProveedor> listaProveedores = new List<clsProveedor>();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            clsProveedor oProveedor;

            try
            {


                miComando.CommandText = "SELECT * FROM Proveedores";

                miComando.Connection = clsConexion.GetConnection(); ;

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        oProveedor = new clsProveedor();

                        oProveedor.IdProveedor = (int)miLector["IdProveedor"];

                        oProveedor.Nombre = (string)miLector["Nombre"];

                        oProveedor.Correo = (string)miLector["Correo"];

                        oProveedor.Telefono = (string)miLector["Telefono"];

                        oProveedor.Direccion = (string)miLector["Direccion"];

                        oProveedor.Pais = (string)miLector["Pais"];
                        

                        listaProveedores.Add(oProveedor);
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
                clsConexion.Desconectar();
            }
            return listaProveedores;
        }

        public static List<clsProveedor> obtenerListadoProveedoresPorPaisDAL(string pais)
        {
            List<clsProveedor> listaProveedores = new List<clsProveedor>();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            clsProveedor oProveedor;

            try
            {
                miComando.CommandText = "EXEC FiltrarProveedoresPorPais @Pais";
                miComando.Parameters.AddWithValue("@Pais", pais);

                miComando.Connection = clsConexion.GetConnection();

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        oProveedor = new clsProveedor();

                        oProveedor.IdProveedor = (int)miLector["IdProveedor"];

                        oProveedor.Nombre = (string)miLector["Nombre"];

                        oProveedor.Correo = (string)miLector["Correo"];

                        oProveedor.Telefono = (string)miLector["Telefono"];

                        oProveedor.Direccion = (string)miLector["Direccion"];

                        oProveedor.Pais = (string)miLector["Pais"];


                        listaProveedores.Add(oProveedor);
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
                clsConexion.Desconectar();
            }

            return listaProveedores;
        }
    }
}
