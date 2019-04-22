using System;

namespace StartUp
{
    public static class StartUp
    {
        public static void Main(string[] args)
        {
            IHttpServer server = new HttpServer();
            server.Start();
        }
    }
}
