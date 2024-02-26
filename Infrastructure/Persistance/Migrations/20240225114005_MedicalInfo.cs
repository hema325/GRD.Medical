using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class MedicalInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Hight = table.Column<float>(type: "real", nullable: false),
                    Wight = table.Column<float>(type: "real", nullable: false),
                    Diabetic = table.Column<bool>(type: "bit", nullable: false),
                    Hypertension = table.Column<bool>(type: "bit", nullable: false),
                    Hypotension = table.Column<bool>(type: "bit", nullable: false),
                    Smoker = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalInfos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedOn",
                value: new DateTime(2022, 11, 14, 11, 40, 5, 342, DateTimeKind.Utc).AddTicks(12));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishedOn",
                value: new DateTime(2023, 8, 10, 11, 40, 5, 342, DateTimeKind.Utc).AddTicks(373));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishedOn",
                value: new DateTime(2023, 8, 21, 11, 40, 5, 342, DateTimeKind.Utc).AddTicks(853));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishedOn",
                value: new DateTime(2022, 2, 26, 11, 40, 5, 342, DateTimeKind.Utc).AddTicks(1165));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 5,
                column: "PublishedOn",
                value: new DateTime(2024, 2, 14, 11, 40, 5, 342, DateTimeKind.Utc).AddTicks(1498));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 6,
                column: "PublishedOn",
                value: new DateTime(2021, 9, 29, 11, 40, 5, 342, DateTimeKind.Utc).AddTicks(1899));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 7,
                column: "PublishedOn",
                value: new DateTime(2021, 8, 12, 11, 40, 5, 342, DateTimeKind.Utc).AddTicks(2255));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 8,
                column: "PublishedOn",
                value: new DateTime(2022, 10, 29, 11, 40, 5, 342, DateTimeKind.Utc).AddTicks(2584));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 9,
                column: "PublishedOn",
                value: new DateTime(2024, 1, 5, 11, 40, 5, 342, DateTimeKind.Utc).AddTicks(3227));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 10,
                column: "PublishedOn",
                value: new DateTime(2022, 11, 26, 11, 40, 5, 342, DateTimeKind.Utc).AddTicks(3593));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 11,
                column: "PublishedOn",
                value: new DateTime(2022, 10, 10, 11, 40, 5, 342, DateTimeKind.Utc).AddTicks(4100));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 12,
                column: "PublishedOn",
                value: new DateTime(2023, 1, 14, 11, 40, 5, 342, DateTimeKind.Utc).AddTicks(4466));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedOn",
                value: new DateTime(2022, 11, 10, 11, 40, 5, 341, DateTimeKind.Utc).AddTicks(5118));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishedOn",
                value: new DateTime(2023, 1, 14, 11, 40, 5, 341, DateTimeKind.Utc).AddTicks(6439));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishedOn",
                value: new DateTime(2021, 11, 25, 11, 40, 5, 341, DateTimeKind.Utc).AddTicks(6842));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishedOn",
                value: new DateTime(2023, 12, 21, 11, 40, 5, 341, DateTimeKind.Utc).AddTicks(7139));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5,
                column: "PublishedOn",
                value: new DateTime(2021, 4, 7, 11, 40, 5, 341, DateTimeKind.Utc).AddTicks(7436));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 6,
                column: "PublishedOn",
                value: new DateTime(2023, 3, 24, 11, 40, 5, 341, DateTimeKind.Utc).AddTicks(7741));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 7,
                column: "PublishedOn",
                value: new DateTime(2022, 10, 10, 11, 40, 5, 341, DateTimeKind.Utc).AddTicks(8027));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 8,
                column: "PublishedOn",
                value: new DateTime(2021, 9, 5, 11, 40, 5, 341, DateTimeKind.Utc).AddTicks(8358));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 9,
                column: "PublishedOn",
                value: new DateTime(2023, 10, 19, 11, 40, 5, 341, DateTimeKind.Utc).AddTicks(8672));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 10,
                column: "PublishedOn",
                value: new DateTime(2024, 2, 21, 11, 40, 5, 341, DateTimeKind.Utc).AddTicks(8979));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 11,
                column: "PublishedOn",
                value: new DateTime(2022, 12, 2, 11, 40, 5, 341, DateTimeKind.Utc).AddTicks(9312));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 12,
                column: "PublishedOn",
                value: new DateTime(2023, 5, 19, 11, 40, 5, 341, DateTimeKind.Utc).AddTicks(9633));

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInfos_UserId",
                table: "MedicalInfos",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalInfos");

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedOn",
                value: new DateTime(2023, 12, 31, 9, 27, 59, 360, DateTimeKind.Utc).AddTicks(2952));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishedOn",
                value: new DateTime(2023, 9, 21, 9, 27, 59, 360, DateTimeKind.Utc).AddTicks(4745));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishedOn",
                value: new DateTime(2023, 8, 27, 9, 27, 59, 360, DateTimeKind.Utc).AddTicks(7742));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishedOn",
                value: new DateTime(2024, 1, 2, 9, 27, 59, 360, DateTimeKind.Utc).AddTicks(9305));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 5,
                column: "PublishedOn",
                value: new DateTime(2021, 12, 7, 9, 27, 59, 364, DateTimeKind.Utc).AddTicks(6097));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 6,
                column: "PublishedOn",
                value: new DateTime(2021, 6, 26, 9, 27, 59, 364, DateTimeKind.Utc).AddTicks(8372));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 7,
                column: "PublishedOn",
                value: new DateTime(2021, 6, 18, 9, 27, 59, 365, DateTimeKind.Utc).AddTicks(583));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 8,
                column: "PublishedOn",
                value: new DateTime(2021, 4, 12, 9, 27, 59, 365, DateTimeKind.Utc).AddTicks(2360));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 9,
                column: "PublishedOn",
                value: new DateTime(2023, 1, 15, 9, 27, 59, 365, DateTimeKind.Utc).AddTicks(5452));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 10,
                column: "PublishedOn",
                value: new DateTime(2023, 12, 19, 9, 27, 59, 365, DateTimeKind.Utc).AddTicks(6566));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 11,
                column: "PublishedOn",
                value: new DateTime(2023, 11, 28, 9, 27, 59, 365, DateTimeKind.Utc).AddTicks(8752));

            migrationBuilder.UpdateData(
                table: "Advices",
                keyColumn: "Id",
                keyValue: 12,
                column: "PublishedOn",
                value: new DateTime(2023, 7, 25, 9, 27, 59, 366, DateTimeKind.Utc).AddTicks(2477));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedOn",
                value: new DateTime(2023, 3, 24, 9, 27, 59, 357, DateTimeKind.Utc).AddTicks(6874));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishedOn",
                value: new DateTime(2022, 3, 19, 9, 27, 59, 358, DateTimeKind.Utc).AddTicks(9));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishedOn",
                value: new DateTime(2023, 11, 8, 9, 27, 59, 358, DateTimeKind.Utc).AddTicks(6011));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishedOn",
                value: new DateTime(2023, 12, 16, 9, 27, 59, 358, DateTimeKind.Utc).AddTicks(7001));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5,
                column: "PublishedOn",
                value: new DateTime(2022, 5, 19, 9, 27, 59, 358, DateTimeKind.Utc).AddTicks(7941));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 6,
                column: "PublishedOn",
                value: new DateTime(2021, 8, 7, 9, 27, 59, 358, DateTimeKind.Utc).AddTicks(9228));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 7,
                column: "PublishedOn",
                value: new DateTime(2023, 4, 3, 9, 27, 59, 359, DateTimeKind.Utc).AddTicks(1850));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 8,
                column: "PublishedOn",
                value: new DateTime(2023, 2, 24, 9, 27, 59, 359, DateTimeKind.Utc).AddTicks(4585));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 9,
                column: "PublishedOn",
                value: new DateTime(2023, 5, 12, 9, 27, 59, 359, DateTimeKind.Utc).AddTicks(7115));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 10,
                column: "PublishedOn",
                value: new DateTime(2022, 6, 14, 9, 27, 59, 359, DateTimeKind.Utc).AddTicks(8789));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 11,
                column: "PublishedOn",
                value: new DateTime(2022, 5, 22, 9, 27, 59, 360, DateTimeKind.Utc).AddTicks(281));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 12,
                column: "PublishedOn",
                value: new DateTime(2022, 9, 23, 9, 27, 59, 360, DateTimeKind.Utc).AddTicks(1837));
        }
    }
}
