using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Classroom.Context.Migrations
{
    public partial class AddedUserTasktable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_AspNetUsers_UserId",
                table: "UserTask");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTask_Tasks_TaskId",
                table: "UserTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTask",
                table: "UserTask");

            migrationBuilder.RenameTable(
                name: "UserTask",
                newName: "UserTasks");

            migrationBuilder.RenameIndex(
                name: "IX_UserTask_UserId",
                table: "UserTasks",
                newName: "IX_UserTasks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTask_TaskId",
                table: "UserTasks",
                newName: "IX_UserTasks_TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTasks",
                table: "UserTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_AspNetUsers_UserId",
                table: "UserTasks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_Tasks_TaskId",
                table: "UserTasks",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_AspNetUsers_UserId",
                table: "UserTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_Tasks_TaskId",
                table: "UserTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTasks",
                table: "UserTasks");

            migrationBuilder.RenameTable(
                name: "UserTasks",
                newName: "UserTask");

            migrationBuilder.RenameIndex(
                name: "IX_UserTasks_UserId",
                table: "UserTask",
                newName: "IX_UserTask_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTasks_TaskId",
                table: "UserTask",
                newName: "IX_UserTask_TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTask",
                table: "UserTask",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_AspNetUsers_UserId",
                table: "UserTask",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTask_Tasks_TaskId",
                table: "UserTask",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
