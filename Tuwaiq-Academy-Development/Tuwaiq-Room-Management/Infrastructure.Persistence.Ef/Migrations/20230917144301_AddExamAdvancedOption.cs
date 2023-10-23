using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Ef.Migrations
{
    /// <inheritdoc />
    public partial class AddExamAdvancedOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExamAdvancedOptionId",
                table: "FormTemplates",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "ExamAdvancedOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FormTemplateId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Duration = table.Column<TimeOnly>(type: "time(6)", nullable: true),
                    RequireAuthentication = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    QuestionRandomize = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    QuestionPerPage = table.Column<int>(type: "int", nullable: false),
                    TimePerPage = table.Column<TimeOnly>(type: "time(6)", nullable: true),
                    CanGoBack = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAdvancedOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamAdvancedOptions_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAdvancedOptions_FormTemplateId",
                table: "ExamAdvancedOptions",
                column: "FormTemplateId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamAdvancedOptions");

            migrationBuilder.DropColumn(
                name: "ExamAdvancedOptionId",
                table: "FormTemplates");
        }
    }
}
