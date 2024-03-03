using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerTracker.Migrations.Local.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Entity");

            migrationBuilder.EnsureSchema(
                name: "Donations");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "Entities");

            migrationBuilder.EnsureSchema(
                name: "Program");

            migrationBuilder.CreateTable(
                name: "ListTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "Entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Suffix = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                schema: "Program",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListValues",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ListTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListValues_ListTypes_ListTypeId",
                        column: x => x.ListTypeId,
                        principalSchema: "dbo",
                        principalTable: "ListTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "Entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ContactId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Persons_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "Entities",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "Entity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Line1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Line2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Zip = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "Entities",
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Entities",
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Donors",
                schema: "Donations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donors_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "Entities",
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Donors_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Entities",
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                schema: "Donations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    DonorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_Donors_DonorId",
                        column: x => x.DonorId,
                        principalSchema: "Donations",
                        principalTable: "Donors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Campaigns_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "Entities",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonationSchedules",
                schema: "Donations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DonorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonationSchedules_Donors_DonorId",
                        column: x => x.DonorId,
                        principalSchema: "Donations",
                        principalTable: "Donors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                schema: "Donations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    DonorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: true),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalSchema: "Donations",
                        principalTable: "Campaigns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Donations_Donors_DonorId",
                        column: x => x.DonorId,
                        principalSchema: "Donations",
                        principalTable: "Donors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OrganizationId",
                schema: "Entity",
                table: "Addresses",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonId",
                schema: "Entity",
                table: "Addresses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_DonorId",
                schema: "Donations",
                table: "Campaigns",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_OrganizationId",
                schema: "Donations",
                table: "Campaigns",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_CampaignId",
                schema: "Donations",
                table: "Donations",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonorId",
                schema: "Donations",
                table: "Donations",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationSchedules_DonorId",
                schema: "Donations",
                table: "DonationSchedules",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_OrganizationId",
                schema: "Donations",
                table: "Donors",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_PersonId",
                schema: "Donations",
                table: "Donors",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ListValues_ListTypeId",
                schema: "dbo",
                table: "ListValues",
                column: "ListTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_ContactId",
                schema: "Entities",
                table: "Organizations",
                column: "ContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "Entity");

            migrationBuilder.DropTable(
                name: "Donations",
                schema: "Donations");

            migrationBuilder.DropTable(
                name: "DonationSchedules",
                schema: "Donations");

            migrationBuilder.DropTable(
                name: "ListValues",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Programs",
                schema: "Program");

            migrationBuilder.DropTable(
                name: "Campaigns",
                schema: "Donations");

            migrationBuilder.DropTable(
                name: "ListTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Donors",
                schema: "Donations");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "Entities");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "Entities");
        }
    }
}
