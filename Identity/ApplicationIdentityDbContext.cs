using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
// using login.Entities;
namespace login.Identity
{
       public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {
        }

        // Add your DbSet for custom application entities here

        

    }
    public class ApplicationUser : IdentityUser<int>
    {
        // internal string Addres2;
        public string Addres2 { get; internal set; }
        public string StName { get; internal set; }
        
    }

    public class ApplicationRole : IdentityRole<int>
    {
    }
}