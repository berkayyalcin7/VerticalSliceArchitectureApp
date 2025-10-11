using Catalog.API.Features.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;

namespace Catalog.API.Repositories
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> modelBuilder)
        {
            modelBuilder.ToCollection("courses");
            // Collection Tablo / Document Satır / field
            modelBuilder.ToCollection("courses");
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Property(x => x.Name).HasElementName("name").HasMaxLength(100);
            modelBuilder.Property(x => x.Description).HasElementName("description").HasMaxLength(1000);
            modelBuilder.Property(x => x.UserId).HasElementName("userId");
            modelBuilder.Property(x => x.Picture).HasElementName("picture");
            modelBuilder.Ignore(x => x.Category);


            modelBuilder.OwnsOne(c => c.Feature, feature =>
            {
                feature.HasElementName("feature");
                feature.Property(f => f.Duration).HasElementName("duration");
                feature.Property(f => f.Rating).HasElementName("rating");
                feature.Property(f => f.EducatorFullName).HasElementName("educatorFullName").HasMaxLength(100);
            });
        }
    }
}
