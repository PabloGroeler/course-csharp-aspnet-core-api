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
    public class RetornoNotFound
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "Return Not Found Get Test.")]
        public async Task E_Possivel_Invocar_a_Controller_Get()
        {
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(m => m.GetCompleteByIbge(It.IsAny<int>())).Returns(Task.FromResult((MunicipioDtoComplete)null));

            _controller = new MunicipiosController(serviceMock.Object);
            var result = await _controller.GetCompleteByIbge(1);
            Assert.True(result is NotFoundResult);

        }
    }
}
