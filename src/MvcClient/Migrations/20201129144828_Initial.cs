using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcClient.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExecDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActionTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    Action = table.Column<int>(type: "INTEGER", nullable: false),
                    ExecUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetId = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetName = table.Column<string>(type: "TEXT", nullable: true),
                    TargetStatusName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbLogs_Users_ExecUserId",
                        column: x => x.ExecUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToDoTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RegisteredUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Scope = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoTasks_Users_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttachedFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ToDoTaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    FileUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachedFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttachedFiles_ToDoTasks_ToDoTaskId",
                        column: x => x.ToDoTaskId,
                        principalTable: "ToDoTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ToDoTaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    PostDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_ToDoTasks_ToDoTaskId",
                        column: x => x.ToDoTaskId,
                        principalTable: "ToDoTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JointUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToDoTaskId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JointUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JointUsers_ToDoTasks_ToDoTaskId",
                        column: x => x.ToDoTaskId,
                        principalTable: "ToDoTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JointUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttachedFiles_ToDoTaskId",
                table: "AttachedFiles",
                column: "ToDoTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ToDoTaskId",
                table: "Comments",
                column: "ToDoTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DbLogs_ExecUserId",
                table: "DbLogs",
                column: "ExecUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JointUsers_ToDoTaskId",
                table: "JointUsers",
                column: "ToDoTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_JointUsers_UserId_ToDoTaskId",
                table: "JointUsers",
                columns: new[] { "UserId", "ToDoTaskId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTasks_RegisteredUserId",
                table: "ToDoTasks",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttachedFiles");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "DbLogs");

            migrationBuilder.DropTable(
                name: "JointUsers");

            migrationBuilder.DropTable(
                name: "ToDoTasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
