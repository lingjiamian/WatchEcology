using System;
using Microsoft.EntityFrameworkCore;

namespace WatchEcology.Models
{
    public partial class WatchecologyContext : DbContext
    {
        public virtual DbSet<AnimalNewsDetail> Anewsdetail { get; set; }
        public virtual DbSet<AnimalNews> Animalnews { get; set; }
        public virtual DbSet<Option> Option { get; set; }
        public virtual DbSet<Question> Question { get; set; }

        public WatchecologyContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Data Source=localhost;Database=watchecology;User Id=root;Password=111111;port=3306;SslMode=None;");
                Database.EnsureCreatedAsync();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimalNewsDetail>(entity =>
            {
                entity.ToTable("anewsdetail");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Url).HasMaxLength(250);
            });

            modelBuilder.Entity<AnimalNews>(entity =>
            {
                entity.ToTable("animalnews");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ImgUrl).HasMaxLength(500);

                entity.Property(e => e.PubDate).HasColumnType("datetime");

                entity.Property(e => e.Source).HasMaxLength(45);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.Property(e => e.Url).HasMaxLength(250);
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.ToTable("option");

                entity.HasIndex(e => e.QuestionId)
                    .HasName("fk_option_question");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OptionContent).HasMaxLength(300);

                entity.Property(e => e.QuestionId)
                    .HasColumnName("QuestionID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Option)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("fk_option_question");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("question");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Answer).HasMaxLength(100);
                entity.Property(e => e.OptionA).HasMaxLength(100);
                entity.Property(e => e.OptionB).HasMaxLength(100);
                entity.Property(e => e.OptionC).HasMaxLength(100);
                entity.Property(e => e.OptionD).HasMaxLength(100);
                entity.Property(e => e.BgKlg).HasMaxLength(500);

                entity.Property(e => e.QuestionContent).HasMaxLength(300);
            });
        }
    }
}
