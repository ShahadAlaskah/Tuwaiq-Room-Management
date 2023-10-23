using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Ef.Migrations
{
    /// <inheritdoc />
    public partial class FixExamAdvancedOptionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamAdvancedOptions_FormTemplates_FormTemplateId",
                table: "ExamAdvancedOptions");

            migrationBuilder.DropIndex(
                name: "IX_ExamAdvancedOptions_FormTemplateId",
                table: "ExamAdvancedOptions");

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplates_ExamAdvancedOptionId",
                table: "FormTemplates",
                column: "ExamAdvancedOptionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplates_ExamAdvancedOptions_ExamAdvancedOptionId",
                table: "FormTemplates",
                column: "ExamAdvancedOptionId",
                principalTable: "ExamAdvancedOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplates_ExamAdvancedOptions_ExamAdvancedOptionId",
                table: "FormTemplates");

            migrationBuilder.DropIndex(
                name: "IX_FormTemplates_ExamAdvancedOptionId",
                table: "FormTemplates");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAdvancedOptions_FormTemplateId",
                table: "ExamAdvancedOptions",
                column: "FormTemplateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamAdvancedOptions_FormTemplates_FormTemplateId",
                table: "ExamAdvancedOptions",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
