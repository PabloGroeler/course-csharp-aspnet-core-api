using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Users;
using Api.Domain.Repository;
using Api.Domain.Dtos;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Api.Domain.Entities;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private SigningConfigurations _signingConfigurations;
        private TokenConfiguration _tokenConfiguration;
        private IConfiguration _configuration { get; }

        public LoginService(IUserRepository repository, SigningConfigurations signingConfigurations, TokenConfiguration tokenConfiguration, IConfiguration configuration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfiguration = tokenConfiguration;
            _configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            UserEntity userFind = null;
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                userFind = await _repository.FindByLogin(user.Email);
                if (userFind == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Falha na autenticação"
                    };
                }
                else
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(userFind.Email),
                        new[]{
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                        }
                    );

                    DateTime date = DateTime.Now;
                    DateTime expirationDate = date + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    string token = GenerateToken(identity, date, expirationDate, handler);
                    return SuccessObject(date, expirationDate, token, user);
                }
            }
            else
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
        }

        private string GenerateToken(ClaimsIdentity identity, DateTime date, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfigurations.signingCredentials,
                Subject = identity,
                NotBefore = date,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private Object SuccessObject(DateTime date, DateTime expirationDate, string token, LoginDto user)
        {
            return new
            {
                authenticated = true,
                created = date.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = user.Email,
                name = user.Name,
                message = "Usuário logado com sucesso."
            };
        }
    }
}