using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Domain.Entities.Migrations
{
    public partial class AddOrganizationContactAndStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "logo",
                table: "organizations",
                type: "varchar(2048)",
                maxLength: 2048,
                nullable: true,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "organizations",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "organizations",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "organizations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "logo", table: "organizations");
            migrationBuilder.DropColumn(name: "email", table: "organizations");
            migrationBuilder.DropColumn(name: "phoneNumber", table: "organizations");
            migrationBuilder.DropColumn(name: "isActive", table: "organizations");
        }
    }
}
