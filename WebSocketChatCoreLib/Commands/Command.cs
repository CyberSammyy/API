using System.Threading.Tasks;
using WebSocketChatCoreLib.Models;

namespace WebSocketChatCoreLib.Commands
{
    public abstract class Command
    {
        public string[] Args { get; protected set; }
        protected Command(string[] args)
        {
            Args = args;
        }

        public abstract Task ProcessMessage(SocketUser sender, SocketHandler socketHandler);
    }
}
