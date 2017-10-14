using System.ComponentModel;

namespace CVGS.ViewModels
{
    public class ReportTypeViewModel
    {
        [DisplayName("Report Type")]
        public string ReportType { get; set; }

        [DisplayName("Enabled")]
        public bool Enabled { get; set; }

        public string ReportURL { get; set; }
    }
}