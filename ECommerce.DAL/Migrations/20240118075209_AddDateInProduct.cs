using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.DAL.Migrations
{
    public partial class AddDateInProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Catogeries",
                newName: "MaxTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfManufacture",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023,01,01));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfManufacture",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "MaxTime",
                table: "Catogeries",
                newName: "Description");
        }
    }
}
