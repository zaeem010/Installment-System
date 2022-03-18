using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AR_IS.Models
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
        //Registrations
        public DbSet<Brand> tbl_Brand{ get; set; }
        public DbSet<Company> tbl_Company { get; set; }
        public DbSet<Category> tbl_Category { get; set; }
        public DbSet<Product> tbl_Product { get; set; }
        public DbSet<Town> tbl_Town { get; set; }
        public DbSet<Province> tbl_Province { get; set; }
        public DbSet<Customer> tbl_Customer { get; set; }
        public DbSet<Supplier> tbl_Supplier { get; set; }
        public DbSet<MeasuringUnit> tbl_MeasuringUnit { get; set; }
        public DbSet<References> tbl_References { get; set; }
        public DbSet<Cargo> tbl_Cargo { get; set; }
        public DbSet<InstallmentPlan> tbl_InstallmentPlan { get; set; }
        //Purchase 
        public DbSet<PurDetail> tbl_PurDetail { get; set; }
        public DbSet<PurMaster> tbl_PurMaster { get; set; }
        public DbSet<PurDetailVehicle> tbl_PurDetailVehicle { get; set; }
        public DbSet<PurMasterVehicle> tbl_PurMasterVehicle { get; set; }
        public DbSet<PurDetailReturn> tbl_PurDetailReturn { get; set; }
        public DbSet<PurMasterReturn> tbl_PurMasterReturn { get; set; }
        //Sale
        public DbSet<SaleDetail> tbl_SaleDetail { get; set; }
        public DbSet<SaleMaster> tbl_SaleMaster { get; set; }
        public DbSet<SWI> tbl_SWI { get; set; }
        public DbSet<SaleVehicleInstallment> tbl_SaleVehicleInstallment { get; set; }
        public DbSet<SaleDetailReturn> tbl_SaleDetailReturn { get; set; }
        public DbSet<SaleMasterReturn> tbl_SaleMasterReturn { get; set; }

       //Accounts
        public DbSet<Firstlevel> tbl_Firstlevel { get; set; }
        public DbSet<Secondlevel> tbl_Secondlevel { get; set; }
        public DbSet<ThirdLevel> tbl_ThirdLevel { get; set; }
        public DbSet<AccountHead> tbl_Ac_Head { get; set; }
        public DbSet<TranscationDetail> tbl_TranscationDetail { get; set; }
        //Setting 
        public DbSet<Setting> tbl_Setting { get; set; }
        //Chat
        public DbSet<Chat> tbl_Chat { get; set; }
        //User
        public DbSet<GeneralUser> tbl_GeneralUser { get; set; }
        public DbSet<Userlogin> tbl_Userlogin { get; set; }
        public DbSet<UserAccess> tbl_UserAccess { get; set; }
        public DbSet<UserAccessCopy> tbl_UserAccessCopy { get; set; }
        public DbSet<SideFirst> tbl_SideFirst { get; set; }
        public DbSet<SideSecond> tbl_SideSecond { get; set; }
        public DbSet<SideThird> tbl_SideThird { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}