namespace MusicWebsite.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MusicWebsite.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MusicWebsite.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MusicWebsite.Models.ApplicationDbContext context)
        {
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                rolemanager.Create(new IdentityRole { Name = "Admin" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!context.Users.Any(u => u.Email == "sharma.sahilsharma207@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    
                    Email = "sharma.sahilsharma207@gmail.com",
                    UserName = "sharma.sahilsharma207@gmail.com",
                    
                    PhoneNumber = "336-303-2992",
                }, "Abc123!");
            }

            var user = userManager.FindByEmail("sharma.sahilsharma207@gmail.com");
            userManager.AddToRole(user.Id, "Admin");


        }
    }
}
