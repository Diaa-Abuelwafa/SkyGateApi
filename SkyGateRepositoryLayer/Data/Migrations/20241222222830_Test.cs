using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyGateRepositoryLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassporyNumber",
                table: "AspNetUsers",
                newName: "PassportNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassportNumber",
                table: "AspNetUsers",
                newName: "PassporyNumber");
        }
    }
}
