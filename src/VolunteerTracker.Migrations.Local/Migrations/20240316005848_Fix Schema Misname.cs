using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerTracker.Migrations.Local.Migrations
{
    /// <inheritdoc />
    public partial class FixSchemaMisname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Addresses",
                schema: "Entity",
                newName: "Addresses",
                newSchema: "Entities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Entity");

            migrationBuilder.RenameTable(
                name: "Addresses",
                schema: "Entities",
                newName: "Addresses",
                newSchema: "Entity");
        }
    }
}
