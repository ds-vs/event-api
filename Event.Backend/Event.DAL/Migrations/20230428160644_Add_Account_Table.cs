using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_Account_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    account_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    login = table.Column<string>(type: "text", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "text", maxLength: 50, nullable: false),
                    password_hash = table.Column<string>(type: "text", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.account_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account");
        }
    }
}
