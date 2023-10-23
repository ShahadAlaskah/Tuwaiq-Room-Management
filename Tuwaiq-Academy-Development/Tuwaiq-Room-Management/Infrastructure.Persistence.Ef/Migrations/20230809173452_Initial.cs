using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Ef.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Event = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Source = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubjectIdentifier = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubjectName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubjectType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubjectAdditionalData = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Action = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TableName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OldValues = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NewValues = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AffectedColumns = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrimaryKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AuditView",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Args = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tables = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditView", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DataProtectionKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FriendlyName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Xml = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProtectionKeys", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FormTemplateCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
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
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplateSections", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QRTZ_CALENDARS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(type: "varchar(120)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CALENDAR_NAME = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CALENDAR = table.Column<byte[]>(type: "blob", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_CALENDARS", x => new { x.SCHED_NAME, x.CALENDAR_NAME });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QRTZ_FIRED_TRIGGERS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(type: "varchar(120)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ENTRY_ID = table.Column<string>(type: "varchar(140)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRIGGER_NAME = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRIGGER_GROUP = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    INSTANCE_NAME = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FIRED_TIME = table.Column<long>(type: "bigint(19)", nullable: false),
                    SCHED_TIME = table.Column<long>(type: "bigint(19)", nullable: false),
                    PRIORITY = table.Column<int>(type: "integer", nullable: false),
                    STATE = table.Column<string>(type: "varchar(16)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JOB_NAME = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JOB_GROUP = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IS_NONCONCURRENT = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    REQUESTS_RECOVERY = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_FIRED_TRIGGERS", x => new { x.SCHED_NAME, x.ENTRY_ID });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QRTZ_JOB_DETAILS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(type: "varchar(120)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JOB_NAME = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JOB_GROUP = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DESCRIPTION = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JOB_CLASS_NAME = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IS_DURABLE = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IS_NONCONCURRENT = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IS_UPDATE_DATA = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    REQUESTS_RECOVERY = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    JOB_DATA = table.Column<byte[]>(type: "blob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_JOB_DETAILS", x => new { x.SCHED_NAME, x.JOB_NAME, x.JOB_GROUP });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QRTZ_LOCKS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(type: "varchar(120)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LOCK_NAME = table.Column<string>(type: "varchar(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_LOCKS", x => new { x.SCHED_NAME, x.LOCK_NAME });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QRTZ_PAUSED_TRIGGER_GRPS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(type: "varchar(120)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRIGGER_GROUP = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_PAUSED_TRIGGER_GRPS", x => new { x.SCHED_NAME, x.TRIGGER_GROUP });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QRTZ_SCHEDULER_STATE",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(type: "varchar(120)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    INSTANCE_NAME = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LAST_CHECKIN_TIME = table.Column<long>(type: "bigint(19)", nullable: false),
                    CHECKIN_INTERVAL = table.Column<long>(type: "bigint(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_SCHEDULER_STATE", x => new { x.SCHED_NAME, x.INSTANCE_NAME });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FormTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PublishState = table.Column<int>(type: "int", nullable: false),
                    FormTemplateCategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormTemplates_FormTemplateCategories_FormTemplateCategoryId",
                        column: x => x.FormTemplateCategoryId,
                        principalTable: "FormTemplateCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QRTZ_TRIGGERS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(type: "varchar(120)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRIGGER_NAME = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRIGGER_GROUP = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JOB_NAME = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JOB_GROUP = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DESCRIPTION = table.Column<string>(type: "varchar(250)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NEXT_FIRE_TIME = table.Column<long>(type: "bigint(19)", nullable: true),
                    PREV_FIRE_TIME = table.Column<long>(type: "bigint(19)", nullable: true),
                    PRIORITY = table.Column<int>(type: "integer", nullable: true),
                    TRIGGER_STATE = table.Column<string>(type: "varchar(16)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRIGGER_TYPE = table.Column<string>(type: "varchar(8)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    START_TIME = table.Column<long>(type: "bigint(19)", nullable: false),
                    END_TIME = table.Column<long>(type: "bigint(19)", nullable: true),
                    CALENDAR_NAME = table.Column<string>(type: "varchar(200)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MISFIRE_INSTR = table.Column<short>(type: "smallint(2)", nullable: true),
                    JOB_DATA = table.Column<byte[]>(type: "blob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_TRIGGERS", x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP });
                    table.ForeignKey(
                        name: "FK_QRTZ_TRIGGERS_QRTZ_JOB_DETAILS_SCHED_NAME_JOB_NAME_JOB_GROUP",
                        columns: x => new { x.SCHED_NAME, x.JOB_NAME, x.JOB_GROUP },
                        principalTable: "QRTZ_JOB_DETAILS",
                        principalColumns: new[] { "SCHED_NAME", "JOB_NAME", "JOB_GROUP" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FormTemplateQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsRequired = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ShowCondition = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuestionType = table.Column<int>(type: "int", nullable: false),
                    FormTemplateSectionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    FormTemplateId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
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
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QRTZ_BLOB_TRIGGERS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(type: "varchar(120)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRIGGER_NAME = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRIGGER_GROUP = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BLOB_DATA = table.Column<byte[]>(type: "blob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_BLOB_TRIGGERS", x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP });
                    table.ForeignKey(
                        name: "FK_QRTZ_BLOB_TRIGGERS_QRTZ_TRIGGERS_SCHED_NAME_TRIGGER_NAME_TRI~",
                        columns: x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP },
                        principalTable: "QRTZ_TRIGGERS",
                        principalColumns: new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QRTZ_CRON_TRIGGERS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(type: "varchar(120)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRIGGER_NAME = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRIGGER_GROUP = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CRON_EXPRESSION = table.Column<string>(type: "varchar(120)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TIME_ZONE_ID = table.Column<string>(type: "varchar(80)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_CRON_TRIGGERS", x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP });
                    table.ForeignKey(
                        name: "FK_QRTZ_CRON_TRIGGERS_QRTZ_TRIGGERS_SCHED_NAME_TRIGGER_NAME_TRI~",
                        columns: x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP },
                        principalTable: "QRTZ_TRIGGERS",
                        principalColumns: new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QRTZ_SIMPLE_TRIGGERS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(type: "varchar(120)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRIGGER_NAME = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRIGGER_GROUP = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    REPEAT_COUNT = table.Column<long>(type: "bigint(7)", nullable: false),
                    REPEAT_INTERVAL = table.Column<long>(type: "bigint(12)", nullable: false),
                    TIMES_TRIGGERED = table.Column<long>(type: "bigint(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_SIMPLE_TRIGGERS", x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP });
                    table.ForeignKey(
                        name: "FK_QRTZ_SIMPLE_TRIGGERS_QRTZ_TRIGGERS_SCHED_NAME_TRIGGER_NAME_T~",
                        columns: x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP },
                        principalTable: "QRTZ_TRIGGERS",
                        principalColumns: new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QRTZ_SIMPROP_TRIGGERS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(type: "varchar(120)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRIGGER_NAME = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRIGGER_GROUP = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    STR_PROP_1 = table.Column<string>(type: "varchar(512)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    STR_PROP_2 = table.Column<string>(type: "varchar(512)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    STR_PROP_3 = table.Column<string>(type: "varchar(512)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    INT_PROP_1 = table.Column<int>(type: "int", nullable: true),
                    INT_PROP_2 = table.Column<int>(type: "int", nullable: true),
                    LONG_PROP_1 = table.Column<long>(type: "BIGINT", nullable: true),
                    LONG_PROP_2 = table.Column<long>(type: "BIGINT", nullable: true),
                    DEC_PROP_1 = table.Column<decimal>(type: "NUMERIC(13,4)", nullable: true),
                    DEC_PROP_2 = table.Column<decimal>(type: "NUMERIC(13,4)", nullable: true),
                    BOOL_PROP_1 = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    BOOL_PROP_2 = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    TIME_ZONE_ID = table.Column<string>(type: "varchar(80)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_SIMPROP_TRIGGERS", x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP });
                    table.ForeignKey(
                        name: "FK_QRTZ_SIMPROP_TRIGGERS_QRTZ_TRIGGERS_SCHED_NAME_TRIGGER_NAME_~",
                        columns: x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP },
                        principalTable: "QRTZ_TRIGGERS",
                        principalColumns: new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionOptionDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Label = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlaceHolder = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MinDate = table.Column<DateOnly>(type: "date", nullable: true),
                    MaxDate = table.Column<DateOnly>(type: "date", nullable: true),
                    MinTime = table.Column<TimeOnly>(type: "time(6)", nullable: true),
                    MaxTime = table.Column<TimeOnly>(type: "time(6)", nullable: true),
                    MinToday = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    MaxToday = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    DefaultValue = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AllowTime = table.Column<bool>(type: "tinyint(1)", nullable: false)
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
                    Label = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlaceHolder = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileAcceptType = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    MaxSizeInMb = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    MaxFileCount = table.Column<int>(type: "int", nullable: false)
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
                    Label = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Options = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefaultValues = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: true)
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
                    Label = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlaceHolder = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Min = table.Column<double>(type: "double", nullable: true),
                    Max = table.Column<double>(type: "double", nullable: true),
                    Step = table.Column<double>(type: "double", nullable: true),
                    DefaultValue = table.Column<double>(type: "double", nullable: true),
                    AllowDecimal = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    AllowNegative = table.Column<bool>(type: "tinyint(1)", nullable: true)
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
                    SingleChoiceType = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Options = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefaultValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                    Label = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Steps = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefaultValue = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
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
                    Label = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlaceHolder = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MinLength = table.Column<int>(type: "int", nullable: true),
                    MaxLength = table.Column<int>(type: "int", nullable: true),
                    DefaultValue = table.Column<string>(type: "longtext", nullable: true)
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
                name: "IX_FormTemplates_FormTemplateCategoryId",
                table: "FormTemplates",
                column: "FormTemplateCategoryId");

            migrationBuilder.CreateIndex(
                name: "IDX_QRTZ_FT_JOB_GROUP",
                table: "QRTZ_FIRED_TRIGGERS",
                column: "JOB_GROUP");

            migrationBuilder.CreateIndex(
                name: "IDX_QRTZ_FT_JOB_NAME",
                table: "QRTZ_FIRED_TRIGGERS",
                column: "JOB_NAME");

            migrationBuilder.CreateIndex(
                name: "IDX_QRTZ_FT_JOB_REQ_RECOVERY",
                table: "QRTZ_FIRED_TRIGGERS",
                column: "REQUESTS_RECOVERY");

            migrationBuilder.CreateIndex(
                name: "IDX_QRTZ_FT_TRIG_GROUP",
                table: "QRTZ_FIRED_TRIGGERS",
                column: "TRIGGER_GROUP");

            migrationBuilder.CreateIndex(
                name: "IDX_QRTZ_FT_TRIG_INST_NAME",
                table: "QRTZ_FIRED_TRIGGERS",
                column: "INSTANCE_NAME");

            migrationBuilder.CreateIndex(
                name: "IDX_QRTZ_FT_TRIG_NAME",
                table: "QRTZ_FIRED_TRIGGERS",
                column: "TRIGGER_NAME");

            migrationBuilder.CreateIndex(
                name: "IDX_QRTZ_FT_TRIG_NM_GP",
                table: "QRTZ_FIRED_TRIGGERS",
                columns: new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP" });

            migrationBuilder.CreateIndex(
                name: "IDX_QRTZ_J_REQ_RECOVERY",
                table: "QRTZ_JOB_DETAILS",
                column: "REQUESTS_RECOVERY");

            migrationBuilder.CreateIndex(
                name: "IDX_QRTZ_T_NEXT_FIRE_TIME",
                table: "QRTZ_TRIGGERS",
                column: "NEXT_FIRE_TIME");

            migrationBuilder.CreateIndex(
                name: "IDX_QRTZ_T_NFT_ST",
                table: "QRTZ_TRIGGERS",
                columns: new[] { "NEXT_FIRE_TIME", "TRIGGER_STATE" });

            migrationBuilder.CreateIndex(
                name: "IDX_QRTZ_T_STATE",
                table: "QRTZ_TRIGGERS",
                column: "TRIGGER_STATE");

            migrationBuilder.CreateIndex(
                name: "IX_QRTZ_TRIGGERS_SCHED_NAME_JOB_NAME_JOB_GROUP",
                table: "QRTZ_TRIGGERS",
                columns: new[] { "SCHED_NAME", "JOB_NAME", "JOB_GROUP" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "AuditView");

            migrationBuilder.DropTable(
                name: "DataProtectionKeys");

            migrationBuilder.DropTable(
                name: "QRTZ_BLOB_TRIGGERS");

            migrationBuilder.DropTable(
                name: "QRTZ_CALENDARS");

            migrationBuilder.DropTable(
                name: "QRTZ_CRON_TRIGGERS");

            migrationBuilder.DropTable(
                name: "QRTZ_FIRED_TRIGGERS");

            migrationBuilder.DropTable(
                name: "QRTZ_LOCKS");

            migrationBuilder.DropTable(
                name: "QRTZ_PAUSED_TRIGGER_GRPS");

            migrationBuilder.DropTable(
                name: "QRTZ_SCHEDULER_STATE");

            migrationBuilder.DropTable(
                name: "QRTZ_SIMPLE_TRIGGERS");

            migrationBuilder.DropTable(
                name: "QRTZ_SIMPROP_TRIGGERS");

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
                name: "QRTZ_TRIGGERS");

            migrationBuilder.DropTable(
                name: "FormTemplateQuestions");

            migrationBuilder.DropTable(
                name: "QRTZ_JOB_DETAILS");

            migrationBuilder.DropTable(
                name: "FormTemplateSections");

            migrationBuilder.DropTable(
                name: "FormTemplates");

            migrationBuilder.DropTable(
                name: "FormTemplateCategories");
        }
    }
}
