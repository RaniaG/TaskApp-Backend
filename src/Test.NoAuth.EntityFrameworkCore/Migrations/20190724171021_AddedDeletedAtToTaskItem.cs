using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.NoAuth.Migrations
{
    public partial class AddedDeletedAtToTaskItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Tasks");
        }
    }
}
