using GovHistoryRepository.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovHistoryRepository.Rbac
{
    [Table("PERMISSIONS")]
    public class PERMISSION
    {
        [Key]
        public int PermissionId { get; set; }

        [Required]
        [StringLength(50)]
        public string PermissionDescription { get; set; }

        public virtual List<ApplicationRole> ROLES { get; set; }
    }
}
