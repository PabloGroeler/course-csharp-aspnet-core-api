using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;


namespace Api.Application.Test.Municipio.RequestGetByIbge
{
    public class RetornoOk
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "Return Get Ok Test.")]
        public async Task E_Possivel_Invocar_a_Controller_Get()
        {
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(m => m.GetCompleteByIbge(It.IsAny<int>())).ReturnsAsync(
                 new MunicipioDtoComplete
                 {
                     Id = Guid.NewGuid(),
                     Nome = "São Paulo",
                 }
            );

            _controller = new MunicipiosController(serviceMock.Object);

            var result = await _controller.GetCompleteByIbge(1);
            Assert.True(result is OkObjectResult);

        }
    }
}
