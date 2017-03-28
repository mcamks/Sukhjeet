using System.Web.Mvc;

namespace BlueSignal.Controllers
{
    public class HelpController : BaseController
    {
        // GET: Help
        public ActionResult Index()
        {
            return View();
        }
    }
}