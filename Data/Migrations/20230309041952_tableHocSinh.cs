using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    public partial class tableHocSinh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LopHoc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaLopHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenLop = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHoc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HocSinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaSV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoVaTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LopHocId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocSinh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HocSinh_LopHoc_LopHocId",
                        column: x => x.LopHocId,
                        principalTable: "LopHoc",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HocSinh_LopHocId",
                table: "HocSinh",
                column: "LopHocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HocSinh");

            migrationBuilder.DropTable(
                name: "LopHoc");
        }
    }
}
