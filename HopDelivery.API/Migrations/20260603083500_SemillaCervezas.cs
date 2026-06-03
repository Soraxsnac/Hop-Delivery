using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 

namespace HopDelivery.API.Migrations
{
 
    public partial class SemillaCervezas : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CatMarcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Marca = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatMarcas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cervezas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ABV = table.Column<double>(type: "double", nullable: false),
                    IBU = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagenURL = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdMarca = table.Column<int>(type: "int", nullable: false),
                    Calificacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cervezas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Cervezas",
                columns: new[] { "Id", "ABV", "Calificacion", "Descripcion", "IBU", "IdMarca", "ImagenURL", "Nombre", "Tipo" },
                values: new object[,]
                {
                    { 1, 4.5, 4, "Lager clara muy refrescante.", 0, 0, "", "Corona Extra", "Lager" },
                    { 2, 4.2000000000000002, 5, "Stout oscura con espuma cremosa.", 0, 0, "", "Guinness Draught", "Stout" },
                    { 3, 5.4000000000000004, 4, "IPA escocesa con notas frutales.", 0, 0, "", "Punk IPA", "IPA" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatMarcas");

            migrationBuilder.DropTable(
                name: "Cervezas");
        }
    }
}
