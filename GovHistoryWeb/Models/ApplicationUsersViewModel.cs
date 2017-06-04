using GovHistoryRepository.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GovHistoryWeb.Models
{
    public class ApplicationUsersViewModel
    {
        public string success { get; set; }
        public List<string> message { get; set; }

        public string order { get; set; }
        public string Totale { get; set; }
        public string limit { get; set; }
        public string page { get; set; }

        public List<ApplicationUser> ListUtenti { get; set; }
    }
}