using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;  

namespace OpenSSL.CLI
{
    class KMS_Server
    {
        static Main_Server kms;
        public KMS_Server(Main_Server kms)
        { KMS_Server.kms = kms; }
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
            static Dictionary<string, bool> dict = new Dictionary<string, bool>();
            // Thread signal.  
            public static ManualResetEvent allDone = new ManualResetEvent(false);

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
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP socket.  
                Socket listener = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Bind the socket to the local endpoint and listen for incoming connections.  
                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(100);
                    kms.Kms_server_started();
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
                    Console.WriteLine(e.ToString());
                }

                Console.WriteLine("\nPress ENTER to continue...");
                Console.Read();

            }

            public static void AcceptCallback(IAsyncResult ar)
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
                            // All the data has been read from the
                            // client. Display it on the console.  
                            Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                                content.Length, content);

                            var userinfo = content.Split('&');
                            var username = userinfo[0].IndexOf("=") + 1;
                            if (content.Contains("username") && content.Contains("password"))
                            {
                                var password = userinfo[1].IndexOf("=") + 1;

                                Check_User ck = new Check_User(userinfo[0].Substring(username), userinfo[1].Substring(username));
                                bool result = ck.check_user();

                                if (result)
                                {
                                    Send(handler, "Login Successfull<EOF>");
                                    if (!dict.ContainsKey(userinfo[0].Substring(username)))
                                        dict.Add(userinfo[0].Substring(username), true);
                                }
                                else
                                    Send(handler, "Username or Password is Incorrect!!<EOF>");
                            }
                            else if (dict.ContainsKey(userinfo[0].Substring(username)) && content.Contains("message"))
                            {
                                var msg = userinfo[1].IndexOf("=") + 1;
                                if (userinfo[1].Substring(msg).Equals("SendUserPrivateKey"))
                                {
                                    Check_User ck = new Check_User(userinfo[0].Substring(username), "");
                                    var data = ck.Get_User_PKI();
                                    Send(handler, data + "<EOF>");
                                }
                                if (userinfo[1].Substring(msg).Equals("SendUsersPublicKey"))
                                {
                                    Check_User ck = new Check_User(userinfo[0].Substring(username), "");
                                    var data = ck.Get_Users_PKI();
                                    Send(handler, data + "<EOF>");
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
                catch (Exception ex)
                { }
            }

            private static void Send(Socket handler, String data)
            {
                // Convert the string data to byte data using ASCII encoding.  
                byte[] byteData = Encoding.ASCII.GetBytes(data);

                // Begin sending the data to the remote device.  
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), handler);
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
