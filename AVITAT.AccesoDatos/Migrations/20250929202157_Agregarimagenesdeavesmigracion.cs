using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVITAT.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class Agregarimagenesdeavesmigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenUrl",
                table: "Aves",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenUrl",
                table: "Aves");
        }
    }
}
