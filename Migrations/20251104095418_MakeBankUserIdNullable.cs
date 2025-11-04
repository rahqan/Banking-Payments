using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking_Payments.Migrations
{
    /// <inheritdoc />
    public partial class MakeBankUserIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_BankUsers_BankUserId",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "BankUserId",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_BankUsers_BankUserId",
                table: "Payments",
                column: "BankUserId",
                principalTable: "BankUsers",
                principalColumn: "BankUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_BankUsers_BankUserId",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "BankUserId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_BankUsers_BankUserId",
                table: "Payments",
                column: "BankUserId",
                principalTable: "BankUsers",
                principalColumn: "BankUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
