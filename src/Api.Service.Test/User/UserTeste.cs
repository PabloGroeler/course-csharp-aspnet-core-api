using Api.Domain.Dtos.User;
using System;
using System.Collections.Generic;

namespace Api.Service.Test.User
{
    public class UserTeste
    {
        public static string NomeUsuario { get; set; }
        public static string EmailUsuario { get; set; }
        public static string NomeUsuarioAlterado { get; set; }
        public static string EmailUsuarioAlterado { get; set; }
        public static Guid IdUsuario { get; set; }
        public List<UserDto> listaUserDto = new List<UserDto>();
        public UserDto userDto;
        public UserDtoCreateResult userDtoCreateResult;
        public UserDtoCreate userDtoCreate;
        public UserDtoUpdate userDtoUpdate;
        public UserDtoUpdateResult userDtoUpdateResult;

        public UserTeste()
        {
            IdUsuario = Guid.NewGuid();
            NomeUsuario = Faker.Name.FullName();
            EmailUsuario = Faker.Internet.Email();
            NomeUsuarioAlterado = Faker.Name.FullName();
            EmailUsuarioAlterado = Faker.Internet.Email();

            for (int i = 0; i < 10; i++)
            {
                var dto = new UserDto() {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                };
                
                listaUserDto.Add(dto);
            }

            userDto = new UserDto() {
                Id = IdUsuario,
                Name = NomeUsuario,
                Email = EmailUsuario
            };

            userDtoCreate = new UserDtoCreate()
            {
                Name = NomeUsuario, 
                Email = EmailUsuario
            };

            userDtoCreateResult = new UserDtoCreateResult()
            {
                Id = IdUsuario,
                Name = NomeUsuario, 
                Email = EmailUsuario,
                CreateAt = DateTime.UtcNow
            };

            userDtoUpdate = new UserDtoUpdate()
            {
                Id = IdUsuario,
                Name = NomeUsuario, 
                Email = EmailUsuario,
            };

            userDtoUpdateResult = new UserDtoUpdateResult()
            {
                Id = IdUsuario,
                Name = NomeUsuario, 
                Email = EmailUsuario,
            };
        }
    }
}