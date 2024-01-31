using EducationalPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalPlatform.Data.Mappings
{
    public class CourseMapping : IEntityTypeConfiguration<CourseModel>
    {
        public void Configure(EntityTypeBuilder<CourseModel> builder)
        {
            builder.ToTable("Course");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id")
                .HasColumnType("UNIQUEIDENTIFIER");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnName("Description")
                .HasColumnType("TEXT");

            builder.Property(x => x.CreateDate)
                .IsRequired()
                .HasColumnName("CreateDate")
                .HasColumnType("DATETIME");

            builder.Property(x => x.LastUpdateDate)
                .HasColumnName("LastUpdateDate")
                .HasColumnType("DATETIME");

            builder.HasMany(x => x.Users)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserCourse",
                    user =>
                        user.HasOne<UserModel>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserCourse_UserId")
                        .OnDelete(DeleteBehavior.Cascade),
                    course =>
                        course.HasOne<CourseModel>()
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK_UserCourse_CourseId")
                        .OnDelete(DeleteBehavior.Cascade));

            builder.HasIndex(x => x.Title, "IX_Course_Title")
                .IsUnique();
        }
    }
}
