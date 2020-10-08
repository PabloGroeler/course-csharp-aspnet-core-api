using System;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.Users;
using Moq;
using Xunit;

namespace Api.Service.Test.Login
{
    public class FindByLogin
    {
        private ILoginService _service;
        private Mock<ILoginService> _serviceMock;
        
        [Fact(DisplayName = "FindByLogin")]
        public async System.Threading.Tasks.Task FindByLoginTestAsync()
        {
            var email = Faker.Internet.Email();
            var objetoRetorno = new 
            {
                authenticated = true,
                created = DateTime.UtcNow,
                expiration = DateTime.UtcNow.AddHours(8),
                acessToken = Guid.NewGuid(),
                userName = email,
                name = Faker.Name.FullName(),
                message = "Usuário logado com sucesso."
            };

            var loginDto = new LoginDto
            {
                Email = email
            };

            _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(m => m.FindByLogin(loginDto)).ReturnsAsync(objetoRetorno);
            _service = _serviceMock.Object;

            var result = await _service.FindByLogin(loginDto);
            Assert.NotNull(result);
        }
    }
}