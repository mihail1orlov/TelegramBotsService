using System.Threading.Tasks;

namespace EnglishTelegramBot.Services
{
    public interface IMessageProcessor
    {
        Task<string> Process(string text, long id);
    }
}