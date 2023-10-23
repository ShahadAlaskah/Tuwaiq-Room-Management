using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Ef.Migrations
{
    /// <inheritdoc />
    public partial class i : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionOptionDates");

            migrationBuilder.DropTable(
                name: "QuestionOptionFiles");

            migrationBuilder.DropTable(
                name: "QuestionOptionMultipleChoices");

            migrationBuilder.DropTable(
                name: "QuestionOptionNumbers");

            migrationBuilder.DropTable(
                name: "QuestionOptionSingleChoices");

            migrationBuilder.DropTable(
                name: "QuestionOptionSliders");

            migrationBuilder.DropTable(
                name: "QuestionOptionTexts");

            migrationBuilder.DropTable(
                name: "FormTemplateQuestions");

            migrationBuilder.DropTable(
                name: "FormTemplateSections");

            migrationBuilder.DropTable(
                name: "FormTemplates");

            migrationBuilder.DropTable(
                name: "ExamAdvancedOptions");

            migrationBuilder.DropTable(
                name: "FormTemplateCategories");

            migrationBuilder.CreateTable(
                name: "AssetType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Icon = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Floor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floor", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoomType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoomTypeEnum = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FloorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BuildingId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoomTypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    IsUnderMaintenance = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Room_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Room_Floor_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Room_RoomType_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Code = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoomId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    AssetTypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    InstalledDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AddedOn = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    RoomId1 = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asset_AssetType_AssetTypeId",
                        column: x => x.AssetTypeId,
                        principalTable: "AssetType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asset_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Asset_Room_RoomId1",
                        column: x => x.RoomId1,
                        principalTable: "Room",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AssetTypeId",
                table: "Asset",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_RoomId",
                table: "Asset",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_RoomId1",
                table: "Asset",
                column: "RoomId1");

            migrationBuilder.CreateIndex(
                name: "IX_Room_BuildingId",
                table: "Room",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_FloorId",
                table: "Room",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_RoomTypeId",
                table: "Room",
                column: "RoomTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "AssetType");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Floor");

            migrationBuilder.DropTable(
                name: "RoomType");

            migrationBuilder.CreateTable(
                name: "ExamAdvancedOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CanGoBack = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Duration = table.Column<TimeOnly>(type: "time(6)", nullable: true),
                    FormTemplateId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    QuestionPerPage = table.Column<int>(type: "int", nullable: false),
                    QuestionRandomize = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RequireAuthentication = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TimePerPage = table.Column<TimeOnly>(type: "time(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAdvancedOptions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FormTemplateCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CategoryType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplateCategories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FormTemplateSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplateSections", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FormTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ExamAdvancedOptionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    FormTemplateCategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PublishState = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormTemplates_ExamAdvancedOptions_ExamAdvancedOptionId",
                        column: x => x.ExamAdvancedOptionId,
                        principalTable: "ExamAdvancedOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormTemplates_FormTemplateCategories_FormTemplateCategoryId",
                        column: x => x.FormTemplateCategoryId,
                        principalTable: "FormTemplateCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FormTemplateQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FormTemplateId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FormTemplateSectionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CorrectAnswer = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FieldName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsRequired = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    QuestionType = table.Column<int>(type: "int", nullable: false),
                    ShowCondition = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplateQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormTemplateQuestions_FormTemplateSections_FormTemplateSecti~",
                        column: x => x.FormTemplateSectionId,
                        principalTable: "FormTemplateSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormTemplateQuestions_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionOptionDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AllowTime = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DefaultValue = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Label = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaxDate = table.Column<DateOnly>(type: "date", nullable: true),
                    MaxTime = table.Column<TimeOnly>(type: "time(6)", nullable: true),
                    MaxToday = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    MinDate = table.Column<DateOnly>(type: "date", nullable: true),
                    MinTime = table.Column<TimeOnly>(type: "time(6)", nullable: true),
                    MinToday = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    PlaceHolder = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptionDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptionDates_FormTemplateQuestions_Id",
                        column: x => x.Id,
                        principalTable: "FormTemplateQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionOptionFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FileAcceptType = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Label = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaxFileCount = table.Column<int>(type: "int", nullable: false),
                    MaxSizeInMb = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    PlaceHolder = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptionFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptionFiles_FormTemplateQuestions_Id",
                        column: x => x.Id,
                        principalTable: "FormTemplateQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionOptionMultipleChoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DefaultValues = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Label = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Options = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptionMultipleChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptionMultipleChoices_FormTemplateQuestions_Id",
                        column: x => x.Id,
                        principalTable: "FormTemplateQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionOptionNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AllowDecimal = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    AllowNegative = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    DefaultValue = table.Column<double>(type: "double", nullable: true),
                    Label = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Max = table.Column<double>(type: "double", nullable: true),
                    Min = table.Column<double>(type: "double", nullable: true),
                    PlaceHolder = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Step = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptionNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptionNumbers_FormTemplateQuestions_Id",
                        column: x => x.Id,
                        principalTable: "FormTemplateQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionOptionSingleChoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DefaultValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Label = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Options = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SingleChoiceType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptionSingleChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptionSingleChoices_FormTemplateQuestions_Id",
                        column: x => x.Id,
                        principalTable: "FormTemplateQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionOptionSliders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DefaultValue = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Label = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Steps = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptionSliders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptionSliders_FormTemplateQuestions_Id",
                        column: x => x.Id,
                        principalTable: "FormTemplateQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionOptionTexts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DefaultValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Label = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaxLength = table.Column<int>(type: "int", nullable: true),
                    MinLength = table.Column<int>(type: "int", nullable: true),
                    PlaceHolder = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TextType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptionTexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptionTexts_FormTemplateQuestions_Id",
                        column: x => x.Id,
                        principalTable: "FormTemplateQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplateQuestions_FormTemplateId",
                table: "FormTemplateQuestions",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplateQuestions_FormTemplateSectionId",
                table: "FormTemplateQuestions",
                column: "FormTemplateSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplates_ExamAdvancedOptionId",
                table: "FormTemplates",
                column: "ExamAdvancedOptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplates_FormTemplateCategoryId",
                table: "FormTemplates",
                column: "FormTemplateCategoryId");
        }
    }
}
