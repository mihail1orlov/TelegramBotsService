using System.Threading.Tasks;

namespace EnglishTelegramBot.Services
{
    public interface IEnglishMessageProcessor : IMessageProcessor
    {
        Task<string> Process(string text, long id);
    }
}