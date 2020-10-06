using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;

namespace Api.Data.Test
{
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.serviceProvider;
        }

        [Fact(DisplayName = "Crud de usuário")]
        [Trait("CRUD", "UserEntity")]
        public async Task EhPossivelRealizarCrudUsuar() {
            using (var context = _serviceProvider.GetService<MyContext>()) {
                UserImplementation _repositorio = new UserImplementation(context);
                UserEntity _entity = new UserEntity {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                var _registroCriado = await _repositorio.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.Email, _registroCriado.Email);
                Assert.Equal(_entity.Name, _registroCriado.Name);
                Assert.False(_registroCriado.Id == Guid.Empty);
            }
        }
    }
}