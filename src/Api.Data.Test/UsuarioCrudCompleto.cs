using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using System.Linq;

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
        public async Task EhPossivelRealizarCrudUsuario() {
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

                _entity.Name = Faker.Name.First();
                var _registroUpdate = await _repositorio.UpdateAsync(_entity);
                Assert.NotNull(_registroUpdate);
                Assert.Equal(_entity.Email, _registroUpdate.Email);
                Assert.Equal(_entity.Name, _registroUpdate.Name);

                var _registroExist = await _repositorio.ExistAsync(_registroUpdate.Id);
                Assert.True(_registroExist);

                var _registroSelect = await _repositorio.SelectAsync(_registroUpdate.Id);
                Assert.NotNull(_registroSelect);
                Assert.Equal(_registroUpdate.Email, _registroSelect.Email);
                Assert.Equal(_registroUpdate.Name, _registroSelect.Name);

                var _todosRegistros = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() > 0);

                var _removeu = await _repositorio.DeleteAsync(_registroSelect.Id);
                Assert.True(_removeu);

                var _usuarioPadrao = await _repositorio.FindByLogin("Pablo@celtasistemas.com.br"); 
                Assert.NotNull(_usuarioPadrao);
                Assert.Equal("Pablo@celtasistemas.com.br", _usuarioPadrao.Email);
                Assert.Equal("Administrador", _usuarioPadrao.Name);
            }
        }
    }
}