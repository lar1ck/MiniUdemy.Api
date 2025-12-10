using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniUdemy.Api.Migrations
{
    /// <inheritdoc />
    public partial class ApplyRestrictDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_AspNetUsers_InstructorId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_AspNetUsers_StudentId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Course_CourseId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonProgress_AspNetUsers_StudentId",
                table: "LessonProgress");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "LessonProgress",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonProgress_StudentId",
                table: "LessonProgress",
                newName: "IX_LessonProgress_AppUserId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Enrollment",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollment_StudentId",
                table: "Enrollment",
                newName: "IX_Enrollment_AppUserId");

            migrationBuilder.RenameColumn(
                name: "InstructorId",
                table: "Course",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Course_InstructorId",
                table: "Course",
                newName: "IX_Course_AppUserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LessonProgress",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedAt",
                table: "LessonProgress",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Enrollment",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Course",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_LessonProgress_UserId",
                table: "LessonProgress",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_UserId",
                table: "Enrollment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_UserId",
                table: "Course",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_AspNetUsers_AppUserId",
                table: "Course",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_AspNetUsers_UserId",
                table: "Course",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_AspNetUsers_AppUserId",
                table: "Enrollment",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_AspNetUsers_UserId",
                table: "Enrollment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Course_CourseId",
                table: "Enrollment",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonProgress_AspNetUsers_AppUserId",
                table: "LessonProgress",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonProgress_AspNetUsers_UserId",
                table: "LessonProgress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_AspNetUsers_AppUserId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_AspNetUsers_UserId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_AspNetUsers_AppUserId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_AspNetUsers_UserId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Course_CourseId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonProgress_AspNetUsers_AppUserId",
                table: "LessonProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonProgress_AspNetUsers_UserId",
                table: "LessonProgress");

            migrationBuilder.DropIndex(
                name: "IX_LessonProgress_UserId",
                table: "LessonProgress");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_UserId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Course_UserId",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "LessonProgress",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonProgress_AppUserId",
                table: "LessonProgress",
                newName: "IX_LessonProgress_StudentId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Enrollment",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollment_AppUserId",
                table: "Enrollment",
                newName: "IX_Enrollment_StudentId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Course",
                newName: "InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_Course_AppUserId",
                table: "Course",
                newName: "IX_Course_InstructorId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LessonProgress",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedAt",
                table: "LessonProgress",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Enrollment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_AspNetUsers_InstructorId",
                table: "Course",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_AspNetUsers_StudentId",
                table: "Enrollment",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Course_CourseId",
                table: "Enrollment",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonProgress_AspNetUsers_StudentId",
                table: "LessonProgress",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
