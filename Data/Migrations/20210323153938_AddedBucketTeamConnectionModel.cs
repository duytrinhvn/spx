using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPX.Data.Migrations
{
    public partial class AddedBucketTeamConnectionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buckets_TeamGroups_TeamGroupId",
                table: "Buckets");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TeamGroups_TeamGroupId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "TeamGroups");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamGroupId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Buckets_TeamGroupId",
                table: "Buckets");

            migrationBuilder.DropColumn(
                name: "TeamGroupId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamGroupId",
                table: "Buckets");

            migrationBuilder.CreateTable(
                name: "BucketTeamConnections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BucketId = table.Column<Guid>(nullable: true),
                    TeamId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BucketTeamConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BucketTeamConnections_Buckets_BucketId",
                        column: x => x.BucketId,
                        principalTable: "Buckets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BucketTeamConnections_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BucketTeamConnections_BucketId",
                table: "BucketTeamConnections",
                column: "BucketId");

            migrationBuilder.CreateIndex(
                name: "IX_BucketTeamConnections_TeamId",
                table: "BucketTeamConnections",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BucketTeamConnections");

            migrationBuilder.AddColumn<Guid>(
                name: "TeamGroupId",
                table: "Teams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeamGroupId",
                table: "Buckets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TeamGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamGroupId",
                table: "Teams",
                column: "TeamGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Buckets_TeamGroupId",
                table: "Buckets",
                column: "TeamGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buckets_TeamGroups_TeamGroupId",
                table: "Buckets",
                column: "TeamGroupId",
                principalTable: "TeamGroups",
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
    }
}
