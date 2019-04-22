using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace StartUp
{
    public class HttpServer : IHttpServer
    {
        private TcpListener tcpListener;
        private bool isWorking;

        public HttpServer()
        {
            this.tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);
        }
        public void Start()
        {
            this.isWorking = true;
            this.tcpListener.Start();
            Console.WriteLine("Started.");
            while (this.isWorking)
            {
                var client = this.tcpListener.AcceptTcpClient();
                var buffer = new byte[10240];
                var stream = client.GetStream();
                var readLength = stream.Read(buffer, 0, buffer.Length);

                var requestText = Encoding.UTF8.GetString(buffer, 0, readLength);
                Console.WriteLine(new string('=', 60));
                Console.WriteLine(requestText);

                var resposeText = File.ReadAllText("index.html");
                var responseBytes = Encoding.UTF8.GetBytes(
                    "HTTP/1.0 200 OK" + Environment.NewLine+
                                      //+ "Content-Type: text/plain" + Environment.NewLine +
                                      "Content-Length" + resposeText.Length + Environment.NewLine +Environment.NewLine +
                                      resposeText
                );
                stream.Write(responseBytes);

            }
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}