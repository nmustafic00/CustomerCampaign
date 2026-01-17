using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerCampaign.Migrations
{
    /// <inheritdoc />
    public partial class AddCampaignTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CampaignDate",
                table: "AgentRewardEntries",
                newName: "RewardDate");

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "AgentRewardEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DailyLimitPerAgent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentRewardEntries_CampaignId",
                table: "AgentRewardEntries",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentRewardEntries_Campaigns_CampaignId",
                table: "AgentRewardEntries",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentRewardEntries_Campaigns_CampaignId",
                table: "AgentRewardEntries");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_AgentRewardEntries_CampaignId",
                table: "AgentRewardEntries");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "AgentRewardEntries");

            migrationBuilder.RenameColumn(
                name: "RewardDate",
                table: "AgentRewardEntries",
                newName: "CampaignDate");
        }
    }
}
