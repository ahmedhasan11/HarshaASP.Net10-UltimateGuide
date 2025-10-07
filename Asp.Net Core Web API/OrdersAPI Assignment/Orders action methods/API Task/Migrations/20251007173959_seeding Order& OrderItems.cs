using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_Task.Migrations
{
    /// <inheritdoc />
    public partial class seedingOrderOrderItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "CustomerName", "OrderDate", "OrderNumber", "TotalAmount" },
                values: new object[,]
                {
                    { new Guid("2ed6362c-0837-4078-ad22-ede5e1066dd7"), "Ayman", new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Order_2025_2", 7000.0 },
                    { new Guid("a68d26e4-40fb-4644-8d4c-4c1c42376202"), "Ahmed", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Order_2025_1", 9000.0 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "ItemId", "OrderID", "ProductName", "Quantity", "TotalPrice", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("09531768-233d-4484-9552-c799c8c65534"), new Guid("2ed6362c-0837-4078-ad22-ede5e1066dd7"), "Jeans", 10, 200.0, 20.0 },
                    { new Guid("826b5b2c-1f66-44f6-8960-7dd4de1febeb"), new Guid("a68d26e4-40fb-4644-8d4c-4c1c42376202"), "Jeans", 20, 600.0, 30.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "ItemId",
                keyValue: new Guid("09531768-233d-4484-9552-c799c8c65534"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "ItemId",
                keyValue: new Guid("826b5b2c-1f66-44f6-8960-7dd4de1febeb"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: new Guid("2ed6362c-0837-4078-ad22-ede5e1066dd7"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: new Guid("a68d26e4-40fb-4644-8d4c-4c1c42376202"));
        }
    }
}
