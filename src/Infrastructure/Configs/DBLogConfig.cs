using AppCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configs
{
    public class DbLogConfig : IEntityTypeConfiguration<DbLog>
    {
        void IEntityTypeConfiguration<DbLog>.Configure(EntityTypeBuilder<DbLog> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();

           
        }
    }
}