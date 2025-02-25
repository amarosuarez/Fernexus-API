using DAL;
using DTO;
using ENT;
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
        public IActionResult Get()
        {
            IActionResult salida;

            List<clsPedido> listadoCompleto = new List<clsPedido>();
            try
            {
                listadoCompleto = clsListadoPedidosDAL.obtenerListadoPedidosCompletoDAL();
                if (listadoCompleto.Count() == 0)
                {
                    salida = NotFound("No se ha encontrado ningún pedido");
                }
                else
                {
                    salida = Ok(listadoCompleto);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest();
            }

            return salida;
        }

        // GET api/<PedidoController>/5
        [HttpGet("{idPedido}")]
        [SwaggerOperation(
            Summary = "Obtiene los datos de un pedido asociado a un ID",
            Description = "Este método recibe un ID y devuelve los datos del pedido asociado a este.<br>" +
            "Si no se encuentra ningún pedido devuelve un mensaje de error."
        )]
        public IActionResult Get(int idPedido)
        {
            IActionResult salida;

            clsPedidoCompletoModel? pedido;

            try
            {
                pedido = clsManejadoraPedidosDAL.buscarPedidoDAL(idPedido);

                if (pedido == null)
                {
                    salida = NotFound("No se han encontrado productos con ese id.");
                }
                else
                {
                    salida = Ok(pedido);
                }
            }

            catch (Exception e)
            {
                salida = BadRequest("Ocurrió un error inesperado al intertar obtener el producto por id. " + e.Message);
            }

            return salida;
        }

        // GET api/<PedidoController>/fechas/:fechaIn :fechaFin
        [HttpGet("fechas/{fechaInicio, fechaFin}")]
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
        [HttpGet("producto/{idProd}")]
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