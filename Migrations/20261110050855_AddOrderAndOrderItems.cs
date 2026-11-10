using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KodaStore.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderAndOrderItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Product",
                keyColumn: "ProductCode",
                keyValue: "PR01");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Product",
                keyColumn: "ProductCode",
                keyValue: "PR02");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Product",
                keyColumn: "ProductCode",
                keyValue: "PR03");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "dbo",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    OrderDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.CheckConstraint("CK_Orders_Status", "[Status] In ('Pending', 'Confirmed')");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "dbo",
                columns: table => new
                {
                    OrderItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "[Quantity]* [UnitPrice]", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.CheckConstraint("CK_Order_Quantity", "[Quantity] > 0");
                    table.CheckConstraint("CK_OrderItem_UnitPrice", "[UnitPrice] >= 0");
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Product_ProductCode",
                        column: x => x.ProductCode,
                        principalSchema: "dbo",
                        principalTable: "Product",
                        principalColumn: "ProductCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId_ProductCode",
                schema: "dbo",
                table: "OrderItems",
                columns: new[] { "OrderId", "ProductCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductCode",
                schema: "dbo",
                table: "OrderItems",
                column: "ProductCode");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderDate",
                schema: "dbo",
                table: "Orders",
                column: "OrderDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "dbo");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Product",
                columns: new[] { "ProductCode", "Category", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { "PR01", "Mie Ayam", null, "Mie Ayam Yamin", 13000m, 250 },
                    { "PR02", "Bakso", "bakso urat besar, lainnya kecil", "Bakso Campuran", 17000m, 250 },
                    { "PR03", "Bakso", "Bakso dg tetelan daging", "Bakso Halus", 15000m, 210 }
                });
        }
    }
}
