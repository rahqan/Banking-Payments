using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking_Payments.Migrations
{
    /// <inheritdoc />
    public partial class updatedbank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banks_Admins_AdminId",
                table: "Banks");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "Banks",
                newName: "CreatedByAdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Banks_AdminId",
                table: "Banks",
                newName: "IX_Banks_CreatedByAdminId");

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 461, DateTimeKind.Utc).AddTicks(8338));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 461, DateTimeKind.Utc).AddTicks(8682));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 461, DateTimeKind.Utc).AddTicks(8685));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 461, DateTimeKind.Utc).AddTicks(8687));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 461, DateTimeKind.Utc).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 461, DateTimeKind.Utc).AddTicks(8691));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 461, DateTimeKind.Utc).AddTicks(8693));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 461, DateTimeKind.Utc).AddTicks(8695));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 461, DateTimeKind.Utc).AddTicks(8697));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 461, DateTimeKind.Utc).AddTicks(8699));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(2197));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(2700));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(2703));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(2705));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(2720));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(2722));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(2724));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(2726));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(2727));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(2729));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 1,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(6930));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 2,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(7237));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 3,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(7239));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 4,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(7241));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 5,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(7242));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 6,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(7244));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 7,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(7245));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 8,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(7247));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 9,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(7248));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 10,
                column: "UploadedAt",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(7249));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(4768));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(5576));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 3,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(5579));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 4,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(5581));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 5,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(5583));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 6,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(5631));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 7,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(5633));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 8,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(5636));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 9,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(5638));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 10,
                column: "PaymentDate",
                value: new DateTime(2025, 11, 4, 14, 14, 13, 462, DateTimeKind.Utc).AddTicks(5639));

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Admins_CreatedByAdminId",
                table: "Banks",
                column: "CreatedByAdminId",
                principalTable: "Admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banks_Admins_CreatedByAdminId",
                table: "Banks");

            migrationBuilder.RenameColumn(
                name: "CreatedByAdminId",
                table: "Banks",
                newName: "AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Banks_CreatedByAdminId",
                table: "Banks",
                newName: "IX_Banks_AdminId");

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
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4456));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4847));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4849));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4852));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4854));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4855));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4857));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4859));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4861));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 13, 49, 28, 260, DateTimeKind.Utc).AddTicks(4863));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Admins_AdminId",
                table: "Banks",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
