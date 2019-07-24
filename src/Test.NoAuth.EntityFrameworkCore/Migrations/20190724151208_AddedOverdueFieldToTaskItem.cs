using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.NoAuth.Migrations
{
    public partial class AddedOverdueFieldToTaskItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Overdue",
                table: "Tasks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Overdue",
                table: "Tasks");
        }
    }
}
