using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            
            public DateTime  FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly CarritoContexto _contexto;
            public Manejador(CarritoContexto contexto)
            {
                _contexto = contexto;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };
                _contexto.CarritoSesion.Add(carritoSesion);
                var valor = await _contexto.SaveChangesAsync();

                if (valor == 0)
                {
                    throw new Exception("No se pudo insertar el Autor");
                }

                int id = carritoSesion.CarritoSesionId;
                foreach (var item in request.ProductoLista)
                {
                    var detalleSession = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionad = item
                    };

                    _contexto.CarritoSesionDetalle.Add(detalleSession);

                }
                valor = await _contexto.SaveChangesAsync();
                if(valor > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar en el carrito de compra");
                    

            }
        }
    }
}
