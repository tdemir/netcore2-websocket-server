using System;
using System.Collections.Generic;
using Fleck;
using netcore2_websocket_server.Model.WebSocket;

namespace netcore2_websocket_server
{
    public class MyWebSocket : IDisposable
    {
        WebSocketServer server;
        readonly List<IWebSocketConnection> SocketClients = new List<IWebSocketConnection>();
        public MyWebSocket()
        {
            server = new WebSocketServer(Program.SocketUrl)
            {
                RestartAfterListenError = true
            };
            server.Start(_socket =>
            {
                //Child connections will not use Nagle's Algorithm
                
                _socket.OnOpen = () =>
                {
                    SocketOnOpen(_socket);
                };

                _socket.OnClose = () =>
                {
                    SocketOnClose(_socket);
                };

                _socket.OnMessage = (message) =>
                {
                    SocketOnMessage(_socket, message);
                };
            });
        }


        private void SocketOnOpen(IWebSocketConnection socket)
        {
            var _id = socket.ConnectionInfo.Id;

            SocketClients.Add(socket);
        }
        private void SocketOnClose(IWebSocketConnection socket)
        {
            var _id = socket.ConnectionInfo.Id;

            SocketClients.Remove(socket);
        }

        private void SocketOnMessage(IWebSocketConnection socket, string msg)
        {
            var _id = socket.ConnectionInfo.Id;

            //get data
            var model = msg.Deserialize<WebSocketDataModel<string>>();

            //send to all clients
            SendMessageToAllClients(model);
        }

         private void SendMessageToAllClients<T>(WebSocketDataModel<T> model)
        {
            model.CurrentDatetime = DateTime.UtcNow;
            var _modelSerialize = model.Serialize();
            foreach (var item in SocketClients)
            {
                item.Send(_modelSerialize);
            }
        }

        private void SendMessage<T>(WebSocketDataModel<T> model, IWebSocketConnection socket)
        {
            model.CurrentDatetime = DateTime.UtcNow;
            var _modelSerialize = model.Serialize();
            socket.Send(_modelSerialize);
        }

        public void Dispose()
        {
            SocketClients.Clear();
            server.Dispose();
        }
    }
}