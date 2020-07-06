using System.Threading.Tasks;
using BotServiceCommon;

namespace CarInfoTelegramBot.Services
{
    public interface ICarInfoMessageProcessor : IMessageProcessor
    {
        Task<string> Process(string text, long id);
    }
}