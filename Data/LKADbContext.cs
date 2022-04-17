using Microsoft.EntityFrameworkCore;
using WebApplicationWithAuth.Models;

namespace WebApplicationWithAuth.Data
{
    public class LKADbContext : DbContext
    {

        public DbSet<Enrollee> Enrollees { get; set; }
        public DbSet<Relative> Relatives { get; set; }
        public DbSet<EnrolleesDoc> EnrollesDocs { get; set; }
        public DbSet<EnrolleesFile> EnrollesFiles { get; set; }
        public DbSet<DocType> DocTypes { get; set; }
        public DbSet<EnrolleesCompetition> EnrollesCompetitions { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectSubstitutor> SubjectSubstitutors { get; set; }
        public DbSet<ApplicationsExam> ApplicationsExams { get; set; }
        public DbSet<CompetitionsExam> CompetitionsExams { get; set; }
        public DbSet<EnrolleesEducation> EnrolleesEducations { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<ApplicationsContract> ApplicationsContracts { get; set; }
        public DbSet<ApplicationsAchievement> ApplicationsAchievements { get; set; }
        public DbSet<EnrolleesStatus> EnrolleesStatuses { get; set; }

        public LKADbContext(DbContextOptions<LKADbContext> options)
            : base(options) { }

        protected override void OnConfiguring(
          DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Enrollee>()
                .Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Enrollee>()
                .Property(c => c.UserID)
                .IsRequired()
                .HasMaxLength(450);

            modelBuilder.Entity<Relative>()
                .Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Relative>()
                .Property(c => c.EnrolleeID)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<EnrolleesDoc>()
                .Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<EnrolleesDoc>()
                .Property(c => c.EnrolleeID)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<EnrolleesDoc>()
                .Property(c => c.DocTypeId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<EnrolleesFile>()
                .Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<EnrolleesFile>()
                .Property(c => c.EnrolleeID)
                .HasMaxLength(10);

            modelBuilder.Entity<EnrolleesFile>()
                .Property(c => c.EnrolleesDocID)
                .HasMaxLength(10);

            modelBuilder.Entity<DocType>()
                .Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Competition>()
                .Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<EnrolleesCompetition>()
                .Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<EnrolleesCompetition>()
                .Property(c => c.EnrolleeID)
                .HasMaxLength(10);


            modelBuilder.Entity<CompetitionsExam>()
                .Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<CompetitionsExam>()
                .Property(c => c.SubjectId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<CompetitionsExam>()
                .Property(c => c.CompetitionId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<ApplicationsExam>()
                .Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<ApplicationsExam>()
                .Property(c => c.SubjectId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<ApplicationsExam>()
                .Property(c => c.CompetitionsExamId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<ApplicationsExam>()
                .Property(c => c.EnrolleesDocId)
                .HasMaxLength(10);

            modelBuilder.Entity<ApplicationsExam>()
                .Property(c => c.SubjectsSubstitutorId)
                .HasMaxLength(10);

            modelBuilder.Entity<Subject>()
                .Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<SubjectSubstitutor>()
                .Property(c => c.SubjectId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<SubjectSubstitutor>()
                .Property(c => c.SubjectsSubstitutorId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<SubjectSubstitutor>()
                .HasNoKey();

            modelBuilder.Entity<EnrolleesEducation>()
                .Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<EnrolleesEducation>()
                .Property(c => c.EnrolleesDocId)
                .HasMaxLength(10);

            modelBuilder.Entity<EnrolleesEducation>()
                .Property(c => c.EducationLevelId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<ApplicationsExam>()
                .Property(c => c.EnrolleeId)
                .HasMaxLength(10);

            modelBuilder.Entity<EducationLevel>()
                .Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<ApplicationsContract>()
                .Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<ApplicationsContract>()
                .Property(c => c.EnrolleeId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<ApplicationsContract>()
                .Property(c => c.EnrolleesDocId)
                .HasMaxLength(10);

            modelBuilder.Entity<ApplicationsAchievement>()
                .Property(c => c.Id)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<ApplicationsAchievement>()
                .Property(c => c.EnrolleeId)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<ApplicationsAchievement>()
                .Property(c => c.EnrolleesDocId)
                .HasMaxLength(10);

            modelBuilder.Entity<EnrolleesStatus>()
            .Property(c => c.Id)
            .IsRequired()
            .HasMaxLength(10);

            modelBuilder.Entity<EnrolleesStatus>()
                .Property(c => c.EnrolleeId)
                .IsRequired()
                .HasMaxLength(10);



            //modelBuilder.Entity<EnrolleesStatus>()
            //    .HasNoKey();




            //modelBuilder.Entity<Relative>()
            //    .HasOne(p => p.EnrolleeID)
            //    .WithMany(c => c.Relatives);

            //modelBuilder.Entity<Enrollee>()
            //    .HasMany(s => s.Relatives)
            //    .WithOne(p => p.Enrollee);




        }
    }
}