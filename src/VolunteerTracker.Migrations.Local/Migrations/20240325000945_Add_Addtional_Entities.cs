using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerTracker.Migrations.Local.Migrations
{
    /// <inheritdoc />
    public partial class Add_Addtional_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_OrganizationId",
                schema: "Entities",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PersonId",
                schema: "Entities",
                table: "Addresses");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                schema: "Entities",
                table: "Organizations",
                type: "uuid",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "Emails",
                schema: "Entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Entities",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                schema: "Entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    Number = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Entities",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OrganizationId",
                schema: "Entities",
                table: "Addresses",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonId",
                schema: "Entities",
                table: "Addresses",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emails_PersonId",
                schema: "Entities",
                table: "Emails",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_PersonId",
                schema: "Entities",
                table: "Phones",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails",
                schema: "Entities");

            migrationBuilder.DropTable(
                name: "Phones",
                schema: "Entities");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_OrganizationId",
                schema: "Entities",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PersonId",
                schema: "Entities",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "Entities",
                table: "Organizations");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OrganizationId",
                schema: "Entities",
                table: "Addresses",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonId",
                schema: "Entities",
                table: "Addresses",
                column: "PersonId");
        }
    }
}
