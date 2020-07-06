using System.Threading.Tasks;
using BotServiceCommon;

namespace EnglishTelegramBot.Services
{
    public interface IEnglishMessageProcessor : IMessageProcessor
    {
        Task<string> Process(string text, long id);
    }
}