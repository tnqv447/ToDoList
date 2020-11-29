using AppCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();

            builder.HasMany(m => m.DbLogs)
                .WithOne(o => o.ExecUser)
                .HasForeignKey(o => o.ExecUserId);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(40)
                .HasAnnotation("MinLength", 3);

            builder.HasIndex(m => m.Username).IsUnique();
            builder.Property(m => m.Username)
                .IsRequired()
                .HasMaxLength(40)
                .HasAnnotation("MinLength", 3);
            
            builder.Property(m => m.Password)
                .IsRequired()
                .HasMaxLength(40)
                .HasAnnotation("MinLength", 3);

            builder.Property(m => m.PhoneNumber)
                .IsRequired()
                .HasMaxLength(40)
                .HasAnnotation("MinLength", 3);

        }
    }
}