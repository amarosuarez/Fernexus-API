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
    public class clsListadoProductosDAL
    {
        /// <summary>
        /// Metodo para obtener el listado de productos completo de la base de datos
        /// Pre: None
        /// Post: Listado de productos puede ser null si la tabla está vacía
        /// </summary>
        /// <returns>Productos completos</returns>
        public static List<clsProductoCompletoModel> obtenerListadoProductosCompletoDAL()
        {
            List<clsProductoCompletoModel> listaProductos = new List<clsProductoCompletoModel>();
            Dictionary<(int, int), clsProductoCompletoModel> productosDiccionario = new Dictionary<(int, int), clsProductoCompletoModel>();

            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;

            try
            {
                miComando.CommandText = "SELECT pv.*, p.Nombre AS NombreProducto, pc.IdCategoria AS IdCategoria, pv.Stock, pvc.*, c.Nombre AS NombreCategoria FROM ProductosCategorias pc \r\nJOIN Productos p ON pc.IdProducto = p.IdProducto \r\nJOIN ProveedoresProductos pv ON pc.IdProducto = pv.IdProducto \r\nJOIN Proveedores pvc ON pv.IdProveedor = pvc.IdProveedor\r\nJOIN Categorias c ON pc.IdCategoria = c.IdCategoria\r\nWHERE p.deletedAt = '1111-11-11';";

                miComando.Connection = clsConexion.GetConnection();
                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        int idProducto = (int)miLector["IdProducto"];
                        int idProveedor = (int)miLector["IdProveedor"];

                        // Crear una clave única para el producto y proveedor
                        var clave = (idProducto, idProveedor);

                        // Verificar si el producto ya existe en el diccionario para este proveedor
                        if (!productosDiccionario.TryGetValue(clave, out clsProductoCompletoModel oProducto))
                        {
                            // Si no existe, crear un nuevo producto
                            oProducto = new clsProductoCompletoModel
                            {
                                idProducto = idProducto,
                                nombre = (string)miLector["NombreProducto"],
                                precioUd = Convert.ToDouble(miLector["PrecioUnidad"]),
                                cantidad = (int)miLector["Stock"],
                                proveedor = new clsProveedor
                                {
                                    IdProveedor = (int)miLector["IdProveedor"],
                                    Nombre = (string)miLector["Nombre"],
                                    Correo = (string)miLector["Correo"],
                                    Telefono = (string)miLector["Telefono"],
                                    Direccion = (string)miLector["Direccion"],
                                    Pais = (string)miLector["Pais"],
                                },
                                categorias = new List<clsCategoria>() // Inicializar la lista de categorías
                            };

                            // Agregar el producto al diccionario
                            productosDiccionario.Add(clave, oProducto);
                        }

                        // Obtener la categoría actual
                        clsCategoria categoria = new clsCategoria
                        {
                            IdCategoria = (int)miLector["IdCategoria"],
                            Nombre = (string)miLector["NombreCategoria"],
                        };

                        // Verificar si la categoría ya existe en la lista de categorías del producto
                        if (!oProducto.categorias.Any(c => c.IdCategoria == categoria.IdCategoria))
                        {
                            // Agregar la categoría al producto solo si no existe
                            oProducto.categorias.Add(categoria);
                        }
                    }
                }

                // Convertir el diccionario a una lista de productos
                listaProductos = productosDiccionario.Values.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                clsConexion.Desconectar();
            }

            return listaProductos;
        }

        /// <summary>
        /// Metodo para obtener un producto a partir de un id numerico
        /// Pre: None
        /// Post: Objeto producto puede estar vacio si el id no existe en la BD
        /// </summary>
        /// <returns>Objeto producto</returns>
        public static List<clsProductoCompletoModel> obtenerProductoPorId(int idProducto)
        {
            List<clsProductoCompletoModel> listaProductos = new List<clsProductoCompletoModel>();
            Dictionary<(int, int), clsProductoCompletoModel> productosDiccionario = new Dictionary<(int, int), clsProductoCompletoModel>();

            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;

            try
            {

                miComando.CommandText = "SELECT pv.*, p.Nombre AS NombreProducto, pc.IdCategoria AS IdCategoria, pv.Stock, pvc.*, c.Nombre AS NombreCategoria FROM ProductosCategorias pc \r\nJOIN Productos p ON pc.IdProducto = p.IdProducto \r\nJOIN ProveedoresProductos pv ON pc.IdProducto = pv.IdProducto \r\nJOIN Proveedores pvc ON pv.IdProveedor = pvc.IdProveedor\r\nJOIN Categorias c ON pc.IdCategoria = c.IdCategoria\r\nWHERE pv.IdProducto = @IdProducto AND p.deletedAt = '1111-11-11';";

                miComando.Parameters.AddWithValue("@IdProducto", idProducto);

                miComando.Connection = clsConexion.GetConnection();

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        //idProducto = (int)miLector["IdProducto"];
                        int idProveedor = (int)miLector["IdProveedor"];

                        // Crear una clave única para el producto y proveedor
                        var clave = (idProducto, idProveedor);

                        // Verificar si el producto ya existe en el diccionario para este proveedor
                        if (!productosDiccionario.TryGetValue(clave, out clsProductoCompletoModel oProducto))
                        {
                            // Si no existe, crear un nuevo producto
                            oProducto = new clsProductoCompletoModel
                            {
                                idProducto = idProducto,
                                nombre = (string)miLector["NombreProducto"],
                                precioUd = Convert.ToDouble(miLector["PrecioUnidad"]),
                                cantidad = (int)miLector["Stock"],
                                proveedor = new clsProveedor
                                {
                                    IdProveedor = (int)miLector["IdProveedor"],
                                    Nombre = (string)miLector["Nombre"],
                                    Correo = (string)miLector["Correo"],
                                    Telefono = (string)miLector["Telefono"],
                                    Direccion = (string)miLector["Direccion"],
                                    Pais = (string)miLector["Pais"],
                                },
                                categorias = new List<clsCategoria>() // Inicializar la lista de categorías
                            };

                            // Agregar el producto al diccionario
                            productosDiccionario.Add(clave, oProducto);
                        }

                        // Obtener la categoría actual
                        clsCategoria categoria = new clsCategoria
                        {
                            IdCategoria = (int)miLector["IdCategoria"],
                            Nombre = (string)miLector["NombreCategoria"],
                        };

                        // Verificar si la categoría ya existe en la lista de categorías del producto
                        if (!oProducto.categorias.Any(c => c.IdCategoria == categoria.IdCategoria))
                        {
                            // Agregar la categoría al producto solo si no existe
                            oProducto.categorias.Add(categoria);
                        }
                    }
                }

                // Convertir el diccionario a una lista de productos
                listaProductos = productosDiccionario.Values.ToList();
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
            return listaProductos;
        }



        /// <summary>
        /// Metodo para obtener el listado de productos filtrado por categoria de la base de datos
        /// Pre: None 
        /// Post: Listado de productos filtrado por categoria puede ser null si la tabla está vacía
        /// </summary>
        /// <param name="categoría">Id numerico de la categoria</param>
        /// <returns>Productos Filtrados</returns>
        public static List<clsProductoCompletoModel> obtenerListadoProductosPorCategoriaDAL(int idCategoria)
        {
            List<clsProductoCompletoModel> listaProductos = new List<clsProductoCompletoModel>();
            Dictionary<(int, int), clsProductoCompletoModel> productosDiccionario = new Dictionary<(int, int), clsProductoCompletoModel>();

            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;

            try
            {
                miComando.CommandText = "EXEC filtrarProductosPorCategoria @categoria";
                miComando.Parameters.AddWithValue("@categoria", idCategoria);

                miComando.Connection = clsConexion.GetConnection();

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        int idProducto = (int)miLector["IdProducto"];
                        int idProveedor = (int)miLector["IdProveedor"];

                        // Crear una clave única para el producto y proveedor
                        var clave = (idProducto, idProveedor);

                        // Verificar si el producto ya existe en el diccionario para este proveedor
                        if (!productosDiccionario.TryGetValue(clave, out clsProductoCompletoModel oProducto))
                        {
                            // Si no existe, crear un nuevo producto
                            oProducto = new clsProductoCompletoModel
                            {
                                idProducto = idProducto,
                                nombre = (string)miLector["Nombre"],
                                precioUd = Convert.ToDouble(miLector["PrecioUnidad"]),
                                cantidad = (int)miLector["Stock"],
                                proveedor = new clsProveedor
                                {
                                    IdProveedor = (int)miLector["IdProveedor"],
                                    Nombre = (string)miLector["NombreProveedor"],
                                    Correo = (string)miLector["Correo"],
                                    Telefono = (string)miLector["Telefono"],
                                    Direccion = (string)miLector["Direccion"],
                                    Pais = (string)miLector["Pais"],
                                },
                                categorias = new List<clsCategoria>() // Inicializar la lista de categorías
                            };

                            // Agregar el producto al diccionario
                            productosDiccionario.Add(clave, oProducto);
                        }

                        // Obtener la categoría actual
                        clsCategoria categoria = new clsCategoria
                        {
                            IdCategoria = (int)miLector["IdCategoria"],
                            Nombre = (string)miLector["NombreCategoria"],
                        };

                        // Verificar si la categoría ya existe en la lista de categorías del producto
                        if (!oProducto.categorias.Any(c => c.IdCategoria == categoria.IdCategoria))
                        {
                            // Agregar la categoría al producto solo si no existe
                            oProducto.categorias.Add(categoria);
                        }
                    }
                }

                // Convertir el diccionario a una lista de productos
                listaProductos = productosDiccionario.Values.ToList();
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
            return listaProductos;
        }
    }
}
