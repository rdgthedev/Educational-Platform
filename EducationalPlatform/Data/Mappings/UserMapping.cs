using EducationalPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalPlatform.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            //builder.ToTable("User");

            //builder.HasKey(x => x.Id);

            //builder.Property(x => x.Id)
            //    .ValueGeneratedOnAdd()
            //    .HasColumnName("Id")
            //    .HasColumnType("UNIQUEIDENTIFIER");

            //builder.Property(x => x.Name)
            //    .IsRequired()
            //    .HasColumnName("Name")
            //    .HasColumnType("NVARCHAR")
            //    .HasMaxLength(80);

            //builder.Property(x => x.Email)
            //    .IsRequired()
            //    .HasColumnName("Email")
            //    .HasColumnType("NVARCHAR")
            //    .HasMaxLength(80);

            //builder.Property(x => x.PasswordHash)
            //    .IsRequired()
            //    .HasColumnName("PasswordHash")
            //    .HasColumnType("NVARCHAR")
            //    .HasMaxLength(255);

            //builder.Property(x => x.BirthDate)
            //    .IsRequired()
            //    .HasColumnName("BirthDate")
            //    .HasColumnType("DATE");

            //builder.HasIndex(x => x.Email, "IX_User_Email")
            //    .IsUnique();
        }
    }
}
