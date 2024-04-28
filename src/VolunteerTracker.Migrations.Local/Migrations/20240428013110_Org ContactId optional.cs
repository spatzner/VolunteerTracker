using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerTracker.Migrations.Local.Migrations
{
    /// <inheritdoc />
    public partial class OrgContactIdoptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Persons_ContactId",
                schema: "Entities",
                table: "Organizations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactId",
                schema: "Entities",
                table: "Organizations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Persons_ContactId",
                schema: "Entities",
                table: "Organizations",
                column: "ContactId",
                principalSchema: "Entities",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Persons_ContactId",
                schema: "Entities",
                table: "Organizations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactId",
                schema: "Entities",
                table: "Organizations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Persons_ContactId",
                schema: "Entities",
                table: "Organizations",
                column: "ContactId",
                principalSchema: "Entities",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
