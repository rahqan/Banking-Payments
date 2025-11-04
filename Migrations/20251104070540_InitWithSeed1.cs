using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Banking_Payments.Migrations
{
    /// <inheritdoc />
    public partial class InitWithSeed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 872, DateTimeKind.Utc).AddTicks(7717));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 872, DateTimeKind.Utc).AddTicks(8458));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 872, DateTimeKind.Utc).AddTicks(8461));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 872, DateTimeKind.Utc).AddTicks(8464));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 872, DateTimeKind.Utc).AddTicks(8466));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 872, DateTimeKind.Utc).AddTicks(8468));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 872, DateTimeKind.Utc).AddTicks(8471));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 872, DateTimeKind.Utc).AddTicks(8473));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 872, DateTimeKind.Utc).AddTicks(8475));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 872, DateTimeKind.Utc).AddTicks(8477));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 873, DateTimeKind.Utc).AddTicks(5756));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 873, DateTimeKind.Utc).AddTicks(6755));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 873, DateTimeKind.Utc).AddTicks(6758));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 873, DateTimeKind.Utc).AddTicks(6760));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 873, DateTimeKind.Utc).AddTicks(6786));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 873, DateTimeKind.Utc).AddTicks(6789));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 873, DateTimeKind.Utc).AddTicks(6791));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 873, DateTimeKind.Utc).AddTicks(6793));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 873, DateTimeKind.Utc).AddTicks(6795));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 873, DateTimeKind.Utc).AddTicks(6798));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 1,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(6814));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 2,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(7568));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 3,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(7587));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 4,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(7589));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 5,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(7591));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 6,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(7592));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 7,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(7594));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 8,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(7596));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 9,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(7597));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 10,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(7599));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(1005));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(4091));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 3,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(4094));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 4,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(4096));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 5,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(4098));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 6,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(4100));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 7,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(4102));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 8,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(4104));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 9,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(4106));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 10,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 5, 39, 874, DateTimeKind.Utc).AddTicks(4108));

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "BankUserId", "BeneficiaryId", "BeneficiaryId1", "ClientId", "PaymentDate", "Type", "status" },
                values: new object[,]
                {
                    { 11, 8500m, 1, 1, null, 1, new DateTime(2025, 2, 10, 10, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 12, 15200m, 11, 1, null, 1, new DateTime(2025, 2, 11, 11, 15, 0, 0, DateTimeKind.Utc), 2, 1 },
                    { 13, 23000m, 1, 1, null, 1, new DateTime(2025, 2, 12, 9, 0, 0, 0, DateTimeKind.Utc), 0, 0 },
                    { 14, 4200m, 1, 1, null, 1, new DateTime(2025, 2, 12, 13, 45, 0, 0, DateTimeKind.Utc), 1, -1 },
                    { 15, 12500m, 1, 1, null, 1, new DateTime(2025, 2, 13, 8, 20, 0, 0, DateTimeKind.Utc), 2, 1 },
                    { 16, 9900m, 11, 1, null, 1, new DateTime(2025, 2, 13, 15, 10, 0, 0, DateTimeKind.Utc), 1, 0 },
                    { 17, 30500m, 1, 1, null, 1, new DateTime(2025, 2, 14, 10, 5, 0, 0, DateTimeKind.Utc), 0, 1 },
                    { 18, 17800m, 1, 1, null, 1, new DateTime(2025, 2, 15, 9, 40, 0, 0, DateTimeKind.Utc), 2, 1 },
                    { 19, 6200m, 1, 1, null, 1, new DateTime(2025, 2, 15, 17, 30, 0, 0, DateTimeKind.Utc), 1, 0 },
                    { 20, 28500m, 2, 1, null, 1, new DateTime(2025, 2, 16, 12, 0, 0, 0, DateTimeKind.Utc), 0, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 20);

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(190));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(945));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(948));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(951));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(954));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(956));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(958));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(960));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(962));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(964));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(8074));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9042));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9045));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9047));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9050));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9081));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9084));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9086));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9088));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9091));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 1,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(7760));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 2,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8433));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 3,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8436));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 4,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8438));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 5,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8439));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 6,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8441));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 7,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8443));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 8,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8444));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 9,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8446));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 10,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8447));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(3151));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5224));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 3,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5227));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 4,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5230));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 5,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5233));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 6,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5235));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 7,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5237));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 8,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5239));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 9,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5240));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 10,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5318));
        }
    }
}
