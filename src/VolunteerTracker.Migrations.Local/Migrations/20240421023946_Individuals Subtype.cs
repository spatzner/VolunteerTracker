using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerTracker.Migrations.Local.Migrations
{
    /// <inheritdoc />
    public partial class IndividualsSubtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                schema: "Entities",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                schema: "Entities",
                table: "Addresses",
                newName: "IndividualId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_PersonId",
                schema: "Entities",
                table: "Addresses",
                newName: "IX_Addresses_IndividualId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "Entities",
                table: "Persons",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Persons_IndividualId",
                schema: "Entities",
                table: "Addresses",
                column: "IndividualId",
                principalSchema: "Entities",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Persons_IndividualId",
                schema: "Entities",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "Entities",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "IndividualId",
                schema: "Entities",
                table: "Addresses",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_IndividualId",
                schema: "Entities",
                table: "Addresses",
                newName: "IX_Addresses_PersonId");

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
    }
}
