namespace WebSocketChatServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            new UserInterface(new UserService())
                .Run(args)
                .GetAwaiter()
                .GetResult();
        }
    }
}
