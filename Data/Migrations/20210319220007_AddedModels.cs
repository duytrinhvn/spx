using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPX.Data.Migrations
{
    public partial class AddedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buckets_TeamGroup_TeamGroupId",
                table: "Buckets");

            migrationBuilder.DropForeignKey(
                name: "FK_Interest_AspNetUsers_ApplicationUserId",
                table: "Interest");

            migrationBuilder.DropForeignKey(
                name: "FK_Interest_Teams_TeamId",
                table: "Interest");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TeamGroup_TeamGroupId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamGroup",
                table: "TeamGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interest",
                table: "Interest");

            migrationBuilder.RenameTable(
                name: "TeamGroup",
                newName: "TeamGroups");

            migrationBuilder.RenameTable(
                name: "Interest",
                newName: "Interests");

            migrationBuilder.RenameIndex(
                name: "IX_Interest_TeamId",
                table: "Interests",
                newName: "IX_Interests_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Interest_ApplicationUserId",
                table: "Interests",
                newName: "IX_Interests_ApplicationUserId");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Buckets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamGroups",
                table: "TeamGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interests",
                table: "Interests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Buckets_TeamGroups_TeamGroupId",
                table: "Buckets",
                column: "TeamGroupId",
                principalTable: "TeamGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_AspNetUsers_ApplicationUserId",
                table: "Interests",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_Teams_TeamId",
                table: "Interests",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TeamGroups_TeamGroupId",
                table: "Teams",
                column: "TeamGroupId",
                principalTable: "TeamGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buckets_TeamGroups_TeamGroupId",
                table: "Buckets");

            migrationBuilder.DropForeignKey(
                name: "FK_Interests_AspNetUsers_ApplicationUserId",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_Interests_Teams_TeamId",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TeamGroups_TeamGroupId",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamGroups",
                table: "TeamGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interests",
                table: "Interests");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Buckets");

            migrationBuilder.RenameTable(
                name: "TeamGroups",
                newName: "TeamGroup");

            migrationBuilder.RenameTable(
                name: "Interests",
                newName: "Interest");

            migrationBuilder.RenameIndex(
                name: "IX_Interests_TeamId",
                table: "Interest",
                newName: "IX_Interest_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Interests_ApplicationUserId",
                table: "Interest",
                newName: "IX_Interest_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamGroup",
                table: "TeamGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interest",
                table: "Interest",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BucketId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Buckets_BucketId",
                        column: x => x.BucketId,
                        principalTable: "Buckets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_BucketId",
                table: "Offers",
                column: "BucketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buckets_TeamGroup_TeamGroupId",
                table: "Buckets",
                column: "TeamGroupId",
                principalTable: "TeamGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interest_AspNetUsers_ApplicationUserId",
                table: "Interest",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interest_Teams_TeamId",
                table: "Interest",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TeamGroup_TeamGroupId",
                table: "Teams",
                column: "TeamGroupId",
                principalTable: "TeamGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
