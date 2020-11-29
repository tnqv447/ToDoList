using AppCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configs
{
    public class ToDoTaskConfig : IEntityTypeConfiguration<ToDoTask>
    {
        void IEntityTypeConfiguration<ToDoTask>.Configure(EntityTypeBuilder<ToDoTask> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();
            builder.Property(m => m.RegisteredUserId).IsRequired();
            
            builder.HasOne(m => m.RegisteredUser)
                .WithMany(o => o.ToDoTasks)
                .HasForeignKey(m => m.RegisteredUserId);

            builder.HasMany(m => m.Comments)
                .WithOne(o => o.ToDoTask)
                .HasForeignKey(o => o.ToDoTaskId);

            builder.HasMany(m => m.JointUsers)
                .WithOne(o => o.ToDoTask)
                .HasForeignKey(o => o.ToDoTaskId);

            builder.HasMany(m => m.AttachedFiles)
                .WithOne(o => o.ToDoTask)
                .HasForeignKey(o => o.ToDoTaskId);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(60)
                .HasAnnotation("MinLength", 3);
        }
    }
}