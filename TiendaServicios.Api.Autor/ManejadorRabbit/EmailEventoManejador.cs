using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaServicios.Api.Autor.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventoQueue>
    {
        
        private readonly ILogger<EmailEventoManejador> _logger;
        public EmailEventoManejador(ILogger<EmailEventoManejador> logger)
        {
            _logger = logger;
        }
     
        public Task Handle(EmailEventoQueue @event)
        {
            _logger.LogInformation($"Esta es la informacion {@event.Titulo}");
            return Task.CompletedTask;
        }
    }
}
