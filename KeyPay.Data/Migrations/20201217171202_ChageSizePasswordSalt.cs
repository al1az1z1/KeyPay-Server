using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KeyPay.Data.Migrations
{
    public partial class ChageSizePasswordSalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(50)",
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "varbinary(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "varbinary(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldMaxLength: 500);
        }
    }
}
