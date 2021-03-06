using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.RequestGet
{
    public class RetornoBadRequest
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "Retorno BadRequest Get.")]
        public async Task RetornoBadRequestTest()
        {
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
                 new MunicipioDto
                 {
                     Id = Guid.NewGuid(),
                     Nome = "São Paulo",
                 }
            );

            _controller = new MunicipiosController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato Inválido");

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is BadRequestObjectResult);

        }
    }
}
