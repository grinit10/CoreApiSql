using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Courses",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Schools",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Grades",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SchoolId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Grades_tbl_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "tbl_Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CourseGrade",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(nullable: false),
                    GradeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CourseGrade", x => new { x.CourseId, x.GradeId });
                    table.ForeignKey(
                        name: "FK_tbl_CourseGrade_tbl_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "tbl_Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_CourseGrade_tbl_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "tbl_Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Sections",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Strength = table.Column<int>(nullable: false),
                    GradeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Sections_tbl_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "tbl_Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Students",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    SectionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Students_tbl_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "tbl_Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CourseStudent",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CourseStudent", x => new { x.CourseId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_tbl_CourseStudent_tbl_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "tbl_Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_CourseStudent_tbl_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "tbl_Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CourseGrade_GradeId",
                table: "tbl_CourseGrade",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CourseStudent_StudentId",
                table: "tbl_CourseStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Grades_SchoolId",
                table: "tbl_Grades",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Sections_GradeId",
                table: "tbl_Sections",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Students_SectionId",
                table: "tbl_Students",
                column: "SectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_CourseGrade");

            migrationBuilder.DropTable(
                name: "tbl_CourseStudent");

            migrationBuilder.DropTable(
                name: "tbl_Courses");

            migrationBuilder.DropTable(
                name: "tbl_Students");

            migrationBuilder.DropTable(
                name: "tbl_Sections");

            migrationBuilder.DropTable(
                name: "tbl_Grades");

            migrationBuilder.DropTable(
                name: "tbl_Schools");
        }
    }
}
