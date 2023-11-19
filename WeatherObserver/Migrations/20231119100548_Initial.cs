using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherObserver.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "weather",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weather", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    RelativeHumidity = table.Column<double>(type: "float", nullable: false),
                    DewPoint = table.Column<double>(type: "float", nullable: false),
                    AtmosphericPressure = table.Column<double>(type: "float", nullable: false),
                    WindDirection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WindSpeed = table.Column<double>(type: "float", nullable: false),
                    CloudCover = table.Column<double>(type: "float", nullable: false),
                    LowerСloudLimit = table.Column<double>(type: "float", nullable: false),
                    HorizontalVisibility = table.Column<double>(type: "float", nullable: false),
                    WeatherPhenomena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeatherExcelInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherModel_weather_WeatherExcelInfoId",
                        column: x => x.WeatherExcelInfoId,
                        principalTable: "weather",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherModel_WeatherExcelInfoId",
                table: "WeatherModel",
                column: "WeatherExcelInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherModel");

            migrationBuilder.DropTable(
                name: "weather");
        }
    }
}
