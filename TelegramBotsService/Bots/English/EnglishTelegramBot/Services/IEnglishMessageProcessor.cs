using System.Threading.Tasks;
using CommonServices;

namespace EnglishTelegramBot.Services
{
    public interface IEnglishMessageProcessor : IMessageProcessor
    {
        Task<string> Process(string text, long id);
    }
}