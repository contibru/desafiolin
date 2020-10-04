using DesafioLin.Domain.Entities;
using System.Collections.Generic;

namespace DesafioLin.DomainServices.Interfaces
{
    public interface IUsuarioService : IServiceBase
    {
        void Insert(Usuario usuario);

        IEnumerable<Usuario> GetAll();

        void Delete(int id);

        void Update(Usuario usuario);

        Usuario GetById(int id);

        bool UsuarioHasAuthorization(int idUsuario, string authName);

        IEnumerable<Usuario> GetAllWithAuthorizations();
    }
}