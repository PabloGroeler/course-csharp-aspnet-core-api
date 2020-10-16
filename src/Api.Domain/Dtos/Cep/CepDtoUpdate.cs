using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Cep
{
    public class CepDtoUpdate
    {

        [Required(ErrorMessage = "Id obrigatório")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Cep obrigatório")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Logradouro obrigatório")]
        public string Logradouro { get; set; }
        public string Numero { get; set; }

        [Required(ErrorMessage = "Município obrigatório")]
        public Guid MunicipioId { get; set; }
    }
}