using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BlueSignalCore.Bal;
using BlueSignalCore.Models;

namespace BlueSignal.Controllers
{
    public class BaseController : Controller
    {
        private MarketBal _marketBal;

        public MarketBal MarketBal
        {
            get
            {
                if (_marketBal == null)
                    _marketBal = new MarketBal();
                return _marketBal;
            }
        }



        /// <summary>
        /// Check If User is Logged In
        /// </summary>
        public class LogonAuthorize : AuthorizeAttribute
        {
            /// <summary>
            /// Function     : HandleUnauthorizedRequest
            /// Objective    : This function to Overide default HandleUnauthorizedRequest function
            /// </summary>
            /// <returns></returns>
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {


                if (filterContext.HttpContext.Request.Url != null)
                {
                    var values = new RouteValueDictionary(new
                    {
                        action = "authFailed",
                        controller = "home",
                        ReturnUrl = filterContext.HttpContext.Request.Url.PathAndQuery
                    });
                    filterContext.Result = new RedirectToRouteResult(values);
                }
                else
                {
                    filterContext.Result =
                       new RedirectResult(
                           "/home/authFailed", false);
                }
            }


            /// <summary>
            /// Function     : OnAuthorization
            /// Objective    : This function to Overide default OnAuthorization function
            /// </summary>
            /// <returns></returns>
            public override void OnAuthorization(AuthorizationContext filterContext)
            {

                var objSessionWrapper = new HttpContextSessionWrapper();
                if ((objSessionWrapper != null && objSessionWrapper.SessionUser == null) || string.IsNullOrEmpty(objSessionWrapper.SessionUser.ID))
                {
                    if (filterContext.HttpContext.Request.Url != null)
                    {
                        var values = new RouteValueDictionary(new
                        {
                            action = "authFailed",
                            controller = "home",
                            ReturnUrl = filterContext.HttpContext.Request.Url.PathAndQuery
                        });
                        filterContext.Result = new RedirectToRouteResult(values);
                    }
                    else
                    {
                        filterContext.Result =
                           new RedirectResult(
                               "/home/authFailed", false);
                    }
                }
            }
        }
    }

    public static class HttpContextSessionWrapperExtension
    {
        public static HttpContextSessionWrapper HttpContextSessionWrapper { get { return new HttpContextSessionWrapper(); } }

        public static WP_User SessionUser { get { return HttpContextSessionWrapper.SessionUser; } }

        public static string UnAuthorizedUserAccessMessage { get { return "You are not authorized to access it, Please upgrade your plan."; } }


    }

    public class HttpContextSessionWrapper
    {


        public string ViewPageType { get; set; }


        private static T GetFromSessionStruct<T>(string key, T defaultValue = default(T)) where T : struct
        {
            var obj = HttpContext.Current.Session[key];
            if (obj == null)
            {
                return defaultValue;
            }
            return (T)obj;
        }
        public T GetFromSession<T>(string key) where T : class
        {
            var obj = HttpContext.Current.Session[key];
            return (T)obj;
        }
        private static void SetInSession<T>(string key, T value)
        {
            HttpContext.Current.Session[key] = value;
        }


        public WP_User SessionUser
        {
            get { return GetFromSession<WP_User>("SystemUser"); }
            set { SetInSession("SystemUser", value); }
        }
    }
}