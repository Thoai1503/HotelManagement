using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Domain.Entities.Migrations
{
    /// <inheritdoc />
    public partial class StoreOrganizationMemberStatusAsString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "organization_members",
                type: "varchar(9)",
                maxLength: 9,
                nullable: false,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.Sql(
                "UPDATE organization_members SET status = CASE status " +
                "WHEN '0' THEN 'INVITED' WHEN '1' THEN 'ACTIVE' " +
                "WHEN '2' THEN 'SUSPENDED' WHEN '3' THEN 'REVOKED' " +
                "ELSE status END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE organization_members SET status = CASE status " +
                "WHEN 'INVITED' THEN '0' WHEN 'ACTIVE' THEN '1' " +
                "WHEN 'SUSPENDED' THEN '2' WHEN 'REVOKED' THEN '3' " +
                "ELSE status END");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "organization_members",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(9)",
                oldMaxLength: 9)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");
        }
    }
}
