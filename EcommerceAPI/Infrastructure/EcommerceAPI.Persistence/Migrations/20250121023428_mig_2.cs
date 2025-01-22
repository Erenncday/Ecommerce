using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Persistence.Migrations
{
    public partial class mig_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "files",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_files_ProductId",
                table: "files",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_files_Products_ProductId",
                table: "files",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_files_Products_ProductId",
                table: "files");

            migrationBuilder.DropIndex(
                name: "IX_files_ProductId",
                table: "files");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "files");
        }
    }
}
