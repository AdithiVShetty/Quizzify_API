using Microsoft.EntityFrameworkCore;
using Quizzify_DAL.ModelClass;

namespace Quizzify_DAL
{
    public class QuizzifyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Category> QuizzifyCategory { get; set; }
        public DbSet<Question> QuizzifyQuestion { get; set; }
        public DbSet<QuestionType> QuizzifyQuestionType { get; set; }
        public DbSet<Answer> QuizzifyAnswer { get; set; }
        public DbSet<Image> QuizzifyImage { get; set; }
        public DbSet<Quiz> QuizzifyQuiz { get; set; }
        public DbSet<QuizQuestion> QuizzifyQuizQuestions { get; set; }
       public DbSet<QuizzifyResponse> QuizzifyResponse { get; set; }
        public DbSet<QuizzifyUserResponse> QuizzifyUserResponse { get; set; }
        public DbSet<Feedback> QuizzifyFeedback { get; set; }
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
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Category)
                .WithMany()
                .HasForeignKey(q => q.CategoryId);
            modelBuilder.Entity<Question>()
               .HasOne(q => q.QuestionType)
               .WithMany()
               .HasForeignKey(q => q.QuestionTypeId);
            modelBuilder.Entity<Question>()
              .HasOne(q => q.User)
              .WithMany()
              .HasForeignKey(q => q.UserId);
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany()
                .HasForeignKey(a => a.QuestionId);
            modelBuilder.Entity<Answer>()
             .HasOne(a => a.Image)
             .WithMany()
             .HasForeignKey(q => q.ImageId);
            modelBuilder.Entity<Question>()
         .HasOne(q => q.Image)
         .WithMany()
         .HasForeignKey(q => q.ImageId);
         //.OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Question>()
                .Property(q => q.IsEnable)
                .HasDefaultValue(true);
            modelBuilder.Entity<Answer>()
                .Property(a => a.IsCorrect)
                .HasDefaultValue(false);
            modelBuilder.Entity<QuizzifyResponse>()
     .HasOne(r => r.Quiz)
     .WithMany()
     .HasForeignKey(r => r.QuizId)
     .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<QuizzifyResponse>()
               .HasOne(r => r.User)
               .WithMany()
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<QuizzifyUserResponse>()
               .HasOne(ur => ur.Response)
               .WithMany()
               .HasForeignKey(ur => ur.ResponseId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<QuizzifyUserResponse>()
               .HasOne(ur => ur.Question)
               .WithMany()
               .HasForeignKey(ur => ur.QuestionId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.HasKey(q => q.Id);
                entity.Property(q => q.IsEnable).HasDefaultValue(true);
                entity.Property(q => q.AutoValidation).HasDefaultValue(true);
                entity.HasOne(q => q.User).WithMany().HasForeignKey(q => q.UserId);
                entity.HasOne(q => q.Organisation).WithMany().HasForeignKey(q => q.OrganisationId)
                .OnDelete(DeleteBehavior.NoAction);
                entity.Property(q => q.TotalMarks)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();
            });

            modelBuilder.Entity<Feedback>()
              .HasOne(f => f.User)
              .WithMany()
              .HasForeignKey(f => f.UserId)
              .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Feedback>()
              .HasOne(f => f.Quiz)
              .WithMany()
              .HasForeignKey(f => f.QuizId)
            .OnDelete(DeleteBehavior.NoAction);
            //    modelBuilder.Entity<QuizQuestion>(entity =>
            //    {
            //        entity.HasKey(qq => qq.Id);
            //        // Define relationships
            //        entity.HasOne(qq => qq.Category)
            //              .WithMany()
            //              .HasForeignKey(qq => qq.CategoryId)
            //              .OnDelete(DeleteBehavior.NoAction);

            //        entity.HasOne(qq => qq.Question)
            //              .WithMany()
            //              .HasForeignKey(qq => qq.QuestionId)
            //              .OnDelete(DeleteBehavior.NoAction);

            //        entity.HasOne(qq => qq.Quiz)
            //              .WithMany()
            //              .HasForeignKey(qq => qq.QuizId)
            //              .OnDelete(DeleteBehavior.NoAction);

            //        // Other configurations for QuizQuestion entity
            //        entity.Property(qq => qq.Marks)
            //              .HasColumnType("decimal(18,2)")
            //              .IsRequired();
            //    });
            //    modelBuilder.Entity<QuizQuestion>()
            //        .HasOne(q => q.Category)
            //        .WithMany()
            //        .HasForeignKey(q => q.CategoryId)
            //        .OnDelete(DeleteBehavior.NoAction); // or DeleteBehavior.NoAction

            //    modelBuilder.Entity<QuizQuestion>()
            //        .HasOne(q => q.Question)
            //        .WithMany()
            //        .HasForeignKey(q => q.QuestionId)
            //        .OnDelete(DeleteBehavior.NoAction); // or DeleteBehavior.NoAction

            //    modelBuilder.Entity<QuizQuestion>()
            //        .HasOne(q => q.Quiz)
            //        .WithMany()
            //        .HasForeignKey(q => q.QuizId)
            //        .OnDelete(DeleteBehavior.NoAction);
        }
    }
}