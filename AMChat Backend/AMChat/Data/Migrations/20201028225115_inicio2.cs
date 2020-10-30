using Microsoft.EntityFrameworkCore.Migrations;

namespace AMChat.Data.Migrations
{
    public partial class inicio2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoom_AspNetUsers_User1Id",
                table: "ChatRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoom_AspNetUsers_User2Id",
                table: "ChatRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_Mensajes_ChatRoom_ChatRoomIdChatRoom",
                table: "Mensajes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatRoom",
                table: "ChatRoom");

            migrationBuilder.RenameTable(
                name: "ChatRoom",
                newName: "ChatRooms");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRoom_User2Id",
                table: "ChatRooms",
                newName: "IX_ChatRooms_User2Id");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRoom_User1Id",
                table: "ChatRooms",
                newName: "IX_ChatRooms_User1Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatRooms",
                table: "ChatRooms",
                column: "IdChatRoom");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_AspNetUsers_User1Id",
                table: "ChatRooms",
                column: "User1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_AspNetUsers_User2Id",
                table: "ChatRooms",
                column: "User2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mensajes_ChatRooms_ChatRoomIdChatRoom",
                table: "Mensajes",
                column: "ChatRoomIdChatRoom",
                principalTable: "ChatRooms",
                principalColumn: "IdChatRoom",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_AspNetUsers_User1Id",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_AspNetUsers_User2Id",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Mensajes_ChatRooms_ChatRoomIdChatRoom",
                table: "Mensajes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatRooms",
                table: "ChatRooms");

            migrationBuilder.RenameTable(
                name: "ChatRooms",
                newName: "ChatRoom");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRooms_User2Id",
                table: "ChatRoom",
                newName: "IX_ChatRoom_User2Id");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRooms_User1Id",
                table: "ChatRoom",
                newName: "IX_ChatRoom_User1Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatRoom",
                table: "ChatRoom",
                column: "IdChatRoom");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoom_AspNetUsers_User1Id",
                table: "ChatRoom",
                column: "User1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoom_AspNetUsers_User2Id",
                table: "ChatRoom",
                column: "User2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mensajes_ChatRoom_ChatRoomIdChatRoom",
                table: "Mensajes",
                column: "ChatRoomIdChatRoom",
                principalTable: "ChatRoom",
                principalColumn: "IdChatRoom",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
