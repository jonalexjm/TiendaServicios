using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.CarritoCompra.Aplicacion;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaServicios.Api.CarritoCompra.Controllers
{
    [Route("api/[controller]")]
    public class CarritoComprasController : ControllerBase
    {

        private readonly IMediator _mediator;
        public CarritoComprasController(IMediator mediator)
        {
            _mediator = mediator;
        }
       

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDto>> GetCarrito(int id)
        {
            return await _mediator.Send(new Consulta.Ejecuta {CarritoSesionId = id});
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Unit>> Post([FromBody]Nuevo.Ejecuta data)
        {
            var result = await _mediator.Send(data);

            return result;
        }

        
    }
}
