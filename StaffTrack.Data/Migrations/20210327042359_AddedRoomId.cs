using Microsoft.EntityFrameworkCore.Migrations;

namespace StaffTrack.Data.Migrations
{
    public partial class AddedRoomId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "StaffActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "StaffActivities");
        }
    }
}
