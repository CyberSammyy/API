using System.Threading.Tasks;
using WebSocketChatCoreLib;
using WebSocketChatServer;

namespace WebSocketChatServerApp.Commands
{
    public abstract class Command
    {
        protected readonly IUserRepository _userRepository;
        public string[] Args { get; protected set; }

        protected Command(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        protected Command(string[] args)
        {
            Args = args;
        }

        public abstract Task ProcessMessage(SocketUser sender, SocketHandler socketHandler);
    }
}
