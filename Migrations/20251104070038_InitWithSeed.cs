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
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    BankUserId = table.Column<int>(type: "int", nullable: false),
                    BeneficiaryId1 = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_Payments_Beneficiaries_BeneficiaryId1",
                        column: x => x.BeneficiaryId1,
                        principalTable: "Beneficiaries",
                        principalColumn: "BeneficiaryId");
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
                    { 3, "ADM003", "charlie@banking.com", "Charlie Brown", "Pass@123" },
                    { 4, "ADM004", "diana@banking.com", "Diana Prince", "Pass@123" },
                    { 5, "ADM005", "ethan@banking.com", "Ethan Hunt", "Pass@123" },
                    { 6, "ADM006", "fiona@banking.com", "Fiona Carter", "Pass@123" },
                    { 7, "ADM007", "george@banking.com", "George Miller", "Pass@123" },
                    { 8, "ADM008", "hannah@banking.com", "Hannah Wells", "Pass@123" },
                    { 9, "ADM009", "ian@banking.com", "Ian Holmes", "Pass@123" },
                    { 10, "ADM010", "julia@banking.com", "Julia Roberts", "Pass@123" }
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "BankId", "Address", "AdminId", "Code", "ContactEmail", "ContactPhone", "CreatedAt", "IsActive", "Name", "PanNumber", "RegistrationNumber" },
                values: new object[,]
                {
                    { 1, "123 Finance St", 1, "B001", "info@fnb.com", "1234567890", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(190), true, "First National Bank", "AAAPL1234C", "REG001" },
                    { 2, "456 Trust Ave", 2, "B002", "contact@gtb.com", "9876543210", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(945), true, "Global Trust Bank", "BBBTY4567P", "REG002" },
                    { 3, "789 Metro Rd", 3, "B003", "support@mfb.com", "5647382910", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(948), true, "Metro Finance Bank", "CCCXY7890K", "REG003" },
                    { 4, "88 Liberty Ln", 4, "B004", "help@unity.com", "9898989898", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(951), true, "Unity Bank", "DDDPL2234J", "REG004" },
                    { 5, "22 Market Rd", 5, "B005", "info@ccb.com", "9000090000", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(954), true, "Central Credit Bank", "EEERW5567L", "REG005" },
                    { 6, "47 Prime Blvd", 6, "B006", "contact@prime.com", "8765432190", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(956), true, "Prime Finance Corp", "FFFTR2233N", "REG006" },
                    { 7, "12 Green St", 7, "B007", "eco@esb.com", "9123456789", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(958), true, "Eco Savings Bank", "GGGTY8907M", "REG007" },
                    { 8, "7 Freedom Way", 8, "B008", "info@liberty.com", "9234567890", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(960), true, "Liberty Bank", "HHHPL5678V", "REG008" },
                    { 9, "4 Heritage Square", 9, "B009", "support@heritage.com", "9345678901", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(962), true, "Heritage Finance", "IIIWR3456C", "REG009" },
                    { 10, "55 Moonlight Rd", 10, "B010", "hello@crescent.com", "9456789012", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(964), true, "Crescent Capital Bank", "JJJER5647R", "REG010" }
                });

            migrationBuilder.InsertData(
                table: "BankUsers",
                columns: new[] { "BankUserId", "BankId", "Code", "Email", "Name", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "BU001", "emma@fnb.com", "Emma Green", "123456", "9876543210" },
                    { 2, 2, "BU002", "liam@gtb.com", "Liam Gray", "123456", "8765432109" },
                    { 3, 3, "BU003", "olivia@mfb.com", "Olivia White", "123456", "7654321098" },
                    { 4, 4, "BU004", "noah@unity.com", "Noah Black", "123456", "6543210987" },
                    { 5, 5, "BU005", "ava@ccb.com", "Ava Brown", "123456", "5432109876" },
                    { 6, 6, "BU006", "will@prime.com", "William Scott", "123456", "4321098765" },
                    { 7, 7, "BU007", "sophia@eco.com", "Sophia Adams", "123456", "3210987654" },
                    { 8, 8, "BU008", "james@liberty.com", "James Walker", "123456", "2109876543" },
                    { 9, 9, "BU009", "mia@heritage.com", "Mia Turner", "123456", "1098765432" },
                    { 10, 10, "BU010", "lucas@crescent.com", "Lucas Reed", "123456", "9988776655" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Address", "ApprovedBy", "BankId", "BankUserId", "BusinessType", "Code", "CreatedAt", "Email", "IsActive", "Name", "Password", "RegisterationNumber", "VerificationStatus" },
                values: new object[,]
                {
                    { 1, "12 Silicon Ave", null, 1, 1, "IT Services", "C001", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(8074), "hr@techwave.com", true, "TechWave Ltd", "client123", null, "Verified" },
                    { 2, "45 Organic Rd", null, 2, 2, "Food Manufacturing", "C002", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9042), "admin@greenfoods.com", true, "GreenFoods Inc", "client123", null, "Pending" },
                    { 3, "78 Brick Lane", null, 3, 3, "Construction", "C003", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9045), "info@urbanbuilders.com", true, "Urban Builders", "client123", null, "Verified" },
                    { 4, "22 Sea View", null, 4, 4, "Travel Agency", "C004", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9047), "info@blueocean.com", true, "BlueOcean Travels", "client123", null, "Verified" },
                    { 5, "88 Medic Way", null, 5, 5, "Pharmaceuticals", "C005", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9050), "contact@healthplus.com", true, "HealthPlus Pharma", "client123", null, "Pending" },
                    { 6, "91 Knowledge Park", null, 6, 6, "Education", "C006", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9081), "hello@brightfuture.com", true, "BrightFuture EdTech", "client123", null, "Verified" },
                    { 7, "32 Green Fields", null, 7, 7, "Agriculture", "C007", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9084), "info@ecofarm.com", true, "EcoFarm Pvt Ltd", "client123", null, "Verified" },
                    { 8, "56 City Heights", null, 8, 8, "Interior Design", "C008", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9086), "contact@skyline.com", true, "Skyline Interiors", "client123", null, "Pending" },
                    { 9, "77 Data Dr", null, 9, 9, "Data Analytics", "C009", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9088), "admin@fincore.com", true, "FinCore Analytics", "client123", null, "Verified" },
                    { 10, "19 Market Lane", null, 10, 10, "Retail Chain", "C010", new DateTime(2025, 11, 4, 7, 0, 37, 327, DateTimeKind.Utc).AddTicks(9091), "info@novaretail.com", true, "Nova Retail", "client123", null, "Pending" }
                });

            migrationBuilder.InsertData(
                table: "Beneficiaries",
                columns: new[] { "BeneficiaryId", "AccountNumber", "ClientId", "ClientId1", "IfscCode", "Name" },
                values: new object[,]
                {
                    { 1, "1234567890", 1, null, "FNB0001", "John Doe" },
                    { 2, "2345678901", 2, null, "GTB0002", "Mary Jane" },
                    { 3, "3456789012", 3, null, "MFB0003", "Peter Parker" },
                    { 4, "4567890123", 4, null, "UNITY0004", "Tony Stark" },
                    { 5, "5678901234", 5, null, "CCB0005", "Bruce Wayne" },
                    { 6, "6789012345", 6, null, "PRIME0006", "Clark Kent" },
                    { 7, "7890123456", 7, null, "ECO0007", "Diana Prince" },
                    { 8, "8901234567", 8, null, "LIB0008", "Barry Allen" },
                    { 9, "9012345678", 9, null, "HER0009", "Hal Jordan" },
                    { 10, "0123456789", 10, null, "CRES00010", "Arthur Curry" }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "DocumentId", "BankUserId", "ClientId", "DocType", "Name", "UploadedAt", "Url" },
                values: new object[,]
                {
                    { 1, 1, 1, "KYC", "PAN Proof", new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(7760), "/docs/pan1.pdf" },
                    { 2, 2, 2, "Registration", "Registration Cert", new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8433), "/docs/reg2.pdf" },
                    { 3, 3, 3, "Address", "Address Proof", new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8436), "/docs/address3.pdf" },
                    { 4, 4, 4, "Business License", "License", new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8438), "/docs/license4.pdf" },
                    { 5, 5, 5, "Tax", "GST Certificate", new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8439), "/docs/gst5.pdf" },
                    { 6, 6, 6, "Insurance", "Insurance Proof", new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8441), "/docs/ins6.pdf" },
                    { 7, 7, 7, "Property", "Office Lease", new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8443), "/docs/lease7.pdf" },
                    { 8, 8, 8, "KYC", "PAN Proof", new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8444), "/docs/pan8.pdf" },
                    { 9, 9, 9, "Financial", "Audit Report", new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8446), "/docs/audit9.pdf" },
                    { 10, 10, 10, "Tax", "Tax Return", new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(8447), "/docs/tax10.pdf" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "BankUserId", "BeneficiaryId", "BeneficiaryId1", "ClientId", "PaymentDate", "Type", "status" },
                values: new object[,]
                {
                    { 1, 10000m, 1, 1, null, 1, new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(3151), 2, 1 },
                    { 2, 25000m, 2, 2, null, 2, new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5224), 0, 1 },
                    { 3, 5000m, 3, 3, null, 3, new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5227), 1, 0 },
                    { 4, 40000m, 4, 4, null, 4, new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5230), 0, 1 },
                    { 5, 12000m, 5, 5, null, 5, new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5233), 2, -1 },
                    { 6, 8700m, 6, 6, null, 6, new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5235), 1, 0 },
                    { 7, 65500m, 7, 7, null, 7, new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5237), 0, 1 },
                    { 8, 9200m, 8, 8, null, 8, new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5239), 2, 1 },
                    { 9, 11100m, 9, 9, null, 9, new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5240), 1, 0 },
                    { 10, 25000m, 10, 10, null, 10, new DateTime(2025, 11, 4, 7, 0, 37, 328, DateTimeKind.Utc).AddTicks(5318), 0, 1 }
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
                name: "IX_Payments_BeneficiaryId1",
                table: "Payments",
                column: "BeneficiaryId1");

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
