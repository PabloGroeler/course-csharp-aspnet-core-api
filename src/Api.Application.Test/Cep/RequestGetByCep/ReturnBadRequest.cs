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
    public class ReturnBadRequest
    {
        private CepsController _controller;

        [Fact(DisplayName = "Return Bad Request Get By Cep Test.")]
        public async Task ReturnBadRequestTest()
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
            _controller.ModelState.AddModelError("Id", "Formato Inválido");

            var result = await _controller.Get("13480000");
            Assert.True(result is BadRequestObjectResult);

        }
    }
}
