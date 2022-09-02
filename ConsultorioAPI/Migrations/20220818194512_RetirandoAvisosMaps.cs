using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsultorioAPI.Migrations
{
    public partial class RetirandoAvisosMaps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_consulta_Paciente_id_paciente",
                table: "tb_consulta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paciente",
                table: "Paciente");

            migrationBuilder.RenameTable(
                name: "Paciente",
                newName: "tb_paciente");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "tb_paciente",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "tb_paciente",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "tb_paciente",
                newName: "cpf");

            migrationBuilder.RenameColumn(
                name: "Celular",
                table: "tb_paciente",
                newName: "celular");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_paciente",
                newName: "id");

            migrationBuilder.AddColumn<int>(
                name: "EspecialidadeId",
                table: "tb_consulta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfissionalId",
                table: "tb_consulta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "tb_paciente",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "tb_paciente",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cpf",
                table: "tb_paciente",
                type: "varchar(11)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "celular",
                table: "tb_paciente",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_paciente",
                table: "tb_paciente",
                column: "id");

            migrationBuilder.CreateTable(
                name: "tb_especialidade",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ativa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_especialidade", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_profissional",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_profissional", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_profissional_especialidade",
                columns: table => new
                {
                    id_profissional = table.Column<int>(type: "int", nullable: false),
                    id_especialidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_profissional_especialidade", x => new { x.id_especialidade, x.id_profissional });
                    table.ForeignKey(
                        name: "FK_tb_profissional_especialidade_tb_especialidade_id_especialidade",
                        column: x => x.id_especialidade,
                        principalTable: "tb_especialidade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_profissional_especialidade_tb_profissional_id_profissional",
                        column: x => x.id_profissional,
                        principalTable: "tb_profissional",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_consulta_EspecialidadeId",
                table: "tb_consulta",
                column: "EspecialidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_consulta_ProfissionalId",
                table: "tb_consulta",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_profissional_especialidade_id_profissional",
                table: "tb_profissional_especialidade",
                column: "id_profissional");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_consulta_tb_especialidade_EspecialidadeId",
                table: "tb_consulta",
                column: "EspecialidadeId",
                principalTable: "tb_especialidade",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_consulta_tb_paciente_id_paciente",
                table: "tb_consulta",
                column: "id_paciente",
                principalTable: "tb_paciente",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_consulta_tb_profissional_ProfissionalId",
                table: "tb_consulta",
                column: "ProfissionalId",
                principalTable: "tb_profissional",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_consulta_tb_especialidade_EspecialidadeId",
                table: "tb_consulta");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_consulta_tb_paciente_id_paciente",
                table: "tb_consulta");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_consulta_tb_profissional_ProfissionalId",
                table: "tb_consulta");

            migrationBuilder.DropTable(
                name: "tb_profissional_especialidade");

            migrationBuilder.DropTable(
                name: "tb_especialidade");

            migrationBuilder.DropTable(
                name: "tb_profissional");

            migrationBuilder.DropIndex(
                name: "IX_tb_consulta_EspecialidadeId",
                table: "tb_consulta");

            migrationBuilder.DropIndex(
                name: "IX_tb_consulta_ProfissionalId",
                table: "tb_consulta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_paciente",
                table: "tb_paciente");

            migrationBuilder.DropColumn(
                name: "EspecialidadeId",
                table: "tb_consulta");

            migrationBuilder.DropColumn(
                name: "ProfissionalId",
                table: "tb_consulta");

            migrationBuilder.RenameTable(
                name: "tb_paciente",
                newName: "Paciente");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Paciente",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Paciente",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "cpf",
                table: "Paciente",
                newName: "Cpf");

            migrationBuilder.RenameColumn(
                name: "celular",
                table: "Paciente",
                newName: "Celular");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Paciente",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)");

            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paciente",
                table: "Paciente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_consulta_Paciente_id_paciente",
                table: "tb_consulta",
                column: "id_paciente",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
