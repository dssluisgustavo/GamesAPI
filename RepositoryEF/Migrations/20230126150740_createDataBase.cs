using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RepositoryEF.Migrations
{
    /// <inheritdoc />
    public partial class createDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "genre",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    genrename = table.Column<string>(name: "genre_name", type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "NULL::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("genre_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "platform",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    platformname = table.Column<string>(name: "platform_name", type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "NULL::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("platform_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "publisher",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    publishername = table.Column<string>(name: "publisher_name", type: "character varying(100)", maxLength: 100, nullable: true, defaultValueSql: "NULL::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("publisher_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "region",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    regionname = table.Column<string>(name: "region_name", type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "NULL::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("region_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "NULL::character varying"),
                    password = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false, defaultValueSql: "NULL::character varying"),
                    email = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "game",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    genreid = table.Column<int>(name: "genre_id", type: "integer", nullable: true),
                    gamename = table.Column<string>(name: "game_name", type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "NULL::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game", x => x.id);
                    table.ForeignKey(
                        name: "fk_gm_gen",
                        column: x => x.genreid,
                        principalSchema: "public",
                        principalTable: "genre",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "game_publisher",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gameid = table.Column<int>(name: "game_id", type: "integer", nullable: true),
                    publisherid = table.Column<int>(name: "publisher_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gamepub", x => x.id);
                    table.ForeignKey(
                        name: "fk_gpu_gam",
                        column: x => x.gameid,
                        principalSchema: "public",
                        principalTable: "game",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gpu_pub",
                        column: x => x.publisherid,
                        principalSchema: "public",
                        principalTable: "publisher",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "game_platform",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gamepublisherid = table.Column<int>(name: "game_publisher_id", type: "integer", nullable: true),
                    platformid = table.Column<int>(name: "platform_id", type: "integer", nullable: true),
                    releaseyear = table.Column<int>(name: "release_year", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gameplatform", x => x.id);
                    table.ForeignKey(
                        name: "fk_gpl_gp",
                        column: x => x.gamepublisherid,
                        principalSchema: "public",
                        principalTable: "game_publisher",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_gpl_pla",
                        column: x => x.platformid,
                        principalSchema: "public",
                        principalTable: "platform",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "region_sales",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    regionid = table.Column<int>(name: "region_id", type: "integer", nullable: true),
                    gameplatformid = table.Column<int>(name: "game_platform_id", type: "integer", nullable: true),
                    numsales = table.Column<decimal>(name: "num_sales", type: "numeric(5,2)", precision: 5, scale: 2, nullable: true, defaultValueSql: "NULL::numeric")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_region_sales", x => x.id);
                    table.ForeignKey(
                        name: "fk_rs_gp",
                        column: x => x.gameplatformid,
                        principalSchema: "public",
                        principalTable: "game_platform",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_rs_reg",
                        column: x => x.regionid,
                        principalSchema: "public",
                        principalTable: "region",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_game_genre_id",
                schema: "public",
                table: "game",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_platform_game_publisher_id",
                schema: "public",
                table: "game_platform",
                column: "game_publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_platform_platform_id",
                schema: "public",
                table: "game_platform",
                column: "platform_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_publisher_game_id",
                schema: "public",
                table: "game_publisher",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_publisher_publisher_id",
                schema: "public",
                table: "game_publisher",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_region_sales_game_platform_id",
                schema: "public",
                table: "region_sales",
                column: "game_platform_id");

            migrationBuilder.CreateIndex(
                name: "IX_region_sales_region_id",
                schema: "public",
                table: "region_sales",
                column: "region_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "region_sales",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user",
                schema: "public");

            migrationBuilder.DropTable(
                name: "game_platform",
                schema: "public");

            migrationBuilder.DropTable(
                name: "region",
                schema: "public");

            migrationBuilder.DropTable(
                name: "game_publisher",
                schema: "public");

            migrationBuilder.DropTable(
                name: "platform",
                schema: "public");

            migrationBuilder.DropTable(
                name: "game",
                schema: "public");

            migrationBuilder.DropTable(
                name: "publisher",
                schema: "public");

            migrationBuilder.DropTable(
                name: "genre",
                schema: "public");
        }
    }
}
