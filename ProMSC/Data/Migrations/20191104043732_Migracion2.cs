using Microsoft.EntityFrameworkCore.Migrations;

namespace ProMSC.Data.Migrations
{
    public partial class Migracion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    idcliente = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    razonsocial = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    ubicacion = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    contacto = table.Column<string>(maxLength: 100, nullable: true),
                    email = table.Column<string>(maxLength: 255, nullable: false),
                    telefono = table.Column<string>(maxLength: 20, nullable: true),
                    estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.idcliente);
                });

            migrationBuilder.CreateTable(
                name: "Servidor",
                columns: table => new
                {
                    idvps = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idcliente = table.Column<int>(nullable: false),
                    nombrevps = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    cpu = table.Column<int>(nullable: false),
                    ram = table.Column<int>(nullable: false),
                    ips = table.Column<int>(nullable: false),
                    discoduro = table.Column<int>(nullable: false),
                    osystem = table.Column<string>(maxLength: 255, nullable: true),
                    database = table.Column<string>(nullable: true),
                    dasktop = table.Column<int>(nullable: false),
                    anchobanda = table.Column<int>(nullable: false),
                    ip_publica = table.Column<string>(maxLength: 255, nullable: false),
                    ip_privada = table.Column<string>(maxLength: 255, nullable: false),
                    descripcion = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servidor", x => x.idvps);
                    table.ForeignKey(
                        name: "FK_servidor_cliente",
                        column: x => x.idcliente,
                        principalTable: "Cliente",
                        principalColumn: "idcliente",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servidor_idcliente",
                table: "Servidor",
                column: "idcliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servidor");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
