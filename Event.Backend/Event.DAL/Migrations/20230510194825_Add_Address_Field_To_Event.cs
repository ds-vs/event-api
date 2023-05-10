using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_Address_Field_To_Event : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "events",
                type: "text",
                maxLength: 80,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "events");
        }
    }
}
