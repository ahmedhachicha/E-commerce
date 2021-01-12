using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechProduct.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dbProducts",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Category = table.Column<string>(maxLength: 100, nullable: false),
                    ImageData = table.Column<byte[]>(nullable: true),
                    ImageMimType = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbProducts", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "dbUsers",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbUsers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dbOrders",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: false),
                    Adress = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Gouvernerat = table.Column<string>(nullable: false),
                    Zip = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    buyerid = table.Column<long>(nullable: true),
                    orderDate = table.Column<DateTime>(nullable: false),
                    state = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbOrders", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbOrders_dbUsers_buyerid",
                        column: x => x.buyerid,
                        principalTable: "dbUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarteLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ShippingDetailid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarteLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarteLine_dbOrders_ShippingDetailid",
                        column: x => x.ShippingDetailid,
                        principalTable: "dbOrders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarteLine_ShippingDetailid",
                table: "CarteLine",
                column: "ShippingDetailid");

            migrationBuilder.CreateIndex(
                name: "IX_dbOrders_buyerid",
                table: "dbOrders",
                column: "buyerid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarteLine");

            migrationBuilder.DropTable(
                name: "dbProducts");

            migrationBuilder.DropTable(
                name: "dbOrders");

            migrationBuilder.DropTable(
                name: "dbUsers");
        }
    }
}
