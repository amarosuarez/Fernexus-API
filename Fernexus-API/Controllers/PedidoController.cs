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

            List<clsPedidoCompletoModel> listadoCompleto = new List<clsPedidoCompletoModel>();
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
                salida = BadRequest(e.Message);
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
                    salida = NotFound("No se ha encontrado ningún pedido con ese id.");
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
        [HttpGet("fechas")]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los pedidos que se encuentre entre las fechas especificadas",
            Description = "Este método recibe dos fechas, obtiene todos los pedidos que se encuentren entre estas fechas y los devuelve como un listado.<br>" +
            "Si no se encuentra ningún pedido devuelve un mensaje de error."
        )]
        public IActionResult Get(String fechaInicio, String fechaFin)
        {
            IActionResult salida;

            List<clsPedidoCompletoModel> listadoCompleto = new List<clsPedidoCompletoModel>();
            try
            {
                listadoCompleto = clsListadoPedidosDAL.obtenerListadoPedidosPorFechaDAL(fechaInicio, fechaFin);
                if (listadoCompleto.Count() == 0)
                {
                    salida = NotFound("No se ha encontrado ningún pedido entre esas fechas");
                }
                else
                {
                    salida = Ok(listadoCompleto);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest(e.Message);
            }

            return salida;
        }

        // GET api/<PedidoController>/producto/:idProducto
        [HttpGet("producto/{idProducto}")]
        [SwaggerOperation(
    Summary = "Obtiene un listado con todos los pedidos que contienen un producto específico",
    Description = "Este método recibe el ID de un producto y devuelve todos los pedidos que lo contienen."
)]
        public IActionResult GetByIdProducto(int idProducto)
        {
            IActionResult salida;
            List<clsPedidoCompletoModel> listadoPedidos = new List<clsPedidoCompletoModel>();

            try
            {
                listadoPedidos = clsListadoPedidosDAL.obtenerListadoPedidosPorProductoDAL(idProducto);

                if (listadoPedidos == null || listadoPedidos.Count == 0)
                {
                    salida = NotFound("No se han encontrado pedidos que contengan el producto especificado.");
                }
                else
                {
                    salida = Ok(listadoPedidos);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest($"Ocurrió un error al intentar obtener los pedidos por producto: {e.Message}");
            }

            return salida;
        }


        // POST api/<PedidoController>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Crea un pedido",
            Description = "Este método recibe listado de productos con todos los detalles y la fecha del pedido y crea un nuevo pedido."
        )]
        public IActionResult Post([FromBody] List<clsProductoCompletoPrecioTotalModel> productos)
        {
            IActionResult salida;
            int numFilasAfectadas = 0;

            try
            {
                numFilasAfectadas = clsManejadoraPedidosDAL.crearPedidoDAL(productos);
                if (numFilasAfectadas == 0)
                {
                    salida = NotFound("No se ha podido crear el pedido");
                }
                else
                {
                    salida = Ok($"Se ha creado el pedido correctamente");
                }
            }
            catch (Exception e)
            {
                salida = BadRequest("Ha ocurrido un error al intentar crear el pedido");
            }

            return salida;
        }

        // PUT api/<PedidoController>/5
        [HttpPut("{idPedido}")]
        [SwaggerOperation(
            Summary = "Actualiza un pedido",
            Description = "Este método recibe el id del pedido y el pedido modificado y lo actualiza."
        )]
        public IActionResult Put(int idPedido, [FromBody] clsPedidoCompletoModel pedido)
        {
            IActionResult salida;
            int numFilasAfectadas = 0;

            try
            {
                numFilasAfectadas = clsManejadoraPedidosDAL.actualizarPedidoDAL(idPedido, pedido);
                if (numFilasAfectadas == 0)
                {
                    salida = NotFound("No se ha podido actualizar el pedido");
                }
                else
                {
                    salida = Ok($"Se ha actualizado el pedido correctamente");
                }
            }
            catch (Exception e)
            {
                salida = BadRequest($"Ha ocurrido un error al intentar actualizar el pedido");
            }

            return salida;
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{idPedido}")]
        [SwaggerOperation(
            Summary = "Elimina un pedido",
            Description = "Este método recibe el id del pedido y lo elimina."
        )]
        public IActionResult Delete(int idPedido)
        {
            IActionResult salida;
            int numFilasAfectadas = 0;

            try
            {
                numFilasAfectadas = clsManejadoraPedidosDAL.eliminarPedidoDAL(idPedido);
                if (numFilasAfectadas == 0)
                {
                    salida = NotFound();
                }
                else
                {
                    salida = Ok();
                }
            }
            catch (Exception e)
            {
                salida = BadRequest(e.Message);
            }

            return salida;
        }
    }
}