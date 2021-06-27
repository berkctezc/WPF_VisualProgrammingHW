using Microsoft.EntityFrameworkCore;

namespace WPF_HospitalManagementSystem._data
{
    public partial class DbHospitalManagementSystemContext : DbContext
    {
        public DbHospitalManagementSystemContext()
        {
        }

        public DbHospitalManagementSystemContext(DbContextOptions<DbHospitalManagementSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAdmin> TblAdmins { get; set; }
        public virtual DbSet<TblBranch> TblBranches { get; set; }
        public virtual DbSet<TblDoctor> TblDoctors { get; set; }
        public virtual DbSet<TblNurse> TblNurses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=(localdb)\\mssqllocaldb;database=DbHospitalManagementSystem; user Id=sa; password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblAdmin>(entity =>
            {
                entity.ToTable("TBL_ADMIN");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Password)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Surname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SURNAME");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");
            });

            modelBuilder.Entity<TblBranch>(entity =>
            {
                entity.ToTable("TBL_BRANCH");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Branch)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BRANCH");

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblDoctor>(entity =>
            {
                entity.ToTable("TBL_DOCTOR");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Birthofdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("BIRTHOFDATE");

                entity.Property(e => e.Branch).HasColumnName("BRANCH");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SURNAME");

                entity.HasOne(d => d.BranchNavigation)
                    .WithMany(p => p.TblDoctors)
                    .HasForeignKey(d => d.Branch)
                    .HasConstraintName("FK_TBL_DOCTOR_TBL_BRANCH");
            });

            modelBuilder.Entity<TblNurse>(entity =>
            {
                entity.ToTable("TBL_NURSES");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Birthofdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("BIRTHOFDATE");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Policlinic).HasColumnName("POLICLINIC");

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Surname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SURNAME");

                entity.HasOne(d => d.PoliclinicNavigation)
                    .WithMany(p => p.TblNurses)
                    .HasForeignKey(d => d.Policlinic)
                    .HasConstraintName("FK_TBL_NURSES_TBL_BRANCH");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
