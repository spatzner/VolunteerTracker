using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerTracker.Migrations.Local.Migrations
{
    /// <inheritdoc />
    public partial class OrgMainPhoneNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MainPhoneId",
                schema: "Entities",
                table: "Organizations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "Entities",
                table: "Organizations",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_MainPhoneId",
                schema: "Entities",
                table: "Organizations",
                column: "MainPhoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Phones_MainPhoneId",
                schema: "Entities",
                table: "Organizations",
                column: "MainPhoneId",
                principalSchema: "Entities",
                principalTable: "Phones",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Phones_MainPhoneId",
                schema: "Entities",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_MainPhoneId",
                schema: "Entities",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "MainPhoneId",
                schema: "Entities",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "Entities",
                table: "Organizations");
        }
    }
}
