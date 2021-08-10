using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TUNIWEB.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alumnosUsuarios",
                columns: table => new
                {
                    idAlumno = table.Column<Guid>(nullable: false),
                    usuario = table.Column<string>(nullable: true),
                    contraseña = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alumnosUsuarios", x => x.idAlumno);
                });

            migrationBuilder.CreateTable(
                name: "catAreasCarrera",
                columns: table => new
                {
                    idArea = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    area = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catAreasCarrera", x => x.idArea);
                });

            migrationBuilder.CreateTable(
                name: "universidadesUsuario",
                columns: table => new
                {
                    idUniversidad = table.Column<Guid>(nullable: false),
                    usuario = table.Column<string>(nullable: true),
                    contraseña = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_universidadesUsuario", x => x.idUniversidad);
                });

            migrationBuilder.CreateTable(
                name: "alumnos",
                columns: table => new
                {
                    idAlumno = table.Column<Guid>(nullable: false),
                    nombre = table.Column<string>(nullable: true),
                    apPaterno = table.Column<string>(nullable: true),
                    apMaterno = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alumnos", x => x.idAlumno);
                    table.ForeignKey(
                        name: "Relacion_usuario_alumno",
                        column: x => x.idAlumno,
                        principalTable: "alumnosUsuarios",
                        principalColumn: "idAlumno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carreraTecnicas",
                columns: table => new
                {
                    noderelcat = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idAlumno = table.Column<Guid>(nullable: false),
                    catalogoCarrerasTecnicasId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carreraTecnicas", x => x.noderelcat);
                    table.ForeignKey(
                        name: "Relacion_usuario_carreraTecnica",
                        column: x => x.idAlumno,
                        principalTable: "alumnosUsuarios",
                        principalColumn: "idAlumno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "datosAcademicos",
                columns: table => new
                {
                    idAlumno = table.Column<Guid>(nullable: false),
                    boletaGlobal = table.Column<string>(nullable: true),
                    doc = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datosAcademicos", x => x.idAlumno);
                    table.ForeignKey(
                        name: "Relacion_usuario_datosAcademicos",
                        column: x => x.idAlumno,
                        principalTable: "alumnosUsuarios",
                        principalColumn: "idAlumno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "informaciones",
                columns: table => new
                {
                    idnoRecon = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idAlumno = table.Column<Guid>(nullable: false),
                    reconocimiento = table.Column<string>(nullable: true),
                    doc = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_informaciones", x => x.idnoRecon);
                    table.ForeignKey(
                        name: "FK_informaciones_alumnosUsuarios_idAlumno",
                        column: x => x.idAlumno,
                        principalTable: "alumnosUsuarios",
                        principalColumn: "idAlumno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catCarreras",
                columns: table => new
                {
                    idCarrera = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idArea = table.Column<int>(nullable: false),
                    Carrera = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catCarreras", x => x.idCarrera);
                    table.ForeignKey(
                        name: "relacion_Areas_Con_Carrera",
                        column: x => x.idArea,
                        principalTable: "catAreasCarrera",
                        principalColumn: "idArea",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carBecas",
                columns: table => new
                {
                    noBeca = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idUniversidad = table.Column<Guid>(nullable: false),
                    becaInstitucional = table.Column<string>(nullable: true),
                    doc = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carBecas", x => x.noBeca);
                    table.ForeignKey(
                        name: "Relacion_usuario_becas",
                        column: x => x.idUniversidad,
                        principalTable: "universidadesUsuario",
                        principalColumn: "idUniversidad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catalogoDeMapasCuarriculares",
                columns: table => new
                {
                    noDeMapaCurricular = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idUniversidad = table.Column<Guid>(nullable: false),
                    idCarrera = table.Column<int>(nullable: false),
                    mapacurricular = table.Column<string>(nullable: true),
                    doc = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalogoDeMapasCuarriculares", x => x.noDeMapaCurricular);
                    table.ForeignKey(
                        name: "Relacion_usuario_CatalogoCurriculares",
                        column: x => x.idUniversidad,
                        principalTable: "universidadesUsuario",
                        principalColumn: "idUniversidad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contactos",
                columns: table => new
                {
                    noDeContacto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idUniversidad = table.Column<Guid>(nullable: false),
                    contacto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactos", x => x.noDeContacto);
                    table.ForeignKey(
                        name: "Rellacion_usuario_contacto",
                        column: x => x.idUniversidad,
                        principalTable: "universidadesUsuario",
                        principalColumn: "idUniversidad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "egresos",
                columns: table => new
                {
                    idUniversidad = table.Column<Guid>(nullable: false),
                    nivelEgreso = table.Column<string>(nullable: true),
                    doc = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_egresos", x => x.idUniversidad);
                    table.ForeignKey(
                        name: "Relacion_usuario_egreso",
                        column: x => x.idUniversidad,
                        principalTable: "universidadesUsuario",
                        principalColumn: "idUniversidad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "empresaAsociadas",
                columns: table => new
                {
                    noDeEmpresaAsociada = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idUniversidad = table.Column<Guid>(nullable: false),
                    empresaAsociada = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresaAsociadas", x => x.noDeEmpresaAsociada);
                    table.ForeignKey(
                        name: "FK_empresaAsociadas_universidadesUsuario_idUniversidad",
                        column: x => x.idUniversidad,
                        principalTable: "universidadesUsuario",
                        principalColumn: "idUniversidad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ingresos",
                columns: table => new
                {
                    idUniversidad = table.Column<Guid>(nullable: false),
                    metodoIngreso = table.Column<string>(nullable: true),
                    doc = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingresos", x => x.idUniversidad);
                    table.ForeignKey(
                        name: "Relacion_usuario_ingreso",
                        column: x => x.idUniversidad,
                        principalTable: "universidadesUsuario",
                        principalColumn: "idUniversidad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "universidades",
                columns: table => new
                {
                    idUnversidad = table.Column<Guid>(nullable: false),
                    nombre = table.Column<string>(nullable: true),
                    direccion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_universidades", x => x.idUnversidad);
                    table.ForeignKey(
                        name: "Relacion_usuarioUniversidad_universidad",
                        column: x => x.idUnversidad,
                        principalTable: "universidadesUsuario",
                        principalColumn: "idUniversidad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catalogoCarrerasT",
                columns: table => new
                {
                    carreTecnicaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    carreraTecnica = table.Column<string>(nullable: true),
                    relcart_Catnoderelcat = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalogoCarrerasT", x => x.carreTecnicaId);
                    table.ForeignKey(
                        name: "Relacion_carreratecnica_catalogodecarrerastecnicas",
                        column: x => x.relcart_Catnoderelcat,
                        principalTable: "carreraTecnicas",
                        principalColumn: "noderelcat",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "carrerasDeseadas",
                columns: table => new
                {
                    noDeCarreraDeseada = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idAlumno = table.Column<Guid>(nullable: false),
                    idCarrera = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carrerasDeseadas", x => x.noDeCarreraDeseada);
                    table.ForeignKey(
                        name: "Relacion_usuario_carreraDeseada",
                        column: x => x.idAlumno,
                        principalTable: "alumnosUsuarios",
                        principalColumn: "idAlumno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carrerasDeseadas_catCarreras_idCarrera",
                        column: x => x.idCarrera,
                        principalTable: "catCarreras",
                        principalColumn: "idCarrera",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carBecas_idUniversidad",
                table: "carBecas",
                column: "idUniversidad");

            migrationBuilder.CreateIndex(
                name: "IX_carrerasDeseadas_idAlumno",
                table: "carrerasDeseadas",
                column: "idAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_carrerasDeseadas_idCarrera",
                table: "carrerasDeseadas",
                column: "idCarrera");

            migrationBuilder.CreateIndex(
                name: "IX_carreraTecnicas_idAlumno",
                table: "carreraTecnicas",
                column: "idAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_catalogoCarrerasT_relcart_Catnoderelcat",
                table: "catalogoCarrerasT",
                column: "relcart_Catnoderelcat");

            migrationBuilder.CreateIndex(
                name: "IX_catalogoDeMapasCuarriculares_idUniversidad",
                table: "catalogoDeMapasCuarriculares",
                column: "idUniversidad");

            migrationBuilder.CreateIndex(
                name: "IX_catCarreras_idArea",
                table: "catCarreras",
                column: "idArea");

            migrationBuilder.CreateIndex(
                name: "IX_contactos_idUniversidad",
                table: "contactos",
                column: "idUniversidad");

            migrationBuilder.CreateIndex(
                name: "IX_empresaAsociadas_idUniversidad",
                table: "empresaAsociadas",
                column: "idUniversidad");

            migrationBuilder.CreateIndex(
                name: "IX_informaciones_idAlumno",
                table: "informaciones",
                column: "idAlumno");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alumnos");

            migrationBuilder.DropTable(
                name: "carBecas");

            migrationBuilder.DropTable(
                name: "carrerasDeseadas");

            migrationBuilder.DropTable(
                name: "catalogoCarrerasT");

            migrationBuilder.DropTable(
                name: "catalogoDeMapasCuarriculares");

            migrationBuilder.DropTable(
                name: "contactos");

            migrationBuilder.DropTable(
                name: "datosAcademicos");

            migrationBuilder.DropTable(
                name: "egresos");

            migrationBuilder.DropTable(
                name: "empresaAsociadas");

            migrationBuilder.DropTable(
                name: "informaciones");

            migrationBuilder.DropTable(
                name: "ingresos");

            migrationBuilder.DropTable(
                name: "universidades");

            migrationBuilder.DropTable(
                name: "catCarreras");

            migrationBuilder.DropTable(
                name: "carreraTecnicas");

            migrationBuilder.DropTable(
                name: "universidadesUsuario");

            migrationBuilder.DropTable(
                name: "catAreasCarrera");

            migrationBuilder.DropTable(
                name: "alumnosUsuarios");
        }
    }
}
