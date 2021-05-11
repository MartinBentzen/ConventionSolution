using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositories.Migrations
{
    public partial class addedSpeakerToConvention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SpeakerId",
                table: "Conventions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conventions_SpeakerId",
                table: "Conventions",
                column: "SpeakerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conventions_User_SpeakerId",
                table: "Conventions",
                column: "SpeakerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conventions_User_SpeakerId",
                table: "Conventions");

            migrationBuilder.DropIndex(
                name: "IX_Conventions_SpeakerId",
                table: "Conventions");

            migrationBuilder.DropColumn(
                name: "SpeakerId",
                table: "Conventions");
        }
    }
}
