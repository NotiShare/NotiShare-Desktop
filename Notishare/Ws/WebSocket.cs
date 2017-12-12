using System;
using SharpSocket = WebSocketSharp;

namespace Notishare.Ws
{
    internal class WebSocket
    {

        private const string DefaultUrl = "ws://192.168.43.89";

        private string url;

        private int port;

        private string id;

        private string deviceDbId;

        private string userDbId;

        private string deviceType;

        private SharpSocket.WebSocket socket;

        private Action<string> sendAction;

        public WebSocket(string url, int port, string id, string deviceDbId, string userDbId, string deviceType, Action<string> sendAction)
        {
            this.url = url;
            this.port = port;
            //this.id = id;
            this.deviceDbId = deviceDbId;
            this.userDbId = userDbId;
            this.deviceType = deviceType;
            this.sendAction = sendAction;
        }



        public void Init()
        {

            socket = new SharpSocket.WebSocket($"{DefaultUrl}:{port}/{url}?id={id}&deviceId={deviceDbId}&userId={userDbId}&type={deviceType}");
            socket.OnOpen += SocketOnOnOpen;
            socket.OnMessage += SocketOnOnMessage;
            socket.OnError += SocketOnOnError;
            socket.OnClose += SocketOnOnClose;
            socket.Connect();
        }

        private void SocketOnOnClose(object sender, SharpSocket.CloseEventArgs closeEventArgs)
        {
            
        }

        private void SocketOnOnError(object sender, SharpSocket.ErrorEventArgs errorEventArgs)
        {
           
        }

        private void SocketOnOnMessage(object sender, SharpSocket.MessageEventArgs messageEventArgs)
        {
            sendAction.Invoke(messageEventArgs.Data);
        }

        private void SocketOnOnOpen(object sender, EventArgs eventArgs)
        {
            
        }


        public void SendData(string message)
        {
            socket.Send(message);
        }


        public void Close()
        {
            socket.Close();
        }
    }
}
