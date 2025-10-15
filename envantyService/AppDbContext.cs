namespace envantyService
{
    using envantyService.Entityframeworks;
    using envantyefe.Entityframeworks;
    using envantyService.Models;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Story> Stories { get; set; }
        public DbSet<StorySeeUser> StorySeeUsers { get; set; }
        public DbSet<VisitorLog> VisitorLogs { get; set; }
        public DbSet<DailyNews> DailyNews { get; set; }
        public DbSet<Activite> Activites { get; set; }
        public DbSet<Sifre> PasswordManagement { get; set; }
        public DbSet<CIP> CIPs { get; set; }
        public DbSet<Duyuru> Duyuruyönetimis { get; set; }
        public DbSet<Kampanya> KampanyaYönetimis { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<SirkettenHaberlerYönetimi> SirkettenHaberlerYönetimis { get; set; }
        public DbSet<CompetitionAdministration> CompetitionAdministrations { get; set; }
        public DbSet<Etkinlik> Etkinliks { get; set; }
        public DbSet<CelebrationManagement> CelebrationManagements { get; set; }
        public DbSet<Ceokösesis> Ceokösesis { get; set; }
        public DbSet<BirthdayManager> BirthdayManagers { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<TakipForm> TakipForms { get; set; }
        public DbSet<DepartmanCategory> DepartmanCategories { get; set; }

        public DbSet<AdminCevaplama> adminCevaplamas { get; set; }

        public DbSet<Departman> Departmans { get; set; }

        public DbSet<TakipFormResponse> TakipFormPool { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<CompititionParticipant> CompetitionParticipants { get; set; }
        public DbSet<CelebrationParticipants> CelebrationParticipants { get; set; }
        public DbSet<FoodMenuList> FoodMenuList { get; set; }
        public DbSet<Omu_Departman> Omu_Departman { get; set; }
        public DbSet<OmuAnswer> OmuAnswer { get; set; }
        public DbSet<Omus> Omus { get; set; }
        public DbSet<AyınProjes> AyınProjes { get; set; }
        public DbSet<ActionHistory> ActionHistory { get; set; }
        public DbSet<AnketYonetimi> AnketYonetimi { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<Sirketler> Sirketlers { get; set; }
        public DbSet<NotificationManagement> NotificationManagement { get; set; }
        public DbSet<AyinProjeKonus> AyınProjeKonus { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<IyilestirmeTipi> Iyileştirmetipis { get; set; }

        public DbSet<FormHistory> FormHistory { get; set; }
        public DbSet<RegistrationHistory> RegistrationHistory { get; set; }

        public DbSet<UlasimYonetimi> UlasimYönetimis { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AyınProjes>().ToTable("AyınProjes");
            modelBuilder.Entity<Omus>()
             .Ignore(o => o.KayitTipi); // KayitTipi'ni yok sayıyoruz
        }
       
    }
 

}
