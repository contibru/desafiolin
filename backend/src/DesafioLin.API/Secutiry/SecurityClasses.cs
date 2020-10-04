using DesafioLin.Domain.Entities;
using System.Collections.Generic;

namespace DesafioLin.API.Security
{
    public class SignUpDTO
    {
        public string login { get; set; }
        public string password { get; set; }

        public ICollection<Authorization> authorizations { get; set; }
    }

    public class AccessCredentials
    {
        public string login { get; set; }
        public string password { get; set; }
        public string RefreshToken { get; set; }
        public string GrantType { get; set; }
        public int idUser { get; set; }
    }

    public class Token
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }

        public string Login { get; set; }
        public int idUser { get; set; }
    }

    public class RefreshTokenData
    {
        public string RefreshToken { get; set; }
        public string Login { get; set; }
    }
}