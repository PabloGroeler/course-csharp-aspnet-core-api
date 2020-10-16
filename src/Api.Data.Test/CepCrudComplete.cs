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
    public class CepCrudComplete : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;
        public CepCrudComplete(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.serviceProvider;
        }

        [Fact(DisplayName = "Crud de CEP")]
        [Trait("CRUD", "CepEntity")]
        public async Task CrudCep()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                MunicipioImplementation _repositoryMunicipio = new MunicipioImplementation(context);
                MunicipioEntity _entityMunicipio = new MunicipioEntity
                {
                    Nome = Faker.Address.City(),
                    CodIbge = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("22ffbd18-cdb9-45cc-97b0-51e97700bf71"),
                };

                var _registroCriadoMunicipio = await _repositoryMunicipio.InsertAsync(_entityMunicipio);
                Assert.NotNull(_registroCriadoMunicipio);
                Assert.Equal(_entityMunicipio.Nome, _registroCriadoMunicipio.Nome);
                Assert.Equal(_entityMunicipio.CodIbge, _registroCriadoMunicipio.CodIbge);
                Assert.Equal(_entityMunicipio.UfId, _registroCriadoMunicipio.UfId);
                Assert.False(_registroCriadoMunicipio.Id == Guid.Empty);

                CepImplementation _repository = new CepImplementation(context);
                CepEntity _entity = new CepEntity
                {
                    Cep = "78556-296",
                    Logradouro = Faker.Address.StreetName(),
                    Numero = "0 a 2000",
                    MunicipioId = _registroCriadoMunicipio.Id
                };

                var _registroCriado = await _repository.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.Cep, _registroCriado.Cep);
                Assert.Equal(_entity.Logradouro, _registroCriado.Logradouro);
                Assert.Equal(_entity.Numero, _registroCriado.Numero);
                Assert.Equal(_entity.MunicipioId, _registroCriado.MunicipioId);
                Assert.False(_registroCriado.Id == Guid.Empty);

                _entity.Logradouro = Faker.Address.StreetName();
                _entity.Id = _registroCriado.Id;
                var _registroAtualizado = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_entity.Cep, _registroAtualizado.Cep);
                Assert.Equal(_entity.Logradouro, _registroAtualizado.Logradouro);
                Assert.Equal(_entity.MunicipioId, _registroAtualizado.MunicipioId);
                Assert.True(_registroCriado.Id == _entity.Id);

                var _registroExiste = await _repository.ExistAsync(_registroAtualizado.Id);
                Assert.True(_registroExiste);

                var _registroSelect = await _repository.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroSelect);
                Assert.Equal(_registroAtualizado.Cep, _registroSelect.Cep);
                Assert.Equal(_registroAtualizado.Logradouro, _registroSelect.Logradouro);
                Assert.Equal(_registroAtualizado.Numero, _registroSelect.Numero);
                Assert.Equal(_registroAtualizado.MunicipioId, _registroSelect.MunicipioId);

                _registroSelect = await _repository.SelectAsync(_registroAtualizado.Cep);
                Assert.NotNull(_registroSelect);
                Assert.Equal(_registroAtualizado.Cep, _registroSelect.Cep);
                Assert.Equal(_registroAtualizado.Logradouro, _registroSelect.Logradouro);
                Assert.Equal(_registroAtualizado.Numero, _registroSelect.Numero);
                Assert.Equal(_registroAtualizado.MunicipioId, _registroSelect.MunicipioId);
                Assert.NotNull(_registroSelect.Municipio);
                Assert.Equal(_entityMunicipio.Nome, _registroSelect.Municipio.Nome);
                Assert.NotNull(_registroSelect.Municipio.Uf);
                Assert.Equal("MT", _registroSelect.Municipio.Uf.Sigla);

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