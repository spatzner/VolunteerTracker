using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerTracker.Migrations.Local.Migrations
{
    /// <inheritdoc />
    public partial class OrgCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Organizations_OrganizationId",
                schema: "Entities",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Organizations_OrganizationId",
                schema: "Entities",
                table: "Phones");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Organizations_OrganizationId",
                schema: "Entities",
                table: "Addresses",
                column: "OrganizationId",
                principalSchema: "Entities",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Organizations_OrganizationId",
                schema: "Entities",
                table: "Phones",
                column: "OrganizationId",
                principalSchema: "Entities",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Organizations_OrganizationId",
                schema: "Entities",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Organizations_OrganizationId",
                schema: "Entities",
                table: "Phones");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Organizations_OrganizationId",
                schema: "Entities",
                table: "Addresses",
                column: "OrganizationId",
                principalSchema: "Entities",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Organizations_OrganizationId",
                schema: "Entities",
                table: "Phones",
                column: "OrganizationId",
                principalSchema: "Entities",
                principalTable: "Organizations",
                principalColumn: "Id");
        }
    }
}
