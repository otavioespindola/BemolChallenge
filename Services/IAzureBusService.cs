using BemolChallenge.Models;

namespace BemolChallenge.Services
{
    public interface IAzureBusService
    {
        Task SendMessageAsync(BemolObject bemol ,string queueName);
    }
}
