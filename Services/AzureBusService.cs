using BemolChallenge.Models;
using Microsoft.Azure.ServiceBus;
using System.Drawing.Imaging;
using System.Text;
using System.Text.Json;

namespace BemolChallenge.Services
{
    public class AzureBusService : IAzureBusService
    {
        private readonly IConfiguration _configuration;
        public AzureBusService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public async Task SendMessageAsync(BemolObject bemol, string queueName)
        {
            //Utilizando string de conexão do appsettings
            var connectionString = _configuration.GetConnectionString("ServiceBusConnectionString");
            var queueClient = new QueueClient (connectionString, queueName);
            //serializando o objeto para envio para a fila
            var messageBody = JsonSerializer.Serialize(bemol);
            //transformando em um byte array
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            //enviando a mensagem
            await queueClient.SendAsync(message);
        }
    }
}
