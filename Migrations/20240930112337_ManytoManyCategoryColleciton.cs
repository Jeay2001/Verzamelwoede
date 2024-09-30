using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Verzamelwoede.Migrations
{
    /// <inheritdoc />
    public partial class ManytoManyCategoryColleciton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Collections_CollectionId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Categories_CategoryId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_CategoryId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CollectionId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryCollection",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryCollection", x => new { x.CategoryId, x.CollectionId });
                    table.ForeignKey(
                        name: "FK_CategoryCollection_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryCollection_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCollection_CollectionId",
                table: "CategoryCollection",
                column: "CollectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryCollection");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Collections",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CategoryId",
                table: "Collections",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CollectionId",
                table: "Categories",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Collections_CollectionId",
                table: "Categories",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Categories_CategoryId",
                table: "Collections",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
