using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitiesManager.Web.Migrations
{
    /// <inheritdoc />
    public partial class RefershTokenExpirationDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpirationDateTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshTokenExpirationDateTime",
                table: "AspNetUsers");
        }
    }
}
