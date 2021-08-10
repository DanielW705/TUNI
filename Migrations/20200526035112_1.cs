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
                name: "AreasTests",
                columns: table => new
                {
                    areaTestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    areaDelTest = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreasTests", x => x.areaTestId);
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
                name: "preguntasDelTestVocacional",
                columns: table => new
                {
                    PregunataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    pregunta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preguntasDelTestVocacional", x => x.PregunataId);
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
                    areasCarreraId = table.Column<int>(nullable: false),
                    areasTestId = table.Column<int>(nullable: false),
                    Carrera = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catCarreras", x => x.idCarrera);
                    table.ForeignKey(
                        name: "relacion_cat_areas",
                        column: x => x.areasCarreraId,
                        principalTable: "catAreasCarrera",
                        principalColumn: "idArea",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catCarreras_AreasTests_areasTestId",
                        column: x => x.areasTestId,
                        principalTable: "AreasTests",
                        principalColumn: "areaTestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "valorPreguntas",
                columns: table => new
                {
                    noDePregunta = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idAlumno = table.Column<Guid>(nullable: false),
                    idPregunta = table.Column<int>(nullable: false),
                    areasTestID = table.Column<int>(nullable: false),
                    valor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_valorPreguntas", x => x.noDePregunta);
                    table.ForeignKey(
                        name: "FK_valorPreguntas_AreasTests_areasTestID",
                        column: x => x.areasTestID,
                        principalTable: "AreasTests",
                        principalColumn: "areaTestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Relacion_usuario_test",
                        column: x => x.idAlumno,
                        principalTable: "alumnosUsuarios",
                        principalColumn: "idAlumno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_valorPreguntas_preguntasDelTestVocacional_idPregunta",
                        column: x => x.idPregunta,
                        principalTable: "preguntasDelTestVocacional",
                        principalColumn: "PregunataId",
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
                        name: "Relacion_usuario_contacto",
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
                name: "Relaciones",
                columns: table => new
                {
                    nodeRelacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idAlumno = table.Column<Guid>(nullable: false),
                    idUniversidad = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relaciones", x => x.nodeRelacion);
                    table.ForeignKey(
                        name: "Relacion_usuario_relacion",
                        column: x => x.idAlumno,
                        principalTable: "alumnosUsuarios",
                        principalColumn: "idAlumno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Relacion_Usuario_universidad_rel",
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

            migrationBuilder.CreateTable(
                name: "carrerasImpartadas",
                columns: table => new
                {
                    noderelacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    usuarioUniversidad = table.Column<Guid>(nullable: false),
                    catCarrerasId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carrerasImpartadas", x => x.noderelacion);
                    table.ForeignKey(
                        name: "relacion_cat_imp",
                        column: x => x.catCarrerasId,
                        principalTable: "catCarreras",
                        principalColumn: "idCarrera",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Relacion_Usuario_carrerasimpartidas",
                        column: x => x.usuarioUniversidad,
                        principalTable: "universidadesUsuario",
                        principalColumn: "idUniversidad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AreasTests",
                columns: new[] { "areaTestId", "areaDelTest" },
                values: new object[,]
                {
                    { 10, "Aire Libre" },
                    { 9, "Mecanico Constructivo" },
                    { 8, "Calculo" },
                    { 7, "Cientifica" },
                    { 1, "Servicio Social" },
                    { 2, "Ejecutiva Persuasiva" },
                    { 3, "Verbal" },
                    { 4, "Artistico Plastico" },
                    { 5, "Musical" },
                    { 6, "Organizacion" }
                });

            migrationBuilder.InsertData(
                table: "catAreasCarrera",
                columns: new[] { "idArea", "area" },
                values: new object[,]
                {
                    { 1, "Fisico matematicas" },
                    { 3, "Ciencias Sociales y administrativas" },
                    { 2, "Ciencias Biologicas, Quimicas de la salud" },
                    { 4, "Humanidades y de las Artes" }
                });

            migrationBuilder.InsertData(
                table: "catalogoCarrerasT",
                columns: new[] { "carreTecnicaId", "carreraTecnica", "relcart_Catnoderelcat" },
                values: new object[,]
                {
                    { 1, "Tecnico en informatica", null },
                    { 2, "Tecnico en enfermeria", null },
                    { 3, "Técnico en Diseño Gráfico", null }
                });

            migrationBuilder.InsertData(
                table: "preguntasDelTestVocacional",
                columns: new[] { "PregunataId", "pregunta" },
                values: new object[,]
                {
                    { 35, "Interprete musical del genero de tu preferencia" },
                    { 34, "Realizar una actividad artistica" },
                    { 33, "Escribir mensajes electronicos o chatear con tus amigos por Internet" },
                    { 32, "Ver a tus amigos organizar una fiesta con ellos" },
                    { 29, "Construir objeto o muebles" },
                    { 30, "Trabajar al aire libre, fuera de la ciudad" },
                    { 28, "LLevar las cuentas de una institucion" },
                    { 27, "Investigar el origen de las costumbres de los pueblos" },
                    { 36, "Diseñador de software" },
                    { 31, "Cuidar a niños pequeños" },
                    { 37, "Asistir a una conferencia cientifica a un museo" },
                    { 45, "Tocar en la orquesta de tu ciudad" },
                    { 39, "Reparar algun aparato descompuesto" },
                    { 40, "Ir de paseo al campo o a la playa" },
                    { 41, "Ser miembro especial de la Cruz Roja Internacional para casos de desastre" },
                    { 42, "Gerente de mercadotecnia de una compañia" },
                    { 43, "Articulista de un periodico" },
                    { 44, "Dieseñador de las portadas de una revista" },
                    { 26, "Aprender a usar programas de programacion" },
                    { 46, "Poner en orden tu coleccion favorita" },
                    { 47, "Coordinador de un grupo cientifico de vanguardia" },
                    { 48, "Contador general de una empresa" },
                    { 38, "Calcular el presupuesto de una familia" },
                    { 25, "Aprender a tocar un instrumento musical" },
                    { 17, "Hacer experimentos en un laboratorio" },
                    { 23, "Hacer versos para una publicacion" },
                    { 1, "Atender y cuidar enfermos" },
                    { 2, "Intervenir activamente en las discuciones de la clase" },
                    { 3, "Escribir cuentos, cronicas o articulos" },
                    { 4, "Dibujar y pintar" },
                    { 5, "Cantar o tocar un instrumento en publico" },
                    { 6, "Llevar en orden tus libros y cuadernos" },
                    { 7, "Conocer y estudiar la estructura de las plantas y de los animales" },
                    { 8, "Resolver cuestionarios de matematicas" },
                    { 9, "Armar y desarmar objetos mecanicos" },
                    { 10, "Salir de excursion" },
                    { 24, "Encargarte del decorado del lugar para un festival" },
                    { 11, "Proteger a los muchachos menores del grupo" },
                    { 13, "Leer obras literarios" },
                    { 14, "Moldear el barrio, plastilina o cualquier otro material" },
                    { 15, "Escuchar musica clasica" },
                    { 16, "Ordenar y clasificar los libro de una biblioteca" },
                    { 49, "Autoridad en la construccion de ciertas estructuras arquitectonocas" },
                    { 18, "Resolver problemas de aritmetica" },
                    { 19, "Manejar herramientas y maquinaria" },
                    { 20, "Pertenecer a un grupo de exploradores" },
                    { 21, "Ser miembro de una sociedad de ayuda y asistencia" },
                    { 22, "Dirigir la campaña politica para un candidato estudiantil" },
                    { 12, "Ser jefe de un grupo" },
                    { 50, "Encargado de la ampliacion de la red carretera del pais" }
                });

            migrationBuilder.InsertData(
                table: "catCarreras",
                columns: new[] { "idCarrera", "Carrera", "areasCarreraId", "areasTestId" },
                values: new object[,]
                {
                    { 3, "Licenciado en Nutrición", 2, 1 },
                    { 4, "Licenciado en Optometría", 2, 1 },
                    { 5, "Contador Público", 3, 1 },
                    { 6, "Licenciatura en Administración Industrial", 3, 1 },
                    { 8, "Arte y Diseño", 4, 4 },
                    { 1, "Ingeniería en Comunicaciones y Electrónica", 1, 6 },
                    { 7, "Administración de Archivos y Gestión Documental", 4, 6 },
                    { 2, "Ingeniería Aeronáutica", 1, 10 }
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
                name: "IX_carrerasImpartadas_catCarrerasId",
                table: "carrerasImpartadas",
                column: "catCarrerasId");

            migrationBuilder.CreateIndex(
                name: "IX_carrerasImpartadas_usuarioUniversidad",
                table: "carrerasImpartadas",
                column: "usuarioUniversidad");

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
                name: "IX_catCarreras_areasCarreraId",
                table: "catCarreras",
                column: "areasCarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_catCarreras_areasTestId",
                table: "catCarreras",
                column: "areasTestId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Relaciones_idAlumno",
                table: "Relaciones",
                column: "idAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_Relaciones_idUniversidad",
                table: "Relaciones",
                column: "idUniversidad");

            migrationBuilder.CreateIndex(
                name: "IX_valorPreguntas_areasTestID",
                table: "valorPreguntas",
                column: "areasTestID");

            migrationBuilder.CreateIndex(
                name: "IX_valorPreguntas_idAlumno",
                table: "valorPreguntas",
                column: "idAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_valorPreguntas_idPregunta",
                table: "valorPreguntas",
                column: "idPregunta");
            string procedure = @"
            create PROCEDURE Realizarcalculodeltest
            @idalumno uniqueidentifier
            AS
            BEGIN
	        declare @query table (ida uniqueidentifier ,area int )
	        insert into  @query  select top 3 val.idAlumno ,art.areaTestId  from valorPreguntas val inner join AreasTests art on val.areasTestID = art.areaTestId where val.idAlumno = @idalumno group by art.areaTestId, val.idAlumno order by SUM(val.valor) desc
	        select*from @query
	        insert into carrerasDeseadas select*from @query
            END";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"DROP PROCEDURE Realizarcalculodeltest";
            migrationBuilder.Sql(procedure);
            migrationBuilder.DropTable(
                name: "alumnos");

            migrationBuilder.DropTable(
                name: "carBecas");

            migrationBuilder.DropTable(
                name: "carrerasDeseadas");

            migrationBuilder.DropTable(
                name: "carrerasImpartadas");

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
                name: "Relaciones");

            migrationBuilder.DropTable(
                name: "universidades");

            migrationBuilder.DropTable(
                name: "valorPreguntas");

            migrationBuilder.DropTable(
                name: "catCarreras");

            migrationBuilder.DropTable(
                name: "carreraTecnicas");

            migrationBuilder.DropTable(
                name: "universidadesUsuario");

            migrationBuilder.DropTable(
                name: "preguntasDelTestVocacional");

            migrationBuilder.DropTable(
                name: "catAreasCarrera");

            migrationBuilder.DropTable(
                name: "AreasTests");

            migrationBuilder.DropTable(
                name: "alumnosUsuarios");
        }
    }
}
