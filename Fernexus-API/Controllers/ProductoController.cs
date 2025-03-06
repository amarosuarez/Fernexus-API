using DTO;
using ENT;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

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
        public IActionResult Get()
        {
            IActionResult salida;
            List<clsProductoCompletoModel> listadoCompleto;

            try
            {
                listadoCompleto = DAL.clsListadoProductosDAL.obtenerListadoProductosCompletoDAL();
                if (listadoCompleto.Count() == 0)
                {
                    salida = NotFound("No se han encontrado productos.");
                }
                else
                {
                    salida = Ok(listadoCompleto);
                }
            }

            catch (Exception e)
            {
                salida = BadRequest($"Ocurrió un error inesperado al intertar obtener los productos. {e.Message}");
            }

            return salida;
        }


        // GET api/<ProductoController>/5
        [HttpGet("{idProducto}")]
        [SwaggerOperation(
            Summary = "Obtiene los datos de un producto asociado a un ID",
            Description = "Este método recibe un ID y devuelve los datos del producto asociado a este como una lista, ya que puede tener más de un proveedor<br>" +
            "Si no se encuentra ningún producto devuelve un mensaje de error."
        )]
        public IActionResult Get(int idProducto)
        {
            IActionResult salida;
            List<clsProductoCompletoModel> producto;

            try
            {
                producto = DAL.clsListadoProductosDAL.obtenerProductoPorId(idProducto);

                if (producto == null)
                {
                    salida = NotFound("No se han encontrado productos con ese id.");
                }
                else
                {
                    salida = Ok(producto);
                }
            }

            catch (Exception e)
            {
                salida = BadRequest($"Ocurrió un error inesperado al intertar obtener el producto por id. {e.Message}");
            }

            return salida;
        }

        // GET api/<ProductoController>/categoria/5
        [HttpGet("categoria/{idCategoria}")]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los productos asociados a un ID de categoría",
            Description = "Este método recibe un ID de categoría y devuelve todos los productos asociados a este.<br>" +
            "Si no se encuentra ningún producto devuelve un mensaje de error."
        )]
        public IActionResult GetByCategoria(int idCategoria)
        {
            IActionResult salida;
            List<clsProductoCompletoModel> listadoFiltradoPorCategoria;
            try
            {
                listadoFiltradoPorCategoria = DAL.clsListadoProductosDAL.obtenerListadoProductosPorCategoriaDAL(idCategoria);
                if (listadoFiltradoPorCategoria.Count() == 0)
                {
                    salida = NotFound("No se han encontrado productos en esa categoria.");
                }
                else
                {
                    salida = Ok(listadoFiltradoPorCategoria);
                }
            }

            catch (Exception e)
            {
                salida = BadRequest($"Ocurrió un error inesperado al intertar obtener los productos filtrados por categoria. {e.Message}");
            }

            return salida;
        }
    }
}