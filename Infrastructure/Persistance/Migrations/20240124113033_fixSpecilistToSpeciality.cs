using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class fixSpecilistToSpeciality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specialists_SpecialistId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Specialists");

            migrationBuilder.RenameColumn(
                name: "SpecialistId",
                table: "Doctors",
                newName: "SpecialityId");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_SpecialistId",
                table: "Doctors",
                newName: "IX_Doctors_SpecialityId");

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Specialities_Name",
                table: "Specialities",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specialities_SpecialityId",
                table: "Doctors",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specialities_SpecialityId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Specialities");

            migrationBuilder.RenameColumn(
                name: "SpecialityId",
                table: "Doctors",
                newName: "SpecialistId");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_SpecialityId",
                table: "Doctors",
                newName: "IX_Doctors_SpecialistId");

            migrationBuilder.CreateTable(
                name: "Specialists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Specialists_Name",
                table: "Specialists",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specialists_SpecialistId",
                table: "Doctors",
                column: "SpecialistId",
                principalTable: "Specialists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
