using System.Threading.Tasks;

namespace BotCommon
{
    public interface IMessageProcessor
    {
        Task<string> Process(string text, long id);
    }
}