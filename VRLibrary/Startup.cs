using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using VRLibrary.Models;

[assembly: OwinStartupAttribute(typeof(VRLibrary.Startup))]
namespace VRLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        // In this method we will create default User roles and Admin user for login   
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                //var user = await UserManager.FindByNameAsync("fertakis@gmail.com");
                user.UserName = "fertakis@gmail.com";
                user.Name = "Konstantinos";
                user.Surname = "Fertakis";
                user.Email = "fertakis@gmail.com";
                user.LibID = 3;
                user.EmailConfirmed = true;

                string userPWD = "Accenture5!";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Librarian role    
            if (!roleManager.RoleExists("Librarian"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Librarian";
                roleManager.Create(role);

            }

            // creating Creating Student role    
            if (!roleManager.RoleExists("Student"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Student";
                roleManager.Create(role);

            }
            // creating Creating PendingUser role    
            if (!roleManager.RoleExists("PendingUser"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "PendingUser";
                roleManager.Create(role);

            }
        }
    }
}
