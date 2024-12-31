using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;

namespace TUNIWEB.Models
{
    public class TUNIDbContext : DbContext
    {
        public TUNIDbContext(DbContextOptions<TUNIDbContext> options) : base(options)
        {
        }

        public TUNIDbContext()
        {
        }
        private void InitializeProcedures()
        {
            string drop_procedure = @"IF OBJECT_ID('Realizarcalculodeltest','P') is not null
                                        BEGIN
	                                        DROP PROCEDURE Realizarcalculodeltest; 
                                        END;
                                    IF OBJECT_ID('Realizarlarelacion','P') is not null
                                        BEGIN
	                                        DROP PROCEDURE Realizarlarelacion; 
                                        END;";
            string Realizarcalculodeltest = @"CREATE PROCEDURE Realizarcalculodeltest(@idalumno uniqueidentifier)
		    AS
		        BEGIN
			        /**********/
			        declare @query table(i int)
			        /**********************/
			        /****Selecciona las areas que salio mejor calculado de la base de datos *****/
			        insert into @query select Top 3 areasTestID as total from valorPreguntas where idAlumno = @idalumno group by areasTestID order by Sum(valor) desc
			        /***************Inserta en carreras deseadas las carreras que salio mejor **************************/
			        insert into carrerasDeseadas select @idalumno, c.idCarrera from @query q inner join catCarreras c on q.i = c.areasTestId order by q.i
			        /*********Realiza la relacion con las carreras deseadas**************/
			        insert into Relaciones select distinct carde.idAlumno, cari.usuarioUniversidad from carrerasDeseadas carde inner join carrerasImpartadas cari on carde.idCarrera = cari.catCarrerasId where idAlumno = @idalumno
		        END;";
            string Realizarlarelacion =
            @"CREATE PROCEDURE Realizarlarelacion(@idalumno uniqueidentifier)
                AS
                    BEGIN
                        declare @query table(idA uniqueidentifier , idu uniqueidentifier )
                        declare @query2 table(idA uniqueidentifier , idu uniqueidentifier )
                        insert into @query  select distinct car.idAlumno, ci.usuarioUniversidad from carrerasDeseadas car join carrerasImpartadas ci on car.idCarrera = ci.catCarrerasId where car.idAlumno = @idalumno
                        if((select COUNT(re.idAlumno) from @query cu , rechazos re where (cu.ida = re.idAlumno) and (cu.idu = re.idUniversidad) ) > 0)
                            Begin
                                /*******/
                                insert into @query2 select cu.ida, cu.idu from @query cu , rechazos re where (cu.ida != re.idAlumno) and (cu.idu != re.idUniversidad)
                                /*******/
                            END
                        else IF((select COUNT(ac.idalumno) from @query cu , aceptados ac where (cu.ida = ac.idAlumno) and (cu.idu = ac.iduniversidad) )>0)
                                Begin
                                    /*********/
                                    insert into @query2 select cu.ida, cu.idu from @query cu , aceptados ac where (cu.ida != ac.idAlumno) and (cu.idu != ac.iduniversidad)
                                    /******/
                                end
                        else IF((select COUNT(cu.idA)from @query cu, solicitar sol where (cu.idA = sol.idAlumno) and (cu.idu = sol.idUniversidad))>0)
                                BEGIN
                                    insert into @query2 select cu.idA, cu.idu from @query cu, solicitar sol where (cu.idA != sol.idAlumno) and (cu.idu != sol.idUniversidad)
                                end
                        /***Sino se cumple****/
                        else
                            begin
                                /**********/
                                insert into @query2 select* from @query 
                                /**********/
                            end
                        insert into Relaciones   select*from  @query2
                        return 1
                        END;";
            using (DbCommand command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = drop_procedure;
                command.CommandType = System.Data.CommandType.Text;
                Database.OpenConnection();
                command.ExecuteNonQuery();
                Database.CloseConnection();



                command.CommandText = Realizarcalculodeltest;
                command.CommandType = System.Data.CommandType.Text;
                Database.OpenConnection();
                command.ExecuteNonQuery();
                Database.CloseConnection();


                command.CommandText = Realizarlarelacion;
                command.CommandType = System.Data.CommandType.Text;
                Database.OpenConnection();
                command.ExecuteNonQuery();
                Database.CloseConnection();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.EnableSensitiveDataLogging(true)
                .UseLoggerFactory(new LoggerFactory().AddConsole((category, level)
                => level == LogLevel.Information
                && category == DbLoggerCategory.Database.Command.Name, true));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UsuarioAlumno>(entity =>
            {
                entity.HasKey(d => d.idAlumno);
                entity.HasOne(a => a.relUs_Al)
                .WithOne(b => b.relAl_Us)
                .HasForeignKey<UsuarioAlumno>(d => d.idAlumno)
                .HasConstraintName("Relacion_usuario_alumno");
                entity.HasOne(a => a.relDaac_Us)
                .WithOne(b => b.Us_relDaac)
                .HasForeignKey<UsuarioAlumno>(d => d.idAlumno)
                .HasConstraintName("Relacion_usuario_datosAcademicos");
                entity.HasMany(a => a.relUs_Cart)
                .WithOne(b => b.relCarrT_Al)
                .HasConstraintName("Relacion_usuario_carreraTecnica");
                entity.HasMany(a => a.relAL_CARRD)
                .WithOne(b => b.relCarrD_AL)
                .HasForeignKey(d => d.idAlumno)
                .HasConstraintName("Relacion_usuario_carreraDeseada");
                entity.HasMany(a => a.relUSA_VALP)
                .WithOne(b => b.rel_valorpregutna_us)
                .HasForeignKey(d => d.idAlumno)
                .HasConstraintName("Relacion_usuario_test");
                entity.HasMany(a => a.relUSA_REL)
                .WithOne(b => b.relrel_USA)
                .HasForeignKey(d => d.idAlumno)
                .HasConstraintName("Relacion_usuario_relacion");
                entity.Property(d => d.usuario).IsRequired();
                entity.Property(d => d.contraseña).IsRequired();
                entity.HasMany(a => a.relUsa_Pu)
                       .WithOne(p => p.relPub_USA)
                       .HasForeignKey(p => p.idUsuario);
                entity.HasMany(a => a.relUSA_COM)
                               .WithOne(p => p.relCom_USA)
                               .HasForeignKey(p => p.IdUsuario);
            });
            builder.Entity<Alumno>(entity =>
            {
                entity.HasKey(d => d.idAlumno);
                entity.HasOne(a => a.relAl_Us)
                .WithOne(b => b.relUs_Al)
                .HasForeignKey<Alumno>(d => d.idAlumno);
                entity.Property(d => d.nombre).IsRequired();
                entity.Property(d => d.apPaterno).IsRequired();
                entity.Property(d => d.apMaterno).IsRequired();
            });
            builder.Entity<DatosAcademicos>(entity =>
            {
                entity.HasKey(d => d.idAlumno);
                entity.HasOne(a => a.Us_relDaac)
                .WithOne(b => b.relDaac_Us)
                .HasForeignKey<DatosAcademicos>(d => d.idAlumno);
            });
            builder.Entity<catalogoCarrerasTecnicas>(entity =>
            {
                entity.HasKey(d => d.carreTecnicaId);
                entity.HasOne(a => a.relcart_Cat)
                .WithMany(b => b.relCat_cart)
                .HasConstraintName("Relacion_carreratecnica_catalogodecarrerastecnicas");
                entity.HasData(
                    new catalogoCarrerasTecnicas { carreTecnicaId = 1, carreraTecnica = "Tecnico en informatica" },
                    new catalogoCarrerasTecnicas { carreTecnicaId = 2, carreraTecnica = "Tecnico en enfermeria" },
                    new catalogoCarrerasTecnicas { carreTecnicaId = 3, carreraTecnica = "Técnico en Diseño Gráfico" });
            });
            builder.Entity<carreraTecnica>(entity =>
            {
                entity.HasKey(d => d.noderelcat);
            });
            builder.Entity<informacion>(entity =>
            {
                entity.HasKey(d => d.idnoRecon);
                entity.HasOne(a => a.info_usuario)
                .WithMany(b => b.rel_us_info)
                .HasForeignKey(d => d.idAlumno);
            });
            builder.Entity<UsuarioUniversidad>(entity =>
            {
                entity.HasKey(d => d.idUniversidad);
                entity.HasOne(a => a.relUSU_U)
                .WithOne(b => b.relU_USU)
                .HasForeignKey<UsuarioUniversidad>(d => d.idUniversidad)
                .HasConstraintName("Relacion_usuarioUniversidad_universidad");
                entity.HasMany(a => a.relUSU_CACU)
                .WithOne(b => b.relCATCU_USU)
                .HasConstraintName("Relacion_usuario_CatalogoCurriculares");
                entity.HasOne(a => a.relUSU_I)
                .WithOne(b => b.relI_USU)
                .HasForeignKey<UsuarioUniversidad>(d => d.idUniversidad)
                .HasConstraintName("Relacion_usuario_ingreso");
                entity.HasOne(a => a.relUSU_E)
                .WithOne(b => b.relE_USU)
                .HasForeignKey<UsuarioUniversidad>(d => d.idUniversidad)
                .HasConstraintName("Relacion_usuario_egreso");
                entity.HasMany(a => a.relUSU_carB)
                .WithOne(b => b.relCB_USU)
                .HasConstraintName("Relacion_usuario_becas");
                entity.HasMany(a => a.relUSU_CON)
                .WithOne(b => b.relC_USU)
                .HasConstraintName("Relacion_usuario_contacto");
                entity.HasMany(a => a.relusu_carri)
                .WithOne(b => b.relcarri_unius)
                .HasForeignKey(d => d.usuarioUniversidad)
                .HasConstraintName("Relacion_Usuario_carrerasimpartidas");
                entity.HasMany(a => a.relUSU_REL)
                .WithOne(b => b.relrel_USU)
                .HasForeignKey(d => d.idUniversidad)
                .HasConstraintName("Relacion_Usuario_universidad_rel");
                entity.HasMany(a => a.relUSU_PU)
                      .WithOne(p => p.relPub_USU)
                      .HasForeignKey(p => p.idUsuario);
                entity.HasMany(a => a.relUSU_COM)
                      .WithOne(p => p.relCom_USU)
                      .HasForeignKey(p => p.IdUsuario);
            });
            builder.Entity<universidad>(entity =>
            {
                entity.HasKey(d => d.idUnversidad);
                entity.HasOne(a => a.relU_USU)
                .WithOne(b => b.relUSU_U)
                .HasForeignKey<universidad>(d => d.idUnversidad);

            });
            builder.Entity<catalogoDeMapasCurriculares>(entity =>
            {
                entity.HasKey(d => d.noDeMapaCurricular);
                entity.HasOne(a => a.relCATCU_USU)
                .WithMany(b => b.relUSU_CACU)
                .HasForeignKey(d => d.idUniversidad);
            });
            builder.Entity<ingreso>(entity =>
            {
                entity.HasKey(d => d.idUniversidad);
                entity.HasOne(a => a.relI_USU)
                .WithOne(b => b.relUSU_I)
                .HasForeignKey<ingreso>(d => d.idUniversidad);

            });
            builder.Entity<egreso>(entity =>
            {
                entity.HasKey(d => d.idUniversidad);
                entity.HasOne(a => a.relE_USU)
                .WithOne(b => b.relUSU_E)
                .HasForeignKey<egreso>(d => d.idUniversidad);
            });
            builder.Entity<carBeca>(entity =>
            {
                entity.HasKey(d => d.noBeca);
                entity.HasOne(a => a.relCB_USU)
                .WithMany(b => b.relUSU_carB)
                .HasForeignKey(d => d.idUniversidad);
            });
            builder.Entity<empresaAsociadas>(entity =>
            {
                entity.HasKey(d => d.noDeEmpresaAsociada);
                entity.HasOne(a => a.relEA_USU)
                .WithMany(b => b.relUSU_EA)
                .HasForeignKey(d => d.idUniversidad);
            });
            builder.Entity<contactos>(entity =>
            {
                entity.HasKey(d => d.noDeContacto);
                entity.HasOne(a => a.relC_USU)
                .WithMany(b => b.relUSU_CON)
                .HasForeignKey(d => d.idUniversidad);
            });
            builder.Entity<areasCarrera>(entity =>
            {
                entity.HasKey(d => d.idArea);
                entity.Property(d => d.idArea).ValueGeneratedOnAdd();
                entity.HasMany<catCarreras>(a => a.relareacarr)
                .WithOne(b => b.relcarrarea)
                .HasForeignKey(d => d.idCarrera)
                .HasConstraintName("relacion_Areas_Con_Carrera");
                entity.HasData(
                    new areasCarrera { idArea = 1, area = "Fisico matematicas" },
                    new areasCarrera { idArea = 2, area = "Ciencias Biologicas, Quimicas de la salud" },
                    new areasCarrera { idArea = 3, area = "Ciencias Sociales y administrativas" },
                    new areasCarrera { idArea = 4, area = "Humanidades y de las Artes" });

            });
            builder.Entity<catCarreras>(entity =>
            {
                entity.HasKey(d => d.idCarrera);
                entity.HasOne(a => a.relcarrarea)
                .WithMany(b => b.relareacarr)
                .HasForeignKey(d => d.areasCarreraId)
                .HasConstraintName("relacion_cat_areas");
                entity.HasData(
                    new catCarreras { idCarrera = 1, areasCarreraId = 1, areasTestId = 6, Carrera = "Ingeniería en Comunicaciones y Electrónica" },
                    new catCarreras { idCarrera = 2, areasCarreraId = 1, areasTestId = 9, Carrera = "Ingeniería Aeronáutica" },
                    new catCarreras { idCarrera = 3, areasCarreraId = 1, areasTestId = 6, Carrera = "Ingeniería en Control y Automatización" },
                    new catCarreras { idCarrera = 4, areasCarreraId = 1, areasTestId = 8, Carrera = "Ingenieria en informatica" },
                    new catCarreras { idCarrera = 5, areasCarreraId = 1, areasTestId = 7, Carrera = "Ingeniería en Inteligencia Artificial" },
                    new catCarreras { idCarrera = 6, areasCarreraId = 1, areasTestId = 11, Carrera = "Ingeniería en Metalurgia y Materiales" },
                    new catCarreras { idCarrera = 7, areasCarreraId = 1, areasTestId = 2, Carrera = "Ingenieria en Negocios Energeticos Sustentables" },
                    new catCarreras { idCarrera = 8, areasCarreraId = 1, areasTestId = 7, Carrera = "Ingenieria en Robotica Industrial" },
                    new catCarreras { idCarrera = 9, areasCarreraId = 1, areasTestId = 7, Carrera = "Ingenieria en Sistemas Automotrices" },
                    new catCarreras { idCarrera = 10, areasCarreraId = 1, areasTestId = 9, Carrera = "Ingenieria en Sistemas Computacionales" },
                    new catCarreras { idCarrera = 11, areasCarreraId = 1, areasTestId = 8, Carrera = "Ingenieria en sistemas Energeticos y Redes Inteligentes" },
                    new catCarreras { idCarrera = 12, areasCarreraId = 1, areasTestId = 7, Carrera = "Ingenieria Quimica Industrial" },
                    new catCarreras { idCarrera = 13, areasCarreraId = 1, areasTestId = 7, Carrera = "Ingenieria Quimica Petrolera" },
                    new catCarreras { idCarrera = 14, areasCarreraId = 1, areasTestId = 7, Carrera = "Ingenieria Telemeatica" },
                    new catCarreras { idCarrera = 15, areasCarreraId = 1, areasTestId = 6, Carrera = "Ingenieria Textil" },
                    new catCarreras { idCarrera = 16, areasCarreraId = 1, areasTestId = 11, Carrera = "Ingeniero Arquitecto" },
                    new catCarreras { idCarrera = 17, areasCarreraId = 1, areasTestId = 4, Carrera = "Ingenieria Metalirgica" },
                    new catCarreras { idCarrera = 18, areasCarreraId = 1, areasTestId = 11, Carrera = "Ingenieria Matematica" },
                    new catCarreras { idCarrera = 19, areasCarreraId = 1, areasTestId = 8, Carrera = "Ingenieria en Transporte" },
                    new catCarreras { idCarrera = 20, areasCarreraId = 1, areasTestId = 11, Carrera = "Ingenieria Farmaceutica" },
                    new catCarreras { idCarrera = 21, areasCarreraId = 1, areasTestId = 1, Carrera = "Ingenieria Topografica y Fotogrametrica" },
                    new catCarreras { idCarrera = 22, areasCarreraId = 1, areasTestId = 10, Carrera = "Licenciatura en ciencia de datos" },
                    new catCarreras { idCarrera = 23, areasCarreraId = 1, areasTestId = 7, Carrera = "Licenciatura en Ciencias de la Informatica" },
                    new catCarreras { idCarrera = 24, areasCarreraId = 1, areasTestId = 7, Carrera = "Licenciatura en Fisica y Matematicas" },
                    new catCarreras { idCarrera = 25, areasCarreraId = 1, areasTestId = 8, Carrera = "Ingenieria Biomedica" },
                    new catCarreras { idCarrera = 26, areasCarreraId = 1, areasTestId = 7, Carrera = "Ingenieria Biomedica" },
                    new catCarreras { idCarrera = 27, areasCarreraId = 1, areasTestId = 11, Carrera = "Ingenieria Bionica" },
                    new catCarreras { idCarrera = 28, areasCarreraId = 1, areasTestId = 11, Carrera = "Ingenieria Bioquimica" },
                    new catCarreras { idCarrera = 29, areasCarreraId = 1, areasTestId = 9, Carrera = "Ingenieria Biotecnologica" },
                    new catCarreras { idCarrera = 30, areasCarreraId = 1, areasTestId = 9, Carrera = "Ingenieria Civil" },
                    new catCarreras { idCarrera = 31, areasCarreraId = 1, areasTestId = 8, Carrera = "Actuaria" },
                    new catCarreras { idCarrera = 32, areasCarreraId = 1, areasTestId = 4, Carrera = "Arquitectura" },
                    new catCarreras { idCarrera = 33, areasCarreraId = 1, areasTestId = 4, Carrera = "Arquitectura de Paisajes" },
                    new catCarreras { idCarrera = 34, areasCarreraId = 1, areasTestId = 8, Carrera = "Ciencias de Datos" },
                    new catCarreras { idCarrera = 35, areasCarreraId = 1, areasTestId = 9, Carrera = "Ciencias de Materiales Sustentables" },
                    new catCarreras { idCarrera = 36, areasCarreraId = 1, areasTestId = 7, Carrera = "Ciencias de la Computacion" },
                    new catCarreras { idCarrera = 37, areasCarreraId = 1, areasTestId = 7, Carrera = "Ciencias de la Tierra" },
                    new catCarreras { idCarrera = 38, areasCarreraId = 1, areasTestId = 11, Carrera = "Diseño Industrial" },
                    new catCarreras { idCarrera = 39, areasCarreraId = 1, areasTestId = 7, Carrera = "Fisica" },
                    new catCarreras { idCarrera = 40, areasCarreraId = 1, areasTestId = 7, Carrera = "Fisica Biomedica" },
                    new catCarreras { idCarrera = 41, areasCarreraId = 1, areasTestId = 10, Carrera = "Geociencias" },
                    new catCarreras { idCarrera = 42, areasCarreraId = 1, areasTestId = 8, Carrera = "Ingenieria Ambiental" },
                    new catCarreras { idCarrera = 43, areasCarreraId = 1, areasTestId = 10, Carrera = "Ingenieria de Minas y Metalurgia" },
                    new catCarreras { idCarrera = 44, areasCarreraId = 1, areasTestId = 11, Carrera = "Ingenieria Electrica Electronica" },
                    new catCarreras { idCarrera = 45, areasCarreraId = 1, areasTestId = 11, Carrera = "Ingenieria en Computacion" },
                    new catCarreras { idCarrera = 46, areasCarreraId = 1, areasTestId = 11, Carrera = "Ingenieria en Energias Renovables" },
                    new catCarreras { idCarrera = 47, areasCarreraId = 1, areasTestId = 8, Carrera = "Ingenieria en Sistemas Biomedicos" },
                    new catCarreras { idCarrera = 48, areasCarreraId = 1, areasTestId = 8, Carrera = "Ingenieria en Telecomunicaicones" },
                    new catCarreras { idCarrera = 49, areasCarreraId = 1, areasTestId = 7, Carrera = "Ingenieria en Telecomunicaciones, Sistemas y Electronica" },
                    new catCarreras { idCarrera = 50, areasCarreraId = 1, areasTestId = 10, Carrera = "Ingenieria Geofisica" },
                    new catCarreras { idCarrera = 51, areasCarreraId = 1, areasTestId = 10, Carrera = "Ingenieria Geologica" },
                    new catCarreras { idCarrera = 52, areasCarreraId = 1, areasTestId = 10, Carrera = "Ingenieria Geomatica" },
                    new catCarreras { idCarrera = 53, areasCarreraId = 1, areasTestId = 8, Carrera = "Ingenieria Industrial" },
                    new catCarreras { idCarrera = 54, areasCarreraId = 1, areasTestId = 8, Carrera = "Ingenieria Mecanica" },
                    new catCarreras { idCarrera = 55, areasCarreraId = 1, areasTestId = 8, Carrera = "Ingenieria Mecanica Electrica" },
                    new catCarreras { idCarrera = 56, areasCarreraId = 1, areasTestId = 7, Carrera = "Ingenieria Mecatronica" },
                    new catCarreras { idCarrera = 57, areasCarreraId = 1, areasTestId = 7, Carrera = "Ingenieria Petrolera" },
                    new catCarreras { idCarrera = 58, areasCarreraId = 1, areasTestId = 7, Carrera = "Ingenieria Quimica" },
                    new catCarreras { idCarrera = 59, areasCarreraId = 1, areasTestId = 9, Carrera = "Ingenieria Quimica Metalurgica" },
                    new catCarreras { idCarrera = 60, areasCarreraId = 1, areasTestId = 6, Carrera = "Matematicas" },
                    new catCarreras { idCarrera = 61, areasCarreraId = 1, areasTestId = 6, Carrera = "Matematicas Aplicadas" },
                    new catCarreras { idCarrera = 62, areasCarreraId = 1, areasTestId = 6, Carrera = "Matematicas Aplicadas y Computacion" },
                    new catCarreras { idCarrera = 63, areasCarreraId = 1, areasTestId = 7, Carrera = "Nanotecnologia" },
                    new catCarreras { idCarrera = 64, areasCarreraId = 1, areasTestId = 7, Carrera = "Tecnologia" },
                    new catCarreras { idCarrera = 65, areasCarreraId = 1, areasTestId = 7, Carrera = "Tecnologias para la informacion en Cinecias" },
                    new catCarreras { idCarrera = 66, areasCarreraId = 1, areasTestId = 1, Carrera = "Urbanismo" },
                    new catCarreras { idCarrera = 67, areasCarreraId = 2, areasTestId = 1, Carrera = "Licenciado en Nutricion" },
                    new catCarreras { idCarrera = 68, areasCarreraId = 2, areasTestId = 1, Carrera = "Licenciado en Optometria" },
                    new catCarreras { idCarrera = 69, areasCarreraId = 2, areasTestId = 7, Carrera = "Licenciado en Biologia" },
                    new catCarreras { idCarrera = 70, areasCarreraId = 2, areasTestId = 1, Carrera = "Licenciado en Diagnostica" },
                    new catCarreras { idCarrera = 71, areasCarreraId = 2, areasTestId = 7, Carrera = "Quimico Farmaceutico Indisctrial" },
                    new catCarreras { idCarrera = 72, areasCarreraId = 2, areasTestId = 1, Carrera = "Medico Cirujano Homeopata" },
                    new catCarreras { idCarrera = 73, areasCarreraId = 2, areasTestId = 11, Carrera = "Medico Cirujano Partero" },
                    new catCarreras { idCarrera = 74, areasCarreraId = 2, areasTestId = 11, Carrera = "Quimico Bacteriologo Parasitologo" },
                    new catCarreras { idCarrera = 75, areasCarreraId = 2, areasTestId = 11, Carrera = "Ciencia Forense" },
                    new catCarreras { idCarrera = 76, areasCarreraId = 2, areasTestId = 10, Carrera = "Ciencias Agroforestales" },
                    new catCarreras { idCarrera = 77, areasCarreraId = 2, areasTestId = 10, Carrera = "Ciencias Agrogenomicas" },
                    new catCarreras { idCarrera = 78, areasCarreraId = 2, areasTestId = 10, Carrera = "Ciencias Ambientales" },
                    new catCarreras { idCarrera = 79, areasCarreraId = 2, areasTestId = 10, Carrera = "Ciencias Genomicas" },
                    new catCarreras { idCarrera = 80, areasCarreraId = 2, areasTestId = 1, Carrera = "Cirujano Dentista" },
                    new catCarreras { idCarrera = 81, areasCarreraId = 2, areasTestId = 1, Carrera = "Ecologia" },
                    new catCarreras { idCarrera = 82, areasCarreraId = 2, areasTestId = 1, Carrera = "Licenciatura en Enfermeria" },
                    new catCarreras { idCarrera = 83, areasCarreraId = 2, areasTestId = 1, Carrera = "Licenciatura en enfermeria y Obstetricia" },
                    new catCarreras { idCarrera = 84, areasCarreraId = 2, areasTestId = 1, Carrera = "Farmacia" },
                    new catCarreras { idCarrera = 85, areasCarreraId = 2, areasTestId = 1, Carrera = "Fisioterapia" },
                    new catCarreras { idCarrera = 86, areasCarreraId = 2, areasTestId = 10, Carrera = "Ingenieria Agricola" },
                    new catCarreras { idCarrera = 87, areasCarreraId = 2, areasTestId = 2, Carrera = "Ingenieria en Alimentos" },
                    new catCarreras { idCarrera = 88, areasCarreraId = 2, areasTestId = 7, Carrera = "Investigacion Biomedica Basica" },
                    new catCarreras { idCarrera = 89, areasCarreraId = 2, areasTestId = 2, Carrera = "Manejo Sustentable de Zonas Costeras" },
                    new catCarreras { idCarrera = 90, areasCarreraId = 2, areasTestId = 1, Carrera = "Medico Cirujano" },
                    new catCarreras { idCarrera = 91, areasCarreraId = 2, areasTestId = 7, Carrera = "Medicina Veterinaria y Zootecnia" },
                    new catCarreras { idCarrera = 92, areasCarreraId = 2, areasTestId = 2, Carrera = "Licenciatura en Medicina" },
                    new catCarreras { idCarrera = 93, areasCarreraId = 2, areasTestId = 7, Carrera = "Neurociencias" },
                    new catCarreras { idCarrera = 94, areasCarreraId = 2, areasTestId = 1, Carrera = "Nutriologia" },
                    new catCarreras { idCarrera = 95, areasCarreraId = 2, areasTestId = 1, Carrera = "Licenciado en Odontologia" },
                    new catCarreras { idCarrera = 96, areasCarreraId = 2, areasTestId = 2, Carrera = "Optometria" },
                    new catCarreras { idCarrera = 97, areasCarreraId = 2, areasTestId = 9, Carrera = "Ortesis y protesis" },
                    new catCarreras { idCarrera = 98, areasCarreraId = 2, areasTestId = 1, Carrera = "Licenciado en Psicologia" },
                    new catCarreras { idCarrera = 99, areasCarreraId = 2, areasTestId = 7, Carrera = "Quimica" },
                    new catCarreras { idCarrera = 100, areasCarreraId = 2, areasTestId = 7, Carrera = "Quimica de Alimentos" },
                    new catCarreras { idCarrera = 101, areasCarreraId = 2, areasTestId = 7, Carrera = "Quimica e Ingenieria en Materiales" },
                    new catCarreras { idCarrera = 102, areasCarreraId = 2, areasTestId = 7, Carrera = "Quimica Farmaceuticao Biologica" },
                    new catCarreras { idCarrera = 103, areasCarreraId = 2, areasTestId = 7, Carrera = "Quimica Industrial" },
                    new catCarreras { idCarrera = 104, areasCarreraId = 3, areasTestId = 8, Carrera = "Contador Publico" },
                    new catCarreras { idCarrera = 105, areasCarreraId = 3, areasTestId = 2, Carrera = "Licenciatura en Administracion Industrial" },
                    new catCarreras { idCarrera = 106, areasCarreraId = 3, areasTestId = 2, Carrera = "Administracion" },
                    new catCarreras { idCarrera = 107, areasCarreraId = 3, areasTestId = 2, Carrera = "Administracion Agropecuaria" },
                    new catCarreras { idCarrera = 108, areasCarreraId = 3, areasTestId = 7, Carrera = "Antropologia" },
                    new catCarreras { idCarrera = 109, areasCarreraId = 3, areasTestId = 3, Carrera = "Ciencias de la Comunicacion" },
                    new catCarreras { idCarrera = 110, areasCarreraId = 3, areasTestId = 2, Carrera = "Ciencias Politicas y Administracion Publica" },
                    new catCarreras { idCarrera = 111, areasCarreraId = 3, areasTestId = 6, Carrera = "Licenciatura en Administracion y Desarrollo Empresarial" },
                    new catCarreras { idCarrera = 112, areasCarreraId = 3, areasTestId = 6, Carrera = "Licenciatura en Archivonomia" },
                    new catCarreras { idCarrera = 113, areasCarreraId = 3, areasTestId = 6, Carrera = "Licencitura en Bibliotecnomia" },
                    new catCarreras { idCarrera = 114, areasCarreraId = 3, areasTestId = 3, Carrera = "Comunicacion" },
                    new catCarreras { idCarrera = 115, areasCarreraId = 3, areasTestId = 3, Carrera = "Comunicacion y Periodismo" },
                    new catCarreras { idCarrera = 116, areasCarreraId = 3, areasTestId = 6, Carrera = "Contaduria" },
                    new catCarreras { idCarrera = 117, areasCarreraId = 3, areasTestId = 1, Carrera = "Licenciado en Relaciones Comerciales" },
                    new catCarreras { idCarrera = 118, areasCarreraId = 3, areasTestId = 1, Carrera = "Licenciado en Turismo" },
                    new catCarreras { idCarrera = 119, areasCarreraId = 3, areasTestId = 1, Carrera = "Derecho" },
                    new catCarreras { idCarrera = 120, areasCarreraId = 3, areasTestId = 1, Carrera = "Desarrollo Comunitario para el Envejecimiento" },
                    new catCarreras { idCarrera = 121, areasCarreraId = 3, areasTestId = 2, Carrera = "Desarrollo Territorial" },
                    new catCarreras { idCarrera = 122, areasCarreraId = 3, areasTestId = 2, Carrera = "Licenciatura en Economia" },
                    new catCarreras { idCarrera = 123, areasCarreraId = 3, areasTestId = 2, Carrera = "Economia Industrial" },
                    new catCarreras { idCarrera = 124, areasCarreraId = 3, areasTestId = 1, Carrera = "Estudios Sociales y Gestion Local" },
                    new catCarreras { idCarrera = 125, areasCarreraId = 3, areasTestId = 8, Carrera = "Geografia" },
                    new catCarreras { idCarrera = 126, areasCarreraId = 3, areasTestId = 8, Carrera = "Geografia Aplicada" },
                    new catCarreras { idCarrera = 127, areasCarreraId = 3, areasTestId = 3, Carrera = "Licenciatura en Negocios Internacionales" },
                    new catCarreras { idCarrera = 128, areasCarreraId = 3, areasTestId = 1, Carrera = "Planificacion para el Desarrollo Agropecuario" },
                    new catCarreras { idCarrera = 129, areasCarreraId = 3, areasTestId = 3, Carrera = "Relaciones Internacionales" },
                    new catCarreras { idCarrera = 130, areasCarreraId = 3, areasTestId = 1, Carrera = "Sociologia" },
                    new catCarreras { idCarrera = 131, areasCarreraId = 3, areasTestId = 1, Carrera = "Licenciatura en Tabajo Social" },
                    new catCarreras { idCarrera = 132, areasCarreraId = 4, areasTestId = 6, Carrera = "Administracion de Arcivos y Gestion Documental" },
                    new catCarreras { idCarrera = 133, areasCarreraId = 4, areasTestId = 4, Carrera = "Arte y Diseño" },
                    new catCarreras { idCarrera = 134, areasCarreraId = 4, areasTestId = 4, Carrera = "Artes Visuales" },
                    new catCarreras { idCarrera = 135, areasCarreraId = 4, areasTestId = 6, Carrera = "Bibliotecologia y Estudios de la informacion" },
                    new catCarreras { idCarrera = 136, areasCarreraId = 4, areasTestId = 4, Carrera = "Cinematografia" },
                    new catCarreras { idCarrera = 137, areasCarreraId = 4, areasTestId = 6, Carrera = "Desarrollo y Gestion Interculturales" },
                    new catCarreras { idCarrera = 138, areasCarreraId = 4, areasTestId = 4, Carrera = "Diseño Grafico" },
                    new catCarreras { idCarrera = 139, areasCarreraId = 4, areasTestId = 4, Carrera = "Diseño y Comunicacion Visual" },
                    new catCarreras { idCarrera = 140, areasCarreraId = 4, areasTestId = 1, Carrera = "Enseñanza de Lengua Extranjera" },
                    new catCarreras { idCarrera = 141, areasCarreraId = 4, areasTestId = 1, Carrera = "Enseñansa de Ingles" },
                    new catCarreras { idCarrera = 142, areasCarreraId = 4, areasTestId = 1, Carrera = "Estudios Latinoamericanos" },
                    new catCarreras { idCarrera = 143, areasCarreraId = 4, areasTestId = 5, Carrera = "Etnomusicologia" },
                    new catCarreras { idCarrera = 144, areasCarreraId = 4, areasTestId = 7, Carrera = "Filosofia" },
                    new catCarreras { idCarrera = 145, areasCarreraId = 4, areasTestId = 7, Carrera = "Geohistoria" },
                    new catCarreras { idCarrera = 146, areasCarreraId = 4, areasTestId = 7, Carrera = "Historia" },
                    new catCarreras { idCarrera = 147, areasCarreraId = 4, areasTestId = 7, Carrera = "Historia del Arte" },
                    new catCarreras { idCarrera = 148, areasCarreraId = 4, areasTestId = 3, Carrera = "Lengua y literaturas Hispanicas" },
                    new catCarreras { idCarrera = 149, areasCarreraId = 4, areasTestId = 3, Carrera = "Lengua y literaturas Modernas" },
                    new catCarreras { idCarrera = 150, areasCarreraId = 4, areasTestId = 3, Carrera = "lenguas Clasicas" },
                    new catCarreras { idCarrera = 151, areasCarreraId = 4, areasTestId = 3, Carrera = "Linguistica Aplicada" },
                    new catCarreras { idCarrera = 152, areasCarreraId = 4, areasTestId = 4, Carrera = "Literatura Dramatica y Teatro" },
                    new catCarreras { idCarrera = 153, areasCarreraId = 4, areasTestId = 3, Carrera = "Literatura Intercultural" },
                    new catCarreras { idCarrera = 154, areasCarreraId = 4, areasTestId = 5, Carrera = "Musica Canto" },
                    new catCarreras { idCarrera = 155, areasCarreraId = 4, areasTestId = 5, Carrera = "Musica Composicion" },
                    new catCarreras { idCarrera = 156, areasCarreraId = 4, areasTestId = 1, Carrera = "Musica Educacion Musical" },
                    new catCarreras { idCarrera = 157, areasCarreraId = 4, areasTestId = 5, Carrera = "Musica Instrumentista" },
                    new catCarreras { idCarrera = 158, areasCarreraId = 4, areasTestId = 5, Carrera = "Musica Piano" },
                    new catCarreras { idCarrera = 159, areasCarreraId = 4, areasTestId = 5, Carrera = "Musica y Tecnologia Artistica" },
                    new catCarreras { idCarrera = 160, areasCarreraId = 4, areasTestId = 1, Carrera = "Pedagogia" },
                    new catCarreras { idCarrera = 161, areasCarreraId = 4, areasTestId = 4, Carrera = "Teatro y actuacion" },
                    new catCarreras { idCarrera = 162, areasCarreraId = 4, areasTestId = 3, Carrera = "Traduccion" }
                    );
            });
            builder.Entity<areasTest>(entity =>
            {
                entity.HasKey(d => d.areaTestId);
                entity.HasData(
                    new areasTest { areaTestId = 1, areaDelTest = "Servicio Social" },
                    new areasTest { areaTestId = 2, areaDelTest = "Ejecutiva Persuasiva" },
                    new areasTest { areaTestId = 3, areaDelTest = "Verbal" },
                    new areasTest { areaTestId = 4, areaDelTest = "Artistico Plastico" },
                    new areasTest { areaTestId = 5, areaDelTest = "Musical" },
                    new areasTest { areaTestId = 6, areaDelTest = "Organizacion" },
                    new areasTest { areaTestId = 7, areaDelTest = "Cientifica" },
                    new areasTest { areaTestId = 8, areaDelTest = "Calculo" },
                    new areasTest { areaTestId = 9, areaDelTest = "Mecanico Constructivo" },
                    new areasTest { areaTestId = 10, areaDelTest = "Aire Libre" },
                    new areasTest { areaTestId = 11, areaDelTest = "Destreza manual" });
            });
            builder.Entity<carrerasDeseadas>(entity =>
            {
                entity.HasKey(d => d.noDeCarreraDeseada);
            });
            builder.Entity<PreguntasTestVocacional>(entity =>
            {
                entity.HasKey(d => d.PregunataId);
                entity.HasData(
                    new PreguntasTestVocacional { PregunataId = 1, pregunta = "Atender y cuidar enfermos" },
                    new PreguntasTestVocacional { PregunataId = 2, pregunta = "Intervenir activamente en las discuciones de la clase" },
                    new PreguntasTestVocacional { PregunataId = 3, pregunta = "Escribir cuentos, cronicas o articulos" },
                    new PreguntasTestVocacional { PregunataId = 4, pregunta = "Dibujar y pintar" },
                    new PreguntasTestVocacional { PregunataId = 5, pregunta = "Cantar o tocar un instrumento en publico" },
                    new PreguntasTestVocacional { PregunataId = 6, pregunta = "Llevar en orden tus libros y cuadernos" },
                    new PreguntasTestVocacional { PregunataId = 7, pregunta = "Conocer y estudiar la estructura de las plantas y de los animales" },
                    new PreguntasTestVocacional { PregunataId = 8, pregunta = "Resolver cuestionarios de matematicas" },
                    new PreguntasTestVocacional { PregunataId = 9, pregunta = "Armar y desarmar objetos mecanicos" },
                    new PreguntasTestVocacional { PregunataId = 10, pregunta = "Salir de excursion" },
                    new PreguntasTestVocacional { PregunataId = 11, pregunta = "Proteger a los muchachos menores del grupo" },
                    new PreguntasTestVocacional { PregunataId = 12, pregunta = "Ser jefe de un grupo" },
                    new PreguntasTestVocacional { PregunataId = 13, pregunta = "Leer obras literarios" },
                    new PreguntasTestVocacional { PregunataId = 14, pregunta = "Moldear el barrio, plastilina o cualquier otro material" },
                    new PreguntasTestVocacional { PregunataId = 15, pregunta = "Escuchar musica clasica" },
                    new PreguntasTestVocacional { PregunataId = 16, pregunta = "Ordenar y clasificar los libro de una biblioteca" },
                    new PreguntasTestVocacional { PregunataId = 17, pregunta = "Hacer experimentos en un laboratorio" },
                    new PreguntasTestVocacional { PregunataId = 18, pregunta = "Resolver problemas de aritmetica" },
                    new PreguntasTestVocacional { PregunataId = 19, pregunta = "Manejar herramientas y maquinaria" },
                    new PreguntasTestVocacional { PregunataId = 20, pregunta = "Pertenecer a un grupo de exploradores" },
                    new PreguntasTestVocacional { PregunataId = 21, pregunta = "Ser miembro de una sociedad de ayuda y asistencia" },
                    new PreguntasTestVocacional { PregunataId = 22, pregunta = "Dirigir la campaña politica para un candidato estudiantil" },
                    new PreguntasTestVocacional { PregunataId = 23, pregunta = "Hacer versos para una publicacion" },
                    new PreguntasTestVocacional { PregunataId = 24, pregunta = "Encargarte del decorado del lugar para un festival" },
                    new PreguntasTestVocacional { PregunataId = 25, pregunta = "Aprender a tocar un instrumento musical" },
                    new PreguntasTestVocacional { PregunataId = 26, pregunta = "Aprender a usar programas de programacion" },
                    new PreguntasTestVocacional { PregunataId = 27, pregunta = "Investigar el origen de las costumbres de los pueblos" },
                    new PreguntasTestVocacional { PregunataId = 28, pregunta = "LLevar las cuentas de una institucion" },
                    new PreguntasTestVocacional { PregunataId = 29, pregunta = "Construir objeto o muebles" },
                    new PreguntasTestVocacional { PregunataId = 30, pregunta = "Trabajar al aire libre, fuera de la ciudad" },
                    new PreguntasTestVocacional { PregunataId = 31, pregunta = "Cuidar a niños pequeños" },
                    new PreguntasTestVocacional { PregunataId = 32, pregunta = "Ver a tus amigos organizar una fiesta con ellos" },
                    new PreguntasTestVocacional { PregunataId = 33, pregunta = "Escribir mensajes electronicos o chatear con tus amigos por Internet", },
                    new PreguntasTestVocacional { PregunataId = 34, pregunta = "Realizar una actividad artistica" },
                    new PreguntasTestVocacional { PregunataId = 35, pregunta = "Interprete musical del genero de tu preferencia" },
                    new PreguntasTestVocacional { PregunataId = 36, pregunta = "Diseñador de software" },
                    new PreguntasTestVocacional { PregunataId = 37, pregunta = "Asistir a una conferencia cientifica a un museo" },
                    new PreguntasTestVocacional { PregunataId = 38, pregunta = "Calcular el presupuesto de una familia" },
                    new PreguntasTestVocacional { PregunataId = 39, pregunta = "Reparar algun aparato descompuesto" },
                    new PreguntasTestVocacional { PregunataId = 40, pregunta = "Ir de paseo al campo o a la playa" },
                    new PreguntasTestVocacional { PregunataId = 41, pregunta = "Ser miembro especial de la Cruz Roja Internacional para casos de desastre" },
                    new PreguntasTestVocacional { PregunataId = 42, pregunta = "Gerente de mercadotecnia de una compañia" },
                    new PreguntasTestVocacional { PregunataId = 43, pregunta = "Articulista de un periodico" },
                    new PreguntasTestVocacional { PregunataId = 44, pregunta = "Dieseñador de las portadas de una revista" },
                    new PreguntasTestVocacional { PregunataId = 45, pregunta = "Tocar en la orquesta de tu ciudad" },
                    new PreguntasTestVocacional { PregunataId = 46, pregunta = "Poner en orden tu coleccion favorita" },
                    new PreguntasTestVocacional { PregunataId = 47, pregunta = "Coordinador de un grupo cientifico de vanguardia" },
                    new PreguntasTestVocacional { PregunataId = 48, pregunta = "Contador general de una empresa" },
                    new PreguntasTestVocacional { PregunataId = 49, pregunta = "Autoridad en la construccion de ciertas estructuras arquitectonocas" },
                    new PreguntasTestVocacional { PregunataId = 50, pregunta = "Encargado de la ampliacion de la red carretera del pais" });
            });
            builder.Entity<ValorPregunta>(entity =>
            {
                entity.HasKey(d => d.noDePregunta);
                entity.HasOne(a => a.rel_valorpregunta_pregunta)
                .WithMany(b => b.rel_pregunta_valor)
                .HasForeignKey(d => d.idPregunta);
            });
            builder.Entity<carrerasimpartidas>(entity =>
            {
                entity.HasKey(d => d.noderelacion);
                entity.HasOne(a => a.relcarri_catcarr)
                 .WithMany(b => b.relcarr_carri)
                 .HasForeignKey(d => d.catCarrerasId)
                 .HasConstraintName("relacion_cat_imp");
            });
            builder.Entity<Relacion>(entity =>
            {
                entity.HasKey(d => d.nodeRelacion);
            });
            builder.Entity<Admon>(entity =>
            {
                entity.HasKey(d => d.idAmon);
                entity.Property(d => d.username).IsRequired();
                entity.Property(d => d.contraseña).IsRequired();
                entity.HasData(
                    new Admon { idAmon = Guid.NewGuid(), username = "ADMINISTRADOR 1", contraseña = "123aEFGJnfsa" },
                    new Admon { idAmon = Guid.NewGuid(), username = "ADMINISTRADOR 2", contraseña = "aggvkKBQ5hp" },
                    new Admon { idAmon = Guid.NewGuid(), username = "ADMINISTRADOR 3", contraseña = "HzmJlaLKU1f" },
                    new Admon { idAmon = Guid.NewGuid(), username = "ADMINISTRADOR 4", contraseña = "Xmxg82RTiuQV" },
                    new Admon { idAmon = Guid.NewGuid(), username = "ADMINISTRADOR 5", contraseña = "bgTR1apIK1ye" }
                 );
            });
            builder.Entity<solicitudes>(entity =>
            {
                entity.HasKey(d => d.nodesolicitud);
                entity.HasOne(a => a.relsAl)
                .WithOne(b => b.relAs)
                .HasConstraintName("RelacionsolicitudAlumno");
                entity.HasOne(b => b.relsU)
                .WithOne(b => b.relUS)
                .HasConstraintName("RelacionsolicutudUni");
            });
            builder.Entity<historialrechazos>(entity =>
            {
                entity.HasKey(d => d.noderechazo);
            });
            builder.Entity<historialdeaceptados>(entity =>
            {
                entity.HasKey(d => d.nodeaceptado);
            });
            builder.Entity<Publicaciones>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasOne(a => a.relPub_USA)
                .WithMany(p => p.relUsa_Pu)
                .HasForeignKey(p => p.idUsuario)
                .HasConstraintName("RelacionPublicacionAlumno")
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.relPub_USU)
                .WithMany(p => p.relUSU_PU)
                .HasForeignKey(p => p.idUsuario)
                .HasConstraintName("RelacionPublicacionUniversidad")
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(c => c.relPub_Com)
                .WithOne(p => p.relCom_Pub)
                .HasConstraintName("RelacionPublicacionComentarios");

                entity.Property(c => c.texto)
                                .IsRequired()
                                .HasMaxLength(300);
            });
            builder.Entity<Comentarios>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.relCom_USA)
                .WithMany(a => a.relUSA_COM)
                .HasForeignKey(c => c.IdUsuario)
                .HasConstraintName("RelacionComentarioUsuarioAlumno")
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(c => c.relCom_USU)
                .WithMany(a => a.relUSU_COM)
                .HasForeignKey(c => c.IdUsuario)
                .HasConstraintName("RelacionComentarioUsuarioUniversidad")
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(c => c.relCom_Pub)
               .WithMany(a => a.relPub_Com)
               .HasForeignKey(c => c.IdUsuario)
               .HasConstraintName("RelacionComentarioPublicacion");
                entity.Property(c => c.comentario)
                                .IsRequired()
                                .HasMaxLength(100);
            });

            InitializeProcedures();
        }
        public DbSet<UsuarioAlumno> alumnosUsuarios { get; set; }
        public DbSet<Alumno> alumnos { get; set; }
        public DbSet<DatosAcademicos> datosAcademicos { get; set; }
        public DbSet<catalogoCarrerasTecnicas> catalogoCarrerasT { get; set; }
        public DbSet<carreraTecnica> carreraTecnicas { get; set; }
        public DbSet<informacion> informaciones { get; set; }
        public DbSet<UsuarioUniversidad> universidadesUsuario { get; set; }
        public DbSet<universidad> universidades { get; set; }
        public DbSet<catalogoDeMapasCurriculares> catalogoDeMapasCuarriculares { get; set; }
        public DbSet<ingreso> ingresos { get; set; }
        public DbSet<egreso> egresos { get; set; }
        public DbSet<carBeca> carBecas { get; set; }
        public DbSet<empresaAsociadas> empresaAsociadas { get; set; }
        public DbSet<contactos> contactos { get; set; }
        public DbSet<areasCarrera> catAreasCarrera { get; set; }
        public DbSet<catCarreras> catCarreras { get; set; }
        public DbSet<carrerasDeseadas> carrerasDeseadas { get; set; }
        public DbSet<areasTest> AreasTests { get; set; }
        public DbSet<PreguntasTestVocacional> preguntasDelTestVocacional { get; set; }
        public DbSet<ValorPregunta> valorPreguntas { get; set; }
        public DbSet<carrerasimpartidas> carrerasImpartadas { get; set; }
        public DbSet<Relacion> Relaciones { get; set; }
        public DbSet<Admon> Administradores { get; set; }
        public DbSet<solicitudes> solicitar { get; set; }
        public DbSet<historialrechazos> rechazos { get; set; }
        public DbSet<historialdeaceptados> aceptados { get; set; }

        public DbSet<Publicaciones> publicaciones { get; set; }

        public DbSet<Comentarios> commentarios { get; set; }
    }
}
