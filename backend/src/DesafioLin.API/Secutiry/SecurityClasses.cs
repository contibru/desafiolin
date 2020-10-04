using DesafioLin.Domain.Entities;
using System.Collections.Generic;

namespace DesafioLin.API.Security
{
    public class SignUpDTO
    {
        public string login { get; set; }
        public string senha { get; set; }

        public ICollection<Authorization> authorizations { get; set; }
    }

    public class AccessCredentials
    {
        public string login { get; set; }
        public string senha { get; set; }
        public string RefreshToken { get; set; }
        public string GrantType { get; set; }
    }

    public class Token
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }

        public virtual string nome_ten { get; set; }
        public virtual string sobrenome_ten { get; set; }

        public virtual string Version { get; set; }
    }

    public class RefreshTokenData
    {
        public string RefreshToken { get; set; }
        public string login_ten { get; set; }
    }
}