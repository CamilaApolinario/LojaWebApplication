using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento
{
    [Route("api/[controller]")]
    public class MensagemController : ControllerBase
    {
        private readonly ConnectionFactory _factory;
        private const string Orcamento = "messages";

        public MensagemController()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 49158,
                UserName = "guest",
                Password = "guest"
            };
        }

        [HttpPost]
        public IActionResult PostMensagem([FromBody] Orcamento orcamento)
        {
            using (var connection = _factory.CreateConnection())
            {
                using var channel = connection.CreateModel();
                {
                    channel.QueueDeclare(
                    queue: Orcamento,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                    var message = JsonSerializer.Serialize(orcamento);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: Orcamento,
                        basicProperties: null,
                        body: body);
                }
            }
            return Accepted(orcamento);
        }
    }
}
