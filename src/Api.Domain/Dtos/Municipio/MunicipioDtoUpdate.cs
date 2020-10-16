using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Municipio
{
    public class MunicipioDtoUpdate
    {
        [Required(ErrorMessage = "Id obrigatório")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome do município obrigatório")]
        [StringLength(60, ErrorMessage = "Nome de Município deve ter no máximo {1} caracteres")]
        public string Nome { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Código do IBGE Inválido")]
        public int CodIbge { get; set; }

        [Required(ErrorMessage = "Uf obrigatória.")]
        public Guid UfId { get; set; }
    }
}