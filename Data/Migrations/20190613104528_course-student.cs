using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class coursestudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "tbl_CourseStudent",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "tbl_CourseStudent",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "tbl_CourseStudent",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "tbl_CourseStudent",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "tbl_CourseStudent",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "tbl_CourseStudent");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "tbl_CourseStudent");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "tbl_CourseStudent");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "tbl_CourseStudent");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "tbl_CourseStudent");
        }
    }
}
