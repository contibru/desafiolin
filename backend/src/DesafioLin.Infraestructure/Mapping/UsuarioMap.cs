using DesafioLin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioLin.Infraestructure.Mapping

{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Login)
              .HasColumnName("Login")
              .HasMaxLength(200);
            builder.Property(x => x.Senha)
              .HasColumnName("Senha")
              .HasMaxLength(200);
        }
    }
}