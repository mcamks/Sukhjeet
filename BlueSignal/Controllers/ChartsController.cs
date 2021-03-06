﻿using BlueSignalCore.Bal;
using BlueSignalCore.Models;
using System;
using System.Linq;
using System.Net;
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
            var sessionValue = Session["TickerToggleState"];
            if (Session["TickerToggleState"] == null)
            {
                sessionValue = "close";
            }
            else
            {
                if (sessionValue == "close")
                {

                    Session["TickerToggleState"] = "open";
                }

                if (sessionValue == "open")
                {

                    Session["TickerToggleState"] = "close";
                }
            }
            state = Convert.ToString(Session["TickerToggleState"]);
           // Session["TickerToggleState"] = state;
            return Json(state, JsonRequestBehavior.AllowGet);
        }

        [LogonAuthorize]
        public ActionResult GetTickerToggleState()
        {
            if (Session["TickerToggleState"] == null)
                Session["TickerToggleState"] = "open";

            return Json(Convert.ToString(Session["TickerToggleState"]), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FirstChart(string p, string c, string css)
        {
            var symbol = "";
            ViewBag.RequestUrl = Request.Url.Authority;
            var sesValue = (WP_User)Session["SystemUser"];
#pragma warning disable CS0472 // The result of the expression is always 'false' since a value of type 'long' is never equal to 'null' of type 'long?'
            if (sesValue == null)
#pragma warning restore CS0472 // The result of the expression is always 'false' since a value of type 'long' is never equal to 'null' of type 'long?'
            {
                return RedirectToAction("Login", "Account");
            }
            var result = _ChartBal.GetUserChartHistory(Convert.ToInt64(sesValue.ID), "1").FirstOrDefault();
            if (result == null)
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
            if (result == null)
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
            else if (chartType == "2")
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

        public ActionResult GetLookUpData(string sym)
        {
            using (WebClient web = new WebClient())
            {
                string query = sym;
                //http://d.yimg.com/aq/autoc?query=y&region=US&lang=en-US&callback=YAHOO.util.ScriptNodeDataSource.callbacks
                string url = "http://d.yimg.com/autoc.finance.yahoo.com/autoc?query=" + query + "&region=us&lang=en-US";//string.Format("http://d.yimg.com/autoc.finance.yahoo.com/autoc?query={0}&region=us&lang=en-US", query);

                string data = web.DownloadString(url);



                //var json = new JavaScriptSerializer().Serialize(data);
                ////remove the class/method name from the JSON
                //Match match = Regex.Match(data, @"YAHOO.Finance.SymbolSuggest.ssCallback");
                //data = match.Groups[1].Value;

                //var serializer = new JavaScriptSerializer();
                //serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
                //dynamic obj = serializer.Deserialize(data, typeof(object));

                //foreach (dynamic result in obj.ResultSet.Result)
                //{
                //    Console.WriteLine(string.Format("{0} {1} {2}", result.symbol, result.name, result.exch));
                //}

                //Console.ReadLine();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            // return Json("1", JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoComplete()
        {
            return View();
        }




    }
}