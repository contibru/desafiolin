using DesafioLin.Domain.Entities;
using System.Collections.Generic;

namespace DesafioLin.Infraestructure.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool UserHasAuthorization(int idUser, string authName);

        IEnumerable<User> GetAllWithAuthorizations();
    }
}