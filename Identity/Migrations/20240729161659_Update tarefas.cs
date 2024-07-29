using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoTarefasApi.Migrations
{
    public partial class Updatetarefas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_UsuarioAssocioadoId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "Tarefas");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_UsuarioAssocioadoId",
                table: "Tarefas",
                newName: "IX_Tarefas_UsuarioAssocioadoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tarefas",
                table: "Tarefas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_AspNetUsers_UsuarioAssocioadoId",
                table: "Tarefas",
                column: "UsuarioAssocioadoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_AspNetUsers_UsuarioAssocioadoId",
                table: "Tarefas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tarefas",
                table: "Tarefas");

            migrationBuilder.RenameTable(
                name: "Tarefas",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_Tarefas_UsuarioAssocioadoId",
                table: "Tasks",
                newName: "IX_Tasks_UsuarioAssocioadoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_UsuarioAssocioadoId",
                table: "Tasks",
                column: "UsuarioAssocioadoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
