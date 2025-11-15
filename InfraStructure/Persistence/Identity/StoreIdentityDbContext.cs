using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer.Identity
{
    public class StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options):IdentityDbContext<ApplicationUser>(options)
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // 7 class provides form Identity 

            //Now we need 3 tables :
            // Users
            //Roles
            //UserRoles 
            builder.Entity<ApplicationUser>().ToTable("Users");// change name from Asp.netUsers To Users
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityRole>().ToTable("Roles");

            // We don't need  another 4 talbes 
            builder.Ignore<IdentityUserClaim<string>>();
            builder.Ignore<IdentityUserToken<string>>();
            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityRoleClaim<string>>();
           
        }

    }
}
