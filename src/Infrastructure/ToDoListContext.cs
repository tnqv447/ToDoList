using AppCore.Models;
using Infrastructure.Configs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new JointUserConfig());
            builder.ApplyConfiguration(new ToDoTaskConfig());
            builder.ApplyConfiguration(new CommentConfig());
            builder.ApplyConfiguration(new AttachedFileConfig());
            builder.ApplyConfiguration(new DbLogConfig());

            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<JointUser> JointUsers { get; set; }
        public DbSet<ToDoTask> ToDoTasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AttachedFile> AttachedFiles { get; set; }
        public DbSet<DbLog> DbLogs { get; set; }
    }
}