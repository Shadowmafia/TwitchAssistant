using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YoutubePlayerLib.FakeServer
{
    public static class HttpServer
    {
        private static readonly HttpListener _listener = new HttpListener();
        private static Func<HttpListenerRequest, string> _responderMethod;

        static HttpServer()
        {
            _listener.Prefixes.Add($"http://localhost:8080/YouPlayer/");
        }

        public static void InitHttpServer(Func<HttpListenerRequest, string> method)
        {

            if (method == null)
                throw new ArgumentException("method");

            _responderMethod = method;

            _listener.Start();
        }

        public static void Run()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem((c) =>
                        {
                            var ctx = c as HttpListenerContext;
                            try
                            {
                                string rstr = _responderMethod(ctx.Request);
                                byte[] buf = Encoding.UTF8.GetBytes(rstr);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            catch { }
                         
                        }, _listener.GetContext());
                    }
                }
                catch { }
            });
        }

        public static void Stop()
        {
            _listener.Stop();
        }

    }
}
