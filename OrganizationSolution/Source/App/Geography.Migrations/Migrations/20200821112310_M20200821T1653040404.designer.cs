namespace Geography.Migrations.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;


    internal partial class M20200821T1653040404designer : Migration
    {
        /// <summary>
        /// The Up.
        /// </summary>
        /// <param name="migrationBuilder">The migrationBuilder<see cref="MigrationBuilder"/>.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Geography");

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "Geography",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    IsoCode = table.Column<string>(maxLength: 255, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                schema: "Geography",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    CountryId = table.Column<long>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Geography",
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_Code",
                schema: "Geography",
                table: "Country",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_Code",
                schema: "Geography",
                table: "State",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryId",
                schema: "Geography",
                table: "State",
                column: "CountryId");
        }

        /// <summary>
        /// The Down.
        /// </summary>
        /// <param name="migrationBuilder">The migrationBuilder<see cref="MigrationBuilder"/>.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "State",
                schema: "Geography");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "Geography");
        }
    }
}
