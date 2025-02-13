using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fernexus_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        // GET: api/<ProductoController>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los productos",
            Description = "Este método obtiene todos los productos y los devuelve como un listado.<br>" +
            "Si no se encuentra ningún producto devuelve un mensaje de error."
        )]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductoController>/5
        [HttpGet("{idProducto}")]
        [SwaggerOperation(
            Summary = "Obtiene los datos de un producto asociado a un ID",
            Description = "Este método recibe un ID y devuelve los datos del producto asociado a este.<br>" +
            "Si no se encuentra ningún producto devuelve un mensaje de error."
        )]
        public string Get(int idProducto)
        {
            return "value";
        }

        // GET api/<ProductoController>/categoria/5
        [HttpGet("categoria/{idCategoria}")]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los productos asociados a un ID de categoría",
            Description = "Este método recibe un ID de categoría y devuelve todos los productos asociados a este.<br>" +
            "Si no se encuentra ningún producto devuelve un mensaje de error."
        )]
        public string GetByCategoria(int idCategoria)
        {
            return "value";
        }
    }
}