using Microsoft.EntityFrameworkCore.Migrations;

namespace MyLeasing.Web.Migrations
{
    public partial class UpdateLessee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Lessees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessees_UserId",
                table: "Lessees",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessees_AspNetUsers_UserId",
                table: "Lessees",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessees_AspNetUsers_UserId",
                table: "Lessees");

            migrationBuilder.DropIndex(
                name: "IX_Lessees_UserId",
                table: "Lessees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Lessees");
        }
    }
}
