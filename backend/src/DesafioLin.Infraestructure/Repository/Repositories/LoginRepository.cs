using DesafioLin.Domain.Entities;
using DesafioLin.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DesafioLin.Infraestructure.Repository
{
    public class LoginRepository
    {
        private readonly DesafioLinContext _context;

        public LoginRepository(DesafioLinContext context)
        {
            _context = context;
        }

        public User Login(string login, string senha)
        {
            return _context.Set<User>()
                .Where(x => x.Login.Equals(login))
                .Where(x => x.Password.Equals(senha))
                .Include(x => x.authorizations)
                .FirstOrDefault();
        }
    }
}