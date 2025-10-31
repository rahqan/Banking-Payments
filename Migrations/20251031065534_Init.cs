using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Banking_Payments.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    BankId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.BankId);
                    table.ForeignKey(
                        name: "FK_Banks_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankUsers",
                columns: table => new
                {
                    BankUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankUsers", x => x.BankUserId);
                    table.ForeignKey(
                        name: "FK_BankUsers_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    ContactDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.ContactDetailsId);
                    table.ForeignKey(
                        name: "FK_ContactDetails_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientBusinessType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientVerificationStatus = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    BankUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_BankUsers_BankUserId",
                        column: x => x.BankUserId,
                        principalTable: "BankUsers",
                        principalColumn: "BankUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "BankId");
                });

            migrationBuilder.CreateTable(
                name: "Beneficiaries",
                columns: table => new
                {
                    BeneficiaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IfscCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ClientId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaries", x => x.BeneficiaryId);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Clients_ClientId1",
                        column: x => x.ClientId1,
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankUserId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_BankUsers_BankUserId",
                        column: x => x.BankUserId,
                        principalTable: "BankUsers",
                        principalColumn: "BankUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IfscCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    BeneficiaryId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    BankUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_BankUsers_BankUserId",
                        column: x => x.BankUserId,
                        principalTable: "BankUsers",
                        principalColumn: "BankUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Beneficiaries_BeneficiaryId",
                        column: x => x.BeneficiaryId,
                        principalTable: "Beneficiaries",
                        principalColumn: "BeneficiaryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                });

            migrationBuilder.CreateTable(
                name: "SalaryDisbursements",
                columns: table => new
                {
                    SalaryDisbursementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryDisbursements", x => x.SalaryDisbursementId);
                    table.ForeignKey(
                        name: "FK_SalaryDisbursements_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaryDisbursements_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "Code", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "ADM001", "alice@banking.com", "Alice Johnson", "Pass@123" },
                    { 2, "ADM002", "bob@banking.com", "Bob Smith", "Pass@123" },
                    { 3, "ADM003", "charlie@banking.com", "Charlie Brown", "Pass@123" }
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "BankId", "Address", "AdminId", "Code", "ContactEmail", "ContactPhone", "CreatedAt", "IsActive", "Name", "PanNumber", "RegistrationNumber" },
                values: new object[,]
                {
                    { 1, "123 Finance St", 1, "B001", "info@fnb.com", "1234567890", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "First National Bank", "AAAPL1234C", "REG001" },
                    { 2, "456 Trust Ave", 2, "B002", "contact@gtb.com", "9876543210", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Global Trust Bank", "BBBTY4567P", "REG002" },
                    { 3, "789 Metro Rd", 3, "B003", "support@mfb.com", "5647382910", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Metro Finance Bank", "CCCXY7890K", "REG003" }
                });

            migrationBuilder.InsertData(
                table: "BankUsers",
                columns: new[] { "BankUserId", "BankId", "Code", "Email", "Name", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "BU001", "emma@fnb.com", "Emma Green", "123456", "9876543210" },
                    { 2, 2, "BU002", "liam@gtb.com", "Liam Gray", "123456", "8765432109" },
                    { 3, 3, "BU003", "olivia@mfb.com", "Olivia White", "123456", "7654321098" }
                });

            migrationBuilder.InsertData(
                table: "ContactDetails",
                columns: new[] { "ContactDetailsId", "BankId", "ContactEmail", "ContactName", "ContactNumber", "IsActive" },
                values: new object[,]
                {
                    { 1, 1, "tom@fnb.com", "Tom Hardy", "9998887771", true },
                    { 2, 2, "anna@gtb.com", "Anna Scott", "9998887772", true },
                    { 3, 3, "chris@mfb.com", "Chris Evans", "9998887773", true }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "BankId", "BankUserId", "ClientAddress", "ClientBusinessType", "ClientCode", "ClientEmail", "ClientName", "ClientVerificationStatus", "CreatedAt", "IsActive", "Password" },
                values: new object[,]
                {
                    { 1, 1, 1, "12 Silicon Ave", "IT Services", "C001", "hr@techwave.com", "TechWave Ltd", 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "client123" },
                    { 2, 2, 2, "45 Organic Rd", "Food Manufacturing", "C002", "admin@greenfoods.com", "GreenFoods Inc", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "client123" },
                    { 3, 3, 3, "78 Brick Lane", "Construction", "C003", "info@urbanbuilders.com", "Urban Builders", 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "client123" }
                });

            migrationBuilder.InsertData(
                table: "Beneficiaries",
                columns: new[] { "BeneficiaryId", "AccountNumber", "ClientId", "ClientId1", "IfscCode", "Name" },
                values: new object[,]
                {
                    { 1, "1234567890", 1, null, "FNB0001", "John Doe" },
                    { 2, "2345678901", 2, null, "GTB0002", "Mary Jane" },
                    { 3, "3456789012", 3, null, "MFB0003", "Peter Parker" }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "DocumentId", "BankUserId", "ClientId", "DocType", "Name", "UploadedAt", "Url" },
                values: new object[,]
                {
                    { 1, 1, 1, null, "PAN Proof", new DateTime(2025, 10, 31, 6, 55, 34, 221, DateTimeKind.Utc).AddTicks(8622), "/docs/pan1.pdf" },
                    { 2, 2, 2, null, "Registration Cert", new DateTime(2025, 10, 31, 6, 55, 34, 221, DateTimeKind.Utc).AddTicks(9913), "/docs/reg2.pdf" },
                    { 3, 3, 3, null, "Address Proof", new DateTime(2025, 10, 31, 6, 55, 34, 221, DateTimeKind.Utc).AddTicks(9915), "/docs/address3.pdf" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "AccountNumber", "Address", "ClientId", "IfscCode", "Name", "Salary" },
                values: new object[,]
                {
                    { 1, "9876543210", "101 Lakeview", 1, "FNB0001", "Sarah Wilson", 50000m },
                    { 2, "8765432109", "202 Hilltop", 2, "GTB0002", "David Lee", 45000m },
                    { 3, "7654321098", "303 Park St", 3, "MFB0003", "Sophia Brown", 55000m }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "BankUserId", "BeneficiaryId", "ClientId", "PaymentDate", "Type", "status" },
                values: new object[,]
                {
                    { 1, 10000m, 1, 1, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 2, 25000m, 2, 2, 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 3, 5000m, 3, 3, 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "SalaryDisbursements",
                columns: new[] { "SalaryDisbursementId", "Amount", "ClientId", "CreatedAt", "EmployeeId" },
                values: new object[,]
                {
                    { 1, 50000m, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 45000m, 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 55000m, 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_AdminId",
                table: "Banks",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_BankUsers_BankId",
                table: "BankUsers",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_ClientId",
                table: "Beneficiaries",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_ClientId1",
                table: "Beneficiaries",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_BankId",
                table: "Clients",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_BankUserId",
                table: "Clients",
                column: "BankUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_BankId",
                table: "ContactDetails",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_BankUserId",
                table: "Documents",
                column: "BankUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ClientId",
                table: "Documents",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ClientId",
                table: "Employees",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BankUserId",
                table: "Payments",
                column: "BankUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BeneficiaryId",
                table: "Payments",
                column: "BeneficiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClientId",
                table: "Payments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryDisbursements_ClientId",
                table: "SalaryDisbursements",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryDisbursements_EmployeeId",
                table: "SalaryDisbursements",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "SalaryDisbursements");

            migrationBuilder.DropTable(
                name: "Beneficiaries");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "BankUsers");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Admins");
        }
    }
}
