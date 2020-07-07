using System.Threading.Tasks;
using CommonServices;

namespace CarInfoTelegramBot.Services
{
    public interface ICarInfoMessageProcessor : IMessageProcessor
    {
        Task<string> Process(string text, long id);
    }
}