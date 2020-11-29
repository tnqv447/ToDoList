using AppCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configs
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        void IEntityTypeConfiguration<Comment>.Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();

            builder.Property(m => m.Content)
                .IsRequired();
        }
    }
}