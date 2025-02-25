using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using ENT;
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
        public static int crearPedidoDAL(List<clsProductoCompletoModel> listaProductosCompleto, String fechaPedido)
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
        public static clsPedidoCompletoModel buscarPedidoDAL(int idPedido)
        {
            clsPedidoCompletoModel pedidoCompletoModel = null;
            List<clsProductoCompletoModel> listaProductos = new List<clsProductoCompletoModel>();
            List<int> listaIdProveedores = new List<int>();

            SqlConnection conexion = new SqlConnection();
            SqlCommand miComando = new SqlCommand();
            SqlCommand miComando2 = new SqlCommand();
            SqlDataReader miLector;
            SqlDataReader miLector2;

            try
            {
                conexion = clsConexion.GetConnection();

                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    // Primer comando
                    miComando.CommandText = "EXEC filtrarPedidosConDatosDelProducto @IdPedido";
                    miComando.Parameters.AddWithValue("@IdPedido", idPedido);
                    miComando.Connection = conexion;

                    miLector = miComando.ExecuteReader();

                    // Leer todos los productos primero y almacenar en memoria
                    if (miLector.HasRows)
                    {
                        while (miLector.Read())
                        {
                            if (pedidoCompletoModel == null)
                            {
                                pedidoCompletoModel = new clsPedidoCompletoModel
                                {
                                    idPedido = (int)miLector["IdPedido"],
                                    fechaPedido = ((DateTime)miLector["FechaPedido"]).ToString("yyyy-MM-dd"),
                                    costeTotal = Convert.ToDouble(miLector["Coste"])
                                };
                            }

                            var producto = new clsProductoCompletoModel
                            {
                                idProducto = (int)miLector["IdProducto"],
                                cantidad = (int)miLector["Cantidad"],
                                nombre = (string)miLector["NombreProducto"]
                            };

                            listaProductos.Add(producto);
                        }
                    }
                    miLector.Close(); // Cerramos aquí después de leer todos los datos

                    // Segundo comando
                    miComando2.CommandText = "SELECT IdProveedor FROM ProveedoresPedidos WHERE IdPedido = @IdPedido";
                    miComando2.Parameters.AddWithValue("@IdPedido", idPedido);
                    miComando2.Connection = conexion;

                    miLector2 = miComando2.ExecuteReader();

                    // Leer los proveedores y almacenarlos en memoria
                    if (miLector2.HasRows)
                    {
                        while (miLector2.Read())
                        {
                            listaIdProveedores.Add((int)miLector2["IdProveedor"]);
                        }
                    }
                    miLector2.Close();

                    // Asignar los proveedores a los productos en orden
                    for (int i = 0; i < listaProductos.Count; i++)
                    {
                        if (i < listaIdProveedores.Count)
                        {
                            listaProductos[i].idProovedor = listaIdProveedores[i];
                        }
                    }

                    pedidoCompletoModel.productos = listaProductos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al intentar obtener el producto por ID: " + ex.Message);
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
        public static int eliminarPedidoDAL(int idPedido)
        {
            // TODO: El pedido no se elimina, se cancela
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
        public static int actualizarPedidoDAL(int idPedido, clsPedidoCompletoModel pedidoCompleto)
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
