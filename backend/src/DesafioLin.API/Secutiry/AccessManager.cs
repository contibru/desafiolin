using DesafioLin.Domain.Entities;
using DesafioLin.Infraestructure.Repository;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace DesafioLin.API.Security
{
    public class AccessManager
    {
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly IDistributedCache _cache;
        private readonly LoginRepository _loginRepository;

        public AccessManager(
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations,
            IDistributedCache cache,
            LoginRepository tenantRepository
            )
        {
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _cache = cache;
            _loginRepository = tenantRepository;
        }

        ///<summary> Valida e busca o Tenant no banco de dados com base nas credenciais enviadas.
        ///<para>Retorno: Retorna as credenciais completas, inclusive com dados de acesso ao banco de dados correspondente ao Tenant.</para>
        ///</summary>
        public AccessCredentials ValidateCredentials(AccessCredentials credenciais)
        {
            bool credenciaisValidas = false;
            Usuario usuario;

            if (credenciais != null && !String.IsNullOrWhiteSpace(credenciais.login))
            {
                if (credenciais.GrantType == null || credenciais.GrantType == "password")
                {
                    usuario = _loginRepository.Login(credenciais.login, credenciais.senha);

                    if (usuario == null)
                    {
                        return null;
                    }

                    credenciaisValidas = true;
                }
                else if (credenciais.GrantType == "refresh_token")
                {
                    if (!String.IsNullOrWhiteSpace(credenciais.RefreshToken))
                    {
                        RefreshTokenData refreshTokenBase = null;

                        string strTokenArmazenado =
                            _cache.GetString(credenciais.RefreshToken);
                        if (!String.IsNullOrWhiteSpace(strTokenArmazenado))
                        {
                            refreshTokenBase = JsonConvert
                                .DeserializeObject<RefreshTokenData>(strTokenArmazenado);
                        }

                        credenciaisValidas = (refreshTokenBase != null &&
                            credenciais.RefreshToken == refreshTokenBase.RefreshToken);

                        // Elimina o token de refresh já que um novo será gerado
                        if (credenciaisValidas)
                            _cache.Remove(credenciais.RefreshToken);
                    }
                }
            }

            if (credenciaisValidas)
            {
                return credenciais;
            }
            else
            {
                return null;
            }
        }

        ///<summary> Gera o token conforme as credenciais recebidas por parâmetro.
        ///<para>Retorno: Retorna um objeto Token</para>
        ///</summary>
        public Token GenerateToken(AccessCredentials credenciais)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(credenciais.login, "Login")
                    );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            return new Token()
            {
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                RefreshToken = Guid.NewGuid().ToString().Replace("-", String.Empty),
                Message = "OK"
            };
        }
    }
}