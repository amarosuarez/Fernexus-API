﻿using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fernexus_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProovedorController : ControllerBase
    {
        // GET: api/<ProovedorController>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene un listado con todos los proovedores",
            Description = "Este método obtiene todos los proovedores y los devuelve como un listado.<br>" +
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
            Summary = "Obtiene un listado con todos los proovedores asociados a un país",
            Description = "Este método recibe un país y devuelve todos los proovedores asociado a este.<br>" +
            "Si no se encuentra ningún proovedor devuelve un mensaje de error."
        )]
        public string Get(String pais)
        {
            return "value";
        }
    }
}