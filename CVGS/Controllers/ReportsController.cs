using CVGS.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CVGS.Controllers
{
    public class ReportsController : Controller
    {
        public static List<ReportTypeViewModel> REPORT_TYPES = new List<ReportTypeViewModel>
            {
                new ReportTypeViewModel() { ReportType = "Game List", ReportURL = "Games", Enabled = true },
                new ReportTypeViewModel() { ReportType = "Game Details", ReportURL = "GameDetails", Enabled = false },
                new ReportTypeViewModel() { ReportType = "Member List",  ReportURL = "Members", Enabled = false },
                new ReportTypeViewModel() { ReportType = "Member Details", ReportURL = "MemberDetails", Enabled = false },
                new ReportTypeViewModel() { ReportType = "Wishlist", ReportURL = "Wishlist", Enabled = false },
                new ReportTypeViewModel() { ReportType = "Sales", ReportURL = "Sales", Enabled = false }
            };

        // GET: Report List
        public ActionResult Index()
        {
            if (Session["MemberId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View(REPORT_TYPES);
        }

        public ActionResult Games()
        {
            if (Session["MemberId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            TempData.Add("error", "Games Report is not yet created");
            return RedirectToAction("Index");
        }
    }
}