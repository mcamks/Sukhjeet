using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BlueSignal.Common;
using BlueSignal.Models;
using BlueSignalCore.Bal;
using BlueSignalCore.Dto;
using BlueSignalCore.Models;
using BluSignalHelpMethod;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.IO;

namespace BlueSignal.Controllers
{
    public class HomeController : BaseController
    {
        private readonly MarketBal _marketBal;
        private readonly UserChartHistoryBal _ChartBal;
        private readonly GlobalCodesBal _gcBal;
        public HomeController(MarketBal marketBal, UserChartHistoryBal ChartBal, GlobalCodesBal gcBal)
        {
            _marketBal = marketBal;
            _ChartBal = ChartBal;
            _gcBal = gcBal;
        }

        public string apiKey = BluSignalComman.APIkey;
        WebClientHelp webClientHelp = new WebClientHelp();

        public int AverageVolumn { get; private set; }

        [LogonAuthorize]
        public async Task<ActionResult> Index(string symbol)
        {
            Session["fisrtTabSymbol"] = "SPY";
                 Session["secondTabSymbol"]=  "SPY";
                 Session["thirdTabSymbol"]=   "SPY";
                 Session["forthTabSymbol"] = "SPY";


            if (Session["SystemUser"] == null)
                await Auth();


            if (!string.IsNullOrEmpty(symbol))
            {
                symbol = symbol.Replace("INDEX:", "");
                ViewBag.Symbol = symbol;
                ViewBag.activeTab = "MarketDataLists";
            }

            Session["ActiveCssClass"] = "Settings";
            return await Task.FromResult(View());
        }

        [AllowAnonymous]
        public async Task<ActionResult> Auth()
        {
            var userName = Request.QueryString["E"];
            var Password = Request.QueryString["P"];

            var IsUserExist = await _marketBal.IsUserExists(userName.ToLower(), Password);

            if (IsUserExist == null)
                Session.Add("firstTimeCheck", "1");

            var user = _marketBal.GetWpUser(userName, Password);
            #region ToolTIp
            //Dynamic Tool Tip
            var Firstsymbol = "";
            var firstChartSymbolResult = _ChartBal.GetUserChartHistory(Convert.ToInt64(user.ID), "1").FirstOrDefault();
            if (firstChartSymbolResult == null)
                Firstsymbol = "SPY";
            else
                Firstsymbol = firstChartSymbolResult.Symbol;
            Session["FirstChartSymbol"] = Firstsymbol;


            var secondSymbol = "";
            ViewBag.RequestUrl = Request.Url.Authority;
            var sesValue = (WP_User)Session["SystemUser"];
            var result = _ChartBal.GetUserChartHistory(Convert.ToInt64(user.ID), "2").FirstOrDefault();
            if (result == null)
                secondSymbol = "SPY";
            else
                secondSymbol = result.Symbol;

            ViewBag.Symbol = secondSymbol;
            Session["SecondtChartSymbol"] = secondSymbol;



            var thirdSymbol = "";

            var ThirdResult = _ChartBal.GetUserChartHistory(Convert.ToInt64(user.ID), "3").FirstOrDefault();
            if (ThirdResult == null)
                thirdSymbol = "SPY";
            else
                thirdSymbol = ThirdResult.Symbol;
            Session["ThirdChartSymbol"] = thirdSymbol;

            var gcList = _gcBal.GetGlobalCodesValue("1001").FirstOrDefault();
            Session["LongTermChart"] = gcList.ExternalValue2;
            Session["NearTemChart"] = gcList.ExternalValue1;
            Session["GlobalCodeId"] = gcList.GlobalCodeID;
            #endregion
            if (user == null)
            {
                user = new WP_User();
                user.ID = Convert.ToString(1).Trim();
                user.user_login = Convert.ToString("true").Trim();
                user.user_password = Convert.ToString("true").Trim();
                user.user_nicename = Convert.ToString("true").Trim();
                user.user_email = Convert.ToString("true").Trim();
                user.user_registered = Convert.ToString("true").Trim();
                user.display_name = Convert.ToString("true").Trim();
                user.display_AdminKey = Convert.ToString("administrator").Trim();
            }


            if (user != null)
            {
                user.IsAlreadyRegisteredWithBSPortal = (IsUserExist != null);
                SystemLogin(user);
                await CheckUserBundle(user);
                var loggedInUser = Session["SystemUser"];
                return RedirectToAction("Dashboard");
            }
            else
            {
                SystemLogout();
                //Redirect To Unknown payment page
            }
            return await Task.FromResult(View());
        }

        private async Task CheckUserBundle(WP_User user)
        {
            //string jsonString = @"{'response_code':'200','response_message':'','response_data':{'member_id':44,'first_name':'Dee','last_name':'Menzies','is_complimentary':'false','registered':'2017 - 02 - 17 19:24:36','cancellation_date':'','last_logged_in':'2017 - 02 - 18 21:55:45','last_updated':'2017 - 02 - 18 21:55:45','days_as_member':3,'status':'1','status_name':'Active','membership_level':'2','membership_level_name':'Paid Membership','username':'menzies.dee @gmail.com','email':'menzies.dee @gmail.com','password':null,'phone':'1112223333','billing_address':'123 Maple','billing_city':'Houston','billing_state':'TX','billing_zip':'77002','billing_country':'United States','shipping_address':'123 Maple','shipping_city':'Houston','shipping_state':'TX','shipping_zip':'77002',
            //'shipping_country':'United States','bundles':[{'id':7,'name':'Single System - BluFractal (monthly)'},{'id':3,'name':'Single System - BluNeural (monthly)'}],'custom_fields':[{'id':2,'name':'Terms of Serv','value':''},{'id':3,'name':'Terms of Service','value':'mm_cb_on'}]}}";

            //var objResponse = jsonString;
            //Rootobject facebookFriends = new JavaScriptSerializer().Deserialize<Rootobject>(objResponse);

            //if (facebookFriends != null && facebookFriends.response_code == "200" && facebookFriends.response_data != null && facebookFriends.response_data.bundles != null && facebookFriends.response_data.bundles.Count > 0)
            //{
            //    user.bundles = facebookFriends.response_data.bundles;
            //}






            var client = new HttpClient();
            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("apikey", "9i6t91dkbr"),
                new KeyValuePair<string, string>("apisecret","4tqpbbph1r"),
                new KeyValuePair<string, string>("email",user.user_email)
            });

            string methodName = "getMember";

            HttpResponseMessage response = await client.PostAsync("https://www.blusignalsystems.com/wp-content/plugins/membermouse/api/request.php?q=/" + methodName, requestContent);

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                var jsonString = (await reader.ReadToEndAsync()) + Environment.NewLine;

                var objResponse = jsonString;
                Rootobject facebookFriends = new JavaScriptSerializer().Deserialize<Rootobject>(objResponse);

                if (facebookFriends != null && facebookFriends.response_code == "200" && facebookFriends.response_data != null && facebookFriends.response_data.bundles != null && (facebookFriends.response_data.bundles.Count > 0 || !string.IsNullOrEmpty(facebookFriends.response_data.membership_level_name)))
                {
                    if (facebookFriends.response_data.bundles.Count == 0 && !string.IsNullOrEmpty(facebookFriends.response_data.membership_level_name))
                    {
                        var bundles = new List<bundles>();
                        var nm = facebookFriends.response_data.membership_level_name.ToLower().Trim();

                        if (nm.Contains("fractal"))
                            bundles.Add(new bundles { id = 1, name = "fractal", value = "fractal" });
                        if (nm.Contains("neural"))
                            bundles.Add(new bundles { id = 2, name = "neural", value = "neural" });
                        if (nm.Contains("quant"))
                            bundles.Add(new bundles { id = 3, name = "quant", value = "quant" });

                        user.bundles = bundles;
                    }
                    else
                        user.bundles = facebookFriends.response_data.bundles;
                }
            }


        }

        [LogonAuthorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [LogonAuthorize]
        public async Task<ActionResult> Dashboard()
        {
            if (Session["SystemUser"] == null)
                await Auth();

            Session["ActiveCssClass"] = "Dashboard";
            return View();
        }

        [LogonAuthorize]
        public ActionResult Charts(string p, string c, string css)
        {
            Session["ActiveCssClass"] = css;
            return View();
        }

        [LogonAuthorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [LogonAuthorize]
        public ActionResult GoogleFinance()
        {
            ViewBag.Message = "Google Finance";
            return View();
        }

        [LogonAuthorize]
        public ActionResult MarketData()
        {
            return View();
        }


        public string GetGoogleFinanceInfo(string q)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString("http://finance.google.com/finance/info?client=ig&q=" + q);
                    return s.Replace("//", "");
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (WebException ex) //if server is off it will throw exeception and here we need notify user
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
            }

            return "Fail";

        }


        public async Task<string> GetMarketData(string startDate, string Type, string symbol)
        {
            try
            {
                //using (WebClient client = new WebClient())
                //{
                //    string s = await client.DownloadStringTaskAsync("http://marketdata.websol.barchart.com/getHistory.json?key=" + apiKey + "&symbol=" + symbol + "&type=" + Type + "&startDate=" + startDate);
                //    return s.Replace("//", "");
                //}

                var endDate = BluSignalComman.EndDate;
                var obj = await GetLoggingData(symbol, startDate, endDate, Type);
                //if (obj.results != null && obj.results.Any())
                //{
                //    //var data = JsonConvert.SerializeObject(obj.results, new JsonSerializerSettings { ContractResolver = new DynamicContractResolver("timestamp,tradingDay") });
                //    var data = JsonConvert.SerializeObject(obj.results, new JsonSerializerSettings { ContractResolver = new DynamicContractResolver("") });
                //    //return data.Replace("timestamp1", "timestamp").Replace("tradingDay1", "tradingDay").Replace("//", "");
                //    return data.Replace("//", "");
                //}
                return obj.serializedResult;

            }
            catch (WebException ex) //if server is off it will throw exeception and here we need notify user
            {
                throw ex;
            }
#pragma warning disable CS0162 // Unreachable code detected
            return "Fail";
#pragma warning restore CS0162 // Unreachable code detected

        }

        public async Task<JsonResult> GetMarketChartData(string startDate, string Type, string symbol)
        {
            try
            {
                startDate = BluSignalComman.DateTime9MonthBack;
                var endDate = BluSignalComman.EndDate;

                if (Type == "weekly")
                    startDate = BluSignalComman.DateTime9MonthWeeksBack;

                if (Type == "dailySecond" || Type == "dailyBulQuantData" || Type == "dailyBuleFractal" || Type == "dailyBlueNeural")
                    Type = "daily";

                Session["DefaultKey"] = symbol;

                //using (WebClient client = new WebClient())
                //{
                //    string s = client.DownloadString("http://marketdata.websol.barchart.com/getHistory.json?key=" + apiKey + "&symbol=" + symbol + "&type=" + Type + "&startDate=" + startDate);
                //    var returnstring = s.Replace("//", "");
                //    var chartJsonData = Helpers.GetJsonResponse<List<resultsData>>(returnstring, "results");
                //    var stringtoReturn = chartJsonData.ToList().Select(item => new object[]
                //    {
                //       item.tradingDay.ToString(),
                //       //Convert.ToDecimal(Convert.ToDateTime(item.tradingDay).ToString("yyyyMMddHHmmssfff")),
                //       Math.Round(Convert.ToDecimal((item.open)),2),
                //       Math.Round(Convert.ToDecimal((item.high)),2),
                //       Math.Round(Convert.ToDecimal((item.low)),2),
                //       Math.Round(Convert.ToDecimal((item.close)),2)
                //    });
                //    return Json(stringtoReturn, JsonRequestBehavior.AllowGet);
                //}

                var obj = await GetLoggingData(symbol, startDate, endDate, Type);
                if (obj.results != null && obj.results.Any())
                {
                    var stringtoReturn = obj.results.Select(item => new object[] {
                        item.tradingDay.ToString(),
                        Math.Round(Convert.ToDecimal((item.open)),2),
                        Math.Round(Convert.ToDecimal((item.high)),2),
                        Math.Round(Convert.ToDecimal((item.low)),2),
                        Math.Round(Convert.ToDecimal((item.close)),2)
                    });
                    var jsonResult = new JsonResult { Data = stringtoReturn, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = int.MaxValue };
                    return jsonResult;
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (WebException ex) //if server is off it will throw exeception and here we need notify user
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
            }
            return Json("");
        }

        private JsonResult GetNameFromSymbol(string symbol)
        {
            // symbol = "QQQ";
            string ss = "http://marketdata.websol.barchart.com/getQuote.json?key=" + apiKey + "&symbols=" + symbol;

            string ss2 = webClientHelp.GetWebClientResponse(ss);


            return Json(ss2);


        }



        public async Task<JsonResult> GetAllMarketData(string startDate, string Type, string selectedCode, string tabType)
        {

            if(tabType=="1")
            {
                Session["fisrtTabSymbol"] = selectedCode;
            }
            else if(tabType=="2")
            {
                Session["secondTabSymbol"] = selectedCode;
            }
            else if (tabType == "3")
            {
                Session["thirdTabSymbol"] = selectedCode;
            }
            else
            {
                Session["forthTabSymbol"] = selectedCode;
            }

           



            startDate = BluSignalComman.DateTime9MonthBack;
            MarketDataViewModel vm = new MarketDataViewModel();

            var result = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(selectedCode) && selectedCode.IndexOf("-") != -1)
                {
                    var codeArray = selectedCode.Split('-');
                    vm.Name = codeArray[0].Trim();
                    vm.Code = codeArray[1].Trim();
                }
                else { vm.Code = selectedCode; }

                var dddd = GetNameFromSymbol(vm.Code);
                if (dddd != null)
                {
                    vm.SymbolNameData = Convert.ToString(dddd.Data);
                }


                Session["DefaultKey"] = vm.Code;
                vm.LastTradingDay = "02 Feb 2017";

                using (WebClient client = new WebClient())
                {
                    var apiUrl = string.Empty;

                    #region Commented Code
                    /**DAILY DATA**/
                    //apiUrl = string.Format("http://marketdata.websol.barchart.com/getHistory.json?key=" + apiKey + "&symbol={0}&type=daily&startDate={1}", vm.Code, startDate);
                    //result = await client.DownloadStringTaskAsync(new Uri(apiUrl));
                    //if (!string.IsNullOrEmpty(result))
                    //{
                    //    result = result.Replace("//", "");
                    //    vm.MarketDataDaily = result;

                    //    var data = JsonConvert.DeserializeObject<Data>(result);
                    //    if (data != null && data.results != null && data.results.Any())
                    //    {
                    //        int lastDays = 90;
                    //        var maxRexord = data.results.OrderByDescending(a => a.tradingDay).Take(lastDays).Sum(x => Convert.ToSingle(x.volume));
                    //        if (maxRexord > 0)
                    //            vm.AverageVolumn = Convert.ToString(Math.Round((maxRexord / lastDays), 0));
                    //    }
                    //    vm.ClosingPrice = string.Empty;
                    //}
                    /**DAILY DATA**/


                    /**Weekly DATA**/
                    //apiUrl = string.Format("http://marketdata.websol.barchart.com/getHistory.json?key=" + apiKey + "&symbol={0}&type=weekly&startDate={1}", vm.Code, startDate);
                    //result = await client.DownloadStringTaskAsync(new System.Uri(apiUrl));
                    //if (!string.IsNullOrEmpty(result))
                    //{
                    //    result = result.Replace("//", "");
                    //    vm.MarketDataWeekly = result;
                    //}
                    /**Weekly DATA**/



                    /**Monthly DATA**/
                    //apiUrl = string.Format("http://marketdata.websol.barchart.com/getHistory.json?key=" + apiKey + "&symbol={0}&type=monthly&startDate={1}", vm.Code, startDate);
                    //result = await client.DownloadStringTaskAsync(new System.Uri(apiUrl));
                    //if (!string.IsNullOrEmpty(result))
                    //{
                    //    result = result.Replace("//", "");
                    //    vm.MarketDataMonthly = result;

                    //    var data = JsonConvert.DeserializeObject<Data>(result);
                    //    if (data != null && data.results != null && data.results.Any())
                    //    {
                    //        var maxRexord = data.results.OrderByDescending(a => a.timestamp).First();
                    //        if (maxRexord != null)
                    //            vm.ClosingPrice = maxRexord.close;
                    //    }
                    //}
                    /**Monthly DATA**/



                    /**Quarterly DATA**/
                    //apiUrl = string.Format("http://marketdata.websol.barchart.com/getHistory.json?key=" + apiKey + "&symbol={0}&type=quarterly&startDate={1}", vm.Code, startDate);
                    //result = await client.DownloadStringTaskAsync(new System.Uri(apiUrl));
                    //if (!string.IsNullOrEmpty(result))
                    //{
                    //    result = result.Replace("//", "");
                    //    vm.MarketDataQuartely = result;
                    //}
                    /**Quarterly DATA**/



                    /**Yearly DATA**/
                    //apiUrl = string.Format("http://marketdata.websol.barchart.com/getHistory.json?key=" + apiKey + "&symbol={0}&type=yearly&startDate={1}", vm.Code, startDate);
                    //result = await client.DownloadStringTaskAsync(new Uri(apiUrl));
                    //if (!string.IsNullOrEmpty(result))
                    //{
                    //    result = result.Replace("//", "");
                    //    vm.MarketDataYearly = result;
                    //}
                    /**Yearly DATA**/
                    #endregion

                    ///*
                    // * Daily, Monthly, Yearly and Quartely Data
                    // */
                    //var chartResult = await GetMinutesChartData(vm.SymbolName, startDate);
                    //if (chartResult.results != null && chartResult.results.Any())
                    //{
                    //    result = JsonConvert.SerializeObject(chartResult.results, new JsonSerializerSettings { ContractResolver = new DynamicContractResolver("timestamp,tradingDay") });
                    //    result = result.Replace("timestamp1", "timestamp").Replace("tradingDay1", "tradingDay").Replace("//", "");
                    //    int lastDays = 90;

                    //    var averageVolumn = chartResult.results.OrderByDescending(a => a.tradingDay).Take(lastDays).Sum(x => Convert.ToSingle(x.volume));
                    //    if (averageVolumn > 0)
                    //        vm.AverageVolumn = Convert.ToString(Math.Round((averageVolumn / lastDays), 0));

                    //    var maxRexord = chartResult.results.OrderByDescending(a => a.timestamp).FirstOrDefault();
                    //    if (maxRexord != null)
                    //        vm.ClosingPrice = maxRexord.close;

                    //    vm.MarketDataDaily = result;
                    //    //vm.MarketDataWeekly = result;
                    //    //vm.MarketDataMonthly = result;
                    //    //vm.MarketDataQuartely = result;
                    //    //vm.MarketDataYearly = result;
                    //}

                    //var chartJsonData = Helpers.GetJsonResponse<List<resultsData>>(result, "result");
                    //var ChartData = new List<ChartDataModel>();
                    //ChartData.AddRange(chartJsonData.ToList().Select(item => new ChartDataModel()
                    //{
                    //    symbol = item.symbol,
                    //    close = Convert.ToDecimal(item.close),
                    //    high = Convert.ToDecimal(item.high),
                    //    low = Convert.ToDecimal(item.low),
                    //    open = Convert.ToDecimal(item.open),
                    //    TradingDayTimeStamp = Convert.ToDateTime(item.tradingDay).ToString("yyyyMMddHHmmssfff"),
                    //}));

                    //var ChartData = new List<ChartDataModel>();
                    //ChartData.AddRange(chartResult.results.Select(item => new ChartDataModel()
                    //{
                    //    symbol = item.symbol,
                    //    close = Convert.ToDecimal(item.close),
                    //    high = Convert.ToDecimal(item.high),
                    //    low = Convert.ToDecimal(item.low),
                    //    open = Convert.ToDecimal(item.open),
                    //    TradingDayTimeStamp = Convert.ToDateTime(item.tradingDay).ToString("yyyyMMddHHmmssfff"),
                    //}));
                    //vm.ChartData = ChartData;
                }

                vm.MarketLists = await _marketBal.GetMarketData();
                vm.Categories = await _marketBal.GetActiveMarketCategories();
                vm.FirstTabSymbol = Convert.ToString(Session["fisrtTabSymbol"]);
                vm.SecondTabSymbol = Convert.ToString(Session["secondTabSymbol"]);
                vm.ThirdTabSymbol = Convert.ToString(Session["thirdTabSymbol"]);
                vm.ForthTabSymbol = Convert.ToString(Session["forthTabSymbol"]);
            }
            catch (WebException ex) //if server is off it will throw exeception and here we need notify user
            {
                throw ex;
            }
            var jsonResult = new JsonResult { Data = vm, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = int.MaxValue };
            return jsonResult;
        }



        [HttpPost]
        public async Task<ActionResult> Contact(ContactViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var db = new Comman.DBAccess.BluSignalsEntities();

                    //var conact = new Comman.DBAccess.ContactLog()
                    //{

                    //    Name = model.Name,
                    //    Email = model.Email,
                    //    Message = model.Message,
                    //    Subject = model.Subject,
                    //    CreatedDate = DateTime.UtcNow

                    //};
                    //var emailTemp = db.EmailTemplates.Where(e => e.EmailType == 1).FirstOrDefault();

                    //var EB = new StringBuilder(emailTemp.EmailBody);

                    //EB.Replace("@Name", conact.Name);
                    //EB.Replace("@Email", conact.Email);
                    //EB.Replace("@Subject", conact.Subject);
                    //EB.Replace("@Query", conact.Message);
                    //MailHelper.SendMailMessage(CommonConfig.AdminEmailID, "", "", emailTemp.EmailSubject + "-" + conact.Subject, EB.ToString(), true);

                    //db.ContactLogs.Add(conact);
                    //db.SaveChanges();

                    var contact = new ContactLog
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Message = model.Message,
                        Subject = model.Subject,
                        CreatedDate = DateTime.UtcNow
                    };
                    var emailTemp = await _marketBal.SaveContactLog(contact);

                    if (emailTemp != null)
                    {
                        var EB = new StringBuilder(emailTemp.EmailBody);

                        EB.Replace("@Name", contact.Name);
                        EB.Replace("@Email", contact.Email);
                        EB.Replace("@Subject", contact.Subject);
                        EB.Replace("@Query", contact.Message);
                        MailHelper.SendMailMessage(CommonConfig.AdminEmailID, "", "", emailTemp.EmailSubject + "-" + contact.Subject, EB.ToString(), true);
                    }

                    var model2 = new ContactViewModel();
                    ModelState.Clear();
                    model2.IsSuccess = 2;
                    //model2.Name = string.Empty;
                    //model2.Email = string.Empty;
                    //model2.Message = string.Empty;
                    //model2.Subject = string.Empty;





                    return PartialView("_ContactLog", model2);
                }
                else
                {
                    model.IsSuccess = 1;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return PartialView("_ContactLog", model);
        }


        [HttpPost]
        public async Task<ActionResult> SetPassword(SetPassowrdModel model)
        {
            try
            {
                model.pwd = model.cpwd;

                if (ModelState.IsValid)
                {
                    model.IsSuccess = 0;

                    var loggedInUser = HttpContextSessionWrapperExtension.SessionUser;
                    if (loggedInUser != null)
                    {
                        loggedInUser.IsAlreadyRegisteredWithBSPortal = false;

                        var existsAndPwdUpdated = await _marketBal.CheckUserAndUpdatePassword(loggedInUser.user_email.ToLower(), loggedInUser.user_email, model.pwd);
                        if (existsAndPwdUpdated)
                        {
                            model.IsSuccess = 2;
                            Session.Remove("firstTimeCheck");
                            ModelState.AddModelError("cpwd", "Success!!. User has been Authenticated!");
                            loggedInUser.IsAlreadyRegisteredWithBSPortal = true;
                            SystemLogin(loggedInUser);
                            await CheckUserBundle(loggedInUser);
                        }
                        else
                            ModelState.AddModelError("cpwd", "");

                        return PartialView("_BSLogin", model);
                    }

                    var model2 = new SetPassowrdModel();
                    ModelState.Clear();
                    model2.IsSuccess = 2;
                    return PartialView("_BSLogin", model2);
                }
                else
                {
                    model.IsSuccess = 1;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return PartialView("_BSLogin", model);
        }




        public ActionResult authFailed()
        {
            return View();
        }



        private void SystemLogin(WP_User user)
        {
            if (Session["SystemUser"] != null)
                Session.Remove("SystemUser");
            Session.Add("SystemUser", user);
        }

        private void SystemLogout()
        {
            Session.Remove("SystemUser");
        }




        #region Market Data Admin Setup Actions

        public async Task<JsonResult> GetMarketDataById(long id)
        {
            JsonResult json = null;
            try
            {
                var vm = await _marketBal.GetMarketDataById(id);
                json = new JsonResult
                {
                    Data = vm,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (WebException ex) //if server is off it will throw exeception and here we need notify user
            {
                throw ex;
            }

            return json;

        }

        public async Task<JsonResult> GetMarketSetupData()
        {
            JsonResult json = null;
            try
            {
                var list = await _marketBal.GetMarketData();
                var categories = await _marketBal.GetActiveMarketCategories();
                json = new JsonResult
                {
                    Data = new
                    {
                        blueFractal = list.Where(a => a.ProductTypeID.Equals("102")),//list.Where(a => a.ProductTypeID.Equals("101") || a.ProductTypeID.Equals("102")),
                        blueQuant = list.Where(a => a.ProductTypeID.Equals("103")),
                        livePortfolio = list.Where(a => a.ProductTypeID.Equals("104")),
                        Last10CompletedTrades = list.Where(a => a.ExitDate.HasValue && a.ProductTypeID.Equals("105")).OrderBy(n => n.ExitDate).Take(10),
                        categories
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            }
            catch (WebException ex) //if server is off it will throw exeception and here we need notify user
            {
                throw ex;
            }

            return json;

        }

        public async Task<JsonResult> GetMarketSetDataByType(string typeId)
        {
            JsonResult json = null;
            try
            {
                var list = await _marketBal.GetMarketData(typeId);
                json = new JsonResult
                {
                    Data = list,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (WebException ex) //if server is off it will throw exeception and here we need notify user
            {
                throw ex;
            }

            return json;

        }

        public async Task<JsonResult> SaveMarketData(MarketDataDto vm)
        {
            var result = await _marketBal.SaveMarketData(vm);
            if (result >= 0)
            {
                var limitedData = await GetMarketSetDataByType(vm.ProductTypeID);

                var jsonResult = await GetAllMarketSetupData();
                jsonResult.Data = new { allData = jsonResult.Data, limitedData = limitedData.Data };
                jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            return Json(new List<MarketDataDto>(), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeleteMarketData(long id)
        {
            var result = await _marketBal.DeleteMarketData(id);
            if (result >= 0)
            {
                var jsonResult = await GetMarketSetDataByType(Convert.ToString(result));
                jsonResult.Data = new { list = jsonResult.Data, productTypeId = result };
                jsonResult.MaxJsonLength = int.MaxValue;
                jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return jsonResult;
            }
            return Json(new List<MarketDataDto>(), JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> GetAllMarketSetupData()
        {
            JsonResult json = null;
            try
            {
                var list = await _marketBal.GetMarketData();
                var categories = await _marketBal.GetActiveMarketCategories();
                json = new JsonResult
                {
                    Data = new
                    {
                        list = list,
                        categories = categories,
                        livePortfolio1 = list.Where(a => a.ProductTypeID.Equals("104")),
                        lastCompletedTradesMain = list.Where(a => a.ExitDate.HasValue && a.ProductTypeID.Equals("105")).OrderBy(n => n.ExitDate).Take(10),
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (WebException ex) //if server is off it will throw exeception and here we need notify user
            {
                throw ex;
            }

            return json;

        }
        #endregion

        [LogonAuthorize]
        public ActionResult Admin()
        {
            Session["ActiveCssClass"] = "Admin";
            return View();
        }
        [LogonAuthorize]
        public ActionResult Data()
        {
            var codeInSession = Convert.ToString(Session["DefaultKey"]);
            ViewBag.Code = codeInSession;
            Session["ActiveCssClass"] = "Data";
            return View();
        }


        [LogonAuthorize]
        public ActionResult Research()
        {
            var codeInSession = Convert.ToString(Session["DefaultKey"]);
            ViewBag.Code = codeInSession;
            Session["ActiveCssClass"] = "Research";
            return View();
        }

        [LogonAuthorize]
        public ActionResult Detail()
        {
            var codeInSession = Convert.ToString(Session["DefaultKey"]);
            ViewBag.Code = codeInSession;
            return View();
        }

        public async Task<ActionResult> FinanceInfoView(string symbol)
        {
            if (!string.IsNullOrEmpty(symbol))
                symbol = symbol.Replace("INDEX:", "").Replace("NASDAQ:", "");

            var result = string.Empty;
            using (WebClient client = new WebClient())
            {
                string s = await client.DownloadStringTaskAsync(new Uri("http://finance.google.com/finance/info?client=ig&q=" + symbol));
                result = s.Replace("//", "");
            }
            return View(new FinanceInfo { Result = result });
        }

        public async Task<JsonResult> ReloadFinanceInfo(string symbol)
        {
            if (!string.IsNullOrEmpty(symbol))
                symbol = symbol.Replace("INDEX:", "").Replace("NASDAQ:", "");

            var result = string.Empty;
            using (WebClient client = new WebClient())
            {
                string s = await client.DownloadStringTaskAsync(new Uri("http://finance.google.com/finance/info?client=ig&q=" + symbol));
                result = s.Replace("//", "");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetMarketChartDataOnLoad(string startDate, string symbol)
        {
            //For Daily
           
            string lTermYear = Convert.ToString(Session["LongTermChart"]);
            string nTermYear = Convert.ToString(Session["NearTemChart"]);
            if (lTermYear == "" || nTermYear=="")
            {
                lTermYear= BluSignalComman.DateTime1YearBack;
                nTermYear= BluSignalComman.DateTime5YearsBack;
            }

                string logTerm = DateTime.Now.AddYears(-Convert.ToInt32(lTermYear)).ToString("yyyyMMdd000000");
                 string nTerm = DateTime.Now.AddYears(-Convert.ToInt32(nTermYear)).ToString("yyyyMMdd000000");

            //startDate = BluSignalComman.DateTime1YearBack;
            startDate = nTerm;

            var endDate = BluSignalComman.EndDate;

            Session["DefaultKey"] = symbol;

            var chartResult = await GetLoggingData(symbol, startDate, endDate, "daily");
            var pD = chartResult.results.OrderByDescending(a => a.tradingDay).Take(2).ToList();
            var avgVolume = string.Empty;
            var closingPrice = string.Empty;
            var marketDataDaily = string.Empty;
            var previousprice = pD[1].close;
            decimal prePricePercent = 0;
            string finalPercent = string.Empty;



            if (chartResult.results != null && chartResult.results.Any())
            {
                //marketDataDaily = JsonConvert.SerializeObject(chartResult.results, new JsonSerializerSettings { ContractResolver = new DynamicContractResolver("timestamp,tradingDay") });
                //marketDataDaily = marketDataDaily.Replace("//", "");

                marketDataDaily = chartResult.serializedResult;

                int lastDays = 90;

                var averageVolumn = chartResult.results.OrderByDescending(a => a.tradingDay).Take(lastDays).Sum(x => Convert.ToSingle(x.volume));
                if (averageVolumn > 0)
                    avgVolume = Convert.ToString(Math.Round((averageVolumn / lastDays), 0));

                var maxRexord = chartResult.results.OrderByDescending(a => a.timestamp).FirstOrDefault();
                var timeStampValue=   maxRexord.timestamp.Date;
                var currentDate = DateTime.Now.Date;
                var preVDayData = currentDate.AddDays(-1);
                if(timeStampValue == currentDate)
                {
                    maxRexord = chartResult.results.Where(x => x.timestamp == preVDayData).FirstOrDefault();

                }
                else
                {
                    maxRexord = chartResult.results.OrderByDescending(a => a.timestamp).FirstOrDefault();
                }
                if (maxRexord != null)
                    closingPrice = maxRexord.close;

                var PercentValue = ((Convert.ToDecimal(closingPrice) - Convert.ToDecimal(previousprice)) / Convert.ToDecimal(closingPrice));
                prePricePercent = PercentValue * 100;
                //finalPercent = Convert.ToString(Math.Round(prePricePercent, 3) + "%");
                finalPercent = Convert.ToString(Math.Round(prePricePercent, 3));


            }
            var dataTakeValue = 200;

            if (chartResult.results != null && chartResult.results.Any())
            {
                var dailyData = chartResult.results.Select(item => new object[]
                {
                       item.tradingDay.ToString(),
                      //(item.tradingDay- new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds,
                       Math.Round(Convert.ToDecimal((item.open)),2),
                       Math.Round(Convert.ToDecimal((item.high)),2),
                       Math.Round(Convert.ToDecimal((item.low)),2),
                       Math.Round(Convert.ToDecimal((item.close)),2),
                       //Convert.ToDouble( item.volume)
            });

                //For Weekly
                //startDate = BluSignalComman.DateTime5YearsBack;
                startDate = logTerm;

                chartResult = await GetLoggingData(symbol, startDate, endDate, "weekly");

                var weeklyData = chartResult.results.Select(item => new object[]
                {
                       item.tradingDay.ToString(),
                       //(item.tradingDay- new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds,
                       Math.Round(Convert.ToDecimal((item.open)),2),
                       Math.Round(Convert.ToDecimal((item.high)),2),
                       Math.Round(Convert.ToDecimal((item.low)),2),
                       Math.Round(Convert.ToDecimal((item.close)),2),
                       // Convert.ToDouble( item.volume)
                });

                var jsonToReturn = new
                {
                    dailyData,
                    weeklyData,
                    marketDataDaily,
                    avgVolume,
                    closingPrice,
                    finalPercent
                };
                var jsonResult = new JsonResult { Data = jsonToReturn, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = int.MaxValue };
                return jsonResult;
            }


            return Json("Fail");
        }

        private async Task<Data> GetLoggingData(string symbol, string startDate, string endDate = "", string intervalType = "", string order = "")
        {
            var obj = new Data();
            try
            {

                #region commented
                //var pData = new LoggingData { param1 = string.IsNullOrEmpty(symbol) ? "SPY" : symbol };

                //if (!string.IsNullOrEmpty(startDate))
                //    pData.param4 = startDate;

                //if (!string.IsNullOrEmpty(endDate))
                //    pData.param5 = endDate;

                //if (!string.IsNullOrEmpty(intervalType))
                //    pData.param6 = intervalType;

                //var client = new HttpClient();
                //var url = $"{ApiBaseUrl}{ApiActions.chartData}";
                //var data = (await client.PostAsync(url, pData, new JsonMediaTypeFormatter())).Content.ReadAsAsync<IEnumerable<LoggingData>>().Result;
                //if (data != null && data.Any())
                //{
                //    obj.status = new status { code = "200", message = "success" };
                //    var list = new List<resultsData>();
                //    foreach (var item in data)
                //    {
                //        var dateValue = DateTime.Parse(item.param10);
                //        list.Add(new resultsData
                //        {
                //            symbol = item.param1,
                //            timestamp1 = item.param10,
                //            tradingDay1 = item.param10,
                //            timestamp = dateValue,
                //            tradingDay = dateValue.Date,
                //            open = item.param4,
                //            high = item.param5,
                //            low = item.param6,
                //            close = item.param7,
                //            volume = item.param8,
                //            openInterest = item.param9
                //        });
                //    }

                //    obj.results = list.OrderByDescending(a => a.tradingDay).ToList();
                //} 
                #endregion

                order = !string.IsNullOrEmpty(order) ? $"&order={order}" : "&order=asc";

                var url = $"{ApiBaseUrl}&symbol={symbol}&type={intervalType}&startDate={startDate}{order}";
                var loggingData = new LoggingData
                {
                    param1 = symbol,
                    param4 = startDate,
                    param5 = intervalType,
                    param6 = order
                };
                var client = new HttpClient();

                var result = (await client.PostAsJsonAsync(ApiBaseUrl, loggingData)).Content.ReadAsStringAsync().Result;
                
#pragma warning disable CS0618 // 'JsonConvert.DeserializeObjectAsync<T>(string)' is obsolete: 'DeserializeObjectAsync is obsolete. Use the Task.Factory.StartNew method to deserialize JSON asynchronously: Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(value))'
                var data = await JsonConvert.DeserializeObjectAsync<Data>(result);
#pragma warning restore CS0618 // 'JsonConvert.DeserializeObjectAsync<T>(string)' is obsolete: 'DeserializeObjectAsync is obsolete. Use the Task.Factory.StartNew method to deserialize JSON asynchronously: Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(value))'
                if (data.results != null && data.results.Any())
                {
                    data.results = data.results.OrderByDescending(a => a.tradingDay);
                    //data.serializedResult = result.Replace("{\"status\":{\"code\":200,\"message\":\"Success.\"},\"results\":", "").Replace("//", "").Trim();
                    data.serializedResult = result;

                    //data.serializedResult = data.serializedResult.Remove(data.serializedResult.Length - 1);
                }
                else
                {
                    data.results = new List<resultsData>();
                    data.serializedResult = string.Empty;
                }

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
#pragma warning disable CS0162 // Unreachable code detected
            return obj;
#pragma warning restore CS0162 // Unreachable code detected
        }

        public ActionResult DataList(string symbol)
        {
            ViewBag.Symbol = symbol;
            return View();
        }


        public async Task<JsonResult> GetDailyMarketData(string symbol)
        {
            //For Daily
            var startDate = BluSignalComman.DateTime9MonthBack;
            var endDate = BluSignalComman.EndDate;
            var chartResult = await GetLoggingData(symbol, startDate, endDate, "daily", order: "desc");


            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            var chartList = new JavaScriptSerializer().Deserialize<RootObject>(chartResult.serializedResult);
            var listcount = chartList.results.Count();
            decimal yesterdayClosingPrice = 0.0M;
            int _lpIndex  = 1;
            foreach (var item in chartList.results)
            {
                if(listcount==_lpIndex)
                {
                    yesterdayClosingPrice = 0.0M;
                }
                else
                {
                    yesterdayClosingPrice = Convert.ToDecimal(chartList.results[_lpIndex].close);

                }
               
                    //item.change = ((Convert.ToDecimal(item.close) - yesterdayClosingPrice) / Convert.ToDecimal(item.close));
                    item.change = ((Convert.ToDecimal(item.close) - yesterdayClosingPrice) / Convert.ToDecimal(item.close));
                    item.change= item.change * 100;
                    item.change = Math.Round(Convert.ToDecimal((item.change)), 3);
                     item.CustomChangeValue = Convert.ToString(item.change) + "%";
                     _lpIndex++;

                if (item.change > 0)
                {
                    item.cssClass = "green_text";
                }
                else
                {
                    item.cssClass = "red_text";
                }
            }

            #region Commented Code
            //var marketDataDaily = string.Empty;

            //if (chartResult.results != null && chartResult.results.Any())
            //{
            //    marketDataDaily = JsonConvert.SerializeObject(chartResult.results, new JsonSerializerSettings
            //    {
            //        //ContractResolver = new DynamicContractResolver("timestamp,tradingDay")
            //        ContractResolver = new DynamicContractResolver("")
            //    });
            //    //marketDataDaily = marketDataDaily.Replace("timestamp1", "timestamp").Replace("tradingDay1", "tradingDay").Replace("//", "");
            //    marketDataDaily = marketDataDaily.Replace("//", "");
            //} 
            #endregion
            //var jsonResult = new JsonResult { Data = chartResult.serializedResult, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = int.MaxValue };
            //var jsonResult = new JsonResult { chartList, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = int.MaxValue };
        return    Json(chartList, JsonRequestBehavior.AllowGet);

            //return jsonResult;
        }
        public class Status
        {
            public string code { get; set; }
            public string message { get; set; }
        }

        public class Result
        {
            public string symbol { get; set; }
            public string timestamp { get; set; }
            public string tradingDay { get; set; }
            public string open { get; set; }
            public string high { get; set; }
            public string low { get; set; }
            public string close { get; set; }
            public string volume { get; set; }
            public object openInterest { get; set; }
            public decimal change { get; set; }
            public string cssClass { get; set; }
            public string CustomChangeValue { get; set; }

        }

        public class RootObject
        {
            public Status status { get; set; }
            public List<Result> results { get; set; }
            public object serializedResult { get; set; }
        }

        public class LoggingData
        {
            public string param1 { get; set; }
            public DateTime? param2 { get; set; }
            public DateTime? param3 { get; set; }
            public string param4 { get; set; }
            public string param5 { get; set; }
            public string param6 { get; set; }
            public string param7 { get; set; }
            public string param8 { get; set; }
            public string param9 { get; set; }
            public string param10 { get; set; }
        }



        //public class results
        //{
        //    public string symbol { get; set; }
        //    public string timestamp { get; set; }
        //    public string tradingDay { get; set; }
        //    public string open { get; set; }
        //    public string high { get; set; }
        //    public string low { get; set; }
        //    public string close { get; set; }
        //    public string volume { get; set; }
        //    public string openInterest { get; set; }
        //}



        public ActionResult UpateGlobalCodeValue(string nearTem, string longTerm)
        {
            string gcId = Convert.ToString(Session["GlobalCodeId"]);
           var model= _gcBal.GetGlobalCodes(Convert.ToInt64(gcId)).FirstOrDefault();
            model.ExternalValue1 = nearTem;
            model.ExternalValue2 = longTerm;
            var result=  _gcBal.UpdateGlobalCodes(model);
            if(result> 0)
            {
                Session["LongTermChart"] = longTerm;
                Session["NearTemChart"] = nearTem;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}