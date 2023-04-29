using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_Refresh_Token_To_Account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "refresh_token",
                table: "accounts",
                type: "text",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "token_created",
                table: "accounts",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "token_expires",
                table: "accounts",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "refresh_token",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "token_created",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "token_expires",
                table: "accounts");
        }
    }
}
