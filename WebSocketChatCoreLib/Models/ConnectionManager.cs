using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketChatServerApp
{
    public class ConnectionManager : IEnumerable<SocketUser>
    {
        private readonly object _locker;

        private readonly List<SocketUser> _connections;

        public ConnectionManager()
        {
            _locker = new object();
            _connections = new List<SocketUser>();
        }

        public bool AddSocket(WebSocket socket)
        {
            lock(_locker)
            {
                try
                {
                    _connections.Add(new SocketUser(socket));

                    return true;
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch(Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    return false;
                }
            }
        }

        public async Task<bool> RemoveSocket(Guid id)
        {
            WebSocket socketToClose;

            lock(_locker)
            {
                socketToClose = _connections.FirstOrDefault(x => x.Id == id)?.WebSocket;
            }

            if(socketToClose != null && 
                (socketToClose.State == WebSocketState.Open || socketToClose.State == WebSocketState.CloseReceived))
            {
                await socketToClose.CloseAsync(WebSocketCloseStatus.NormalClosure, 
                    Consts.Messages.ConnectionClosedMessage, 
                    CancellationToken.None);

                return true;
            }

            return false;
        }

        public Guid GetId(WebSocket socket)
        {
            lock(_locker)
            {
                return _connections.FirstOrDefault(x => x.WebSocket == socket)?.Id ?? Guid.Empty;
            }
        }

        public SocketUser GetUserById(Guid id)
        {
            lock(_locker)
            {
                return _connections.FirstOrDefault(x => x.Id == id) 
                    ?? throw new NullReferenceException(string.Format(
                        Consts.ExceptionMessages.NullReferenceExceptionMessage, 
                        nameof(SocketUser),
                        nameof(_connections), 
                        nameof(Guid)));
            }
        }

        public Guid this[string Nickname, bool isSearchingByNickname]
        {
            get
            {
                lock(_locker)
                {
                    return _connections.FirstOrDefault(x => x.Nickname == Nickname).Id;
                }
            }
        }

        public SocketUser this[WebSocket socket]
        {
            get
            {
                lock (_locker)
                {
                    return _connections.FirstOrDefault(x => x.WebSocket == socket) ?? 
                        throw new NullReferenceException(string.Format(
                         Consts.ExceptionMessages.NullReferenceExceptionMessage, 
                         nameof(SocketUser), 
                         nameof(_connections), 
                         nameof(WebSocket)));
                }
            }
        }

        public SocketUser this[string id]
        {
            get
            {
                lock (_locker)
                {
                    return _connections.Find(x => x.Nickname == id) ??
                           _connections.Find(x => x.Id.ToString(Consts.IdFormat) == id) ??
                           throw new NullReferenceException(string.Format(
                            Consts.ExceptionMessages.NullReferenceExceptionMessage,
                            nameof(SocketUser),
                            nameof(_connections),
                            nameof(String)));
                }
            }
        }

        public IEnumerator<SocketUser> GetEnumerator()
        {
            return _connections.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
