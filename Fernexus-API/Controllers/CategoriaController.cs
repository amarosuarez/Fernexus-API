using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;  // Asegúrate de tener esta directiva de using para Swagger

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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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