using DesafioLin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioLin.Infraestructure.Mapping

{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Login)
              .HasColumnName("Login")
              .IsRequired()
              .HasMaxLength(200);

            builder.Property(x => x.Password)
              .HasColumnName("Password")
              .HasMaxLength(200);

            builder.HasIndex(u => u.Login).IsUnique();
        }
    }
}