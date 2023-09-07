using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace product_service.Migrations
{
    /// <inheritdoc />
    public partial class updateproduct02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageUris_Products_ProductId",
                table: "ImageUris");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ImageUris",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageUris_Products_ProductId",
                table: "ImageUris",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageUris_Products_ProductId",
                table: "ImageUris");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ImageUris",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageUris_Products_ProductId",
                table: "ImageUris",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
