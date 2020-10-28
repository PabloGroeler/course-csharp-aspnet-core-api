using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Update_10272020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("25ac651d-6a59-41f3-9e87-fe4c3bc5528e"));

            migrationBuilder.UpdateData(
                table: "Uf",
                keyColumn: "Id",
                keyValue: new Guid("22ffbd18-cdb9-45cc-97b0-51e97700bf71"),
                column: "CreateAt",
                value: new DateTime(2020, 10, 27, 18, 38, 5, 831, DateTimeKind.Utc).AddTicks(3976));

            migrationBuilder.UpdateData(
                table: "Uf",
                keyColumn: "Id",
                keyValue: new Guid("7cc33300-586e-4be8-9a4d-bd9f01ee9ad8"),
                column: "CreateAt",
                value: new DateTime(2020, 10, 27, 18, 38, 5, 831, DateTimeKind.Utc).AddTicks(4025));

            migrationBuilder.UpdateData(
                table: "Uf",
                keyColumn: "Id",
                keyValue: new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                column: "CreateAt",
                value: new DateTime(2020, 10, 27, 18, 38, 5, 831, DateTimeKind.Utc).AddTicks(4030));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateAt", "Email", "Name", "UpdateAt" },
                values: new object[] { new Guid("c6e809b2-fd7b-4b57-8c33-7926475dc804"), new DateTime(2020, 10, 27, 14, 38, 5, 828, DateTimeKind.Local).AddTicks(9532), "Pablo@celtasistemas.com.br", "Administrador", new DateTime(2020, 10, 27, 14, 38, 5, 830, DateTimeKind.Local).AddTicks(311) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("c6e809b2-fd7b-4b57-8c33-7926475dc804"));

            migrationBuilder.UpdateData(
                table: "Uf",
                keyColumn: "Id",
                keyValue: new Guid("22ffbd18-cdb9-45cc-97b0-51e97700bf71"),
                column: "CreateAt",
                value: new DateTime(2020, 10, 16, 1, 53, 39, 734, DateTimeKind.Utc).AddTicks(6759));

            migrationBuilder.UpdateData(
                table: "Uf",
                keyColumn: "Id",
                keyValue: new Guid("7cc33300-586e-4be8-9a4d-bd9f01ee9ad8"),
                column: "CreateAt",
                value: new DateTime(2020, 10, 16, 1, 53, 39, 734, DateTimeKind.Utc).AddTicks(6886));

            migrationBuilder.UpdateData(
                table: "Uf",
                keyColumn: "Id",
                keyValue: new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                column: "CreateAt",
                value: new DateTime(2020, 10, 16, 1, 53, 39, 734, DateTimeKind.Utc).AddTicks(6886));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateAt", "Email", "Name", "UpdateAt" },
                values: new object[] { new Guid("25ac651d-6a59-41f3-9e87-fe4c3bc5528e"), new DateTime(2020, 10, 15, 21, 53, 39, 729, DateTimeKind.Local).AddTicks(646), "Pablo@celtasistemas.com.br", "Administrador", new DateTime(2020, 10, 15, 21, 53, 39, 731, DateTimeKind.Local).AddTicks(2962) });
        }
    }
}
