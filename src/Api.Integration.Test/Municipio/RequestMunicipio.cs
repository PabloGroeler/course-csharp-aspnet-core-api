using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Municipio
{
    public class RequestMunicipio : BaseIntegration
    {
        [Fact]
        public async Task RequestMunicipioTest()
        {
            await AdicionarToken();

            var municipioDto = new MunicipioDtoCreate()
            {
                Nome = "Sinop",
                CodIbge = 3550308,
                UfId = new Guid("22ffbd18-cdb9-45cc-97b0-51e97700bf71")
            };


            //Post
            var response = await PostJsonAsync(municipioDto, $"{hostApi}municipios", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("Sinop", registroPost.Nome);
            Assert.Equal(3550308, registroPost.CodIbge);
            Assert.True(registroPost.Id != default(Guid));

            //Get All
            response = await client.GetAsync($"{hostApi}municipios");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<MunicipioDto>>(jsonResult);
            Assert.NotNull(listaFromJson);
            Assert.True(listaFromJson.Count() > 0);
            Assert.True(listaFromJson.Where(r => r.Id == registroPost.Id).Count() == 1);

            var updateMunicipioDto = new MunicipioDtoUpdate()
            {
                Id = registroPost.Id,
                Nome = "Sinop",
                CodIbge = 3526902,
                UfId = new Guid("22ffbd18-cdb9-45cc-97b0-51e97700bf71")
            };

            //PUT
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateMunicipioDto),
                                    Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}Municipios", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<MunicipioDtoUpdateResult>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Sinop", registroAtualizado.Nome);
            Assert.Equal(3526902, registroAtualizado.CodIbge);

            //GET Id
            response = await client.GetAsync($"{hostApi}Municipios/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<MunicipioDto>(jsonResult);
            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroSelecionado.Nome, registroAtualizado.Nome);
            Assert.Equal(registroSelecionado.CodIbge, registroAtualizado.CodIbge);

            //GET Complete/Id
            response = await client.GetAsync($"{hostApi}Municipios/Complete/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionadoCompleto = JsonConvert.DeserializeObject<MunicipioDtoComplete>(jsonResult);
            Assert.NotNull(registroSelecionadoCompleto);
            Assert.Equal(registroSelecionadoCompleto.Nome, registroAtualizado.Nome);
            Assert.Equal(registroSelecionadoCompleto.CodIbge, registroAtualizado.CodIbge);
            Assert.NotNull(registroSelecionadoCompleto.uf);
            Assert.Equal("Mato Grosso", registroSelecionadoCompleto.uf.Nome);
            Assert.Equal("MT", registroSelecionadoCompleto.uf.Sigla);

            //GET byCodIbge
            response = await client.GetAsync($"{hostApi}Municipios/byIbge/{registroAtualizado.CodIbge}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionadoIBGECompleto = JsonConvert.DeserializeObject<MunicipioDtoComplete>(jsonResult);
            Assert.NotNull(registroSelecionadoIBGECompleto);
            Assert.Equal(registroSelecionadoIBGECompleto.Nome, registroAtualizado.Nome);
            Assert.Equal(registroSelecionadoIBGECompleto.CodIbge, registroAtualizado.CodIbge);
            Assert.NotNull(registroSelecionadoIBGECompleto.uf);
            Assert.Equal("Mato Grosso", registroSelecionadoIBGECompleto.uf.Nome);
            Assert.Equal("MT", registroSelecionadoIBGECompleto.uf.Sigla);

            //DELETE
            response = await client.DeleteAsync($"{hostApi}Municipios/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //GET ID depois do DELETE
            response = await client.GetAsync($"{hostApi}Municipios/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}
