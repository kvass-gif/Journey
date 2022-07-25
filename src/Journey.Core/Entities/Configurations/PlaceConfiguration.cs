using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Journey.DataAccess.Entities.Configurations
{
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.HasKey(a => a.Id);
            //imagine one city, place name must be unique within the single city
            builder.HasIndex(a => a.PlaceName).IsUnique();
            builder.HasOne(p => p.ApplicationUser).WithMany(u => u.Places);
        }
    }
}
