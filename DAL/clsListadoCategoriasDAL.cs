using ENT;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class clsListadoCategoriasDAL
    {
        /// <summary>
        /// Función que obtiene todas las categorías y las devuelve como un listado
        /// </summary>
        /// <returns>Listado de Categorías</returns>
        public static List<clsCategoria> obtenerListadoCategoriasCompletoDAL()
        {
            List<clsCategoria> listadoCategorias = new List<clsCategoria>();

            SqlConnection conexion = new SqlConnection();
            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;
            clsCategoria categoria;

            try
            {
                conexion = clsConexion.GetConnection();
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    miComando.CommandText = "SELECT * FROM Categorias";
                    miComando.Connection = conexion;
                    miLector = miComando.ExecuteReader();

                    if (miLector.HasRows)
                    {
                        while (miLector.Read())
                        {
                            categoria = new clsCategoria();
                            categoria.IdCategoria = (int)miLector["IdCategoria"];
                            categoria.Nombre = (string)miLector["nombre"];

                            listadoCategorias.Add(categoria);
                        }
                    }
                    miLector.Close();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            finally
            {
                clsConexion.Desconectar();
            }

            return listadoCategorias;
        }

        /// <summary>
        /// Función que recibe un ID y busca en la BD una categoría con dicho ID<br>
        /// Pre: El ID debe ser mayor que 0</br>
        /// Post: Ninguno
        /// </summary>
        /// <param name="idCategoria">ID de la categoría a buscar</param>
        /// <returns>Categoría</returns>
        public static clsCategoria obtenerCategoriaPorIdDAL(int idCategoria)
        {
            clsCategoria categoria = null;

            SqlConnection conexion = new SqlConnection();
            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;

            try
            {
                conexion = clsConexion.GetConnection();

                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    miComando.Parameters.Add("@IdCategoria", System.Data.SqlDbType.Int).Value = idCategoria;
                    miComando.CommandText = "SELECT * FROM Categorias WHERE IdCategoria = @IdCategoria";
                    miComando.Connection = conexion;
                    miLector = miComando.ExecuteReader();

                    if (miLector.HasRows)
                    {
                        while (miLector.Read())
                        {
                            categoria = new clsCategoria();
                            categoria.IdCategoria = (int)miLector["IdCategoria"];
                            categoria.Nombre = (string)miLector["nombre"];
                        }
                    }
                    miLector.Close();
                }
            }
            catch (SqlException ex) {
                throw;
            }
            finally
            {
                clsConexion.Desconectar();
            }

            return categoria;
        }

    }
}
