using IdentityModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SchoolManagementSystem2.Models;

[assembly: OwinStartupAttribute(typeof(SchoolManagementSystem2.Startup))]
namespace SchoolManagementSystem2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateEditRoles();
        }
        public void CreateEditRoles()
        {
            var context = new ApplicationDbContext();

            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!rolemanager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                rolemanager.Create(role);

                //var user = new ApplicationUser
                //{
                //    UserName = "Admin",
                //    Email = "Admin@schmng.com"
                //};
                //var password = "Password";

                //var usr = usermanager.Create(user, password);

                //if (usr.Succeeded)
                //{
                //    var result = usermanager.AddToRole(user.Id, "Admin");
                //}
            }

            if (!rolemanager.RoleExists("Teacher"))
            {
                var role = new IdentityRole();
                role.Name = "Teacher";
                rolemanager.Create(role);
            }

            if (!rolemanager.RoleExists("Supervisor"))
            {
                var role = new IdentityRole();
                role.Name = "Supervisor";
                rolemanager.Create(role);
            }
        }
    }
}
