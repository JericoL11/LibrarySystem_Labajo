using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem_Labajo.Migrations
{
    public partial class Penalty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Penalty",
                columns: table => new
                {

                    Penalty_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "400000, 1"),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Penalty_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSettled = table.Column<bool>(type: "bit", nullable: true),
                    P_details_Id = table.Column<int>(type: "int", nullable: true),
                    details_id = table.Column<int>(type: "int", nullable: true),
                    Penalty_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalty", x => x.Penalty_Id);
                    table.ForeignKey(
                        name: "FK_Penalty_Details_details_id",
                        column: x => x.details_id,
                        principalTable: "Details",
                        principalColumn: "details_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Penalty_details_id",
                table: "Penalty",
                column: "details_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Penalty");
        }
    }
}
