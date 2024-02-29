using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Orders",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Shipped",
                table: "Orders",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Shipped",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Orders",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");
        }
    }
}
