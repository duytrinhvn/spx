using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPX.Data.Migrations
{
    public partial class DeletedUserInterests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_AspNetUsers_ApplicationUserId",
                table: "Leagues");

            migrationBuilder.DropForeignKey(
                name: "FK_SportCategories_AspNetUsers_ApplicationUserId",
                table: "SportCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_ApplicationUserId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ApplicationUserId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_SportCategories_ApplicationUserId",
                table: "SportCategories");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_ApplicationUserId",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "SportCategories");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Leagues");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "SportCategories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Leagues",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ApplicationUserId",
                table: "Teams",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SportCategories_ApplicationUserId",
                table: "SportCategories",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_ApplicationUserId",
                table: "Leagues",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_AspNetUsers_ApplicationUserId",
                table: "Leagues",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SportCategories_AspNetUsers_ApplicationUserId",
                table: "SportCategories",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_ApplicationUserId",
                table: "Teams",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
