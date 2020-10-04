using DesafioLin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioLin.Infraestructure.Mapping

{
    public class AuthorizationMap : IEntityTypeConfiguration<Authorization>
    {
        public void Configure(EntityTypeBuilder<Authorization> builder)
        {
            builder.ToTable("authorization");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
              .HasColumnName("Name")
              .HasMaxLength(200);
            builder.Property(x => x.Value)
              .HasColumnName("Value");

            builder.HasOne(x => x.usuario)
                .WithMany(c => c.authorizations);
        }
    }
}