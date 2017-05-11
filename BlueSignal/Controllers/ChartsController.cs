using BlueSignalCore.Bal;
using BlueSignalCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueSignal.Controllers
{
    public class ChartsController : BaseController
    {
        private readonly UserChartHistoryBal _ChartBal;

        public ChartsController(UserChartHistoryBal ChartBal)
        {
            _ChartBal = ChartBal;
        }
        [LogonAuthorize]
        public ActionResult Charts(string p, string c, string css)
        {
            Session["ActiveCssClass"] = css;
            if (!string.IsNullOrEmpty(css))
            {
                switch (css.ToLower())
                {
                    case "chart1":
                        Session["ActiveCssClass"] = "Charts1";
                        break;
                    case "chart2":
                        Session["ActiveCssClass"] = "Charts2";
                        break;
                    case "chart3":
                        Session["ActiveCssClass"] = "Charts3";
                        break;
                    default:
                        break;
                }
            }
            return View("_Chart1View");
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
                Session["TickerToggleState"] = "close";

            return Json(Convert.ToString(Session["TickerToggleState"]), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FirstChart(string p, string c, string css)
        {
            var symbol = "";
            ViewBag.RequestUrl = Request.Url.Authority;
            var sesValue= (WP_User)Session["SystemUser"];
            var result=  _ChartBal.GetUserChartHistory(Convert.ToInt64(sesValue.ID), "1").FirstOrDefault();
            if(result==null)
                 symbol = "SPY";
            else
                symbol = result.Symbol;

            ViewBag.Symbol = symbol;
            Session["ActiveCssClass"] = css;
            Session["FirstChartSymbol"] = symbol;
            if (!string.IsNullOrEmpty(css))
            {
                switch (css.ToLower())
                {
                    case "chart1":
                        Session["ActiveCssClass"] = "Charts1";
                        break;
                    case "chart2":
                        Session["ActiveCssClass"] = "Charts2";
                        break;
                    case "chart3":
                        Session["ActiveCssClass"] = "Charts3";
                        break;
                    default:
                        break;
                }
            }
            return View();
        }
        public ActionResult SecondChart(string p, string c, string css)
        {
            var symbol = "";
            ViewBag.RequestUrl = Request.Url.Authority;
            var sesValue = (WP_User)Session["SystemUser"];
            var result = _ChartBal.GetUserChartHistory(Convert.ToInt64(sesValue.ID), "2").FirstOrDefault();
            if (result == null)
                symbol = "SPY";
            else
                symbol = result.Symbol;

            ViewBag.Symbol = symbol;
            Session["SecondtChartSymbol"] = symbol;
            Session["ActiveCssClass"] = css;
            if (!string.IsNullOrEmpty(css))
            {
                switch (css.ToLower())
                {
                    case "chart1":
                        Session["ActiveCssClass"] = "Charts1";
                        break;
                    case "chart2":
                        Session["ActiveCssClass"] = "Charts2";
                        break;
                    case "chart3":
                        Session["ActiveCssClass"] = "Charts3";
                        break;
                    default:
                        break;
                }
            }
            return View();
        }

        public ActionResult ThirdChart(string p, string c, string css)
        {
            var symbol = "";
            ViewBag.RequestUrl = Request.Url.Authority;
            var sesValue = (WP_User)Session["SystemUser"];
            var result = _ChartBal.GetUserChartHistory(Convert.ToInt64(sesValue.ID), "3").FirstOrDefault();
            if (result == null)
                symbol = "SPY";
            else
                symbol = result.Symbol;

            ViewBag.Symbol = symbol;
            Session["ThirdChartSymbol"] = symbol;
            Session["ActiveCssClass"] = css;
            if (!string.IsNullOrEmpty(css))
            {
                switch (css.ToLower())
                {
                    case "chart1":
                        Session["ActiveCssClass"] = "Charts1";
                        break;
                    case "chart2":
                        Session["ActiveCssClass"] = "Charts2";
                        break;
                    case "chart3":
                        Session["ActiveCssClass"] = "Charts3";
                        break;
                    default:
                        break;
                }
            }
            return View();
        }

        public ActionResult SaveUserChartHistory(string symbol, string chartType, string viewType)
        {
            var sesValue = (WP_User)Session["SystemUser"];
            var result = _ChartBal.GetUserChartHistory(Convert.ToInt64(sesValue.ID), chartType).FirstOrDefault();
            if(result==null)
            {
                _ChartBal.SaveUserChartHistory(symbol, Convert.ToInt64(86), chartType);
            }
           else
            {
                result.Symbol = symbol;
                _ChartBal.UpdateChartHistory(result);
            }
            ViewBag.Symbol = symbol;
            if (chartType == "1")
            {
                Session["FirstChartSymbol"] = symbol;
            }
            else if(chartType=="2")
            {
                Session["SecondtChartSymbol"] = symbol;
            }
            else
            {
                Session["ThirdChartSymbol"] = symbol;
            }
            return View(viewType);
        }

        public ActionResult Lookup()
        {
            return View();
        }
      
    }
}