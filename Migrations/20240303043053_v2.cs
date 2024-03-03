using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnterpriceWeb.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "us_start_year",
                table: "users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "us_phone",
                table: "users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "us_image",
                table: "users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "us_gmail",
                table: "users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "us_gender",
                table: "users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "us_end_year",
                table: "users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "us_role",
                table: "users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "f_status",
                table: "faculties",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "article_Files",
                columns: table => new
                {
                    article_file_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    article_file_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    article_file_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_Files", x => x.article_file_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "magazines",
                columns: table => new
                {
                    magazine_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    magazine_closure_date = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    magazine_final_closure_date = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    magazine_title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    magazine_academic_year = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    magazine_status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    magazine_deleted = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_magazines", x => x.magazine_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    article_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    magazine_id = table.Column<int>(type: "int", nullable: false),
                    us_id = table.Column<int>(type: "int", nullable: false),
                    article_submit_date = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    article_accept_date = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    article_views = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    article_status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.article_id);
                    table.ForeignKey(
                        name: "FK_articles_magazines_magazine_id",
                        column: x => x.magazine_id,
                        principalTable: "magazines",
                        principalColumn: "magazine_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_articles_users_us_id",
                        column: x => x.us_id,
                        principalTable: "users",
                        principalColumn: "us_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "faculties",
                columns: new[] { "f_id", "f_name", "f_status" },
                values: new object[,]
                {
                    { 1, "faculty1", "1" },
                    { 2, "faculty2", "1" },
                    { 3, "faculty3", "1" }
                });

            migrationBuilder.InsertData(
                table: "magazines",
                columns: new[] { "magazine_id", "magazine_academic_year", "magazine_closure_date", "magazine_deleted", "magazine_final_closure_date", "magazine_status", "magazine_title" },
                values: new object[] { 1, "2019-2024", "03/02/2024", "0", "27/03/2024", "0", "Tabloid" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "us_id", "f_id", "us_end_year", "us_gender", "us_gmail", "us_image", "us_name", "us_password", "us_phone", "us_role", "us_start_year" },
                values: new object[] { 1, 1, null, null, null, null, "thaihuynh", "huynhthai", null, "admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_articles_magazine_id",
                table: "articles",
                column: "magazine_id");

            migrationBuilder.CreateIndex(
                name: "IX_articles_us_id",
                table: "articles",
                column: "us_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_Files");

            migrationBuilder.DropTable(
                name: "articles");

            migrationBuilder.DropTable(
                name: "magazines");

            migrationBuilder.DeleteData(
                table: "faculties",
                keyColumn: "f_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "faculties",
                keyColumn: "f_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "us_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "faculties",
                keyColumn: "f_id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "us_role",
                table: "users");

            migrationBuilder.DropColumn(
                name: "f_status",
                table: "faculties");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "us_start_year",
                keyValue: null,
                column: "us_start_year",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "us_start_year",
                table: "users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "us_phone",
                keyValue: null,
                column: "us_phone",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "us_phone",
                table: "users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "us_image",
                keyValue: null,
                column: "us_image",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "us_image",
                table: "users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "us_gmail",
                keyValue: null,
                column: "us_gmail",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "us_gmail",
                table: "users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "us_gender",
                keyValue: null,
                column: "us_gender",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "us_gender",
                table: "users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "us_end_year",
                keyValue: null,
                column: "us_end_year",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "us_end_year",
                table: "users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
