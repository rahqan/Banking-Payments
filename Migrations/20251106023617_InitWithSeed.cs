using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Banking_Payments.Migrations
{
    /// <inheritdoc />
    public partial class InitWithSeed : Migration
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
                    CreatedByAdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.BankId);
                    table.ForeignKey(
                        name: "FK_Banks_Admins_CreatedByAdminId",
                        column: x => x.CreatedByAdminId,
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
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfscCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    VerificationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: true),
                    BankUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_BankUsers_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "BankUsers",
                        principalColumn: "BankUserId");
                    table.ForeignKey(
                        name: "FK_Clients_BankUsers_BankUserId",
                        column: x => x.BankUserId,
                        principalTable: "BankUsers",
                        principalColumn: "BankUserId");
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
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelationShip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaries", x => x.BeneficiaryId);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Clients_ClientId",
                        column: x => x.ClientId,
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
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeneficiaryId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    BankUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_BankUsers_BankUserId",
                        column: x => x.BankUserId,
                        principalTable: "BankUsers",
                        principalColumn: "BankUserId");
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
                values: new object[] { 1, "ADM001", "admin@bankingsys.com", "Super Admin", "admin123" });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "BankId", "Address", "Code", "ContactEmail", "ContactPhone", "CreatedAt", "CreatedByAdminId", "IsActive", "Name", "PanNumber", "RegistrationNumber" },
                values: new object[,]
                {
                    { 1, "Headquarters Tower 1, Connaught Place, New Delhi", "BNK001", "info1@globalbank.com", "987650001", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(2386), 1, true, "Global Bank 1", "PANBANK1XYZ", "REG1BANK2025" },
                    { 2, "Headquarters Tower 2, Connaught Place, New Delhi", "BNK002", "info2@globalbank.com", "987650002", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(2749), 1, true, "Global Bank 2", "PANBANK2XYZ", "REG2BANK2025" },
                    { 3, "Headquarters Tower 3, Connaught Place, New Delhi", "BNK003", "info3@globalbank.com", "987650003", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(2831), 1, true, "Global Bank 3", "PANBANK3XYZ", "REG3BANK2025" },
                    { 4, "Headquarters Tower 4, Connaught Place, New Delhi", "BNK004", "info4@globalbank.com", "987650004", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(2838), 1, true, "Global Bank 4", "PANBANK4XYZ", "REG4BANK2025" },
                    { 5, "Headquarters Tower 5, Connaught Place, New Delhi", "BNK005", "info5@globalbank.com", "987650005", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(2844), 1, true, "Global Bank 5", "PANBANK5XYZ", "REG5BANK2025" },
                    { 6, "Headquarters Tower 6, Connaught Place, New Delhi", "BNK006", "info6@globalbank.com", "987650006", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(2857), 1, true, "Global Bank 6", "PANBANK6XYZ", "REG6BANK2025" },
                    { 7, "Headquarters Tower 7, Connaught Place, New Delhi", "BNK007", "info7@globalbank.com", "987650007", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(2940), 1, true, "Global Bank 7", "PANBANK7XYZ", "REG7BANK2025" },
                    { 8, "Headquarters Tower 8, Connaught Place, New Delhi", "BNK008", "info8@globalbank.com", "987650008", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(2948), 1, true, "Global Bank 8", "PANBANK8XYZ", "REG8BANK2025" },
                    { 9, "Headquarters Tower 9, Connaught Place, New Delhi", "BNK009", "info9@globalbank.com", "987650009", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(2954), 1, true, "Global Bank 9", "PANBANK9XYZ", "REG9BANK2025" },
                    { 10, "Headquarters Tower 10, Connaught Place, New Delhi", "BNK0010", "info10@globalbank.com", "9876500010", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(2964), 1, true, "Global Bank 10", "PANBANK10XYZ", "REG10BANK2025" }
                });

            migrationBuilder.InsertData(
                table: "BankUsers",
                columns: new[] { "BankUserId", "BankId", "Code", "Email", "Name", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "BU001", "alice.kumar@globalbank1.com", "Alice Kumar", "alice123", "9000010001" },
                    { 2, 1, "BU002", "ravi.singh@globalbank1.com", "Ravi Singh", "ravi123", "9000010002" },
                    { 3, 1, "BU003", "meena.das@globalbank1.com", "Meena Das", "meena123", "9000010003" },
                    { 4, 1, "BU004", "suresh.nair@globalbank1.com", "Suresh Nair", "suresh123", "9000010004" },
                    { 5, 1, "BU005", "tanya.joseph@globalbank1.com", "Tanya Joseph", "tanya123", "9000010005" },
                    { 6, 1, "BU006", "neeraj.chauhan@globalbank1.com", "Neeraj Chauhan", "neeraj123", "9000010006" },
                    { 7, 1, "BU007", "rohini.sharma@globalbank1.com", "Rohini Sharma", "rohini123", "9000010007" },
                    { 8, 1, "BU008", "irfan.malik@globalbank1.com", "Irfan Malik", "irfan123", "9000010008" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "AccountNumber", "Address", "ApprovedBy", "Balance", "BankId", "BankUserId", "BusinessType", "Code", "CreatedAt", "Email", "IfscCode", "IsActive", "Name", "Password", "RegisterationNumber", "VerificationStatus" },
                values: new object[,]
                {
                    { 1, null, "Gurgaon, Haryana", null, 480000.0, 1, 1, "IT Services", "CL001", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(5409), "info@technova.com", null, true, "TechNova Pvt Ltd", "tech123", null, "Verified" },
                    { 2, null, "Noida Sector 5", null, 210000.0, 1, 2, "Food Supply Chain", "CL002", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(7386), "contact@greenfoods.com", null, true, "GreenFoods Ltd", "green123", null, "Verified" },
                    { 3, null, "Kolkata, West Bengal", null, 75000.0, 1, 3, "Educational NGO", "CL003", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(7392), "hello@edusmart.org", null, true, "EduSmart Foundation", "edu123", null, "Pending" },
                    { 4, null, "Pune, Maharashtra", null, 325000.0, 1, 4, "Automotive Parts", "CL004", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(7394), "sales@autoworks.com", null, true, "AutoWorks India", "auto123", null, "Verified" },
                    { 5, null, "Hyderabad, Telangana", null, 455000.0, 1, 5, "Healthcare", "CL005", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(7396), "admin@medcare.com", null, true, "MedCare Diagnostics", "med123", null, "Verified" },
                    { 6, null, "Bangalore, Karnataka", null, 180000.0, 1, 6, "Retail Chain", "CL006", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(7398), "support@quickmart.in", null, true, "QuickMart Retail", "mart123", null, "Verified" },
                    { 7, null, "Mumbai, Maharashtra", null, 650000.0, 1, 7, "Real Estate", "CL007", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(7400), "contact@urbanspaces.in", null, true, "UrbanSpaces Realty", "urban123", null, "Verified" },
                    { 8, null, "Chennai, Tamil Nadu", null, 275000.0, 1, 8, "Software Services", "CL008", new DateTime(2025, 11, 6, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(7402), "hr@brightware.com", null, true, "BrightWare Solutions", "bright123", null, "Pending" }
                });

            migrationBuilder.InsertData(
                table: "Beneficiaries",
                columns: new[] { "BeneficiaryId", "AccountNumber", "BankName", "ClientId", "IfscCode", "Name", "RelationShip" },
                values: new object[,]
                {
                    { 1, "1212121212", "Global Bank 1", 1, "GBNK0010001", "Rohit Sharma", "Supplier" },
                    { 2, "1313131313", "Global Bank 1", 1, "GBNK0010002", "Sneha Gupta", "Contractor" },
                    { 3, "1414141414", "Global Bank 1", 2, "GBNK0010003", "Vikram Iyer", "Farmer" },
                    { 4, "1515151515", "Global Bank 1", 2, "GBNK0010004", "Megha Tiwari", "Vendor" },
                    { 5, "1616161616", "Global Bank 1", 3, "GBNK0010005", "Aditi Bose", "Teacher" },
                    { 6, "1717171717", "Global Bank 1", 4, "GBNK0010006", "Manish Goel", "Supplier" },
                    { 7, "1818181818", "Global Bank 1", 4, "GBNK0010007", "Rahul Prasad", "Consultant" },
                    { 8, "1919191919", "Global Bank 1", 5, "GBNK0010008", "Priya Nair", "Doctor" },
                    { 9, "2020202020", "Global Bank 1", 5, "GBNK0010009", "Dr. Arvind Rao", "Supplier" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "BankUserId", "BeneficiaryId", "ClientId", "PaymentDate", "Remarks", "Type", "status" },
                values: new object[,]
                {
                    { 1, 12500m, 1, 1, 1, new DateTime(2025, 10, 27, 8, 6, 16, 437, DateTimeKind.Local).AddTicks(9664), "Monthly IT Contract", 2, 1 },
                    { 2, 7500m, 1, 2, 1, new DateTime(2025, 11, 1, 8, 6, 16, 438, DateTimeKind.Local).AddTicks(776), "Hardware Purchase", 1, 0 },
                    { 3, 34000m, 2, 3, 2, new DateTime(2025, 10, 30, 8, 6, 16, 438, DateTimeKind.Local).AddTicks(784), "Raw Material Payment", 0, 1 },
                    { 4, 15000m, 4, 7, 4, new DateTime(2025, 11, 4, 8, 6, 16, 438, DateTimeKind.Local).AddTicks(787), "Consultant Fees", 2, 1 },
                    { 5, 9800m, 5, 9, 5, new DateTime(2025, 11, 5, 8, 6, 16, 438, DateTimeKind.Local).AddTicks(790), "Doctor Honorarium", 1, 0 },
                    { 6, 18500m, 5, 9, 5, new DateTime(2025, 11, 3, 8, 6, 16, 438, DateTimeKind.Local).AddTicks(792), "Supplier Settlement", 0, 1 },
                    { 7, 5600m, 3, 5, 3, new DateTime(2025, 11, 2, 8, 6, 16, 438, DateTimeKind.Local).AddTicks(794), "Teacher Stipend", 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CreatedByAdminId",
                table: "Banks",
                column: "CreatedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_BankUsers_BankId",
                table: "BankUsers",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_ClientId",
                table: "Beneficiaries",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ApprovedBy",
                table: "Clients",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_BankId",
                table: "Clients",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_BankUserId",
                table: "Clients",
                column: "BankUserId");

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
