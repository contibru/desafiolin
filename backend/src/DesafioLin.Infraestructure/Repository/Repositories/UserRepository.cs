using DesafioLin.Domain.Entities;
using DesafioLin.Infraestructure.Context;
using DesafioLin.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DesafioLin.Infraestructure.Repository
{
    public class UserRepository : RepositoryEF<User>, IUserRepository
    {
        public UserRepository(DesafioLinContext context) : base(context)
        {
        }

        public bool UserHasAuthorization(int idUser, string authName)
        {
            return dbContext.Set<Authorization>()
                .Where(x => x.user.Id == idUser && x.Name.Trim().ToLower() == authName.Trim().ToLower())
                .Count() > 0;
        }

        public IEnumerable<User> GetAllWithAuthorizations()
        {
            return dbContext.Set<User>().Include(u => u.authorizations).ToList();
        }
    }
}