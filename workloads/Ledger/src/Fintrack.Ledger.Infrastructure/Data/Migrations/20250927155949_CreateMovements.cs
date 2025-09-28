using Fintrack.Ledger.Domain.Movements;

#nullable disable

namespace Fintrack.Ledger.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateMovements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ledger");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:ledger.movement_kind", "expense,income");

            migrationBuilder.CreateTable(
                name: "movements",
                schema: "ledger",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    kind = table.Column<MovementKind>(type: "ledger.movement_kind", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    description = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    occurred_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movements", x => x.id);
                    table.CheckConstraint("ck_movement_amount_positive", "amount > 0");
                });

            migrationBuilder.CreateIndex(
                name: "ix_movements_user_id",
                schema: "ledger",
                table: "movements",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movements",
                schema: "ledger");
        }
    }
}
