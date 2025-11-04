using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking_Payments.Migrations
{
    /// <inheritdoc />
    public partial class InitWithSeed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 16, DateTimeKind.Utc).AddTicks(7285));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 16, DateTimeKind.Utc).AddTicks(8354));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 16, DateTimeKind.Utc).AddTicks(8361));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 16, DateTimeKind.Utc).AddTicks(8364));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 16, DateTimeKind.Utc).AddTicks(8367));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 16, DateTimeKind.Utc).AddTicks(8368));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 16, DateTimeKind.Utc).AddTicks(8370));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 16, DateTimeKind.Utc).AddTicks(8373));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 16, DateTimeKind.Utc).AddTicks(8375));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 16, DateTimeKind.Utc).AddTicks(8377));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 17, DateTimeKind.Utc).AddTicks(4599));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 17, DateTimeKind.Utc).AddTicks(5489));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 17, DateTimeKind.Utc).AddTicks(5491));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 17, DateTimeKind.Utc).AddTicks(5494));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 17, DateTimeKind.Utc).AddTicks(5538));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 17, DateTimeKind.Utc).AddTicks(5540));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 17, DateTimeKind.Utc).AddTicks(5542));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 17, DateTimeKind.Utc).AddTicks(5561));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 17, DateTimeKind.Utc).AddTicks(5563));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 17, DateTimeKind.Utc).AddTicks(5565));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 1,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(3203));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 2,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(3852));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 3,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(3854));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 4,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(3856));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 5,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(3857));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 6,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(3859));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 7,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(3875));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 8,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(3877));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 9,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(3878));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 10,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(3880));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 17, DateTimeKind.Utc).AddTicks(9258));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(955));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 3,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(978));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 4,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(982));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 5,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(984));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 6,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(985));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 7,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(988));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 8,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(989));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 9,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(991));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 10,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 7, 51, 23, 18, DateTimeKind.Utc).AddTicks(993));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
