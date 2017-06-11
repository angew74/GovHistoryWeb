using GovHistoryRepository.Identity;
using GovHistoryRepository.Rbac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GovHistoryWeb.Models
{
    public class PermissionsViewModel
    {
        internal string description;

        public string success { get; set; }
        public string message { get; set; }
        public List<PERMISSION> ListDiritti { get; set; }

        public List<ApplicationRole> ListGruppi { get; set; }

        public string Totale { get; set; }
        public string page { get; set; }
        public string limit { get; set; }
        public string IdRole { get; set; }
        public string IdPermission { get; set; }
    }
}