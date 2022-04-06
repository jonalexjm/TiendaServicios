using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TiendaServicios.Mensajeria.Email.SendGridLibreria.Interface;
using TiendaServicios.Mensajeria.Email.SendGridLibreria.Modelo;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaServicios.Api.Autor.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventoQueue>
    {
        
        private readonly ILogger<EmailEventoManejador> _logger;
        private readonly ISendGridEnviar _sendGridEnviar;
        private readonly IConfiguration _configuracion;
        public EmailEventoManejador(ILogger<EmailEventoManejador> logger, 
                                    ISendGridEnviar sendGridEnviar,
                                    IConfiguration configuration)
        {
            _logger = logger;
            _sendGridEnviar = sendGridEnviar;
            _configuracion = configuration;
        }
     
        public async Task Handle(EmailEventoQueue @event)
        {
            _logger.LogInformation($"Esta es la informacion {@event.Titulo}");
            var objData = new SendGridData();
            objData.Titulo = @event.Titulo;
            objData.Contenido = @event.Contenido;
            objData.EmailDestinatario = @event.Destinatario;
            objData.NombreDestinatario = @event.Destinatario;
            objData.SendGridAPIKey = _configuracion.GetSection("SendGrid:ApiKey").Value;           

            var resultado = await _sendGridEnviar.EnviarEmail(objData);
            if (resultado.resultado)
            {
                await Task.CompletedTask;
                return;
            }
        }
    }
}
