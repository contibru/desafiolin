using DesafioLin.Domain.Entities;
using System.Collections.Generic;

namespace DesafioLin.Infraestructure.Repository.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        bool UsuarioHasAuthorization(int idUsuario, string authName);
        IEnumerable<Usuario> GetAllWithAuthorizations();
    }
}