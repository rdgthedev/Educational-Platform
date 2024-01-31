using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigurationToIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseModelUserModel_CourseModel_CoursesId",
                table: "CourseModelUserModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseModel",
                table: "CourseModel");

            migrationBuilder.RenameTable(
                name: "CourseModel",
                newName: "Course");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModelUserModel_Course_CoursesId",
                table: "CourseModelUserModel",
                column: "CoursesId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseModelUserModel_Course_CoursesId",
                table: "CourseModelUserModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "CourseModel");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseModel",
                table: "CourseModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModelUserModel_CourseModel_CoursesId",
                table: "CourseModelUserModel",
                column: "CoursesId",
                principalTable: "CourseModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
