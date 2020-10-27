using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;


namespace Api.Application.Test.Cep.RequestGetByCep
{
    public class ReturnOk
    {
        private CepsController _controller;

        [Fact(DisplayName = "Return Get By Cep Ok Get Test.")]
        public async Task ReturnOkTest()
        {
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(m => m.Get(It.IsAny<string>())).ReturnsAsync(
                 new CepDto
                 {
                     Id = Guid.NewGuid(),
                     Logradouro = "Teste de Rua",
                 }
            );

            _controller = new CepsController(serviceMock.Object);

            var result = await _controller.Get("13480000");
            Assert.True(result is OkObjectResult);

        }
    }
}
