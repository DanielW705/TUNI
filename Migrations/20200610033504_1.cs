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
                name: "Administradores",
                columns: table => new
                {
                    idAmon = table.Column<Guid>(nullable: false),
                    username = table.Column<string>(nullable: false),
                    contraseña = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.idAmon);
                });

            migrationBuilder.CreateTable(
                name: "alumnosUsuarios",
                columns: table => new
                {
                    idAlumno = table.Column<Guid>(nullable: false),
                    usuario = table.Column<string>(nullable: false),
                    contraseña = table.Column<string>(nullable: false)
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
                name: "rechazos",
                columns: table => new
                {
                    noderechazo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idUniversidad = table.Column<Guid>(nullable: false),
                    idAlumno = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rechazos", x => x.noderechazo);
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
                    nombre = table.Column<string>(nullable: false),
                    apPaterno = table.Column<string>(nullable: false),
                    apMaterno = table.Column<string>(nullable: false)
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
                name: "solicitar",
                columns: table => new
                {
                    nodesolicitud = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idAlumno = table.Column<Guid>(nullable: false),
                    idUniversidad = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solicitar", x => x.nodesolicitud);
                    table.ForeignKey(
                        name: "RelacionsolicitudAlumno",
                        column: x => x.idAlumno,
                        principalTable: "alumnosUsuarios",
                        principalColumn: "idAlumno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "RelacionsolicutudUni",
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
                table: "Administradores",
                columns: new[] { "idAmon", "contraseña", "username" },
                values: new object[,]
                {
                    { new Guid("21865e15-4a84-4b33-b07f-0247fe861867"), "123aEFGJnfsa", "ADMINISTRADOR 1" },
                    { new Guid("5c7c227c-ba0c-4430-8b86-b5c0fd63d394"), "aggvkKBQ5hp", "ADMINISTRADOR 2" },
                    { new Guid("670540ff-2db8-4618-b35b-c50755c964fe"), "HzmJlaLKU1f", "ADMINISTRADOR 3" },
                    { new Guid("a09e073c-0d66-4f26-bd80-fc68a82de2b3"), "Xmxg82RTiuQV", "ADMINISTRADOR 4" },
                    { new Guid("5106f317-7d84-496e-b269-e00c8ca050d4"), "bgTR1apIK1ye", "ADMINISTRADOR 5" }
                });

            migrationBuilder.InsertData(
                table: "AreasTests",
                columns: new[] { "areaTestId", "areaDelTest" },
                values: new object[,]
                {
                    { 11, "Destreza manual" },
                    { 10, "Aire Libre" },
                    { 9, "Mecanico Constructivo" },
                    { 7, "Cientifica" },
                    { 6, "Organizacion" },
                    { 5, "Musical" },
                    { 4, "Artistico Plastico" },
                    { 8, "Calculo" },
                    { 2, "Ejecutiva Persuasiva" },
                    { 1, "Servicio Social" },
                    { 3, "Verbal" }
                });

            migrationBuilder.InsertData(
                table: "catAreasCarrera",
                columns: new[] { "idArea", "area" },
                values: new object[,]
                {
                    { 4, "Humanidades y de las Artes" },
                    { 3, "Ciencias Sociales y administrativas" },
                    { 2, "Ciencias Biologicas, Quimicas de la salud" },
                    { 1, "Fisico matematicas" }
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
                    { 27, "Investigar el origen de las costumbres de los pueblos" },
                    { 30, "Trabajar al aire libre, fuera de la ciudad" },
                    { 29, "Construir objeto o muebles" },
                    { 28, "LLevar las cuentas de una institucion" },
                    { 36, "Diseñador de software" },
                    { 31, "Cuidar a niños pequeños" },
                    { 37, "Asistir a una conferencia cientifica a un museo" },
                    { 48, "Contador general de una empresa" },
                    { 39, "Reparar algun aparato descompuesto" },
                    { 40, "Ir de paseo al campo o a la playa" },
                    { 41, "Ser miembro especial de la Cruz Roja Internacional para casos de desastre" },
                    { 42, "Gerente de mercadotecnia de una compañia" },
                    { 43, "Articulista de un periodico" },
                    { 44, "Dieseñador de las portadas de una revista" },
                    { 45, "Tocar en la orquesta de tu ciudad" },
                    { 46, "Poner en orden tu coleccion favorita" },
                    { 47, "Coordinador de un grupo cientifico de vanguardia" },
                    { 26, "Aprender a usar programas de programacion" },
                    { 38, "Calcular el presupuesto de una familia" },
                    { 25, "Aprender a tocar un instrumento musical" },
                    { 14, "Moldear el barrio, plastilina o cualquier otro material" },
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
                    { 49, "Autoridad en la construccion de ciertas estructuras arquitectonocas" },
                    { 15, "Escuchar musica clasica" },
                    { 16, "Ordenar y clasificar los libro de una biblioteca" },
                    { 17, "Hacer experimentos en un laboratorio" },
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
                    { 21, "Ingenieria Topografica y Fotogrametrica", 1, 1 },
                    { 88, "Investigacion Biomedica Basica", 2, 7 },
                    { 91, "Medicina Veterinaria y Zootecnia", 2, 7 },
                    { 93, "Neurociencias", 2, 7 },
                    { 99, "Quimica", 2, 7 },
                    { 100, "Quimica de Alimentos", 2, 7 },
                    { 101, "Quimica e Ingenieria en Materiales", 2, 7 },
                    { 102, "Quimica Farmaceuticao Biologica", 2, 7 },
                    { 71, "Quimico Farmaceutico Indisctrial", 2, 7 },
                    { 103, "Quimica Industrial", 2, 7 },
                    { 144, "Filosofia", 4, 7 },
                    { 145, "Geohistoria", 4, 7 },
                    { 146, "Historia", 4, 7 },
                    { 147, "Historia del Arte", 4, 7 },
                    { 4, "Ingenieria en informatica", 1, 8 },
                    { 11, "Ingenieria en sistemas Energeticos y Redes Inteligentes", 1, 8 },
                    { 19, "Ingenieria en Transporte", 1, 8 },
                    { 108, "Antropologia", 3, 7 },
                    { 25, "Ingenieria Biomedica", 1, 8 },
                    { 69, "Licenciado en Biologia", 2, 7 },
                    { 64, "Tecnologia", 1, 7 },
                    { 9, "Ingenieria en Sistemas Automotrices", 1, 7 },
                    { 12, "Ingenieria Quimica Industrial", 1, 7 },
                    { 13, "Ingenieria Quimica Petrolera", 1, 7 },
                    { 14, "Ingenieria Telemeatica", 1, 7 },
                    { 23, "Licenciatura en Ciencias de la Informatica", 1, 7 },
                    { 24, "Licenciatura en Fisica y Matematicas", 1, 7 },
                    { 26, "Ingenieria Biomedica", 1, 7 },
                    { 65, "Tecnologias para la informacion en Cinecias", 1, 7 },
                    { 36, "Ciencias de la Computacion", 1, 7 },
                    { 39, "Fisica", 1, 7 },
                    { 40, "Fisica Biomedica", 1, 7 },
                    { 49, "Ingenieria en Telecomunicaciones, Sistemas y Electronica", 1, 7 },
                    { 56, "Ingenieria Mecatronica", 1, 7 },
                    { 57, "Ingenieria Petrolera", 1, 7 },
                    { 58, "Ingenieria Quimica", 1, 7 },
                    { 63, "Nanotecnologia", 1, 7 },
                    { 37, "Ciencias de la Tierra", 1, 7 },
                    { 31, "Actuaria", 1, 8 },
                    { 34, "Ciencias de Datos", 1, 8 },
                    { 42, "Ingenieria Ambiental", 1, 8 },
                    { 76, "Ciencias Agroforestales", 2, 10 },
                    { 77, "Ciencias Agrogenomicas", 2, 10 },
                    { 78, "Ciencias Ambientales", 2, 10 },
                    { 79, "Ciencias Genomicas", 2, 10 },
                    { 86, "Ingenieria Agricola", 2, 10 },
                    { 6, "Ingeniería en Metalurgia y Materiales", 1, 11 },
                    { 16, "Ingeniero Arquitecto", 1, 11 },
                    { 52, "Ingenieria Geomatica", 1, 10 },
                    { 18, "Ingenieria Matematica", 1, 11 },
                    { 27, "Ingenieria Bionica", 1, 11 },
                    { 28, "Ingenieria Bioquimica", 1, 11 },
                    { 38, "Diseño Industrial", 1, 11 },
                    { 44, "Ingenieria Electrica Electronica", 1, 11 },
                    { 45, "Ingenieria en Computacion", 1, 11 },
                    { 46, "Ingenieria en Energias Renovables", 1, 11 },
                    { 73, "Medico Cirujano Partero", 2, 11 },
                    { 20, "Ingenieria Farmaceutica", 1, 11 },
                    { 51, "Ingenieria Geologica", 1, 10 },
                    { 50, "Ingenieria Geofisica", 1, 10 },
                    { 43, "Ingenieria de Minas y Metalurgia", 1, 10 },
                    { 47, "Ingenieria en Sistemas Biomedicos", 1, 8 },
                    { 48, "Ingenieria en Telecomunicaicones", 1, 8 },
                    { 53, "Ingenieria Industrial", 1, 8 },
                    { 54, "Ingenieria Mecanica", 1, 8 },
                    { 55, "Ingenieria Mecanica Electrica", 1, 8 },
                    { 104, "Contador Publico", 3, 8 },
                    { 125, "Geografia", 3, 8 },
                    { 126, "Geografia Aplicada", 3, 8 },
                    { 2, "Ingeniería Aeronáutica", 1, 9 },
                    { 10, "Ingenieria en Sistemas Computacionales", 1, 9 },
                    { 29, "Ingenieria Biotecnologica", 1, 9 },
                    { 30, "Ingenieria Civil", 1, 9 },
                    { 35, "Ciencias de Materiales Sustentables", 1, 9 },
                    { 59, "Ingenieria Quimica Metalurgica", 1, 9 },
                    { 97, "Ortesis y protesis", 2, 9 },
                    { 22, "Licenciatura en ciencia de datos", 1, 10 },
                    { 41, "Geociencias", 1, 10 },
                    { 8, "Ingenieria en Robotica Industrial", 1, 7 },
                    { 5, "Ingeniería en Inteligencia Artificial", 1, 7 },
                    { 137, "Desarrollo y Gestion Interculturales", 4, 6 },
                    { 135, "Bibliotecologia y Estudios de la informacion", 4, 6 },
                    { 130, "Sociologia", 3, 1 },
                    { 131, "Licenciatura en Tabajo Social", 3, 1 },
                    { 140, "Enseñanza de Lengua Extranjera", 4, 1 },
                    { 141, "Enseñansa de Ingles", 4, 1 },
                    { 142, "Estudios Latinoamericanos", 4, 1 },
                    { 156, "Musica Educacion Musical", 4, 1 },
                    { 160, "Pedagogia", 4, 1 },
                    { 128, "Planificacion para el Desarrollo Agropecuario", 3, 1 },
                    { 7, "Ingenieria en Negocios Energeticos Sustentables", 1, 2 },
                    { 89, "Manejo Sustentable de Zonas Costeras", 2, 2 },
                    { 92, "Licenciatura en Medicina", 2, 2 },
                    { 96, "Optometria", 2, 2 },
                    { 105, "Licenciatura en Administracion Industrial", 3, 2 },
                    { 106, "Administracion", 3, 2 },
                    { 107, "Administracion Agropecuaria", 3, 2 },
                    { 110, "Ciencias Politicas y Administracion Publica", 3, 2 },
                    { 87, "Ingenieria en Alimentos", 2, 2 },
                    { 124, "Estudios Sociales y Gestion Local", 3, 1 },
                    { 120, "Desarrollo Comunitario para el Envejecimiento", 3, 1 },
                    { 119, "Derecho", 3, 1 },
                    { 66, "Urbanismo", 1, 1 },
                    { 67, "Licenciado en Nutricion", 2, 1 },
                    { 68, "Licenciado en Optometria", 2, 1 },
                    { 70, "Licenciado en Diagnostica", 2, 1 },
                    { 72, "Medico Cirujano Homeopata", 2, 1 },
                    { 80, "Cirujano Dentista", 2, 1 },
                    { 81, "Ecologia", 2, 1 },
                    { 82, "Licenciatura en Enfermeria", 2, 1 },
                    { 83, "Licenciatura en enfermeria y Obstetricia", 2, 1 },
                    { 84, "Farmacia", 2, 1 },
                    { 85, "Fisioterapia", 2, 1 },
                    { 90, "Medico Cirujano", 2, 1 },
                    { 94, "Nutriologia", 2, 1 },
                    { 95, "Licenciado en Odontologia", 2, 1 },
                    { 98, "Licenciado en Psicologia", 2, 1 },
                    { 117, "Licenciado en Relaciones Comerciales", 3, 1 },
                    { 118, "Licenciado en Turismo", 3, 1 },
                    { 121, "Desarrollo Territorial", 3, 2 },
                    { 74, "Quimico Bacteriologo Parasitologo", 2, 11 },
                    { 122, "Licenciatura en Economia", 3, 2 },
                    { 109, "Ciencias de la Comunicacion", 3, 3 },
                    { 154, "Musica Canto", 4, 5 },
                    { 155, "Musica Composicion", 4, 5 },
                    { 157, "Musica Instrumentista", 4, 5 },
                    { 158, "Musica Piano", 4, 5 },
                    { 159, "Musica y Tecnologia Artistica", 4, 5 },
                    { 1, "Ingeniería en Comunicaciones y Electrónica", 1, 6 },
                    { 3, "Ingeniería en Control y Automatización", 1, 6 },
                    { 143, "Etnomusicologia", 4, 5 },
                    { 15, "Ingenieria Textil", 1, 6 },
                    { 61, "Matematicas Aplicadas", 1, 6 },
                    { 62, "Matematicas Aplicadas y Computacion", 1, 6 },
                    { 111, "Licenciatura en Administracion y Desarrollo Empresarial", 3, 6 },
                    { 112, "Licenciatura en Archivonomia", 3, 6 },
                    { 113, "Licencitura en Bibliotecnomia", 3, 6 },
                    { 116, "Contaduria", 3, 6 },
                    { 132, "Administracion de Arcivos y Gestion Documental", 4, 6 },
                    { 60, "Matematicas", 1, 6 },
                    { 161, "Teatro y actuacion", 4, 4 },
                    { 152, "Literatura Dramatica y Teatro", 4, 4 },
                    { 139, "Diseño y Comunicacion Visual", 4, 4 },
                    { 114, "Comunicacion", 3, 3 },
                    { 115, "Comunicacion y Periodismo", 3, 3 },
                    { 127, "Licenciatura en Negocios Internacionales", 3, 3 },
                    { 129, "Relaciones Internacionales", 3, 3 },
                    { 148, "Lengua y literaturas Hispanicas", 4, 3 },
                    { 149, "Lengua y literaturas Modernas", 4, 3 },
                    { 150, "lenguas Clasicas", 4, 3 },
                    { 151, "Linguistica Aplicada", 4, 3 },
                    { 153, "Literatura Intercultural", 4, 3 },
                    { 162, "Traduccion", 4, 3 },
                    { 17, "Ingenieria Metalirgica", 1, 4 },
                    { 32, "Arquitectura", 1, 4 },
                    { 33, "Arquitectura de Paisajes", 1, 4 },
                    { 133, "Arte y Diseño", 4, 4 },
                    { 134, "Artes Visuales", 4, 4 },
                    { 136, "Cinematografia", 4, 4 },
                    { 138, "Diseño Grafico", 4, 4 },
                    { 123, "Economia Industrial", 3, 2 },
                    { 75, "Ciencia Forense", 2, 11 }
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
                name: "IX_solicitar_idAlumno",
                table: "solicitar",
                column: "idAlumno",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_solicitar_idUniversidad",
                table: "solicitar",
                column: "idUniversidad",
                unique: true);

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
---=======================================
--Author:		Daniel Gonzalez Martinez
-- Create date: 25 / 05 / 2020
-- Description: select* from alumnosUsuarios
--select* from carrerasDeseadas
--select* from Relaciones
--delete from alumnosUsuarios
--  Este store procedure realiza la accion de buscar el test del alumno que se necesita
-- =============================================
create PROCEDURE Realizarcalculodeltest
@idalumno uniqueidentifier
AS
BEGIN
declare @query table(ida uniqueidentifier, area int)
insert into  @query select top 3 val.idAlumno ,art.areaTestId from valorPreguntas val inner join AreasTests art on val.areasTestID = art.areaTestId where val.idAlumno = @idalumno group by art.areaTestId, val.idAlumno order by SUM(val.valor) desc
select* from @query
 insert into carrerasDeseadas select* from @query
  insert into Relaciones select distinct car.idAlumno, ci.usuarioUniversidad from carrerasDeseadas car join carrerasImpartadas ci on car.idCarrera = ci.catCarrerasId where car.idAlumno = @idalumno
END
GO
-- =============================================
--Author:		Daniel
-- Create date: 26 / 05 / 2020
-- Description: SOLO REALIZA LA RELACION
-- =============================================
CREATE PROCEDURE Realizarlarelacion
@idalumno uniqueidentifier
AS
BEGIN
insert into  Relaciones select distinct car.idAlumno, ci.usuarioUniversidad from carrerasDeseadas car join carrerasImpartadas ci on car.idCarrera = ci.catCarrerasId where car.idAlumno = @idalumno
END
GO";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administradores");

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
                name: "rechazos");

            migrationBuilder.DropTable(
                name: "Relaciones");

            migrationBuilder.DropTable(
                name: "solicitar");

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
