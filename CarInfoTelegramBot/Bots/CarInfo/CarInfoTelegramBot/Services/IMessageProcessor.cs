using System.Threading.Tasks;

namespace CarInfoTelegramBot.Services
{
    public interface IMessageProcessor
    {
        Task<string> Process(string text, long id);
    }
}