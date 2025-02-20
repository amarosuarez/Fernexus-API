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

        /// <summary>
        /// Función que busca un Proveedor por su ID en la base de datos
        /// Pre: Id mayor que 0
        /// Post: El proveedor puede ser null si no se encuentra
        /// </summary>
        /// <param name="idProveedor">Id del proveedor</param>
        /// <returns>Proveedor buscado</returns>
        public static clsProveedor obtenerProveedorPorIdDAL(int idProveedor)
        {
            clsProveedor oProveedor;

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            try
            {
                miComando.CommandText = "EXEC ObtenerProveedorPorId @IdProveedor";
                miComando.Parameters.AddWithValue("@IdProveedor", idProveedor);

                miComando.Connection = clsConexion.GetConnection();

                miLector = miComando.ExecuteReader();
                oProveedor = new clsProveedor();
                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {

                        oProveedor.IdProveedor = (int)miLector["IdProveedor"];

                        oProveedor.Nombre = (string)miLector["Nombre"];

                        oProveedor.Correo = (string)miLector["Correo"];

                        oProveedor.Telefono = (string)miLector["Telefono"];

                        oProveedor.Direccion = (string)miLector["Direccion"];

                        oProveedor.Pais = (string)miLector["Pais"];
                    }
                }
                
            }
            catch (Exception ex) {
                throw;
            }finally
            {
                clsConexion.Desconectar();
            }
            return oProveedor;
        }

        /// <summary>
        /// Función que obtiene los proveedores filtrados por pais de la base de datos y los devuelve como un listado
        /// Pre: Ninguna
        /// Post: El listado de proveedores puede ser null si la tabla está vacía
        /// </summary>
        /// <param name="pais">Pais del proveedor</param>
        /// <returns>Listado de Proveedores filtrado por pais</returns>
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
