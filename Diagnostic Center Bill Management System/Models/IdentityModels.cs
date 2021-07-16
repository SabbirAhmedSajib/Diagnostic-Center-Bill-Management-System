using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Diagnostic_Center_Bill_Management_System.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //   // modelBuilder.Entity<TestSetup>().// specify the name
        //        modelBuilder.Entity<TestSetup>().HasIndex(u => u.).IsUnique();
        //}

        public System.Data.Entity.DbSet<Diagnostic_Center_Bill_Management_System.Models.TestType> TestTypes { get; set; }
        public System.Data.Entity.DbSet<Diagnostic_Center_Bill_Management_System.Models.TestSetup> TestSetup { get; set; }

        public System.Data.Entity.DbSet<Diagnostic_Center_Bill_Management_System.Models.RequestMaster> RequestMasters { get; set; }
        public System.Data.Entity.DbSet<Diagnostic_Center_Bill_Management_System.Models.RequestDetail> RequestDetails { get; set; }

       
    }

    
}