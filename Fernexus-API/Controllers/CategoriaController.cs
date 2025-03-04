using DAL;
using ENT;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fernexus_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        // GET: api/<CategoriaController>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todas las categorías",
            Description = "Este método obtiene todas las categorías y las devuelve como un listado.<br>" +
            "Si no se encuentra ninguna categoría devuelve un mensaje de error."
        )]
        [SwaggerResponse(200, "Lista de categorías obtenida correctamente", typeof(List<clsCategoria>))]
        [SwaggerResponse(404, "No se encontraron categorías")]
        [SwaggerResponse(500, "Error interno del servidor")]
        public IActionResult Get()
        {
            IActionResult salida;
            List<clsCategoria> listadoCompleto = new List<clsCategoria>();
            try
            {
                listadoCompleto = clsListadoCategoriasDAL.obtenerListadoCategoriasCompletoDAL();
                if (listadoCompleto == null || listadoCompleto.Count() == 0)
                {
                    salida = NotFound("No se ha encontrado ninguna categoría");
                }
                else
                {
                    salida = Ok(listadoCompleto);
                }
            }
            catch
            {
                salida = BadRequest();
            }
            return salida;
        }

        // GET: api/<CategoriaController>/id
        [HttpGet("{idCategoria}")]
        [SwaggerOperation(
            Summary = "Obtiene los datos de una categoría asociada a un ID",
            Description = "Este método recibe un ID y devuelve los datos de la categoría asociada a este.<br>" +
            "Si no se encuentra ninguna categoría devuelve un mensaje de error."
        )]
        [SwaggerResponse(200, "Categoría obtenida correctamente", typeof(List<clsCategoria>))]
        [SwaggerResponse(404, "No se encontró ninguna categoría con ese ID")]
        [SwaggerResponse(500, "Error interno del servidor")]
        public IActionResult Get(int idCategoria)
        {
            IActionResult salida;
            clsCategoria categoria = null;

            try
            {
                categoria = clsListadoCategoriasDAL.obtenerCategoriaPorIdDAL(idCategoria);
                if (categoria == null)
                {
                    salida = NotFound("No se ha encontrado ninguna categoría con ese ID");
                }
                else
                {
                    salida = Ok(categoria);
                }
            }
            catch
            {
                salida = BadRequest();
            }
            return salida;
        }

    }
}