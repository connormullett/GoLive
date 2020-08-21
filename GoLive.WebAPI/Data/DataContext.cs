using System.Collections.Generic;
using GoLive.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GoLive.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectSubscription> ProjectSubscriptions { get; set; }
        public DbSet<ProjectOwner> ProjectOwners { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // project subscription join table and relationship definition
            modelBuilder.Entity<ProjectSubscription>()
                .HasKey(ps => new { ps.ProjectId, ps.UserId });
            modelBuilder.Entity<ProjectSubscription>()
                .HasOne(ps => ps.Project)
                .WithMany(project => project.Subscribers)
                .HasForeignKey(ps => ps.ProjectId);
            modelBuilder.Entity<ProjectSubscription>()
                .HasOne(ps => ps.User)
                .WithMany(user => user.SubscribedProjects)
                .HasForeignKey(ps => ps.UserId);

            modelBuilder.Entity<ProjectOwner>()
                .HasKey(ps => new { ps.ProjectId, ps.UserId});
            modelBuilder.Entity<ProjectOwner>()
                .HasOne(ps => ps.Project)
                .WithMany(p => p.Owners)
                .HasForeignKey(ps => ps.ProjectId);
            modelBuilder.Entity<ProjectOwner>()
                .HasOne(ps => ps.User)
                .WithMany(u => u.OwnedProjects)
                .HasForeignKey(ps => ps.UserId);
        }
    }
}