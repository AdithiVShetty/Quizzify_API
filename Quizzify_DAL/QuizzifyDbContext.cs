using Microsoft.EntityFrameworkCore;

namespace Quizzify_DAL
{
    public class QuizzifyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public QuizzifyDbContext(DbContextOptions<QuizzifyDbContext> options) : base(options)
        {

        }

        public QuizzifyDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = 172.16.18.17\\SQLEXPRESS01; Database = QuizApp; user Id = quizapp; Password = Quiz@1; TrustServerCertificate = true");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Organisation)
                .WithMany()
                .HasForeignKey(u => u.OrganisationId);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);
            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);
            modelBuilder.Entity<User>()
                .Property(u => u.IsApproved)
                .HasDefaultValue(false);
            modelBuilder.Entity<User>()
                .Property(u => u.RoleId)
                .HasDefaultValue(3);
            modelBuilder.Entity<Feedback>()
                .HasOne(u=>u.Quiz)
                .WithMany()
                .HasForeignKey(u=>u.QuizId);
            modelBuilder.Entity<Feedback>()
                .HasOne(u=> u.User)
                .WithMany()
                .HasForeignKey(u=>u.UserId);

        }
    }
}