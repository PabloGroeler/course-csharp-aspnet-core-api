using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class MunicipioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public MunicipioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.serviceProvider;
        }

        [Fact(DisplayName = "Crud Municipio")]
        [Trait("CRUD", "MunicipioEntity")]
        public async Task CrudMunicipio()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                MunicipioImplementation _repository = new MunicipioImplementation(context);
                MunicipioEntity _entity = new MunicipioEntity
                {
                    Nome = Faker.Address.City(),
                    CodIbge = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("22ffbd18-cdb9-45cc-97b0-51e97700bf71"),
                };

                var _registroCriado = await _repository.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.Nome, _registroCriado.Nome);
                Assert.Equal(_entity.CodIbge, _registroCriado.CodIbge);
                Assert.Equal(_entity.UfId, _registroCriado.UfId);
                Assert.False(_registroCriado.Id == Guid.Empty);

                _entity.Nome = Faker.Address.City();
                var _registroAtualizado = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_entity.CodIbge, _registroAtualizado.CodIbge);
                Assert.Equal(_entity.UfId, _registroAtualizado.UfId);
                Assert.True(_registroCriado.Id == _entity.Id);

                var _registroExiste = await _repository.ExistAsync(_registroAtualizado.Id);
                Assert.True(_registroExiste);

                var _registroSelect = await _repository.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroSelect);
                Assert.Equal(_registroAtualizado.Nome, _registroSelect.Nome);
                Assert.Equal(_registroAtualizado.CodIbge, _registroSelect.CodIbge);
                Assert.Equal(_registroAtualizado.UfId, _registroSelect.UfId);
                Assert.Null(_registroSelect.Uf);

                _registroSelect = await _repository.GetCompleteByIbge(_registroAtualizado.CodIbge);
                Assert.NotNull(_registroSelect);
                Assert.Equal(_registroAtualizado.Nome, _registroSelect.Nome);
                Assert.Equal(_registroAtualizado.CodIbge, _registroSelect.CodIbge);
                Assert.Equal(_registroAtualizado.UfId, _registroSelect.UfId);
                Assert.NotNull(_registroSelect.Uf);

                _registroSelect = await _repository.GetCompleteById(_registroAtualizado.Id);
                Assert.NotNull(_registroSelect);
                Assert.Equal(_registroAtualizado.Nome, _registroSelect.Nome);
                Assert.Equal(_registroAtualizado.CodIbge, _registroSelect.CodIbge);
                Assert.Equal(_registroAtualizado.UfId, _registroSelect.UfId);
                Assert.NotNull(_registroSelect.Uf);

                var _todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() > 0);

                var _remove = await _repository.DeleteAsync(_registroSelect.Id);
                Assert.True(_remove);

                _todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() == 0);
            }
        }
    }
}