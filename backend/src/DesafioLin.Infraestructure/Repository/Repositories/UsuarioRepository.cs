using DesafioLin.Domain.Entities;
using DesafioLin.Infraestructure.Context;
using DesafioLin.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DesafioLin.Infraestructure.Repository
{
    public class UsuarioRepository : RepositoryEF<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DesafioLinContext context) : base(context)
        {
        }

        public bool UsuarioHasAuthorization(int idUsuario, string authName)
        {
            return dbContext.Set<Authorization>()
                .Where(x => x.usuario.Id == idUsuario && x.Name.Trim() == authName.Trim())
                .Count() > 0;
        }

        public IEnumerable<Usuario> GetAllWithAuthorizations()
        {
            return dbContext.Set<Usuario>().Include(u => u.authorizations).ToList();
        }
    }
}