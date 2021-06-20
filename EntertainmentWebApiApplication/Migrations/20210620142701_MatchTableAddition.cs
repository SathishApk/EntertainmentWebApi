using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntertainmentWebApiApplication.Migrations
{
    public partial class MatchTableAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamA_Id = table.Column<int>(type: "int", nullable: true),
                    TeamA_Score = table.Column<int>(type: "int", nullable: false),
                    TeamB_Id = table.Column<int>(type: "int", nullable: true),
                    TeamB_Score = table.Column<int>(type: "int", nullable: false),
                    Winner_Id = table.Column<int>(type: "int", nullable: true),
                    RecordCreatedDttm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TeamA_Id",
                        column: x => x.TeamA_Id,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TeamB_Id",
                        column: x => x.TeamB_Id,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Winner_Id",
                        column: x => x.Winner_Id,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamA_Id",
                table: "Matches",
                column: "TeamA_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamB_Id",
                table: "Matches",
                column: "TeamB_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Winner_Id",
                table: "Matches",
                column: "Winner_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
