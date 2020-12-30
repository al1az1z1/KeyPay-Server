using Microsoft.EntityFrameworkCore.Migrations;

namespace KeyPay.Data.Migrations
{
    public partial class ChangeStrigToBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsMain",
                table: "Photoes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsMain",
                table: "Photoes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
