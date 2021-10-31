using System.Threading.Tasks;

namespace GitHubNotificatorBot.Services
{
    /// <summary>
    /// Provides processing of received messages 
    /// </summary>
    public interface IMessageProcessor
    {
        /// <summary>
        /// Processes the received message
        /// </summary>
        /// <param name="text">The received message</param>
        /// <param name="id">The chat id</param>
        /// <returns>Process message</returns>
        Task Process(string text, long id);
    }
}