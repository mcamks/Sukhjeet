using System.Web.Mvc;

namespace BlueSignal.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
    }
}