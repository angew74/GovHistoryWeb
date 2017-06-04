using GovHistoryRepository.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GovHistoryWeb.Models
{
    public class ApplicationRolesViewModel
    {
        public string success { get; set; }
        public string message { get; set; }
        public List<ApplicationRole> ListRuoli { get; set; }

        public string Totale { get; set; }
        public string page { get; set; }
        public string limit { get; set; }
        public string IdUser { get; set; }
    }
}