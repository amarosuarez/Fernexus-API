using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class clsManejadoraPedidosDAL
    {
        /// <summary>
        /// Función que recibe una lista de productos completos y la fecha de pedido y crea el pedido
        /// </summary>
        /// <param name="listaProductosCompleto">Listado de Productos completos</param>
        /// <param name="fechaPedido">Fecha del pedido</param>
        /// <returns>Número de filas afectadas</returns>
        public int crearPedidoDAL(List<clsProductoCompletoModel> listaProductosCompleto, String fechaPedido)
        {
            int numFilasAfectadas = 0;
            



            return numFilasAfectadas;
        }

        /// <summary>
        /// Función que recibe el ID de un pedido, los busca en la DB y lo devuelve<br>
        /// Pre: El ID debe ser mayor que 0</br>
        /// Post: Ninguna
        /// </summary>
        /// <param name="idPedido">ID del pedido a buscar</param>
        /// <returns>Pedido completo</returns>
        public clsPedidoCompletoModel buscarPedidoDAL(int idPedido)
        {
            clsPedidoCompletoModel pedidoCompletoModel = null;

            SqlConnection conexion = new SqlConnection();
            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;

            try
            {
                conexion = clsConexion.GetConnection();

                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    // TODO: Esperando al procedure

                    //miComando.Parameters.Add("@IdPedido", System.Data.SqlDbType.Int).Value = idPedido;
                    //miComando.CommandText = "SEARCH WHERE IdPedido = @IdPedido";
                    //miComando.Connection = conexion;

                }

            }
            catch (Exception ex) {
                throw;
            }

            return pedidoCompletoModel;
        }
        
        /// <summary>
        /// Función que recibe el ID de un pedido, y lo elimina si existe<br>
        /// Pre: El ID debe ser mayor que 0</br>
        /// Post: Ninguno
        /// </summary>
        /// <param name="idPedido">ID del pedido a eliminar</param>
        /// <returns>Número de filas afectadas</returns>
        public int eliminarPedidoDAL(int idPedido)
        {
            int numeroFilasAfectadas = 0;

            SqlConnection conexion = new SqlConnection();
            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;

            try
            {
                conexion = clsConexion.GetConnection();

                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    miComando.Parameters.Add("@IdPedido", System.Data.SqlDbType.Int).Value = idPedido;
                    miComando.CommandText = "DELETE FROM Pedidos WHERE IdPedido = @IdPedido";
                    miComando.Connection = conexion;

                    numeroFilasAfectadas = miComando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            } 
            finally
            {
                clsConexion.Desconectar();
            }

            return numeroFilasAfectadas;
        }

        /// <summary>
        /// Función que recibe el ID de un pedido y un pedido completo y actualiza el pedido con dicho ID<br>
        /// Pre: El ID debe ser mayor que 0</br>
        /// Post: Ninguno
        /// </summary>
        /// <param name="idPedido">ID del pedido a actualizar</param>
        /// <param name="pedidoCompleto">Objeto pedido completo con los datos actualizados</param>
        /// <returns>Número de filas afectadas</returns>
        public int actualizarPedidoDAL(int idPedido, clsPedidoCompletoModel pedidoCompleto)
        {
            int numeroFilasAfectadas = 0;
            
            SqlConnection conexion = new SqlConnection();
            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;

            //try
            //{
            //    conexion = clsConexion.GetConnection();

            //    miComando.CommandText = "EXEC filtrarPedidosConDatosDelProducto @IdPedido";
            //    miComando.Parameters.AddWithValue("@IdProveedor", idPedido);

            //    miLector = miComando.ExecuteReader();
            //}


            return numeroFilasAfectadas;
        }
    }
}
