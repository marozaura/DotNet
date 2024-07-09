using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspTestProject.DAL.Migrations
{
    public partial class UpdateUserTableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "Users",
                table: "User",
                newName: "Username");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTimeUtc",
                schema: "Users",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "Users",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "Users",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                schema: "Users",
                table: "User",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                schema: "Users",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTimeUtc",
                schema: "Users",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTimeUtc",
                schema: "Users",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "Users",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "Users",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                schema: "Users",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                schema: "Users",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTimeUtc",
                schema: "Users",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Username",
                schema: "Users",
                table: "User",
                newName: "Name");
        }
    }
}
