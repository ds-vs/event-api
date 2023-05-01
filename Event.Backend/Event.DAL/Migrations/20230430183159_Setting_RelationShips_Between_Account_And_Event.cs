using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Setting_RelationShips_Between_Account_And_Event : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "account_id",
                table: "events",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "accounts_to_events",
                columns: table => new
                {
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts_to_events", x => new { x.account_id, x.event_id });
                    table.ForeignKey(
                        name: "FK_accounts_to_events_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_accounts_to_events_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "event_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_events_account_id",
                table: "events",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_to_events_event_id",
                table: "accounts_to_events",
                column: "event_id");

            migrationBuilder.AddForeignKey(
                name: "FK_events_accounts_account_id",
                table: "events",
                column: "account_id",
                principalTable: "accounts",
                principalColumn: "account_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_accounts_account_id",
                table: "events");

            migrationBuilder.DropTable(
                name: "accounts_to_events");

            migrationBuilder.DropIndex(
                name: "IX_events_account_id",
                table: "events");

            migrationBuilder.DropColumn(
                name: "account_id",
                table: "events");
        }
    }
}
