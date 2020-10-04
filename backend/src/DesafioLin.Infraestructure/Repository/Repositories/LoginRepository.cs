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

        public Usuario Login(string login, string senha)
        {
            return _context.Set<Usuario>()
                .Where(x => x.Login.Equals(login))
                .Where(x => x.Senha.Equals(senha))
                .Include(x => x.authorizations)
                .FirstOrDefault();
        }
    }
}