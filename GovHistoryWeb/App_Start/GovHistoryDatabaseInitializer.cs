using GovHistoryRepository.Identity;
using GovHistoryRepository.Rbac;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovHistoryRepository
{
    public class GovHistoryDatabaseInitializer : CreateDatabaseIfNotExists<GovHistoryDbContext>
    {
        private readonly string c_SysAdmin = "System Administrator";
        private readonly string c_DefaultUser = "Utente Standard";

        protected override void Seed(GovHistoryDbContext context)
        {
            //Create Default Roles...
            IList<ApplicationRole> defaultRoles = new List<ApplicationRole>();
            defaultRoles.Add(new ApplicationRole { Name = c_SysAdmin, RoleDescription = "Amministratore di sistema di Utenti/Ruoli/Permessi", LastModified = DateTime.Now, IsSysAdmin = true });
            defaultRoles.Add(new ApplicationRole { Name = c_DefaultUser, RoleDescription = "Ruolo di default con permessi limitati", LastModified = DateTime.Now, IsSysAdmin = false });

            ApplicationRoleManager RoleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));
            foreach (ApplicationRole role in defaultRoles)
            {
                RoleManager.Create(role);
            }

            //Create User...
            var user = new ApplicationUser { UserName = "Administrator", Email = "agnew74@gmail.com", Firstname = "System", Lastname = "Administrator", LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };

            ApplicationUserManager UserManager = new ApplicationUserManager(new ApplicationUserStore(context));
            var result = UserManager.Create(user, "Roberta4@");

            if (result.Succeeded)
            {
                //Add User to Admin Role...
                UserManager.AddToRole(user.Id, c_SysAdmin);
            }


            //Create Default User...
            user = new ApplicationUser { UserName = "Utente", Email = "agnew74@gmail.com", Firstname = "Default", Lastname = "User", LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };
            result = UserManager.Create(user, "Roberta4@");

            if (result.Succeeded)
            {
                //Add User to Admin Role...
                UserManager.AddToRole(user.Id, c_DefaultUser);
            }

            //Create User with NO Roles...
            user = new ApplicationUser { UserName = "Ospite", Email = "agnew74@gmail.com", Firstname = "Guest", Lastname = "User", LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };
            result = UserManager.Create(user, "Roberta4@");
            if (result.Succeeded)
            {
                //Add User to Admin Role...
                UserManager.AddToRole(user.Id, c_DefaultUser);
            }

            base.Seed(context);

            //Create a permission...
            PERMISSION _permission = new PERMISSION();
            _permission.PermissionDescription = "Home-Reports";
            ApplicationRoleManager.AddPermission(_permission);

            //Add Permission to DefaultUser Role...
            ApplicationRoleManager.AddPermission2Role(context.Roles.Where(p => p.Name == c_DefaultUser).First().Id, context.PERMISSIONS.First().PermissionId);
        }
    }
}
