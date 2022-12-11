using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHDCShop.Web.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    public class ChatController : Controller
    {
        // GET: Admin/Chat
        public ActionResult Index()
        {
            return View();
        }
    }
}