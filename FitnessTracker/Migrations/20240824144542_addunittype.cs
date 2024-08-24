using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Migrations
{
    public partial class addunittype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitType",
                table: "RunningActivity",
                newName: "DistanceUnitType");

            migrationBuilder.AddColumn<double>(
                name: "HeightUnitType",
                table: "User",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "WeightUnitType",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeightUnitType",
                table: "User");

            migrationBuilder.DropColumn(
                name: "WeightUnitType",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "DistanceUnitType",
                table: "RunningActivity",
                newName: "UnitType");
        }
    }
}
