using ENT;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class clsListadoPedidosDAL
    {
        /// <summary>
        /// Función que obtiene todos los pedidos de la base de datos y los devuelve como un listado
        /// Pre: Ninguna
        /// Post: El listado de pedidos puede ser null si la tabla está vacía
        /// </summary>
        /// <returns>Listado de Pedidos</returns>
        public static List<clsPedido> obtenerListadoPedidosCompletoDAL()
        {
            List<clsPedido> listadoPedidos = new List<clsPedido>();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            clsPedido oPedido;

            try
            {
                miComando.CommandText = "SELECT * FROM Pedidos";

                miComando.Connection = clsConexion.GetConnection(); ;

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        oPedido = new clsPedido();

                        oPedido.IdPedido = (int)miLector["IdPedido"];

                        oPedido.FechaPedido = (DateTime)miLector["FechaPedido"];

                        oPedido.Coste = (double)miLector["Coste"];

                        listadoPedidos.Add(oPedido);
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
            return listadoPedidos;
        }
        /// <summary>
        /// Función que obtiene los pedidos filtrados por fecha de la base de datos y los devuelve como un listado
        /// Pre: Formato de las fechas debe ser yyyy-mm-dd
        /// Post: El listado de pedidos puede ser null si la tabla está vacía
        /// </summary>
        /// <param name="fechaInicial">Fecha de inicio del rango</param>
        /// <param name="fechaFinal">Fecha de fin del rango</param>
        /// <returns>Listado de Pedidos filtrado por fecha</returns>
        public static List<clsPedido> obtenerListadoPedidosPorFechaDAL(string fechaInicial, string fechaFinal)
        {
            List<clsPedido> listadoPedidos = new List<clsPedido>();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            clsPedido oPedido;

            try
            {
                miComando.CommandText = "EXEC filtrarPedidosPorFechas @fechaInicio,@fechaFin";
                miComando.Parameters.AddWithValue("@fechaInicio", fechaInicial);
                miComando.Parameters.AddWithValue("@fechaFin", fechaFinal);

                miComando.Connection = clsConexion.GetConnection();

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        oPedido = new clsPedido();

                        oPedido.IdPedido = (int)miLector["IdPedido"];

                        oPedido.FechaPedido = (DateTime)miLector["FechaPedido"];

                        oPedido.Coste = (double)miLector["Coste"];

                        listadoPedidos.Add(oPedido);
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
            return listadoPedidos;
        }

        /// <summary>
        /// Función que obtiene los pedidos filtrados por producto de la base de datos y los devuelve como un listado
        /// Pre: ID mayor que 0
        /// Post: El listado de pedidos puede ser null si la tabla está vacía
        /// </summary>
        /// <param name="idProducto">Id del producto</param>
        /// <returns>Listado de Pedidos filtrado por producto</returns>
        public static List<clsPedido> obtenerListadoPedidosPorProductoDAL(int idProducto)
        {
            List<clsPedido> listadoPedidos = new List<clsPedido>();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            clsPedido oPedido;

            try
            {
                miComando.CommandText = "EXEC pedidosPorProducto @idProducto";
                miComando.Parameters.AddWithValue("@idProducto", idProducto);
                miLector = miComando.ExecuteReader();
                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        oPedido = new clsPedido();

                        oPedido.IdPedido = (int)miLector["IdPedido"];

                        oPedido.FechaPedido = (DateTime)miLector["FechaPedido"];

                        oPedido.Coste = (double)miLector["Coste"];

                        listadoPedidos.Add(oPedido);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }finally {
                clsConexion.Desconectar();
            }
            return listadoPedidos;
        }
    }
}
