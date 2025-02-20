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
    }

    /// <summary>
    /// Función que busca 
    /// 
    /// </summary>
    /// <param name="idCategoria"></param>
    /// <returns></returns>
    //public clsCategoria buscarCategoriaPorId(int idCategoria)
    //{
    //    return null;
    //}
}
