﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounting.WebAPI.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LookupTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyNo = table.Column<int>(type: "int", maxLength: 4, nullable: true),
                    RegistrationCode = table.Column<int>(type: "int", maxLength: 4, nullable: true),
                    EconomicCode = table.Column<int>(type: "int", maxLength: 4, nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    BirthPlaceId = table.Column<int>(type: "int", nullable: true),
                    NationalityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Lookups_BirthPlaceId",
                        column: x => x.BirthPlaceId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_People_Lookups_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Lookups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cashes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RealPersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cashes_People_RealPersonId",
                        column: x => x.RealPersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocNo = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    DocTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Cashes_CashId",
                        column: x => x.CashId,
                        principalTable: "Cashes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_Lookups_DocTypeId",
                        column: x => x.DocTypeId,
                        principalTable: "Lookups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "LookupTypeId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "withdrawal" },
                    { 2, 1, "Payment" },
                    { 3, 2, "Iranian" },
                    { 4, 2, "Foreigner" },
                    { 5, 3, "Tehran" },
                    { 6, 3, "Karaj" },
                    { 7, 3, "Shiraz" },
                    { 8, 3, "Gilan" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Address", "Age", "BirthDate", "BirthPlaceId", "Discriminator", "Email", "FatherName", "FirstName", "LastName", "NationalCode", "NationalityId", "PhoneNumber" },
                values: new object[,]
                {
                    { 2, "Tehran", 20, new DateTime(1985, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "RealPerson", "tehraniAli480", "Kamal", "Ali", "Tehrani", "1234567890", 3, "09171619993" },
                    { 3, "Abadan", 15, new DateTime(1997, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "RealPerson", "sdlksdkvnksnv", "Ali", "Reza", "Bogari", "0440799996", 3, "09174856699" },
                    { 7, "Karaj", 45, new DateTime(1997, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "RealPerson", "tehranimohammad480", "Ali", "Ahmad Reza", "Tehrani", "4565654568", 3, "01478745454" },
                    { 1, "Karaj", 10, new DateTime(1997, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "RealPerson", "tehranimohammad480", "Ali", "Mohammad Reza", "Tehrani", "0440799996", 3, "09177973283" },
                    { 4, "Tehran", 47, new DateTime(1985, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "RealPerson", "tehraniAli480", "Kamal", "Mahyar", "Bogari", "2546845865", 3, "01478747879" },
                    { 8, "Tehran", 20, new DateTime(1985, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "RealPerson", "tehraniAli480", "Kamal", "Ali", "Tehrani", "1234567890", 3, "09171619993" },
                    { 5, "Karaj", 14, new DateTime(1997, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "RealPerson", "tehranimohammad480", "Ali", "Mohammad Reza", "Tehrani", "147569874", 3, "01478954789" },
                    { 9, "Karaj", 25, new DateTime(1788, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "RealPerson", "djjfgodf", "Parsa", "Mohammad Ali", "Tehrani", "1578947524", 4, "01478954758" },
                    { 6, "Tehran", 12, new DateTime(1985, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "RealPerson", "rggrg", "Kamal", "Ali", "Tehrani", "9898989745", 3, "01236987474" },
                    { 10, "Tehran", 16, new DateTime(1975, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "RealPerson", "tehraniAli480", "Akbar", "Asghar", "Bogari", "1475369514", 4, "09171619993" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cashes_RealPersonId",
                table: "Cashes",
                column: "RealPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CashId",
                table: "Documents",
                column: "CashId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocTypeId",
                table: "Documents",
                column: "DocTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_PersonId",
                table: "Documents",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_People_BirthPlaceId",
                table: "People",
                column: "BirthPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_People_NationalityId",
                table: "People",
                column: "NationalityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Cashes");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Lookups");
        }
    }
}
