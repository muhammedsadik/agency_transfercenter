using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace agency_transfercenter.Migrations
{
    /// <inheritdoc />
    public partial class AddEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LineType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnitName = table.Column<string>(type: "text", nullable: false),
                    ManagerName = table.Column<string>(type: "text", nullable: false),
                    ManagerSurname = table.Column<string>(type: "text", nullable: false),
                    ManagerGsm = table.Column<string>(type: "text", nullable: false),
                    UnitPhone = table.Column<string>(type: "text", nullable: false),
                    UnitMail = table.Column<string>(type: "text", nullable: false),
                    ManagerMail = table.Column<string>(type: "text", nullable: false),
                    Address_City = table.Column<string>(type: "text", nullable: false),
                    Address_Street = table.Column<string>(type: "text", nullable: false),
                    Address_Number = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    TransferCenterId = table.Column<int>(type: "integer", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Units_TransferCenterId",
                        column: x => x.TransferCenterId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppStations",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "integer", nullable: false),
                    LineId = table.Column<int>(type: "integer", nullable: false),
                    StationNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppStations", x => new { x.UnitId, x.LineId });
                    table.ForeignKey(
                        name: "FK_AppStations_AppLines_LineId",
                        column: x => x.LineId,
                        principalTable: "AppLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppStations_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppLines_Name",
                table: "AppLines",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppStations_LineId",
                table: "AppStations",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_TransferCenterId",
                table: "Units",
                column: "TransferCenterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppStations");

            migrationBuilder.DropTable(
                name: "AppLines");

            migrationBuilder.DropTable(
                name: "Units");
        }
    }
}
