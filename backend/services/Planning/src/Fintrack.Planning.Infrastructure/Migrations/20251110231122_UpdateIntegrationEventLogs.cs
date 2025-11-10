using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fintrack.Planning.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIntegrationEventLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IntegrationEventLog",
                schema: "planning",
                table: "IntegrationEventLog");

            migrationBuilder.RenameTable(
                name: "IntegrationEventLog",
                schema: "planning",
                newName: "IntegrationEventLogs",
                newSchema: "planning");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntegrationEventLogs",
                schema: "planning",
                table: "IntegrationEventLogs",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IntegrationEventLogs",
                schema: "planning",
                table: "IntegrationEventLogs");

            migrationBuilder.RenameTable(
                name: "IntegrationEventLogs",
                schema: "planning",
                newName: "IntegrationEventLog",
                newSchema: "planning");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntegrationEventLog",
                schema: "planning",
                table: "IntegrationEventLog",
                column: "EventId");
        }
    }
}
