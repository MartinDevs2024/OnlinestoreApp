using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Migrations
{
    /// <inheritdoc />
    public partial class addCommentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainComment_Posts_PostId",
                table: "MainComment");

            migrationBuilder.DropForeignKey(
                name: "FK_SubComment_MainComment_MainCommentId",
                table: "SubComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubComment",
                table: "SubComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainComment",
                table: "MainComment");

            migrationBuilder.RenameTable(
                name: "SubComment",
                newName: "SubComments");

            migrationBuilder.RenameTable(
                name: "MainComment",
                newName: "MainComments");

            migrationBuilder.RenameIndex(
                name: "IX_SubComment_MainCommentId",
                table: "SubComments",
                newName: "IX_SubComments_MainCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_MainComment_PostId",
                table: "MainComments",
                newName: "IX_MainComments_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubComments",
                table: "SubComments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainComments",
                table: "MainComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MainComments_Posts_PostId",
                table: "MainComments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubComments_MainComments_MainCommentId",
                table: "SubComments",
                column: "MainCommentId",
                principalTable: "MainComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainComments_Posts_PostId",
                table: "MainComments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubComments_MainComments_MainCommentId",
                table: "SubComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubComments",
                table: "SubComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainComments",
                table: "MainComments");

            migrationBuilder.RenameTable(
                name: "SubComments",
                newName: "SubComment");

            migrationBuilder.RenameTable(
                name: "MainComments",
                newName: "MainComment");

            migrationBuilder.RenameIndex(
                name: "IX_SubComments_MainCommentId",
                table: "SubComment",
                newName: "IX_SubComment_MainCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_MainComments_PostId",
                table: "MainComment",
                newName: "IX_MainComment_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubComment",
                table: "SubComment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainComment",
                table: "MainComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MainComment_Posts_PostId",
                table: "MainComment",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubComment_MainComment_MainCommentId",
                table: "SubComment",
                column: "MainCommentId",
                principalTable: "MainComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
