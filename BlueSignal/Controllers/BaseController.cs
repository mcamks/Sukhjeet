using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BlueSignalCore.Bal;
using BlueSignalCore.Models;
using System.Configuration;
using System.IO;

namespace BlueSignal.Controllers
{
    public class BaseController : Controller
    {
        public string ApiBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiBaseUrl"];
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
                        action = "login",
                        controller = "account",
                        ReturnUrl = filterContext.HttpContext.Request.Url.PathAndQuery
                    });
                    filterContext.Result = new RedirectToRouteResult(values);
                }
                else
                {
                    filterContext.Result =
                       new RedirectResult(
                           "/account/login", false);
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
                            action = "login",
                            controller = "account",
                            ReturnUrl = filterContext.HttpContext.Request.Url.PathAndQuery
                        });
                        filterContext.Result = new RedirectToRouteResult(values);
                    }
                    else
                    {
                        filterContext.Result =
                           new RedirectResult(
                               "/account/login", false);
                    }
                }
            }
        }


        public string RenderPartialViewToStringBase(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
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