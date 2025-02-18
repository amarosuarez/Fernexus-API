﻿using ENT;
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
        public static List<clsProducto> obtenerListadoProductosCompletoDAL()
        {
            List<clsProducto> listaProductos = new List<clsProducto>();

            clsConexion miConexion = new clsConexion();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            clsProducto oProducto;

            try
            {

                miComando.CommandText = "SELECT * FROM Productos";

                miComando.Connection = miConexion.ObtenerConexion();

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        oProducto = new clsProducto();

                        oProducto.IdProducto = (int)miLector["IdProducto"];

                        oProducto.Nombre = (string)miLector["Nombre"];

                        oProducto.Precio = (double)miLector["Precio"];

                        oProducto.IdCategoria = (int)miLector["IdCategoria"];

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
                miConexion.Desconectar();
            }
            return listaProductos;
        }

        /// <summary>
        /// Metodo para obtener el listado de productos filtrado por  de la base de datos
        /// Pre: None 
        /// Post: Listado de productos filtrado por categoria puede ser null si la tabla está vacía
        /// </summary>
        /// <param name="categoría">Id numerico de la categoria</param>
        /// <returns>Productos Filtrados</returns>
        public static List<clsProducto> obtenerListadoProductosPorCategoria(int categoria)
        {
            List<clsProducto> listaProductos = new List<clsProducto>();

            clsConexion miConexion = new clsConexion();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            clsProducto oProducto;

            try
            {
                miComando.Parameters.Add("@categoria", System.Data.SqlDbType.Int).Value = categoria;
                miComando.CommandText = "EXEC filtrarProductosPorCategoria @idCategoria = @categoria;";

                miComando.Connection = miConexion.ObtenerConexion();

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        oProducto = new clsProducto();

                        oProducto.IdProducto = (int)miLector["IdProducto"];

                        oProducto.Nombre = (string)miLector["Nombre"];

                        oProducto.Precio = (double)miLector["Precio"];

                        oProducto.IdCategoria = (int)miLector["IdCategoria"];

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
                miConexion.Desconectar();
            }
            return listaProductos;
        }
    }
}
