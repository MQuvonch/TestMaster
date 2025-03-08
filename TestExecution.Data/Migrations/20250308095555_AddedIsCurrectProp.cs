using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestExecution.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsCurrectProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsCurrectAnswerCount",
                table: "UserAttempts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrectAnswerCount",
                table: "UserAttempts");
        }
    }
}
