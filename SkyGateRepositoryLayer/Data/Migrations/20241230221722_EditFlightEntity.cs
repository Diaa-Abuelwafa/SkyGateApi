using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyGateRepositoryLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditFlightEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalDate",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DepartureDate",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Flights");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalDateTime",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureDateTime",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalDateTime",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DepartureDateTime",
                table: "Flights");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ArrivalDate",
                table: "Flights",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "ArrivalTime",
                table: "Flights",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DepartureDate",
                table: "Flights",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "DepartureTime",
                table: "Flights",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
