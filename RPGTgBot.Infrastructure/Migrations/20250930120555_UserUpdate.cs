using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPGTgBot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TelegramId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelegramId",
                table: "Users");
        }
    }
}
