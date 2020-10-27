using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Uf.RequestGet
{
    public class RetornoBadRequest
    {
        private UfsController _controller;

        [Fact(DisplayName = "Retorno Bad Request Test.")]
        public async Task RetornoBadRequestTest()
        {
            var serviceMock = new Mock<IUfService>();

            serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
                 new UfDto
                 {
                     Id = Guid.NewGuid(),
                     Nome = "São Paulo",
                     Sigla = "SP"
                 }
            );

            _controller = new UfsController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato Inválido");

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is BadRequestObjectResult);

        }
    }
}
