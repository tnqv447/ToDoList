using AppCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs
{
    public class JointUserConfig : IEntityTypeConfiguration<JointUser>
    {
        void IEntityTypeConfiguration<JointUser>.Configure(EntityTypeBuilder<JointUser> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();

            builder.HasIndex(m => new { m.UserId, m.ToDoTaskId }).IsUnique();

        }
    }
}