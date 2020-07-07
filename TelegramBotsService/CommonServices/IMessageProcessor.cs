using System.Threading.Tasks;

namespace CommonServices
{
    public interface IMessageProcessor
    {
        Task<string> Process(string text, long id);
    }
}