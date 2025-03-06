using System;
using System.Collections.Generic;
using System.Data;
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
        /// <returns>Número de filas afectadas</returns>
        public static int crearPedidoDAL(List<clsProductoCompletoPrecioTotalModel> listaProductosCompleto)
        {
            int numFilasAfectadas = 0;
            clsPedidoCompletoModel nuevoPedido = null;

            SqlConnection conexion = null;
            SqlCommand comando = null;

            try
            {
                conexion = clsConexion.GetConnection(); // Obtén la conexión
                comando = new SqlCommand("crearPedido", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Crear DataTable con las 2 columnas que espera el tipo de tabla
                DataTable tablaProductos = new DataTable();
                tablaProductos.Columns.Add("IdProducto", typeof(int));
                tablaProductos.Columns.Add("IdProveedor", typeof(int));
                tablaProductos.Columns.Add("Cantidad", typeof(int));

                // Llenar la DataTable con los productos
                foreach (var producto in listaProductosCompleto)
                {
                    tablaProductos.Rows.Add(producto.idProducto, producto.proveedor.IdProveedor, producto.cantidad);  // Añadir fila con idProducto y IdProveedor
                }

                // Agregar parámetros al procedimiento almacenado
                SqlParameter parametroLista = new SqlParameter("@lista", SqlDbType.Structured)
                {
                    TypeName = "ListaProductos", // Nombre del tipo tabla en SQL Server
                    Value = tablaProductos
                };

                comando.Parameters.Add(parametroLista);


                // Ejecutar el procedimiento y obtener el número de filas afectadas
                numFilasAfectadas = comando.ExecuteNonQuery(); // Usamos ExecuteNonQuery para obtener el número de filas afectadas

            }
            catch (Exception e)
            {
                // Puedes agregar un log aquí para obtener más detalles del error si lo necesitas.
                throw;
            }
            finally
            {
                // Cerrar la conexión y liberar los recursos de forma explícita
                if (comando != null)
                {
                    comando.Dispose();
                }

                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }

                clsConexion.Desconectar(); // Llamada a la desconexión manual si es necesario
            }

            return numFilasAfectadas; // Devolver el número de filas afectadas
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

            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;

            try
            {
                miComando.Connection = clsConexion.GetConnection();

                miComando.CommandText = "EXEC pedidoCompletoPorId @IdPedido";
                miComando.Parameters.AddWithValue("@IdPedido", idPedido);

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    pedidoCompletoModel = new clsPedidoCompletoModel();

                    while (miLector.Read())
                    {
                        //int idPedido = (int)miLector["IdPedido"];
                        DateTime fechaPedido = (DateTime)miLector["FechaPedido"];

                        pedidoCompletoModel = new clsPedidoCompletoModel
                        {
                            IdPedido = idPedido,
                            FechaPedido = fechaPedido,
                            Productos = new List<clsProductoCompletoPrecioTotalModel>(),
                            CosteTotal = 0
                        };

                        // Creamos un nuevo producto
                        var oProducto = new clsProductoCompletoPrecioTotalModel
                        {
                            idProducto = (int)miLector["IdProducto"],
                            proveedor = clsListadoProveedoresDAL.obtenerProveedorPorIdDAL((int)miLector["IdProveedor"]),
                            nombre = (string)miLector["Nombre"],
                            cantidad = (int)miLector["Cantidad"],
                            precioTotal = Convert.ToDouble(miLector["PrecioTotal"]),
                            precioUd = Convert.ToDouble(miLector["PrecioUnidad"]),
                            categorias = new List<clsCategoria>()
                        };

                        // Obtenemos la categoría actual
                        var categoria = clsListadoCategoriasDAL.obtenerCategoriaPorIdDAL((int)miLector["IdCategoria"]);

                        // Comprobamos si el producto ya está en el pedido
                        var productoExistente = pedidoCompletoModel.Productos.FirstOrDefault(p => p.idProducto == oProducto.idProducto);
                        if (productoExistente == null)
                        {
                            // Si el producto no existe, lo añadimos al pedido
                            oProducto.categorias.Add(categoria);
                            pedidoCompletoModel.Productos.Add(oProducto);
                        }
                        else
                        {
                            // Si el producto ya existe, añadimos la categoría a la lista de categorías del producto existente
                            productoExistente.categorias.Add(categoria);

                            // Actualizamos las propiedades del producto existente
                            productoExistente.cantidad += oProducto.cantidad;
                            productoExistente.precioTotal += oProducto.precioTotal;
                        }

                        // Actualizamos el coste total del pedido
                        pedidoCompletoModel.CosteTotal += oProducto.precioTotal;
                    }
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
            return pedidoCompletoModel;
        }

        //public static clsPedidoCompletoModel buscarPedidoDAL(int idPedido)
        //{
        //    clsPedidoCompletoModel pedidoCompletoModel = null;
        //    List<clsProductoCompletoModel> listaProductos = new List<clsProductoCompletoModel>();
        //    List<int> listaIdProveedores = new List<int>();

        //    SqlConnection conexion = new SqlConnection();
        //    SqlCommand miComando = new SqlCommand();
        //    SqlCommand miComando2 = new SqlCommand();
        //    SqlDataReader miLector;
        //    SqlDataReader miLector2;

        //    try
        //    {
        //        conexion = clsConexion.GetConnection();

        //        if (conexion.State == System.Data.ConnectionState.Open)
        //        {
        //            // Primer comando
        //            miComando.CommandText = "EXEC filtrarPedidosConDatosDelProducto @IdPedido";
        //            miComando.Parameters.AddWithValue("@IdPedido", idPedido);
        //            miComando.Connection = conexion;

        //            miLector = miComando.ExecuteReader();

        //            // Leer todos los productos primero y almacenar en memoria
        //            if (miLector.HasRows)
        //            {
        //                while (miLector.Read())
        //                {
        //                    if (pedidoCompletoModel == null)
        //                    {
        //                        pedidoCompletoModel = new clsPedidoCompletoModel();
        //                        //pedidoCompletoModel = new clsPedidoCompletoModel
        //                        //{
        //                        //    idPedido = (int)miLector["IdPedido"],
        //                        //    fechaPedido = ((DateTime)miLector["FechaPedido"]).ToString("yyyy-MM-dd"),
        //                        //    costeTotal = Convert.ToDouble(miLector["Coste"])
        //                        //};
        //                    }

        //                    var producto = new clsProductoCompletoModel
        //                    {
        //                        idProducto = (int)miLector["IdProducto"],
        //                        cantidad = (int)miLector["Cantidad"],
        //                        nombre = (string)miLector["NombreProducto"]
        //                    };

        //                    listaProductos.Add(producto);
        //                }
        //            }
        //            miLector.Close(); // Cerramos aquí después de leer todos los datos

        //            // Segundo comando
        //            miComando2.CommandText = "SELECT IdProveedor FROM ProveedoresPedidos WHERE IdPedido = @IdPedido";
        //            miComando2.Parameters.AddWithValue("@IdPedido", idPedido);
        //            miComando2.Connection = conexion;

        //            miLector2 = miComando2.ExecuteReader();

        //            // Leer los proveedores y almacenarlos en memoria
        //            if (miLector2.HasRows)
        //            {
        //                while (miLector2.Read())
        //                {
        //                    listaIdProveedores.Add((int)miLector2["IdProveedor"]);
        //                }
        //            }
        //            miLector2.Close();

        //            // Asignar los proveedores a los productos en orden
        //            for (int i = 0; i < listaProductos.Count; i++)
        //            {
        //                if (i < listaIdProveedores.Count)
        //                {
        //                    //listaProductos[i].idProovedor = listaIdProveedores[i];
        //                }
        //            }

        //            //pedidoCompletoModel.productos = listaProductos;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Ocurrió un error inesperado al intentar obtener el producto por ID: " + ex.Message);
        //    }

        //    return pedidoCompletoModel;
        //}



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
            int numFilasAfectadas = 0;
            clsPedidoCompletoModel nuevoPedido = null;

            SqlConnection conexion = null;
            SqlCommand comando = null;

            try
            {
                conexion = clsConexion.GetConnection(); // Obtén la conexión
                comando = new SqlCommand("modificarPedido", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Crear DataTable con las 2 columnas que espera el tipo de tabla
                DataTable tablaProductos = new DataTable();
                tablaProductos.Columns.Add("IdProducto", typeof(int));
                tablaProductos.Columns.Add("IdProveedor", typeof(int));
                tablaProductos.Columns.Add("Cantidad", typeof(int));

                // Llenar la DataTable con los productos
                foreach (var producto in pedidoCompleto.Productos)
                {
                    tablaProductos.Rows.Add(producto.idProducto, producto.proveedor.IdProveedor, producto.cantidad);  // Añadir fila con idProducto y IdProveedor
                }

                // Agregar parámetros al procedimiento almacenado
                comando.Parameters.Add("@idPedido", System.Data.SqlDbType.Int).Value = idPedido;

                SqlParameter parametroLista = new SqlParameter("@lista", SqlDbType.Structured)
                {
                    TypeName = "ListaProductos", // Nombre del tipo tabla en SQL Server
                    Value = tablaProductos
                };

                comando.Parameters.Add(parametroLista);


                // Ejecutar el procedimiento y obtener el número de filas afectadas
                numFilasAfectadas = comando.ExecuteNonQuery(); // Usamos ExecuteNonQuery para obtener el número de filas afectadas

            }
            catch (Exception e)
            {
                // Puedes agregar un log aquí para obtener más detalles del error si lo necesitas.
                throw;
            }
            finally
            {
                // Cerrar la conexión y liberar los recursos de forma explícita
                if (comando != null)
                {
                    comando.Dispose();
                }

                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }

                clsConexion.Desconectar(); // Llamada a la desconexión manual si es necesario
            }

            return numFilasAfectadas; // Devolver el número de filas afectadas
        }
    }
}
