using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Ef.Migrations
{
    /// <inheritdoc />
    public partial class connectFormTemplateWithQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplateQuestions_FormTemplates_FormTemplateId",
                table: "FormTemplateQuestions");

            migrationBuilder.AlterColumn<Guid>(
                name: "FormTemplateId",
                table: "FormTemplateQuestions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplateQuestions_FormTemplates_FormTemplateId",
                table: "FormTemplateQuestions",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplateQuestions_FormTemplates_FormTemplateId",
                table: "FormTemplateQuestions");

            migrationBuilder.AlterColumn<Guid>(
                name: "FormTemplateId",
                table: "FormTemplateQuestions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplateQuestions_FormTemplates_FormTemplateId",
                table: "FormTemplateQuestions",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "Id");
        }
    }
}
