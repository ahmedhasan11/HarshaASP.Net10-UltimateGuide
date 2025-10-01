using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class seedcountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "CountryID",
                keyValue: new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                column: "Countryname",
                value: "China");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "CountryID",
                keyValue: new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                column: "Countryname",
                value: "Philippines");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "CountryID",
                keyValue: new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                column: "Countryname",
                value: "China");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "CountryID",
                keyValue: new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                column: "Countryname",
                value: "Thailand");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "CountryID",
                keyValue: new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                column: "Countryname",
                value: "Palestinian Territory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "CountryID",
                keyValue: new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                column: "Countryname",
                value: null);

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "CountryID",
                keyValue: new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                column: "Countryname",
                value: null);

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "CountryID",
                keyValue: new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                column: "Countryname",
                value: null);

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "CountryID",
                keyValue: new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                column: "Countryname",
                value: null);

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "CountryID",
                keyValue: new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                column: "Countryname",
                value: null);
        }
    }
}
