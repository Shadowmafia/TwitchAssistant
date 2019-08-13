using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace TwitchAuthortization
{
    public static class HttpServer
    {
        private static readonly HttpListener _listener = new HttpListener();
        private static Func<string,Task<TwitchAuthResponse>> _responderMethod;
  
        public static void InitHttpServer(Func<string, Task<TwitchAuthResponse>> method,string redirectUri)
        {
            if (method == null)
                throw new ArgumentException("method");

            _listener.Prefixes.Clear();
            _listener.Prefixes.Add(redirectUri+"/");
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
                                _responderMethod(ctx.Request.QueryString.GetValues("code")[0]);
                                string rstr = $"<HTML><BODY>Thanks for allowing Twitch Assistant to authenticate<br></BODY></HTML>";                             
                                byte[] buf = Encoding.UTF8.GetBytes(rstr);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);                             
                            }
                            catch { }
                            finally
                            {
                                ctx.Response.OutputStream.Close();
                                Stop();
                            }
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
