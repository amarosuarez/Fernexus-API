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
        static List<clsPedidoCompletoModel> pedidos = new List<clsPedidoCompletoModel>
            {
                new clsPedidoCompletoModel(1, new List<clsProductoCompletoModel>
                {
                    new clsProductoCompletoModel(101, new clsProveedor(1, "Proveedor A", "contacto@proveedora.com", "123456789", "Calle 123", "España"), "Producto 1", 10.5, 2, 21.0, new clsCategoria(1, "Categoría 1")),
                    new clsProductoCompletoModel(102, new clsProveedor(2, "Proveedor B", "contacto@proveedorb.com", "987654321", "Avenida 456", "Francia"), "Producto 2", 15.0, 1, 15.0, new clsCategoria(2, "Categoría 2"))
                }, 36.0, DateTime.Now),

                new clsPedidoCompletoModel(2, new List<clsProductoCompletoModel>
                {
                    new clsProductoCompletoModel(103, new clsProveedor(3, "Proveedor C", "info@proveedorc.com", "654987321", "Calle 789", "Italia"), "Producto 3", 8.0, 5, 40.0, new clsCategoria(3, "Categoría 3")),
                    new clsProductoCompletoModel(104, new clsProveedor(1, "Proveedor A", "contacto@proveedora.com", "123456789", "Calle 123", "España"), "Producto 4", 20.0, 2, 40.0, new clsCategoria(1, "Categoría 1"))
                }, 80.0, DateTime.Now.AddDays(-1)),

                new clsPedidoCompletoModel(3, new List<clsProductoCompletoModel>
                {
                    new clsProductoCompletoModel(105, new clsProveedor(2, "Proveedor B", "contacto@proveedorb.com", "987654321", "Avenida 456", "Francia"), "Producto 5", 12.0, 3, 36.0, new clsCategoria(2, "Categoría 2")),
                    new clsProductoCompletoModel(106, new clsProveedor(3, "Proveedor C", "info@proveedorc.com", "654987321", "Calle 789", "Italia"), "Producto 6", 7.5, 4, 30.0, new clsCategoria(3, "Categoría 3"))
                }, 66.0, DateTime.Now.AddDays(-2))
            };

        /// <summary>
        /// Función que obtiene todos los pedidos de la base de datos y los devuelve como un listado
        /// Pre: Ninguna
        /// Post: El listado de pedidos puede ser null si la tabla está vacía
        /// </summary>
        /// <returns>Listado de Pedidos</returns>
        public static List<clsPedidoCompletoModel> obtenerListadoPedidosCompletoDAL()
        {
            List<clsPedidoCompletoModel> listadoPedidos = new List<clsPedidoCompletoModel>();

            List<clsProductoCompletoModel> listaProductos = new List<clsProductoCompletoModel>();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            clsPedidoCompletoModel oPedido;

            clsProductoCompletoModel oProducto;

            try
            {
                miComando.CommandText = "EXEC pedidoCompleto";

                miComando.Connection = clsConexion.GetConnection();

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        oPedido = new clsPedidoCompletoModel();

                        oProducto = new clsProductoCompletoModel();

                        oPedido.IdPedido = (int)miLector["IdPedido"];

                        oPedido.FechaPedido = (DateTime)miLector["FechaPedido"];

                        if ((int)miLector["IdPedido"] == oPedido.IdPedido)
                        {
                            oProducto.idProducto = (int)miLector["IdProducto"];

                            oProducto.proveedor = clsListadoProveedoresDAL.obtenerProveedorPorIdDAL((int)miLector["IdProveedor"]);

                            oProducto.nombre = (string)miLector["Nombre"];

                            oProducto.categoria = clsListadoCategoriasDAL.obtenerCategoriaPorIdDAL((int)miLector["IdCategoria"]);

                            oProducto.precioUd = Convert.ToDouble(miLector["PrecioUnidad"]);

                            oProducto.cantidad = (int)miLector["Cantidad"];

                            oProducto.precioTotal = Convert.ToDouble(miLector["PrecioTotal"]);

                            oPedido.CosteTotal += oProducto.precioTotal;

                            listaProductos.Add(oProducto);
                        }

                        oPedido.Productos = listaProductos;

                        // Para no duplicar pedidos y añadir sus productos correctamente
                        if (listadoPedidos.Any(p => p.IdPedido == oPedido.IdPedido))
                        {
                            var pedidoExistente = listadoPedidos.FirstOrDefault(p => p.IdPedido == oPedido.IdPedido);

                            if (pedidoExistente != null)
                            {
                                // Sumar el precioTotal del nuevo producto al CosteTotal del pedido
                                pedidoExistente.CosteTotal += listaProductos.Sum(p => p.precioTotal);

                                // Concatenar la lista anterior con la nueva lista de productos
                                pedidoExistente.Productos = pedidoExistente.Productos.Concat(listaProductos).ToList();
                            }

                        } else
                        {
                            listadoPedidos.Add(oPedido);
                        }

                        //listaProductos = null;
                        listaProductos = new List<clsProductoCompletoModel>();
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
            //return pedidos;
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

            List<clsProductoCompletoModel> listaProductos = new List<clsProductoCompletoModel>();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            clsPedidoCompletoModel oPedido;

            clsProductoCompletoModel oProducto;

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
                        oPedido = new clsPedidoCompletoModel();

                        oProducto = new clsProductoCompletoModel();

                        oPedido.IdPedido = (int)miLector["IdPedido"];

                        oPedido.FechaPedido = (DateTime)miLector["FechaPedido"];

                        if ((int)miLector["IdPedido"] == oPedido.IdPedido)
                        {
                            oProducto.idProducto = (int)miLector["IdProducto"];

                            oProducto.proveedor = clsListadoProveedoresDAL.obtenerProveedorPorIdDAL((int)miLector["IdProveedor"]);

                            oProducto.nombre = (string)miLector["Nombre"];

                            oProducto.categoria = clsListadoCategoriasDAL.obtenerCategoriaPorIdDAL((int)miLector["IdCategoria"]);

                            oProducto.precioUd = Convert.ToDouble(miLector["PrecioUnidad"]);

                            oProducto.cantidad = (int)miLector["Cantidad"];

                            oProducto.precioTotal = Convert.ToDouble(miLector["PrecioTotal"]);

                            oPedido.CosteTotal += oProducto.precioTotal;

                            listaProductos.Add(oProducto);
                        }

                        oPedido.Productos = listaProductos;

                        // Para no duplicar pedidos y añadir sus productos correctamente
                        if (listadoPedidos.Any(p => p.IdPedido == oPedido.IdPedido))
                        {
                            var pedidoExistente = listadoPedidos.FirstOrDefault(p => p.IdPedido == oPedido.IdPedido);

                            if (pedidoExistente != null)
                            {
                                // Sumar el precioTotal del nuevo producto al CosteTotal del pedido
                                pedidoExistente.CosteTotal += listaProductos.Sum(p => p.precioTotal);

                                // Concatenar la lista anterior con la nueva lista de productos
                                pedidoExistente.Productos = pedidoExistente.Productos.Concat(listaProductos).ToList();
                            }

                        }
                        else
                        {
                            listadoPedidos.Add(oPedido);
                        }

                        //listaProductos = null;
                        listaProductos = new List<clsProductoCompletoModel>();
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

            List<clsProductoCompletoModel> listaProductos = new List<clsProductoCompletoModel>();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            clsPedidoCompletoModel oPedido;

            clsProductoCompletoModel oProducto;

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
                        oPedido = new clsPedidoCompletoModel();

                        oProducto = new clsProductoCompletoModel();

                        oPedido.IdPedido = (int)miLector["IdPedido"];

                        oPedido.FechaPedido = (DateTime)miLector["FechaPedido"];

                        if ((int)miLector["IdPedido"] == oPedido.IdPedido)
                        {
                            oProducto.idProducto = (int)miLector["IdProducto"];

                            oProducto.proveedor = clsListadoProveedoresDAL.obtenerProveedorPorIdDAL((int)miLector["IdProveedor"]);

                            oProducto.nombre = (string)miLector["Nombre"];

                            oProducto.categoria = clsListadoCategoriasDAL.obtenerCategoriaPorIdDAL((int)miLector["IdCategoria"]);

                            oProducto.precioUd = Convert.ToDouble(miLector["PrecioUnidad"]);

                            oProducto.cantidad = (int)miLector["Cantidad"];

                            oProducto.precioTotal = Convert.ToDouble(miLector["PrecioTotal"]);

                            oPedido.CosteTotal += oProducto.precioTotal;

                            listaProductos.Add(oProducto);
                        }

                        oPedido.Productos = listaProductos;

                        // Para no duplicar pedidos y añadir sus productos correctamente
                        if (listadoPedidos.Any(p => p.IdPedido == oPedido.IdPedido))
                        {
                            var pedidoExistente = listadoPedidos.FirstOrDefault(p => p.IdPedido == oPedido.IdPedido);

                            if (pedidoExistente != null)
                            {
                                // Sumar el precioTotal del nuevo producto al CosteTotal del pedido
                                pedidoExistente.CosteTotal += listaProductos.Sum(p => p.precioTotal);

                                // Concatenar la lista anterior con la nueva lista de productos
                                pedidoExistente.Productos = pedidoExistente.Productos.Concat(listaProductos).ToList();
                            }

                        }
                        else
                        {
                            listadoPedidos.Add(oPedido);
                        }

                        //listaProductos = null;
                        listaProductos = new List<clsProductoCompletoModel>();
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
