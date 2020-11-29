using AppCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configs
{
    public class AttachedFileConfig : IEntityTypeConfiguration<AttachedFile>
    {
        void IEntityTypeConfiguration<AttachedFile>.Configure(EntityTypeBuilder<AttachedFile> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();

            builder.Property(m => m.FileUrl)
                .IsRequired();
        }
    }
}