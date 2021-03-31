using Microsoft.EntityFrameworkCore.Migrations;

namespace SPX.Data.Migrations
{
    public partial class FixModelFieldNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buckets_SportCategories_sportCategoryId",
                table: "Buckets");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Buckets_bucketId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Leagues_leagueId",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "leagueId",
                table: "Teams",
                newName: "LeagueId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_leagueId",
                table: "Teams",
                newName: "IX_Teams_LeagueId");

            migrationBuilder.RenameColumn(
                name: "bucketId",
                table: "Offers",
                newName: "BucketId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_bucketId",
                table: "Offers",
                newName: "IX_Offers_BucketId");

            migrationBuilder.RenameColumn(
                name: "sportCategoryId",
                table: "Buckets",
                newName: "SportCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Buckets_sportCategoryId",
                table: "Buckets",
                newName: "IX_Buckets_SportCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buckets_SportCategories_SportCategoryId",
                table: "Buckets",
                column: "SportCategoryId",
                principalTable: "SportCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Buckets_BucketId",
                table: "Offers",
                column: "BucketId",
                principalTable: "Buckets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Leagues_LeagueId",
                table: "Teams",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buckets_SportCategories_SportCategoryId",
                table: "Buckets");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Buckets_BucketId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Leagues_LeagueId",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "LeagueId",
                table: "Teams",
                newName: "leagueId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams",
                newName: "IX_Teams_leagueId");

            migrationBuilder.RenameColumn(
                name: "BucketId",
                table: "Offers",
                newName: "bucketId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_BucketId",
                table: "Offers",
                newName: "IX_Offers_bucketId");

            migrationBuilder.RenameColumn(
                name: "SportCategoryId",
                table: "Buckets",
                newName: "sportCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Buckets_SportCategoryId",
                table: "Buckets",
                newName: "IX_Buckets_sportCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buckets_SportCategories_sportCategoryId",
                table: "Buckets",
                column: "sportCategoryId",
                principalTable: "SportCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Buckets_bucketId",
                table: "Offers",
                column: "bucketId",
                principalTable: "Buckets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Leagues_leagueId",
                table: "Teams",
                column: "leagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
