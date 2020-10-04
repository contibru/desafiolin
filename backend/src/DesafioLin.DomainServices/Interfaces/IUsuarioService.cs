using DesafioLin.Domain.Entities;
using System.Collections.Generic;

namespace DesafioLin.DomainServices.Interfaces
{
    public interface IUserService : IServiceBase
    {
        void Insert(User user);

        IEnumerable<User> GetAll();

        void Delete(int id);

        void Update(User user);

        User GetById(int id);

        bool UserHasAuthorization(int idUser, string authName);

        IEnumerable<User> GetAllWithAuthorizations();
    }
}