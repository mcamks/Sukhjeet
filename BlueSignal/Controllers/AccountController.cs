using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BlueSignal.Models;
//using Comman.DBAccess;
using BlueSignalCore.Models;
using System.Web.Script.Serialization;
using BlueSignalCore.Bal;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;

namespace BlueSignal.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly MarketBal _marketBal;
        public AccountController(MarketBal marketBal)
        {
            _marketBal = marketBal;
        }

        //public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var IsUserExist = await _marketBal.IsUserExists(model.Email.ToLower(), model.Password);

            //BluSignalsEntities db = new BluSignalsEntities();
            //var IsUserExist = db.Users.FirstOrDefault(x => x.Email.ToLower() == model.Email.ToLower() && x.PasswordHash == model.Password);

            if (IsUserExist != null)
            {
                var userName = IsUserExist.Email;
                var Password = "Test"; //Option
                var user = _marketBal.GetWpUser(userName, Password);
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
                    user.IsAlreadyRegisteredWithBSPortal = true;
                }

                Session.Remove("firstTimeCheck");
                if (user != null)
                {
                    user.IsAlreadyRegisteredWithBSPortal = true;
                    SystemLogin(user);
                    await CheckUserBundle(user);
                    var loggedInUser = Session["SystemUser"];
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    SystemLogout();
                    //Redirect To Unknown payment page
                }
                return await Task.FromResult(View());
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            return View(model);
        }

        private void SystemLogin(WP_User user)
        {
            if (Session["SystemUser"] != null)
            {
                Session.Remove("SystemUser");
            }
            Session.Add("SystemUser", user);
        }

        private void SystemLogout()
        {
            Session.Remove("SystemUser");
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
            var requestContent = new FormUrlEncodedContent(new[] {
            new KeyValuePair<string, string>("apikey", "9i6t91dkbr"),
            new KeyValuePair<string, string>("apisecret","4tqpbbph1r"),
            new KeyValuePair<string, string>("email",user.user_email)//"brown@gmail.com")
        });
            string methodName = "getMember";

            HttpResponseMessage response = await client.PostAsync("https://www.blusignalsystems.com/wp-content/plugins/membermouse/api/request.php?q=/" + methodName, requestContent);

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                var jsonString = (await reader.ReadToEndAsync()) + Environment.NewLine;

                //                jsonString = @"{'response_code':'200','response_message':'','response_data':{'member_id':44,'first_name':'Dee','last_name':'Menzies','is_complimentary':'false','registered':'2017 - 02 - 17 19:24:36','cancellation_date':'','last_logged_in':'2017 - 02 - 18 21:55:45','last_updated':'2017 - 02 - 18 21:55:45','days_as_member':3,'status':'1','status_name':'Active','membership_level':'2','membership_level_name':'Paid Membership','username':'menzies.dee @gmail.com','email':'menzies.dee @gmail.com','password':null,'phone':'1112223333','billing_address':'123 Maple','billing_city':'Houston','billing_state':'TX','billing_zip':'77002','billing_country':'United States','shipping_address':'123 Maple','shipping_city':'Houston','shipping_state':'TX','shipping_zip':'77002',
                //'shipping_country':'United States','bundles':[{'id':7,'name':'Single System - BluFractal (monthly)'},{'id':3,'name':'Single System - BluNeural (monthly)'}],'custom_fields':[{'id':2,'name':'Terms of Serv','value':''},{'id':3,'name':'Terms of Service','value':'mm_cb_on'}]}}";

                var objResponse = jsonString;
                Rootobject facebookFriends = new JavaScriptSerializer().Deserialize<Rootobject>(objResponse);

                if (facebookFriends != null && facebookFriends.response_code == "200" && facebookFriends.response_data != null && facebookFriends.response_data.bundles != null && (facebookFriends.response_data.bundles.Count > 0 || !string.IsNullOrEmpty(facebookFriends.response_data.membership_level_name)))
                {
                    if (facebookFriends.response_data.bundles.Count == 0 && !string.IsNullOrEmpty(facebookFriends.response_data.membership_level_name))
                    {
                        var bundles = new List<bundles>();
                        var nm = facebookFriends.response_data.membership_level_name.ToLower().Trim();

                        if (nm.Contains("blufractal"))
                            bundles.Add(new bundles { id = 1, name = "blufractal", value = "blufractal" });
                        if (nm.Contains("bluneural"))
                            bundles.Add(new bundles { id = 2, name = "bluneural", value = "bluneural" });
                        if (nm.Contains("bluquant"))
                            bundles.Add(new bundles { id = 3, name = "bluquant", value = "bluquant" });

                        user.bundles = bundles;
                    }
                    else
                        user.bundles = facebookFriends.response_data.bundles;
                }
            }
        }


        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        public ActionResult LogOff()
        {
            Session.RemoveAll();
            Session.Abandon();

            // Invalidate the Cache on the Client Side
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();


            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}