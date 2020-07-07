using System.Threading.Tasks;
using EnglishTelegramBot.Services;

namespace CarInfoTelegramBot.Services
{
    public interface ICarInfoMessageProcessor : IMessageProcessor
    {
        Task<string> Process(string text, long id);
    }
}