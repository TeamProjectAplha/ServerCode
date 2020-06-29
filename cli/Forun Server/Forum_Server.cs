using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;  

namespace OpenSSL.CLI
{
    class Forum_Server
    {
        static Main_Server forum;
        public Forum_Server(Main_Server forum)
        { Forum_Server.forum = forum; }
        public void StartServer()
        {
            AsynchronousSocketListener.StartListening();
        }

        // State object for reading client data asynchronously  
        public class StateObject
        {
            // Client  socket.  
            public Socket workSocket = null;
            // Size of receive buffer.  
            public const int BufferSize = 1024;
            // Receive buffer.  
            public byte[] buffer = new byte[BufferSize];
            // Received data string.  
            public StringBuilder sb = new StringBuilder();
        }

        public class AsynchronousSocketListener
        {
            static Dictionary<string, StateObject> Clients = new Dictionary<string, StateObject>();
            static Dictionary<string, StateObject> Clientsid_Socket = new Dictionary<string, StateObject>();
            static Dictionary<string, bool> dict = new Dictionary<string, bool>();
            // Thread signal.  
            public static ManualResetEvent allDone = new ManualResetEvent(false);
            private const string allmsgs = "GetAllMessages";
            public AsynchronousSocketListener()
            {
            }

            public static void StartListening()
            {
                // Establish the local endpoint for the socket.  
                // The DNS name of the computer  
                // running the listener is "host.contoso.com".  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 12000);

                // Create a TCP/IP socket.  
                Socket listener = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Bind the socket to the local endpoint and listen for incoming connections.  
                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(100);
                    forum.Forum_server_started();
                    while (true)
                    {
                        // Set the event to nonsignaled state.  
                        allDone.Reset();

                        // Start an asynchronous socket to listen for connections.  
                        Console.WriteLine("Waiting for a connection...");
                        listener.BeginAccept(
                            new AsyncCallback(AcceptCallback),
                            listener);

                        // Wait until a connection is made before continuing.  
                        allDone.WaitOne();
                    }
                }
                catch (Exception e)
                {

                }
            }

            public static void AcceptCallback(IAsyncResult ar)
            {
                try
                {
                    // Signal the main thread to continue.  
                    allDone.Set();

                    // Get the socket that handles the client request.  
                    Socket listener = (Socket)ar.AsyncState;
                    Socket handler = listener.EndAccept(ar);

                    // Create the state object.  
                    StateObject state = new StateObject();
                    state.workSocket = handler;

                    if (!Clients.ContainsKey(state.workSocket.RemoteEndPoint.ToString()))
                        Clients.Add(state.workSocket.RemoteEndPoint.ToString(), state);

                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                }
                catch (Exception e)
                {

                }
            }

            public static void ReadCallback(IAsyncResult ar)
            {
                try
                {
                    String content = String.Empty;

                    // Retrieve the state object and the handler socket  
                    // from the asynchronous state object.  
                    StateObject state = (StateObject)ar.AsyncState;
                    Socket handler = state.workSocket;


                    // Read data from the client socket.
                    int bytesRead = handler.EndReceive(ar);

                    if (bytesRead > 0)
                    {
                        // There  might be more data, so store the data received so far.  
                        state.sb.Append(Encoding.ASCII.GetString(
                            state.buffer, 0, bytesRead));

                        // Check for end-of-file tag. If it is not there, read
                        // more data.  
                        content = state.sb.ToString();
                        if (content.IndexOf("<EOF>") > -1)
                        {
                            state.sb.Clear();
                            content = content.Replace("<EOF>", "");
                           
                            var userinfo = content.Split('&');
                            var username = userinfo[0].Substring(userinfo[0].IndexOf("=") + 1);

                            if (content.Contains("username") && content.Contains("message"))
                            {
                                if (!Clientsid_Socket.ContainsKey(username))
                                    Clientsid_Socket.Add(username, state);

                                var msgg = userinfo[1].Substring(userinfo[1].IndexOf("=") + 1);


                                if (msgg.Equals(allmsgs))
                                {
                                    Forum_msgs FM = new Forum_msgs();
                                    var data = FM.GetMessages(msgg);
                                    Send(handler, data + "<EOF>");
                                }
                                else
                                {
                                    Forum_msgs FM = new Forum_msgs();
                                    FM.send_Msgs(username, msgg);
                                    ///Send to online users

                                    foreach (var item in Clientsid_Socket)
                                    {
                                        var soc = item.Value;
                                        var hndlr = soc.workSocket;
                                        Send(hndlr, content + "<EOF>");                                        
                                    }
                                }
                            }
                            // Echo the data back to the client.                          
                            //Send(handler, content);
                        }
                        else
                        {
                            // Not all data received. Get more.  
                            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                            new AsyncCallback(ReadCallback), state);
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }

            private static void Send(Socket handler, String data)
            {
                try
                {
                    // Convert the string data to byte data using ASCII encoding.  
                    byte[] byteData = Encoding.ASCII.GetBytes(data);

                    // Begin sending the data to the remote device.  
                    handler.BeginSend(byteData, 0, byteData.Length, 0,
                        new AsyncCallback(SendCallback), handler);
                }
                catch (Exception e)
                {

                }
            }

            private static void SendCallback(IAsyncResult ar)
            {
                try
                {
                    // Retrieve the socket from the state object.  
                    Socket handler = (Socket)ar.AsyncState;

                    // Complete sending the data to the remote device.  
                    int bytesSent = handler.EndSend(ar);
                    Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                    var state = Clients[handler.RemoteEndPoint.ToString()];

                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                    //handler.Shutdown(SocketShutdown.Both);
                    //handler.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

        }
    }
}
