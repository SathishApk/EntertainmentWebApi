using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntertainmentWebApiApplication.Migrations
{
    public partial class TableUpdation : Migration
    {
        private const string Nlog_Migration = @"Migrations\Scripts\NlogScript.sql";
        private const string View_Migration = @"Migrations\Scripts\ResultsView.sql";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDttm",
                table: "Teams",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDttm",
                table: "Matches",
                type: "datetime2",
                nullable: true);

            var sqlpath = Path.Combine(AppContext.BaseDirectory, Nlog_Migration);
            migrationBuilder.Sql(File.ReadAllText(sqlpath));

            var sqlpath2 = Path.Combine(AppContext.BaseDirectory, View_Migration);
            migrationBuilder.Sql(File.ReadAllText(sqlpath2));

            migrationBuilder.Sql("INSERT INTO [dbo].[Teams]([TeamName],[Country] ,[CreatedBy] ,[CreatedDttm] ,[LastUpdatedDttm]) VALUES ('CSK' ,'India'  ,'Dhoni' ,GETUTCDATE() ,null)");
            migrationBuilder.Sql("INSERT INTO[dbo].[Teams]([TeamName],[Country],[CreatedBy],[CreatedDttm],[LastUpdatedDttm]) VALUES('KKR', 'India', 'Dhawan', GETUTCDATE(), null)");
            migrationBuilder.Sql("INSERT INTO[dbo].[Teams]([TeamName],[Country],[CreatedBy],[CreatedDttm],[LastUpdatedDttm]) VALUES('MI', 'India', 'Sachin', GETUTCDATE(), null)");


            migrationBuilder.Sql("INSERT INTO [dbo].[Matches]([MatchLocation],[TeamA_Id],[TeamA_Score] ,[TeamB_Id],[TeamB_Score],[Winner_Id],[RecordCreatedDttm],[LastUpdatedDttm])VALUES('Pune',1 ,180,2,160,1,GETUTCDATE(),null)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Matches]([MatchLocation],[TeamA_Id],[TeamA_Score] ,[TeamB_Id],[TeamB_Score],[Winner_Id],[RecordCreatedDttm],[LastUpdatedDttm])VALUES('Pune',2 ,180,3,200,3,GETUTCDATE(),null)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Matches]([MatchLocation],[TeamA_Id],[TeamA_Score] ,[TeamB_Id],[TeamB_Score],[Winner_Id],[RecordCreatedDttm],[LastUpdatedDttm])VALUES('Pune',1 ,180,3,180,null,GETUTCDATE(),null)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedDttm",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDttm",
                table: "Matches");
        }
    }
}
