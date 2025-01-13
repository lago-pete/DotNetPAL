using Microsoft.EntityFrameworkCore;
using DotnetAPI.Models;


namespace DotnetAPI.Data
{
    public class DataContextEF : DbContext 
    {
        private readonly IConfiguration _config;

        public DataContextEF (IConfiguration config)
        {

            _config = config;

        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                    optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            } 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<User>()
                .ToTable("Users", "TutorialAppSchema")
                .HasKey(u => u.UserId);

            modelBuilder.Entity<UserSalary>()
                .ToTable("UserSalary", "TutorialAppSchema")
                .HasKey(u => u.UserId);

            modelBuilder.Entity<UserJobInfo>()
                .ToTable("UserJobInfo", "TutorialAppSchema")
                .HasKey(u => u.UserId);

            modelBuilder.Entity<Auth>()
                .ToTable("Auth", "TutorialAppSchema")
                .HasKey(u=> u.Email);

            modelBuilder.Entity<Post>()
                .ToTable("Post", "TutorialAppSchema")
                .HasKey(u=> u.PostId);
        }

        public virtual DbSet<User> Users {get; set;}
        public virtual DbSet<UserSalary> UsersSalary {get; set;}
        public virtual DbSet<UserJobInfo> UsersJobInfo {get; set;}
        public virtual DbSet<Auth> Auths {get; set;}

        public virtual DbSet<Post> Posts {get; set;}



        









    }
}
