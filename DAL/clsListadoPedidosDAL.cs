using DTO;
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
        public static List<clsPedidoCompletoModel> obtenerListadoPedidosCompletoDAL()
        {
            List<clsPedidoCompletoModel> listadoPedidos = new List<clsPedidoCompletoModel>();

            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;

            try
            {
                miComando.Connection = clsConexion.GetConnection();

                miComando.CommandText = "EXEC pedidoCompleto";
                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        int idPedido = (int)miLector["IdPedido"];
                        DateTime fechaPedido = (DateTime)miLector["FechaPedido"];

                        // Buscar si el pedido ya existe en la lista, si no existe lo creamos
                        var oPedido = listadoPedidos.FirstOrDefault(p => p.IdPedido == idPedido);
                        if (oPedido == null)
                        {
                            oPedido = new clsPedidoCompletoModel
                            {
                                IdPedido = idPedido,
                                FechaPedido = fechaPedido,
                                Productos = new List<clsProductoCompletoPrecioTotalModel>(),
                                CosteTotal = 0
                            };
                            listadoPedidos.Add(oPedido);
                        }

                        // Creamos un nuevo producto
                        var oProducto = new clsProductoCompletoPrecioTotalModel
                        {
                            idProducto = (int)miLector["IdProducto"],
                            proveedor = clsListadoProveedoresDAL.obtenerProveedorPorIdDAL((int)miLector["IdProveedor"]),
                            nombre = (string)miLector["Nombre"],
                            precioUd = Convert.ToDouble(miLector["PrecioUnidad"]),
                            cantidad = (int)miLector["Cantidad"],
                            precioTotal = Convert.ToDouble(miLector["PrecioTotal"]),
                            categorias = new List<clsCategoria>()
                        };

                        // Obtenemos la categoría actual
                        var categoria = clsListadoCategoriasDAL.obtenerCategoriaPorIdDAL((int)miLector["IdCategoria"]);

                        // Comprobamos si el producto ya está en el pedido
                        var productoExistente = oPedido.Productos.FirstOrDefault(p => p.idProducto == oProducto.idProducto);
                        if (productoExistente == null)
                        {
                            // Si el producto no existe, lo añadimos al pedido
                            oProducto.categorias.Add(categoria);
                            oPedido.Productos.Add(oProducto);
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
                        oPedido.CosteTotal += oProducto.precioTotal;
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
        public static List<clsPedidoCompletoModel> obtenerListadoPedidosPorFechaDAL(string fechaInicial, string fechaFinal)
        {
            List<clsPedidoCompletoModel> listadoPedidos = new List<clsPedidoCompletoModel>();

            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;

            try
            {
                miComando.Connection = clsConexion.GetConnection();

                miComando.CommandText = "EXEC filtrarPedidosPorFechas @fechaInicio,@fechaFin";
                miComando.Parameters.AddWithValue("@fechaInicio", fechaInicial);
                miComando.Parameters.AddWithValue("@fechaFin", fechaFinal);

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        int idPedido = (int)miLector["IdPedido"];
                        DateTime fechaPedido = (DateTime)miLector["FechaPedido"];

                        // Buscar si el pedido ya existe en la lista, si no existe lo creamos
                        var oPedido = listadoPedidos.FirstOrDefault(p => p.IdPedido == idPedido);
                        if (oPedido == null)
                        {
                            oPedido = new clsPedidoCompletoModel
                            {
                                IdPedido = idPedido,
                                FechaPedido = fechaPedido,
                                Productos = new List<clsProductoCompletoPrecioTotalModel>(),
                                CosteTotal = 0
                            };
                            listadoPedidos.Add(oPedido);
                        }

                        // Creamos un nuevo producto
                        var oProducto = new clsProductoCompletoPrecioTotalModel
                        {
                            idProducto = (int)miLector["IdProducto"],
                            proveedor = clsListadoProveedoresDAL.obtenerProveedorPorIdDAL((int)miLector["IdProveedor"]),
                            nombre = (string)miLector["Nombre"],
                            cantidad = (int)miLector["Cantidad"],
                            precioTotal = Convert.ToDouble(miLector["PrecioTotal"]),
                            precioUd = 0,
                            //precioUd = Convert.ToDouble(miLector["PrecioUnidad"]),
                            categorias = new List<clsCategoria>()
                        };

                        // Obtenemos la categoría actual
                        var categoria = clsListadoCategoriasDAL.obtenerCategoriaPorIdDAL((int)miLector["IdCategoria"]);

                        // Comprobamos si el producto ya está en el pedido
                        var productoExistente = oPedido.Productos.FirstOrDefault(p => p.idProducto == oProducto.idProducto);
                        if (productoExistente == null)
                        {
                            // Si el producto no existe, lo añadimos al pedido
                            oProducto.categorias.Add(categoria);
                            oPedido.Productos.Add(oProducto);
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
                        oPedido.CosteTotal += oProducto.precioTotal;
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
            return listadoPedidos;
        }

        /// <summary>
        /// Función que obtiene los pedidos filtrados por producto de la base de datos y los devuelve como un listado
        /// Pre: ID mayor que 0
        /// Post: El listado de pedidos puede ser null si la tabla está vacía
        /// </summary>
        /// <param name="idProducto">Id del producto</param>
        /// <returns>Listado de Pedidos filtrado por producto</returns>
        public static List<clsPedidoCompletoModel> obtenerListadoPedidosPorProductoDAL(int idProducto)
        {
            List<clsPedidoCompletoModel> listadoPedidos = new List<clsPedidoCompletoModel>();

            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;

            try
            {
                miComando.Connection = clsConexion.GetConnection();
                miComando.CommandText = "EXEC filtrarPedidosPorProducto @idProducto";
                miComando.Parameters.AddWithValue("@idProducto", idProducto);

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        int idPedido = (int)miLector["IdPedido"];
                        DateTime fechaPedido = (DateTime)miLector["FechaPedido"];

                        // Buscar si el pedido ya existe en la lista, si no existe lo creamos
                        var oPedido = listadoPedidos.FirstOrDefault(p => p.IdPedido == idPedido);
                        if (oPedido == null)
                        {
                            oPedido = new clsPedidoCompletoModel
                            {
                                IdPedido = idPedido,
                                FechaPedido = fechaPedido,
                                Productos = new List<clsProductoCompletoPrecioTotalModel>(),
                                CosteTotal = 0
                            };
                            listadoPedidos.Add(oPedido);
                        }

                        // Creamos un nuevo producto
                        var oProducto = new clsProductoCompletoPrecioTotalModel
                        {
                            idProducto = (int)miLector["IdProducto"],
                            proveedor = clsListadoProveedoresDAL.obtenerProveedorPorIdDAL((int)miLector["IdProveedor"]),
                            nombre = (string)miLector["Nombre"],
                            cantidad = (int)miLector["Cantidad"],
                            precioTotal = Convert.ToDouble(miLector["PrecioTotal"]),
                            precioUd = 0,
                            //precioUd = Convert.ToDouble(miLector["PrecioUnidad"]),
                            categorias = new List<clsCategoria>()
                        };

                        // Obtenemos la categoría actual
                        var categoria = clsListadoCategoriasDAL.obtenerCategoriaPorIdDAL((int)miLector["IdCategoria"]);

                        // Comprobamos si el producto ya está en el pedido
                        var productoExistente = oPedido.Productos.FirstOrDefault(p => p.idProducto == oProducto.idProducto);
                        if (productoExistente == null)
                        {
                            // Si el producto no existe, lo añadimos al pedido
                            oProducto.categorias.Add(categoria);
                            oPedido.Productos.Add(oProducto);
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
                        oPedido.CosteTotal += oProducto.precioTotal;
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

            return listadoPedidos;
        }

    }
}