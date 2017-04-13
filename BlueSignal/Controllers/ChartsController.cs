using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueSignal.Controllers
{
    public class ChartsController : BaseController
    {
        [LogonAuthorize]
        public ActionResult Charts(string p, string c, string css)
        {
            Session["ActiveCssClass"] = css;
            if (!string.IsNullOrEmpty(css))
            {
                switch (css.ToLower())
                {
                    case "chart1":
                        return View("_Chart1View");
                    default:
                        break;
                }
            }

            return View();
        }

        [LogonAuthorize]
        public ActionResult SetTickerToggleState(string state)
        {
            Session["TickerToggleState"] = state;
            return Json(state, JsonRequestBehavior.AllowGet);
        }

        [LogonAuthorize]
        public ActionResult GetTickerToggleState()
        {
            if (Session["TickerToggleState"] == null)
                Session["TickerToggleState"] = "open";

            return Json(Convert.ToString(Session["TickerToggleState"]), JsonRequestBehavior.AllowGet);
        }
    }
}