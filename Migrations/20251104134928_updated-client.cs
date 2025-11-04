using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking_Payments.Migrations
{
    /// <inheritdoc />
    public partial class updatedclient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Clients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "IfscCode",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(825));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(1132));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(1135));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(1137));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(1139));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(1164));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(1166));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(1168));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(1170));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(1172));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 1,
                columns: new[] { "AccountNumber", "Balance", "CreatedAt", "IfscCode" },
                values: new object[] { null, 0.0, new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4456), null });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 2,
                columns: new[] { "AccountNumber", "Balance", "CreatedAt", "IfscCode" },
                values: new object[] { null, 0.0, new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4847), null });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 3,
                columns: new[] { "AccountNumber", "Balance", "CreatedAt", "IfscCode" },
                values: new object[] { null, 0.0, new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4849), null });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 4,
                columns: new[] { "AccountNumber", "Balance", "CreatedAt", "IfscCode" },
                values: new object[] { null, 0.0, new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4852), null });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 5,
                columns: new[] { "AccountNumber", "Balance", "CreatedAt", "IfscCode" },
                values: new object[] { null, 0.0, new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4854), null });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 6,
                columns: new[] { "AccountNumber", "Balance", "CreatedAt", "IfscCode" },
                values: new object[] { null, 0.0, new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4855), null });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 7,
                columns: new[] { "AccountNumber", "Balance", "CreatedAt", "IfscCode" },
                values: new object[] { null, 0.0, new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4857), null });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 8,
                columns: new[] { "AccountNumber", "Balance", "CreatedAt", "IfscCode" },
                values: new object[] { null, 0.0, new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4859), null });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 9,
                columns: new[] { "AccountNumber", "Balance", "CreatedAt", "IfscCode" },
                values: new object[] { null, 0.0, new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4861), null });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 10,
                columns: new[] { "AccountNumber", "Balance", "CreatedAt", "IfscCode" },
                values: new object[] { null, 0.0, new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4863), null });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 1,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(9040));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 2,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(9366));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 3,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(9368));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 4,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(9374));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 5,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(9376));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 6,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(9377));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 7,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(9379));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 8,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(9380));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 9,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(9382));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 10,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(9383));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(7057));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(7859));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 3,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(7862));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 4,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(7864));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 5,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(7866));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 6,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(7868));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 7,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(7895));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 8,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(7897));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 9,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(7899));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 10,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(7900));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IfscCode",
                table: "Clients");

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 218, DateTimeKind.Utc).AddTicks(9287));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 218, DateTimeKind.Utc).AddTicks(9999));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(2));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(4));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(7));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(9));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(11));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(13));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(15));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(17));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(6996));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(7926));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(7928));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(7931));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(7933));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(7935));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(7937));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(7961));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(7963));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 219, DateTimeKind.Utc).AddTicks(7965));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 1,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(6461));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 2,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(7124));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 3,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(7125));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 4,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(7127));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 5,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(7129));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 6,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(7149));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 7,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(7150));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 8,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(7153));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 9,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(7154));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 10,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(7156));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(1749));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(3837));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 3,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(3866));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 4,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(3871));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 5,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(3874));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 6,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(3877));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 7,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(3879));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 8,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(3882));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 9,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(3884));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 10,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 9, 54, 18, 220, DateTimeKind.Utc).AddTicks(3886));
        }
    }
}
