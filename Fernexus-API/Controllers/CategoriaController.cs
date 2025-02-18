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
        public IActionResult Get()
        {
            IActionResult salida;
            List<clsCategoria> listadoCompleto = new List<clsCategoria>();
            try
            {
                listadoCompleto = clsListadoCategoriasDAL.obtenerListadoCategoriasCompletoDAL();
                if (listadoCompleto.Count() == 0)
                {
                    salida = NoContent();
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

        // GET api/<CategoriaController>/5
        [HttpGet("{idCategoria}")]
        [SwaggerOperation(
            Summary = "Obtiene los datos de una categoría asociada a un ID",
            Description = "Este método recibe un ID y devuelve los datos de la categoría asociada a este.<br>" +
            "Si no se encuentra ninguna categoría devuelve un mensaje de error."
        )]
        public string Get(int idCategoria)
        {
            return "value";
        }
    }
}