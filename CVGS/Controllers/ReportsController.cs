using CVGS.Models;
using CVGS.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace CVGS.Controllers
{
    public class ReportsController : Controller
    {
        public static List<ReportTypeViewModel> REPORT_TYPES = new List<ReportTypeViewModel>
        {
            new ReportTypeViewModel() { ReportType = "Game List", ReportURL = "Games", Enabled = true },
            new ReportTypeViewModel() { ReportType = "Game Details", ReportURL = "GameDetails", Enabled = false },
            new ReportTypeViewModel() { ReportType = "Member List",  ReportURL = "Members", Enabled = true },
            new ReportTypeViewModel() { ReportType = "Member Details", ReportURL = "MemberDetails", Enabled = false },
            new ReportTypeViewModel() { ReportType = "Wishlist", ReportURL = "Wishlist", Enabled = false },
            new ReportTypeViewModel() { ReportType = "Sales", ReportURL = "Sales", Enabled = false }
        };
        private CVGSEntities db = new CVGSEntities();

        // GET: Report List
        public ActionResult Index()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login"); ;
            }

            // Only admins and employees can generate reports
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to generate Reports");
            }

            return View(REPORT_TYPES);
        }

        public ActionResult Games()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // Only admins and employees can generate reports
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to generate Reports");
            }

            // Games can be filtered and ordered
            var gamesList = db.GAMEs.ToList();

            return View(gamesList);

            TempData.Add("error", "Games Report is not yet created");
            return RedirectToAction("Index");
        }

        public ActionResult Members()
        {
            // Redirect unauthenticated members
            var memberId = this.Session["MemberId"];
            if (memberId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // Only admins and employees can generate reports
            string memberRole = this.Session["MemberRole"].ToString();
            if (memberRole != "Admin" && memberRole != "Employee")
            {
                return new HttpUnauthorizedResult("You are not authorized to generate Reports");
            }

            var memberList = db.MEMBERs.ToList();
            return View(memberList);

            TempData.Add("error", "Members Report is not yet created");
            return RedirectToAction("Index");

        }
    }
}