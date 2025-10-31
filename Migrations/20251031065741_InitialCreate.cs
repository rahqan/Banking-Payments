using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking_Payments.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 1,
                columns: new[] { "DocType", "UploadedAt" },
                values: new object[] { "KYC", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 2,
                columns: new[] { "DocType", "UploadedAt" },
                values: new object[] { "Registration", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 3,
                columns: new[] { "DocType", "UploadedAt" },
                values: new object[] { "Address", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 1,
                columns: new[] { "DocType", "UploadedAt" },
                values: new object[] { null, new DateTime(2025, 10, 31, 6, 55, 34, 221, DateTimeKind.Utc).AddTicks(8622) });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 2,
                columns: new[] { "DocType", "UploadedAt" },
                values: new object[] { null, new DateTime(2025, 10, 31, 6, 55, 34, 221, DateTimeKind.Utc).AddTicks(9913) });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 3,
                columns: new[] { "DocType", "UploadedAt" },
                values: new object[] { null, new DateTime(2025, 10, 31, 6, 55, 34, 221, DateTimeKind.Utc).AddTicks(9915) });
        }
    }
}
