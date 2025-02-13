using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fernexus_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        // GET: api/<PedidoController>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los pedidos",
            Description = "Este método obtiene todos los pedidos y los devuelve como un listado.<br>" +
            "Si no se encuentra ningún pedido devuelve un mensaje de error."
        )]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PedidoController>/5
        [HttpGet("{idPedido}")]
        [SwaggerOperation(
            Summary = "Obtiene los datos de un pedido asociado a un ID",
            Description = "Este método recibe un ID y devuelve los datos del pedido asociado a este.<br>" +
            "Si no se encuentra ningún pedido devuelve un mensaje de error."
        )]
        public string Get(int idPedido)
        {
            return "value";
        }

        // GET api/<PedidoController>/fechas/:fechaIn :fechaFin
        [HttpGet("{fechaInicio, fechaFin}")]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los pedidos que se encuentre entre las fechas especificadas",
            Description = "Este método recibe dos fechas, obtiene todos los pedidos que se encuentren entre estas fechas y los devuelve como un listado.<br>" +
            "Si no se encuentra ningún pedido devuelve un mensaje de error."
        )]
        public string Get(String fechaInicio, String fechaFin)
        {
            return "value";
        }

        // GET api/<PedidoController>/producto/:idProducto
        [HttpGet("{idProd}")]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los pedidos que se encuentre en las fechas dadas",
            Description = "Este método recibe dos fechas, obtiene todos los pedidos que se encuentren entre estas fechas y los devuelve como un listado.<br>" +
            "Si no se encuentra ningún pedido devuelve un mensaje de error."
        )]
        public string GetByIdProducto(int idProd)
        {
            return "value";
        }

        // POST api/<PedidoController>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Crea un pedido",
            Description = "Este método recibe listado de productos con todos los detalles y la fecha del pedido y crea un nuevo pedido."
        )]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PedidoController>/5
        [HttpPut("{idPedido}")]
        [SwaggerOperation(
            Summary = "Actualiza un pedido",
            Description = "Este método recibe el id del pedido y el pedido modificado y lo actualiza."
        )]
        public void Put(int idPedido, [FromBody] string value)
        {
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{idPedido}")]
        [SwaggerOperation(
            Summary = "Elimina un pedido",
            Description = "Este método recibe el id del pedido y lo elimina."
        )]
        public void Delete(int idPedido)
        {
        }
    }
}