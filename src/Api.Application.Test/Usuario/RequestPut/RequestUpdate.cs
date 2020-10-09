using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.RequestPut
{
    public class RequestUpdate
    {
        private UsersController _controller;

        [Fact(DisplayName = "Put")]
        public async Task PutCreate()
        { 
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(m => m.Put(It.IsAny<UserDtoUpdate>())).ReturnsAsync(
                new UserDtoUpdateResult 
                {
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    UpdateAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(serviceMock.Object);

            var UserDtoUpdate = new UserDtoUpdate
            {
               Id = Guid.NewGuid(),
               Name = nome,
               Email = email 
            };

            var result = await _controller.Put(UserDtoUpdate);
            Assert.True(result is OkObjectResult);

            UserDtoUpdateResult resultValue = ((OkObjectResult) result).Value as UserDtoUpdateResult;
            Assert.NotNull(resultValue);
            Assert.Equal(UserDtoUpdate.Name, resultValue.Name);
            Assert.Equal(UserDtoUpdate.Email, resultValue.Email);
        }
    }
}