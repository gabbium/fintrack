using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fintrack.Planning.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIntegrationEventLogs2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventTypeName",
                schema: "planning",
                table: "IntegrationEventLogs");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                schema: "planning",
                table: "IntegrationEventLogs",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "planning",
                table: "IntegrationEventLogs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventType",
                schema: "planning",
                table: "IntegrationEventLogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventType",
                schema: "planning",
                table: "IntegrationEventLogs");

            migrationBuilder.AlterColumn<int>(
                name: "State",
                schema: "planning",
                table: "IntegrationEventLogs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "planning",
                table: "IntegrationEventLogs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "EventTypeName",
                schema: "planning",
                table: "IntegrationEventLogs",
                type: "text",
                nullable: true);
        }
    }
}
