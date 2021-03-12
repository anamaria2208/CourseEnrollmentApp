using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Seminarski_rad_Olujic_AnaMaria.Models;

[assembly: OwinStartupAttribute(typeof(Seminarski_rad_Olujic_AnaMaria.Startup))]
namespace Seminarski_rad_Olujic_AnaMaria
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRoleAndUser();
        }

        private void createRoleAndUser()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            if (!roleManager.RoleExists("Admin"))
            {

                //kreiranje "Admin" role  
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //kreiranje admin usera                 
                var user = new ApplicationUser();
                user.UserName = "administrator@gmail.com";
                user.Email = "administrator@gmail.com";

                string userPass = "admin$007$";

                var createUser = userManager.Create(user, userPass);

                //dodaj usera u admin tolu    
                if (createUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
