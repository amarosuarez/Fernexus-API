using DAL;
using ENT;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fernexus_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        // GET: api/<ProovedorController>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los proveedores",
            Description = "Este método obtiene todos los proveedores y los devuelve como un listado.<br>" +
            "Si no se encuentra ningún proovedor devuelve un mensaje de error."
        )]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProovedorController>/5
        [HttpGet("{idProovedor}")]
        [SwaggerOperation(
            Summary = "Obtiene los datos de un proovedor asociado a un ID",
            Description = "Este método recibe un ID y devuelve los datos del proovedor asociado a este.<br>" +
            "Si no se encuentra ningún proovedor devuelve un mensaje de error."
        )]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/<ProovedorController>/pais/españa
        [HttpGet("pais/{pais}")]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los proveedores asociados a un país",
            Description = "Este método recibe un país y devuelve todos los proveedores asociado a este.<br>" +
            "Si no se encuentra ningún proovedor devuelve un mensaje de error."
        )]
        public IActionResult Get(String pais)
        {
            IActionResult salida;
            List<clsProveedor> listadoCompleto = new List<clsProveedor>();
            try
            {
                listadoCompleto = clsListadoProveedoresDAL.obtenerListadoProveedoresPorPaisDAL(pais);
                if (listadoCompleto.Count() == 0)
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
    }
}