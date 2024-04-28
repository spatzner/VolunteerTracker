using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerTracker.Migrations.Local.Migrations
{
    /// <inheritdoc />
    public partial class OrgPrimaryKeyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Phones_MainPhoneId",
                schema: "Entities",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Persons_PersonId",
                schema: "Entities",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_MainPhoneId",
                schema: "Entities",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "Entities",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "MainPhoneId",
                schema: "Entities",
                table: "Organizations");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                schema: "Entities",
                table: "Phones",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "Entities",
                table: "Phones",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phones_OrganizationId",
                schema: "Entities",
                table: "Phones",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Organizations_OrganizationId",
                schema: "Entities",
                table: "Phones",
                column: "OrganizationId",
                principalSchema: "Entities",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Persons_PersonId",
                schema: "Entities",
                table: "Phones",
                column: "PersonId",
                principalSchema: "Entities",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Organizations_OrganizationId",
                schema: "Entities",
                table: "Phones");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Persons_PersonId",
                schema: "Entities",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Phones_OrganizationId",
                schema: "Entities",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "Entities",
                table: "Phones");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                schema: "Entities",
                table: "Phones",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                schema: "Entities",
                table: "Organizations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MainPhoneId",
                schema: "Entities",
                table: "Organizations",
                type: "uuid",
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
    }
}
