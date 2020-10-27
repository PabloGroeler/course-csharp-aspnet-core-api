using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.RequestUpdate
{
    public class RetornoOk
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "Return Updated Test.")]
        public async Task RetornoOkTest()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Put(It.IsAny<MunicipioDtoUpdate>())).ReturnsAsync(
                new MunicipioDtoUpdateResult
                {
                    Id = Guid.NewGuid(),
                    Nome = "São Paulo",
                    UpdateAt = DateTime.UtcNow
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);

            var municipioDtoUpdate = new MunicipioDtoUpdate
            {
                Nome = "São Paulo",
                CodIbge = 1
            };

            var result = await _controller.Put(municipioDtoUpdate);
            Assert.True(result is OkObjectResult);

        }

    }
}
