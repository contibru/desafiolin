using DesafioLin.Domain.Entities;
using DesafioLin.DomainServices.Interfaces;
using DesafioLin.Infraestructure.Repository.Interfaces;
using System.Collections.Generic;

namespace DesafioLin.DomainServices.Services
{
    public class UsuarioService : ServiceBase, IUsuarioService
    {
        private readonly IUsuarioRepository _repositoryUsuario;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repositoryUsuario = repository;
        }

        public void Update(Usuario usuario)
        {
            _repositoryUsuario.Update(usuario);
            _repositoryUsuario.Commit();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _repositoryUsuario.GetAll();
        }

        public IEnumerable<Usuario> GetAllWithAuthorizations()
        {
            return _repositoryUsuario.GetAllWithAuthorizations();
        }

        public Usuario GetById(int codigoUsu)
        {
            return _repositoryUsuario.GetById(codigoUsu);
        }

        public void Insert(Usuario usuario)
        {
            _repositoryUsuario.Insert(usuario);
            _repositoryUsuario.Commit();
        }

        public void Delete(int codigo_usu)
        {
            Usuario usuario = _repositoryUsuario.GetById(codigo_usu);
            _repositoryUsuario.Delete(usuario);
            _repositoryUsuario.Commit();
        }

        public bool UsuarioHasAuthorization(int idUsuario, string authName)
        {
            return _repositoryUsuario.UsuarioHasAuthorization(idUsuario, authName);
        }
    }
}