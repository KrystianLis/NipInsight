using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NipInsight.Migrations
{
    /// <inheritdoc />
    public partial class AddEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Nip = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusVat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Regon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Krs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidenceAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkingAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationLegalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationDenialDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationDenialBasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestorationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RestorationBasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemovalBasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasVirtualAccounts = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Nip);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nip = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityAuthorizedClerk",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntityPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityAuthorizedClerk", x => new { x.EntityId, x.EntityPersonId });
                    table.ForeignKey(
                        name: "FK_EntityAuthorizedClerk_Company_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Company",
                        principalColumn: "Nip",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityAuthorizedClerk_Person_EntityPersonId",
                        column: x => x.EntityPersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityPartner",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntityPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityPartner", x => new { x.EntityId, x.EntityPersonId });
                    table.ForeignKey(
                        name: "FK_EntityPartner_Company_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Company",
                        principalColumn: "Nip",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityPartner_Person_EntityPersonId",
                        column: x => x.EntityPersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityRepresentative",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntityPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityRepresentative", x => new { x.EntityId, x.EntityPersonId });
                    table.ForeignKey(
                        name: "FK_EntityRepresentative_Company_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Company",
                        principalColumn: "Nip",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityRepresentative_Person_EntityPersonId",
                        column: x => x.EntityPersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityAuthorizedClerk_EntityPersonId",
                table: "EntityAuthorizedClerk",
                column: "EntityPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityPartner_EntityPersonId",
                table: "EntityPartner",
                column: "EntityPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityRepresentative_EntityPersonId",
                table: "EntityRepresentative",
                column: "EntityPersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityAuthorizedClerk");

            migrationBuilder.DropTable(
                name: "EntityPartner");

            migrationBuilder.DropTable(
                name: "EntityRepresentative");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
