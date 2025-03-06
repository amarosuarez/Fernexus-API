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

            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;

            try
            {
                miComando.CommandText = "SELECT pv.*, p.Nombre AS NombreProducto, pc.IdCategoria AS IdCategoria, pv.Stock FROM ProductosCategorias pc JOIN Productos p ON pc.IdProducto = p.IdProducto JOIN ProveedoresProductos pv ON pc.IdProducto = pv.IdProducto WHERE p.deletedAt = '1111-11-11';";

                miComando.Connection = clsConexion.GetConnection();
                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        // Crear un nuevo producto para cada fila
                        var oProducto = new clsProductoCompletoModel
                        {
                            idProducto = (int)miLector["IdProducto"],
                            nombre = (string)miLector["NombreProducto"],
                            precioUd = Convert.ToDouble(miLector["PrecioUnidad"]),
                            cantidad = (int)miLector["Stock"],
                            proveedor = clsListadoProveedoresDAL.obtenerProveedorPorIdDAL((int)miLector["IdProveedor"]),
                            categorias = new List<clsCategoria>() // Inicializar la lista de categorías
                        };

                        // Obtener la categoría actual
                        var categoria = clsListadoCategoriasDAL.obtenerCategoriaPorIdDAL((int)miLector["IdCategoria"]);

                        // Verificar si la categoría ya existe en la lista de categorías del producto
                        if (!oProducto.categorias.Any(c => c.IdCategoria == categoria.IdCategoria))
                        {
                            // Agregar la categoría al producto solo si no existe
                            oProducto.categorias.Add(categoria);
                        }

                        // Agregar el producto a la lista
                        listaProductos.Add(oProducto);
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

            return listaProductos;
        }

        /// <summary>
        /// Metodo para obtener un producto a partir de un id numerico
        /// Pre: None
        /// Post: Objeto producto puede estar vacio si el id no existe en la BD
        /// </summary>
        /// <returns>Objeto producto</returns>
        public static clsProductoCompletoModel? obtenerProductoPorId(int idProducto)
        {
            clsProductoCompletoModel? producto = null;

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            try
            {

                miComando.CommandText = "SELECT pv.*, p.Nombre AS NombreProducto, pc.IdCategoria AS IdCategoria, pv.Stock as Stock FROM ProductosCategorias pc JOIN Productos p ON pc.IdProducto = p.IdProducto JOIN ProveedoresProductos pv ON pc.IdProducto = pv.IdProducto WHERE pv.IdProducto = @IdProducto AND p.deletedAt = '1111-11-11';\r\n";

                miComando.Parameters.AddWithValue("@IdProducto", idProducto);

                miComando.Connection = clsConexion.GetConnection();

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    producto = new clsProductoCompletoModel();

                    while (miLector.Read())
                    {
                        producto.idProducto = (int)miLector["IdProducto"];
                        producto.nombre = (string)miLector["NombreProducto"];
                        producto.precioUd = Convert.ToDouble(miLector["PrecioUnidad"]);
                        producto.cantidad = (int)miLector["Stock"];
                        producto.proveedor = clsListadoProveedoresDAL.obtenerProveedorPorIdDAL((int)miLector["IdProveedor"]);
                        producto.categorias = new List<clsCategoria>();

                        // Obtener la categoría actual
                        clsCategoria categoria = clsListadoCategoriasDAL.obtenerCategoriaPorIdDAL((int)miLector["IdCategoria"]);

                        // Verificar si la categoría ya existe en la lista de categorías del producto
                        if (!producto.categorias.Any(c => c.IdCategoria == categoria.IdCategoria))
                        {
                            // Agregar la categoría al producto solo si no existe
                            producto.categorias.Add(categoria);
                        }
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
            return producto;
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

            clsConexion miConexion = new clsConexion();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            clsProductoCompletoModel oProducto;

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
                        oProducto = new clsProductoCompletoModel();

                        oProducto.idProducto = (int)miLector["IdProducto"];

                        oProducto.nombre = (string)miLector["Nombre"];

                        oProducto.cantidad = (int)miLector["Stock"];

                        oProducto.proveedor = clsListadoProveedoresDAL.obtenerProveedorPorIdDAL((int)miLector["IdProveedor"]);

                        oProducto.precioUd = Convert.ToDouble(miLector["PrecioUnidad"]);

                        oProducto.categorias = new List<clsCategoria>();

                        // Obtener la categoría actual
                        clsCategoria categoria = clsListadoCategoriasDAL.obtenerCategoriaPorIdDAL(idCategoria);

                        // Verificar si la categoría ya existe en la lista de categorías del producto
                        if (!oProducto.categorias.Any(c => c.IdCategoria == categoria.IdCategoria))
                        {
                            // Agregar la categoría al producto solo si no existe
                            oProducto.categorias.Add(categoria);
                        }

                        listaProductos.Add(oProducto);
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
            return listaProductos;
        }
    }
}
