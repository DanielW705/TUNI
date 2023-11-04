﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TUNIWEB.Models;

namespace TUNIWEB.Migrations
{
    [DbContext(typeof(BB))]
    [Migration("20200526035112_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TUNIWEB.Models.Alumno", b =>
                {
                    b.Property<Guid>("idAlumno");

                    b.Property<string>("apMaterno");

                    b.Property<string>("apPaterno");

                    b.Property<string>("nombre");

                    b.HasKey("idAlumno");

                    b.ToTable("alumnos");
                });

            modelBuilder.Entity("TUNIWEB.Models.areasCarrera", b =>
                {
                    b.Property<int>("idArea")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("area");

                    b.HasKey("idArea");

                    b.ToTable("catAreasCarrera");

                    b.HasData(
                        new { idArea = 1, area = "Fisico matematicas" },
                        new { idArea = 2, area = "Ciencias Biologicas, Quimicas de la salud" },
                        new { idArea = 3, area = "Ciencias Sociales y administrativas" },
                        new { idArea = 4, area = "Humanidades y de las Artes" }
                    );
                });

            modelBuilder.Entity("TUNIWEB.Models.areasTest", b =>
                {
                    b.Property<int>("areaTestId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("areaDelTest");

                    b.HasKey("areaTestId");

                    b.ToTable("AreasTests");

                    b.HasData(
                        new { areaTestId = 1, areaDelTest = "Servicio Social" },
                        new { areaTestId = 2, areaDelTest = "Ejecutiva Persuasiva" },
                        new { areaTestId = 3, areaDelTest = "Verbal" },
                        new { areaTestId = 4, areaDelTest = "Artistico Plastico" },
                        new { areaTestId = 5, areaDelTest = "Musical" },
                        new { areaTestId = 6, areaDelTest = "Organizacion" },
                        new { areaTestId = 7, areaDelTest = "Cientifica" },
                        new { areaTestId = 8, areaDelTest = "Calculo" },
                        new { areaTestId = 9, areaDelTest = "Mecanico Constructivo" },
                        new { areaTestId = 10, areaDelTest = "Aire Libre" }
                    );
                });

            modelBuilder.Entity("TUNIWEB.Models.carBeca", b =>
                {
                    b.Property<int>("noBeca")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("becaInstitucional");

                    b.Property<byte[]>("doc");

                    b.Property<Guid>("idUniversidad");

                    b.HasKey("noBeca");

                    b.HasIndex("idUniversidad");

                    b.ToTable("carBecas");
                });

            modelBuilder.Entity("TUNIWEB.Models.carrerasDeseadas", b =>
                {
                    b.Property<int>("noDeCarreraDeseada")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("idAlumno");

                    b.Property<int>("idCarrera");

                    b.HasKey("noDeCarreraDeseada");

                    b.HasIndex("idAlumno");

                    b.HasIndex("idCarrera");

                    b.ToTable("carrerasDeseadas");
                });

            modelBuilder.Entity("TUNIWEB.Models.carrerasimpartidas", b =>
                {
                    b.Property<int>("noderelacion")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("catCarrerasId");

                    b.Property<Guid>("usuarioUniversidad");

                    b.HasKey("noderelacion");

                    b.HasIndex("catCarrerasId");

                    b.HasIndex("usuarioUniversidad");

                    b.ToTable("carrerasImpartadas");
                });

            modelBuilder.Entity("TUNIWEB.Models.carreraTecnica", b =>
                {
                    b.Property<int>("noderelcat")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("catalogoCarrerasTecnicasId");

                    b.Property<Guid>("idAlumno");

                    b.HasKey("noderelcat");

                    b.HasIndex("idAlumno");

                    b.ToTable("carreraTecnicas");
                });

            modelBuilder.Entity("TUNIWEB.Models.catalogoCarrerasTecnicas", b =>
                {
                    b.Property<int>("carreTecnicaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("carreraTecnica");

                    b.Property<int?>("relcart_Catnoderelcat");

                    b.HasKey("carreTecnicaId");

                    b.HasIndex("relcart_Catnoderelcat");

                    b.ToTable("catalogoCarrerasT");

                    b.HasData(
                        new { carreTecnicaId = 1, carreraTecnica = "Tecnico en informatica" },
                        new { carreTecnicaId = 2, carreraTecnica = "Tecnico en enfermeria" },
                        new { carreTecnicaId = 3, carreraTecnica = "Técnico en Diseño Gráfico" }
                    );
                });

            modelBuilder.Entity("TUNIWEB.Models.catalogoDeMapasCurriculares", b =>
                {
                    b.Property<int>("noDeMapaCurricular")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("doc");

                    b.Property<int>("idCarrera");

                    b.Property<Guid>("idUniversidad");

                    b.Property<string>("mapacurricular");

                    b.HasKey("noDeMapaCurricular");

                    b.HasIndex("idUniversidad");

                    b.ToTable("catalogoDeMapasCuarriculares");
                });

            modelBuilder.Entity("TUNIWEB.Models.catCarreras", b =>
                {
                    b.Property<int>("idCarrera")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Carrera");

                    b.Property<int>("areasCarreraId");

                    b.Property<int>("areasTestId");

                    b.HasKey("idCarrera");

                    b.HasIndex("areasCarreraId");

                    b.HasIndex("areasTestId");

                    b.ToTable("catCarreras");

                    b.HasData(
                        new { idCarrera = 1, Carrera = "Ingeniería en Comunicaciones y Electrónica", areasCarreraId = 1, areasTestId = 6 },
                        new { idCarrera = 2, Carrera = "Ingeniería Aeronáutica", areasCarreraId = 1, areasTestId = 10 },
                        new { idCarrera = 3, Carrera = "Licenciado en Nutrición", areasCarreraId = 2, areasTestId = 1 },
                        new { idCarrera = 4, Carrera = "Licenciado en Optometría", areasCarreraId = 2, areasTestId = 1 },
                        new { idCarrera = 5, Carrera = "Contador Público", areasCarreraId = 3, areasTestId = 1 },
                        new { idCarrera = 6, Carrera = "Licenciatura en Administración Industrial", areasCarreraId = 3, areasTestId = 1 },
                        new { idCarrera = 7, Carrera = "Administración de Archivos y Gestión Documental", areasCarreraId = 4, areasTestId = 6 },
                        new { idCarrera = 8, Carrera = "Arte y Diseño", areasCarreraId = 4, areasTestId = 4 }
                    );
                });

            modelBuilder.Entity("TUNIWEB.Models.contactos", b =>
                {
                    b.Property<int>("noDeContacto")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("contacto");

                    b.Property<Guid>("idUniversidad");

                    b.HasKey("noDeContacto");

                    b.HasIndex("idUniversidad");

                    b.ToTable("contactos");
                });

            modelBuilder.Entity("TUNIWEB.Models.DatosAcademicos", b =>
                {
                    b.Property<Guid>("idAlumno");

                    b.Property<string>("boletaGlobal");

                    b.Property<byte[]>("doc");

                    b.HasKey("idAlumno");

                    b.ToTable("datosAcademicos");
                });

            modelBuilder.Entity("TUNIWEB.Models.egreso", b =>
                {
                    b.Property<Guid>("idUniversidad");

                    b.Property<byte[]>("doc");

                    b.Property<string>("nivelEgreso");

                    b.HasKey("idUniversidad");

                    b.ToTable("egresos");
                });

            modelBuilder.Entity("TUNIWEB.Models.empresaAsociadas", b =>
                {
                    b.Property<int>("noDeEmpresaAsociada")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("empresaAsociada");

                    b.Property<Guid>("idUniversidad");

                    b.HasKey("noDeEmpresaAsociada");

                    b.HasIndex("idUniversidad");

                    b.ToTable("empresaAsociadas");
                });

            modelBuilder.Entity("TUNIWEB.Models.informacion", b =>
                {
                    b.Property<int>("idnoRecon")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("doc");

                    b.Property<Guid>("idAlumno");

                    b.Property<string>("reconocimiento");

                    b.HasKey("idnoRecon");

                    b.HasIndex("idAlumno");

                    b.ToTable("informaciones");
                });

            modelBuilder.Entity("TUNIWEB.Models.ingreso", b =>
                {
                    b.Property<Guid>("idUniversidad");

                    b.Property<byte[]>("doc");

                    b.Property<string>("metodoIngreso");

                    b.HasKey("idUniversidad");

                    b.ToTable("ingresos");
                });

            modelBuilder.Entity("TUNIWEB.Models.PreguntasTestVocacional", b =>
                {
                    b.Property<int>("PregunataId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("pregunta");

                    b.HasKey("PregunataId");

                    b.ToTable("preguntasDelTestVocacional");

                    b.HasData(
                        new { PregunataId = 1, pregunta = "Atender y cuidar enfermos" },
                        new { PregunataId = 2, pregunta = "Intervenir activamente en las discuciones de la clase" },
                        new { PregunataId = 3, pregunta = "Escribir cuentos, cronicas o articulos" },
                        new { PregunataId = 4, pregunta = "Dibujar y pintar" },
                        new { PregunataId = 5, pregunta = "Cantar o tocar un instrumento en publico" },
                        new { PregunataId = 6, pregunta = "Llevar en orden tus libros y cuadernos" },
                        new { PregunataId = 7, pregunta = "Conocer y estudiar la estructura de las plantas y de los animales" },
                        new { PregunataId = 8, pregunta = "Resolver cuestionarios de matematicas" },
                        new { PregunataId = 9, pregunta = "Armar y desarmar objetos mecanicos" },
                        new { PregunataId = 10, pregunta = "Salir de excursion" },
                        new { PregunataId = 11, pregunta = "Proteger a los muchachos menores del grupo" },
                        new { PregunataId = 12, pregunta = "Ser jefe de un grupo" },
                        new { PregunataId = 13, pregunta = "Leer obras literarios" },
                        new { PregunataId = 14, pregunta = "Moldear el barrio, plastilina o cualquier otro material" },
                        new { PregunataId = 15, pregunta = "Escuchar musica clasica" },
                        new { PregunataId = 16, pregunta = "Ordenar y clasificar los libro de una biblioteca" },
                        new { PregunataId = 17, pregunta = "Hacer experimentos en un laboratorio" },
                        new { PregunataId = 18, pregunta = "Resolver problemas de aritmetica" },
                        new { PregunataId = 19, pregunta = "Manejar herramientas y maquinaria" },
                        new { PregunataId = 20, pregunta = "Pertenecer a un grupo de exploradores" },
                        new { PregunataId = 21, pregunta = "Ser miembro de una sociedad de ayuda y asistencia" },
                        new { PregunataId = 22, pregunta = "Dirigir la campaña politica para un candidato estudiantil" },
                        new { PregunataId = 23, pregunta = "Hacer versos para una publicacion" },
                        new { PregunataId = 24, pregunta = "Encargarte del decorado del lugar para un festival" },
                        new { PregunataId = 25, pregunta = "Aprender a tocar un instrumento musical" },
                        new { PregunataId = 26, pregunta = "Aprender a usar programas de programacion" },
                        new { PregunataId = 27, pregunta = "Investigar el origen de las costumbres de los pueblos" },
                        new { PregunataId = 28, pregunta = "LLevar las cuentas de una institucion" },
                        new { PregunataId = 29, pregunta = "Construir objeto o muebles" },
                        new { PregunataId = 30, pregunta = "Trabajar al aire libre, fuera de la ciudad" },
                        new { PregunataId = 31, pregunta = "Cuidar a niños pequeños" },
                        new { PregunataId = 32, pregunta = "Ver a tus amigos organizar una fiesta con ellos" },
                        new { PregunataId = 33, pregunta = "Escribir mensajes electronicos o chatear con tus amigos por Internet" },
                        new { PregunataId = 34, pregunta = "Realizar una actividad artistica" },
                        new { PregunataId = 35, pregunta = "Interprete musical del genero de tu preferencia" },
                        new { PregunataId = 36, pregunta = "Diseñador de software" },
                        new { PregunataId = 37, pregunta = "Asistir a una conferencia cientifica a un museo" },
                        new { PregunataId = 38, pregunta = "Calcular el presupuesto de una familia" },
                        new { PregunataId = 39, pregunta = "Reparar algun aparato descompuesto" },
                        new { PregunataId = 40, pregunta = "Ir de paseo al campo o a la playa" },
                        new { PregunataId = 41, pregunta = "Ser miembro especial de la Cruz Roja Internacional para casos de desastre" },
                        new { PregunataId = 42, pregunta = "Gerente de mercadotecnia de una compañia" },
                        new { PregunataId = 43, pregunta = "Articulista de un periodico" },
                        new { PregunataId = 44, pregunta = "Dieseñador de las portadas de una revista" },
                        new { PregunataId = 45, pregunta = "Tocar en la orquesta de tu ciudad" },
                        new { PregunataId = 46, pregunta = "Poner en orden tu coleccion favorita" },
                        new { PregunataId = 47, pregunta = "Coordinador de un grupo cientifico de vanguardia" },
                        new { PregunataId = 48, pregunta = "Contador general de una empresa" },
                        new { PregunataId = 49, pregunta = "Autoridad en la construccion de ciertas estructuras arquitectonocas" },
                        new { PregunataId = 50, pregunta = "Encargado de la ampliacion de la red carretera del pais" }
                    );
                });

            modelBuilder.Entity("TUNIWEB.Models.Relacion", b =>
                {
                    b.Property<int>("nodeRelacion")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("idAlumno");

                    b.Property<Guid>("idUniversidad");

                    b.HasKey("nodeRelacion");

                    b.HasIndex("idAlumno");

                    b.HasIndex("idUniversidad");

                    b.ToTable("Relaciones");
                });

            modelBuilder.Entity("TUNIWEB.Models.universidad", b =>
                {
                    b.Property<Guid>("idUnversidad");

                    b.Property<string>("direccion");

                    b.Property<string>("nombre");

                    b.HasKey("idUnversidad");

                    b.ToTable("universidades");
                });

            modelBuilder.Entity("TUNIWEB.Models.UsuarioAlumno", b =>
                {
                    b.Property<Guid>("idAlumno")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("contraseña");

                    b.Property<string>("usuario");

                    b.HasKey("idAlumno");

                    b.ToTable("alumnosUsuarios");
                });

            modelBuilder.Entity("TUNIWEB.Models.UsuarioUniversidad", b =>
                {
                    b.Property<Guid>("idUniversidad")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("contraseña");

                    b.Property<string>("usuario");

                    b.HasKey("idUniversidad");

                    b.ToTable("universidadesUsuario");
                });

            modelBuilder.Entity("TUNIWEB.Models.ValorPregunta", b =>
                {
                    b.Property<int>("noDePregunta")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("areasTestID");

                    b.Property<Guid>("idAlumno");

                    b.Property<int>("idPregunta");

                    b.Property<int>("valor");

                    b.HasKey("noDePregunta");

                    b.HasIndex("areasTestID");

                    b.HasIndex("idAlumno");

                    b.HasIndex("idPregunta");

                    b.ToTable("valorPreguntas");
                });

            modelBuilder.Entity("TUNIWEB.Models.Alumno", b =>
                {
                    b.HasOne("TUNIWEB.Models.UsuarioAlumno", "relAl_Us")
                        .WithOne("relUs_Al")
                        .HasForeignKey("TUNIWEB.Models.Alumno", "idAlumno")
                        .HasConstraintName("Relacion_usuario_alumno")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.carBeca", b =>
                {
                    b.HasOne("TUNIWEB.Models.UsuarioUniversidad", "relCB_USU")
                        .WithMany("relUSU_carB")
                        .HasForeignKey("idUniversidad")
                        .HasConstraintName("Relacion_usuario_becas")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.carrerasDeseadas", b =>
                {
                    b.HasOne("TUNIWEB.Models.UsuarioAlumno", "relCarrD_AL")
                        .WithMany("relAL_CARRD")
                        .HasForeignKey("idAlumno")
                        .HasConstraintName("Relacion_usuario_carreraDeseada")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TUNIWEB.Models.catCarreras", "relCarrD_catcarr")
                        .WithMany("relcatcarr_carrD")
                        .HasForeignKey("idCarrera")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.carrerasimpartidas", b =>
                {
                    b.HasOne("TUNIWEB.Models.catCarreras", "relcarri_catcarr")
                        .WithMany("relcarr_carri")
                        .HasForeignKey("catCarrerasId")
                        .HasConstraintName("relacion_cat_imp")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TUNIWEB.Models.UsuarioUniversidad", "relcarri_unius")
                        .WithMany("relusu_carri")
                        .HasForeignKey("usuarioUniversidad")
                        .HasConstraintName("Relacion_Usuario_carrerasimpartidas")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.carreraTecnica", b =>
                {
                    b.HasOne("TUNIWEB.Models.UsuarioAlumno", "relCarrT_Al")
                        .WithMany("relUs_Cart")
                        .HasForeignKey("idAlumno")
                        .HasConstraintName("Relacion_usuario_carreraTecnica")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.catalogoCarrerasTecnicas", b =>
                {
                    b.HasOne("TUNIWEB.Models.carreraTecnica", "relcart_Cat")
                        .WithMany("relCat_cart")
                        .HasForeignKey("relcart_Catnoderelcat")
                        .HasConstraintName("Relacion_carreratecnica_catalogodecarrerastecnicas");
                });

            modelBuilder.Entity("TUNIWEB.Models.catalogoDeMapasCurriculares", b =>
                {
                    b.HasOne("TUNIWEB.Models.UsuarioUniversidad", "relCATCU_USU")
                        .WithMany("relUSU_CACU")
                        .HasForeignKey("idUniversidad")
                        .HasConstraintName("Relacion_usuario_CatalogoCurriculares")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.catCarreras", b =>
                {
                    b.HasOne("TUNIWEB.Models.areasCarrera", "relcarrarea")
                        .WithMany("relareacarr")
                        .HasForeignKey("areasCarreraId")
                        .HasConstraintName("relacion_cat_areas")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TUNIWEB.Models.areasTest", "relareatestcarr")
                        .WithMany("relareasTestcarr")
                        .HasForeignKey("areasTestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.contactos", b =>
                {
                    b.HasOne("TUNIWEB.Models.UsuarioUniversidad", "relC_USU")
                        .WithMany("relUSU_CON")
                        .HasForeignKey("idUniversidad")
                        .HasConstraintName("Relacion_usuario_contacto")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.DatosAcademicos", b =>
                {
                    b.HasOne("TUNIWEB.Models.UsuarioAlumno", "Us_relDaac")
                        .WithOne("relDaac_Us")
                        .HasForeignKey("TUNIWEB.Models.DatosAcademicos", "idAlumno")
                        .HasConstraintName("Relacion_usuario_datosAcademicos")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.egreso", b =>
                {
                    b.HasOne("TUNIWEB.Models.UsuarioUniversidad", "relE_USU")
                        .WithOne("relUSU_E")
                        .HasForeignKey("TUNIWEB.Models.egreso", "idUniversidad")
                        .HasConstraintName("Relacion_usuario_egreso")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.empresaAsociadas", b =>
                {
                    b.HasOne("TUNIWEB.Models.UsuarioUniversidad", "relEA_USU")
                        .WithMany("relUSU_EA")
                        .HasForeignKey("idUniversidad")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.informacion", b =>
                {
                    b.HasOne("TUNIWEB.Models.UsuarioAlumno", "info_usuario")
                        .WithMany("rel_us_info")
                        .HasForeignKey("idAlumno")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.ingreso", b =>
                {
                    b.HasOne("TUNIWEB.Models.UsuarioUniversidad", "relI_USU")
                        .WithOne("relUSU_I")
                        .HasForeignKey("TUNIWEB.Models.ingreso", "idUniversidad")
                        .HasConstraintName("Relacion_usuario_ingreso")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.Relacion", b =>
                {
                    b.HasOne("TUNIWEB.Models.UsuarioAlumno", "relrel_USA")
                        .WithMany("relUSA_REL")
                        .HasForeignKey("idAlumno")
                        .HasConstraintName("Relacion_usuario_relacion")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TUNIWEB.Models.UsuarioUniversidad", "relrel_USU")
                        .WithMany("relUSU_REL")
                        .HasForeignKey("idUniversidad")
                        .HasConstraintName("Relacion_Usuario_universidad_rel")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.universidad", b =>
                {
                    b.HasOne("TUNIWEB.Models.UsuarioUniversidad", "relU_USU")
                        .WithOne("relUSU_U")
                        .HasForeignKey("TUNIWEB.Models.universidad", "idUnversidad")
                        .HasConstraintName("Relacion_usuarioUniversidad_universidad")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TUNIWEB.Models.ValorPregunta", b =>
                {
                    b.HasOne("TUNIWEB.Models.areasTest", "rel_valor_area")
                        .WithMany("rel_area_valor")
                        .HasForeignKey("areasTestID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TUNIWEB.Models.UsuarioAlumno", "rel_valorpregutna_us")
                        .WithMany("relUSA_VALP")
                        .HasForeignKey("idAlumno")
                        .HasConstraintName("Relacion_usuario_test")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TUNIWEB.Models.PreguntasTestVocacional", "rel_valorpregunta_pregunta")
                        .WithMany("rel_pregunta_valor")
                        .HasForeignKey("idPregunta")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
