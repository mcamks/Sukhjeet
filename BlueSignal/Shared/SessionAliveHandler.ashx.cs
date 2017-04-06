using System;
using System.Web;
using System.Web.SessionState;

namespace BlueSignal.Shared
{
    /// <summary>
    /// Summary description for SessionAlive
    /// </summary>
    public class SessionAliveHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Session["KeepSessionAlive"] = DateTime.Now;
        }
    }
}