using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerTracker.Migrations.Local.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeletes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                schema: "Entities",
                table: "Addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                schema: "Entities",
                table: "Addresses",
                column: "PersonId",
                principalSchema: "Entities",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                schema: "Entities",
                table: "Addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                schema: "Entities",
                table: "Addresses",
                column: "PersonId",
                principalSchema: "Entities",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
