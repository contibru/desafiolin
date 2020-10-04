using DesafioLin.Domain.Entities;
using DesafioLin.DomainServices.Interfaces;
using DesafioLin.Infraestructure.Repository.Interfaces;
using System.Collections.Generic;

namespace DesafioLin.DomainServices.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _repositoryUser;

        public UserService(IUserRepository repository)
        {
            _repositoryUser = repository;
        }

        public void Update(User user)
        {
            _repositoryUser.Update(user);
            _repositoryUser.Commit();
        }

        public IEnumerable<User> GetAll()
        {
            return _repositoryUser.GetAll();
        }

        public IEnumerable<User> GetAllWithAuthorizations()
        {
            return _repositoryUser.GetAllWithAuthorizations();
        }

        public User GetById(int idUser)
        {
            return _repositoryUser.GetById(idUser);
        }

        public void Insert(User user)
        {
            _repositoryUser.Insert(user);
            _repositoryUser.Commit();
        }

        public void Delete(int idUser)
        {
            User user = _repositoryUser.GetById(idUser);
            _repositoryUser.Delete(user);
            _repositoryUser.Commit();
        }

        public bool UserHasAuthorization(int idUser, string authName)
        {
            return _repositoryUser.UserHasAuthorization(idUser, authName);
        }
    }
}