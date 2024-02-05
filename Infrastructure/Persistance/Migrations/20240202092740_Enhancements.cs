using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Enhancements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Doctors_SpecialityId",
                table: "Doctors");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "RefreshTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatBotMessages_MessagedOn",
                table: "UserChatBotMessages",
                column: "MessagedOn");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_Day_Start_End",
                table: "TimeSlots",
                columns: new[] { "Day", "Start", "End" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewedOn",
                table: "Reviews",
                column: "ReviewedOn");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_ExpiresOn",
                table: "RefreshTokens",
                column: "ExpiresOn");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostedOn",
                table: "Posts",
                column: "PostedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialityId_Experience_ConsultFee",
                table: "Doctors",
                columns: new[] { "SpecialityId", "Experience", "ConsultFee" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentedOn",
                table: "Comments",
                column: "CommentedOn");

            migrationBuilder.CreateIndex(
                name: "IX_BillingInfos_PaidIn",
                table: "BillingInfos",
                column: "PaidIn");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StartDateTime_EndDateTime",
                table: "Appointments",
                columns: new[] { "StartDateTime", "EndDateTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentMessages_MessagedOn",
                table: "AppointmentMessages",
                column: "MessagedOn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserChatBotMessages_MessagedOn",
                table: "UserChatBotMessages");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_Day_Start_End",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReviewedOn",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_ExpiresOn",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostedOn",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_SpecialityId_Experience_ConsultFee",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentedOn",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_BillingInfos_PaidIn",
                table: "BillingInfos");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_StartDateTime_EndDateTime",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentMessages_MessagedOn",
                table: "AppointmentMessages");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialityId",
                table: "Doctors",
                column: "SpecialityId");
        }
    }
}
