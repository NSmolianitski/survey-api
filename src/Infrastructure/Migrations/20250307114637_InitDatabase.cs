using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    SurveyId = table.Column<int>(type: "integer", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CurrentStep = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interviews_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    SurveyId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InterviewId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    AnswerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Interviews_InterviewId",
                        column: x => x.InterviewId,
                        principalTable: "Interviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Surveys",
                columns: new[] { "Id", "PublicId", "Title" },
                values: new object[] { 1, new Guid("582f970f-b09f-4db7-9c97-85fb3b55c353"), "Test Survey" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "OrderId", "PublicId", "SurveyId", "Text" },
                values: new object[,]
                {
                    { 1, 0, new Guid("1ff64650-26ac-4785-8336-f2eb141becb3"), 1, "Какой тип отдыха вам больше нравится?" },
                    { 2, 1, new Guid("3b6af6e1-159c-4c6b-8e59-2e1f857b8f50"), 1, "Какой напиток вы предпочитаете по утрам?" },
                    { 3, 3, new Guid("67d5f087-66d8-4de6-a336-b96c7492da02"), 1, "Какие жанры фильмов вам нравятся?" }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "PublicId", "QuestionId", "Value" },
                values: new object[,]
                {
                    { 1, new Guid("6011df91-28cb-4b7d-90a4-2cad8780cb47"), 1, "Пляжный отдых" },
                    { 2, new Guid("62137fd5-aa19-42b0-99d2-01546adc920d"), 1, "Походы и природа" },
                    { 3, new Guid("a49a6a03-8f2e-4135-8f27-24ce32c09bb5"), 1, "Городские экскурсии" },
                    { 4, new Guid("a157c9d1-c162-4b2f-9d62-1861fc5d19bb"), 2, "Кофе" },
                    { 5, new Guid("95921471-615e-4e3e-8c9e-311fe9731763"), 2, "Чай" },
                    { 6, new Guid("9862a466-1873-4e41-b878-f0c156513596"), 2, "Сок" },
                    { 7, new Guid("d18e3ace-0fd4-418e-bed3-97feebe4310b"), 2, "Вода" },
                    { 8, new Guid("54018141-dac7-4a7e-93de-faf87a5f5197"), 3, "Ужасы" },
                    { 9, new Guid("c9669b52-d98f-4c28-94c6-97f0eb1eb39c"), 3, "Комедия" },
                    { 10, new Guid("2a04d716-f184-47d4-ba86-c1552982f583"), 3, "Фантастика" },
                    { 11, new Guid("c8aa2ace-742b-487c-9c55-88e45f3755b1"), 3, "Детектив" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_PublicId",
                table: "Interviews",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_SurveyId",
                table: "Interviews",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_PublicId",
                table: "Questions",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyId_OrderId",
                table: "Questions",
                columns: new[] { "SurveyId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_Results_AnswerId",
                table: "Results",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_InterviewId",
                table: "Results",
                column: "InterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_QuestionId",
                table: "Results",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Surveys");
        }
    }
}
