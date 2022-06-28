using LiteNetLib;
using LiteNetLib.Utils;
using Thread = System.Threading.Thread;

namespace GodotModules
{
    public class SceneLiteNetLib : Node
    {
        private CancellationTokenSource _cts_server;
        private CancellationTokenSource _cts_client;

        public override async void _Ready()
        {
            StartServer();
            await Task.Delay(1000);
            StartClient();
        }

        private async void StartClient()
        {
            try
            {
                _cts_client = new CancellationTokenSource();
                using var task = Task.Run(() => ClientThread(), _cts_client.Token);
                await task;
            }
            catch (Exception e)
            {
                GD.PrintErr("Client " + e.Message + " " + e.StackTrace);
            }
        }

        private async void StartServer()
        {
            try
            {
                _cts_server = new CancellationTokenSource();
                using var task = Task.Run(() => ServerThread(), _cts_server.Token);
                await task;
            }
            catch (Exception e)
            {
                GD.PrintErr("Server " + e.Message + " " + e.StackTrace);
            }
        }

        private void ClientThread()
        {
            GD.Print("Starting client");
            EventBasedNetListener listener = new EventBasedNetListener();
            NetManager client = new NetManager(listener) {
                IPv6Enabled = IPv6Mode.Disabled
            };
            client.Start();
            client.Connect("localhost" /* host ip or name */, 25565 /* port */, "SomeConnectionKey" /* text key or NetDataWriter */);
            listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
            {
                GD.Print("We got: {0}", dataReader.GetString(100 /* max length of string */));
                dataReader.Recycle();
            };

            while (true)
            {
                client.PollEvents();
                Thread.Sleep(15);
            }

            //client.Stop();
        }

        private void ServerThread()
        {
            GD.Print("Starting server");
            EventBasedNetListener listener = new EventBasedNetListener();
            NetManager server = new NetManager(listener)
            {
                IPv6Enabled = IPv6Mode.Disabled
            };
            server.Start(25565 /* port */);

            listener.ConnectionRequestEvent += request =>
            {
                if (server.ConnectedPeersCount < 10 /* max connections */)
                    request.AcceptIfKey("SomeConnectionKey");
                else
                    request.Reject();
            };

            listener.PeerConnectedEvent += peer =>
            {
                GD.Print("We got connection: {0}", peer.EndPoint); // Show peer ip
                NetDataWriter writer = new NetDataWriter();                 // Create writer class
                writer.Put("Hello client!");                                // Put some string
                peer.Send(writer, DeliveryMethod.ReliableOrdered);             // Send with reliability
            };

            while (true)
            {
                server.PollEvents();
                Thread.Sleep(15);
            }
            //server.Stop();
        }
    }
}
