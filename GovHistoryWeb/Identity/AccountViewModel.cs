using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GovHistoryRepository.Identity
{

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string success { get; set; }
        public List<string> message { get; set; }

        public string ResponseUrl { get; set; }
    }

    public class RegisterViewModel
    {
        internal string returnUrl;

        [Required]
        [StringLength(15, ErrorMessage = "Il campo {0} deve essere almeno di {2} caratteri.", MinimumLength = 8)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Il campo {0} deve essere almeno di {2} caratteri.", MinimumLength = 2)]
        [Display(Name = "First name")]
        public string Firstname { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Il campo {0} deve essere almeno di {2} caratteri.", MinimumLength = 2)]
        [Display(Name = "Last name")]
        public string Lastname { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Il campo {0} deve essere almeno di {2} caratteri.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "La password e la conferma password non coincide.")]
        public string ConfirmPassword { get; set; }

        [StringLength(30, ErrorMessage = "Il campo {0} deve essere almeno di {2} caratteri.", MinimumLength = 11)]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        public string success { get; set; }
        public string message { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Il campo {0} deve essere almeno di {2} caratteri.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "La password e la conferma password non coincide.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }


    //********** RBAC View Models **************
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} deve essere di almeno {2} caratteri.", MinimumLength = 4)]
        [Display(Name = "Nome Ruolo")]
        public string Name { get; set; }


        [Required]
        [StringLength(30, ErrorMessage = "The {0} deve essere di almeno {2} caratteri.", MinimumLength = 2)]
        [Display(Name = "Descrizione Ruolo")]
        public string RoleDescription { get; set; }

        [Required]
        [Display(Name = "Amministratore di Sistema")]
        public bool IsSysAdmin { get; set; }

        public string success { get; set; }
        public string message { get; set; }
    }

    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Il campo {0} deve essere almeno di {2} caratteri.", MinimumLength = 8)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Il campo {0} deve essere almeno di {2} caratteri.", MinimumLength = 2)]
        [Display(Name = "First name")]
        public string Firstname { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Il campo {0} deve essere almeno di {2} caratteri.", MinimumLength = 2)]
        [Display(Name = "Last name")]
        public string Lastname { get; set; }


    }
}
