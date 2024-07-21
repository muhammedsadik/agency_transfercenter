using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace agency_transfercenter.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserWithUnitId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "AbpUsers",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "AbpUsers");
        }
    }
}
