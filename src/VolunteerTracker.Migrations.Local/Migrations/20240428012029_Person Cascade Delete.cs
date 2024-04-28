using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerTracker.Migrations.Local.Migrations
{
    /// <inheritdoc />
    public partial class PersonCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Persons_PersonId",
                schema: "Entities",
                table: "Phones");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Persons_PersonId",
                schema: "Entities",
                table: "Phones",
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
                name: "FK_Phones_Persons_PersonId",
                schema: "Entities",
                table: "Phones");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Persons_PersonId",
                schema: "Entities",
                table: "Phones",
                column: "PersonId",
                principalSchema: "Entities",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
