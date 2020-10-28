using System;
using System.Reflection.Emit;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Seeds
{
    public static class UfSeeds
    {
        public static void Ufs(ModelBuilder builder)
        {
            builder.Entity<UfEntity>().HasData(
                new UfEntity()
                {
                    Id = new System.Guid("22ffbd18-cdb9-45cc-97b0-51e97700bf71"),
                    Sigla = "MT",
                    Nome = "Mato Grosso",
                    CreateAt = DateTime.UtcNow
                },
                new UfEntity()
                {
                    Id = new System.Guid("7cc33300-586e-4be8-9a4d-bd9f01ee9ad8"),
                    Sigla = "PR",
                    Nome = "Paraná",
                    CreateAt = DateTime.UtcNow
                },
                new UfEntity()
                {
                    Id = new System.Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                    Sigla = "SP",
                    Nome = "São Paulo",
                    CreateAt = DateTime.UtcNow
                }
            );
        }
    }
}