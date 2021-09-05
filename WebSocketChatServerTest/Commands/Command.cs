using System.Threading.Tasks;
using WebSocketChatServerTest.SocketManager;

namespace WebSocketChatServerTest.Commands
{
    public abstract class Command
    {
        public string[] Args { get; protected set; }

        protected Command(string[] args)
        {
            Args = args;
        }

        public abstract Task ProcessMessage(WebSocketClient sender, SocketHandler socketHandler);
    }
}
