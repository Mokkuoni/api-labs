using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Age", "CompanyId", "DepartmentId", "EmployeeId", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("84abbca8-664d-4b20-b5de-024705497d4a"), 58, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), null, null, "Ivan Ivanov", "89099099999" },
                    { new Guid("85abbca8-664d-4b20-b5de-024705497d4a"), 35, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), null, null, "Denis Denisov", "84504504545" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "CompanyId", "Name" },
                values: new object[,]
                {
                    { new Guid("83abbca8-664d-4b20-b5de-024705497d4a"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Accounting" },
                    { new Guid("87abbca8-664d-4b20-b5de-024705497d4a"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "IT department" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("84abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("85abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: new Guid("83abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: new Guid("87abbca8-664d-4b20-b5de-024705497d4a"));
        }
    }
}
