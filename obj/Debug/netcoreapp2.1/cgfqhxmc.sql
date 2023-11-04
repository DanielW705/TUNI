IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [alumnosUsuarios] (
    [idAlumno] uniqueidentifier NOT NULL,
    [usuario] nvarchar(max) NULL,
    [contraseña] nvarchar(max) NULL,
    CONSTRAINT [PK_alumnosUsuarios] PRIMARY KEY ([idAlumno])
);

GO

CREATE TABLE [AreasTests] (
    [areaTestId] int NOT NULL IDENTITY,
    [areaDelTest] nvarchar(max) NULL,
    CONSTRAINT [PK_AreasTests] PRIMARY KEY ([areaTestId])
);

GO

CREATE TABLE [catAreasCarrera] (
    [idArea] int NOT NULL IDENTITY,
    [area] nvarchar(max) NULL,
    CONSTRAINT [PK_catAreasCarrera] PRIMARY KEY ([idArea])
);

GO

CREATE TABLE [preguntasDelTestVocacional] (
    [PregunataId] int NOT NULL IDENTITY,
    [pregunta] nvarchar(max) NULL,
    CONSTRAINT [PK_preguntasDelTestVocacional] PRIMARY KEY ([PregunataId])
);

GO

CREATE TABLE [universidadesUsuario] (
    [idUniversidad] uniqueidentifier NOT NULL,
    [usuario] nvarchar(max) NULL,
    [contraseña] nvarchar(max) NULL,
    CONSTRAINT [PK_universidadesUsuario] PRIMARY KEY ([idUniversidad])
);

GO

CREATE TABLE [alumnos] (
    [idAlumno] uniqueidentifier NOT NULL,
    [nombre] nvarchar(max) NULL,
    [apPaterno] nvarchar(max) NULL,
    [apMaterno] nvarchar(max) NULL,
    CONSTRAINT [PK_alumnos] PRIMARY KEY ([idAlumno]),
    CONSTRAINT [Relacion_usuario_alumno] FOREIGN KEY ([idAlumno]) REFERENCES [alumnosUsuarios] ([idAlumno]) ON DELETE CASCADE
);

GO

CREATE TABLE [carreraTecnicas] (
    [noderelcat] int NOT NULL IDENTITY,
    [idAlumno] uniqueidentifier NOT NULL,
    [catalogoCarrerasTecnicasId] int NOT NULL,
    CONSTRAINT [PK_carreraTecnicas] PRIMARY KEY ([noderelcat]),
    CONSTRAINT [Relacion_usuario_carreraTecnica] FOREIGN KEY ([idAlumno]) REFERENCES [alumnosUsuarios] ([idAlumno]) ON DELETE CASCADE
);

GO

CREATE TABLE [datosAcademicos] (
    [idAlumno] uniqueidentifier NOT NULL,
    [boletaGlobal] nvarchar(max) NULL,
    [doc] varbinary(max) NULL,
    CONSTRAINT [PK_datosAcademicos] PRIMARY KEY ([idAlumno]),
    CONSTRAINT [Relacion_usuario_datosAcademicos] FOREIGN KEY ([idAlumno]) REFERENCES [alumnosUsuarios] ([idAlumno]) ON DELETE CASCADE
);

GO

CREATE TABLE [informaciones] (
    [idnoRecon] int NOT NULL IDENTITY,
    [idAlumno] uniqueidentifier NOT NULL,
    [reconocimiento] nvarchar(max) NULL,
    [doc] varbinary(max) NULL,
    CONSTRAINT [PK_informaciones] PRIMARY KEY ([idnoRecon]),
    CONSTRAINT [FK_informaciones_alumnosUsuarios_idAlumno] FOREIGN KEY ([idAlumno]) REFERENCES [alumnosUsuarios] ([idAlumno]) ON DELETE CASCADE
);

GO

CREATE TABLE [catCarreras] (
    [idCarrera] int NOT NULL IDENTITY,
    [areasCarreraId] int NOT NULL,
    [areasTestId] int NOT NULL,
    [Carrera] nvarchar(max) NULL,
    CONSTRAINT [PK_catCarreras] PRIMARY KEY ([idCarrera]),
    CONSTRAINT [relacion_cat_areas] FOREIGN KEY ([areasCarreraId]) REFERENCES [catAreasCarrera] ([idArea]) ON DELETE CASCADE,
    CONSTRAINT [FK_catCarreras_AreasTests_areasTestId] FOREIGN KEY ([areasTestId]) REFERENCES [AreasTests] ([areaTestId]) ON DELETE CASCADE
);

GO

CREATE TABLE [valorPreguntas] (
    [noDePregunta] int NOT NULL IDENTITY,
    [idAlumno] uniqueidentifier NOT NULL,
    [idPregunta] int NOT NULL,
    [areasTestID] int NOT NULL,
    [valor] int NOT NULL,
    CONSTRAINT [PK_valorPreguntas] PRIMARY KEY ([noDePregunta]),
    CONSTRAINT [FK_valorPreguntas_AreasTests_areasTestID] FOREIGN KEY ([areasTestID]) REFERENCES [AreasTests] ([areaTestId]) ON DELETE CASCADE,
    CONSTRAINT [Relacion_usuario_test] FOREIGN KEY ([idAlumno]) REFERENCES [alumnosUsuarios] ([idAlumno]) ON DELETE CASCADE,
    CONSTRAINT [FK_valorPreguntas_preguntasDelTestVocacional_idPregunta] FOREIGN KEY ([idPregunta]) REFERENCES [preguntasDelTestVocacional] ([PregunataId]) ON DELETE CASCADE
);

GO

CREATE TABLE [carBecas] (
    [noBeca] int NOT NULL IDENTITY,
    [idUniversidad] uniqueidentifier NOT NULL,
    [becaInstitucional] nvarchar(max) NULL,
    [doc] varbinary(max) NULL,
    CONSTRAINT [PK_carBecas] PRIMARY KEY ([noBeca]),
    CONSTRAINT [Relacion_usuario_becas] FOREIGN KEY ([idUniversidad]) REFERENCES [universidadesUsuario] ([idUniversidad]) ON DELETE CASCADE
);

GO

CREATE TABLE [catalogoDeMapasCuarriculares] (
    [noDeMapaCurricular] int NOT NULL IDENTITY,
    [idUniversidad] uniqueidentifier NOT NULL,
    [idCarrera] int NOT NULL,
    [mapacurricular] nvarchar(max) NULL,
    [doc] varbinary(max) NULL,
    CONSTRAINT [PK_catalogoDeMapasCuarriculares] PRIMARY KEY ([noDeMapaCurricular]),
    CONSTRAINT [Relacion_usuario_CatalogoCurriculares] FOREIGN KEY ([idUniversidad]) REFERENCES [universidadesUsuario] ([idUniversidad]) ON DELETE CASCADE
);

GO

CREATE TABLE [contactos] (
    [noDeContacto] int NOT NULL IDENTITY,
    [idUniversidad] uniqueidentifier NOT NULL,
    [contacto] nvarchar(max) NULL,
    CONSTRAINT [PK_contactos] PRIMARY KEY ([noDeContacto]),
    CONSTRAINT [Relacion_usuario_contacto] FOREIGN KEY ([idUniversidad]) REFERENCES [universidadesUsuario] ([idUniversidad]) ON DELETE CASCADE
);

GO

CREATE TABLE [egresos] (
    [idUniversidad] uniqueidentifier NOT NULL,
    [nivelEgreso] nvarchar(max) NULL,
    [doc] varbinary(max) NULL,
    CONSTRAINT [PK_egresos] PRIMARY KEY ([idUniversidad]),
    CONSTRAINT [Relacion_usuario_egreso] FOREIGN KEY ([idUniversidad]) REFERENCES [universidadesUsuario] ([idUniversidad]) ON DELETE CASCADE
);

GO

CREATE TABLE [empresaAsociadas] (
    [noDeEmpresaAsociada] int NOT NULL IDENTITY,
    [idUniversidad] uniqueidentifier NOT NULL,
    [empresaAsociada] nvarchar(max) NULL,
    CONSTRAINT [PK_empresaAsociadas] PRIMARY KEY ([noDeEmpresaAsociada]),
    CONSTRAINT [FK_empresaAsociadas_universidadesUsuario_idUniversidad] FOREIGN KEY ([idUniversidad]) REFERENCES [universidadesUsuario] ([idUniversidad]) ON DELETE CASCADE
);

GO

CREATE TABLE [ingresos] (
    [idUniversidad] uniqueidentifier NOT NULL,
    [metodoIngreso] nvarchar(max) NULL,
    [doc] varbinary(max) NULL,
    CONSTRAINT [PK_ingresos] PRIMARY KEY ([idUniversidad]),
    CONSTRAINT [Relacion_usuario_ingreso] FOREIGN KEY ([idUniversidad]) REFERENCES [universidadesUsuario] ([idUniversidad]) ON DELETE CASCADE
);

GO

CREATE TABLE [Relaciones] (
    [nodeRelacion] int NOT NULL IDENTITY,
    [idAlumno] uniqueidentifier NOT NULL,
    [idUniversidad] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Relaciones] PRIMARY KEY ([nodeRelacion]),
    CONSTRAINT [Relacion_usuario_relacion] FOREIGN KEY ([idAlumno]) REFERENCES [alumnosUsuarios] ([idAlumno]) ON DELETE CASCADE,
    CONSTRAINT [Relacion_Usuario_universidad_rel] FOREIGN KEY ([idUniversidad]) REFERENCES [universidadesUsuario] ([idUniversidad]) ON DELETE CASCADE
);

GO

CREATE TABLE [universidades] (
    [idUnversidad] uniqueidentifier NOT NULL,
    [nombre] nvarchar(max) NULL,
    [direccion] nvarchar(max) NULL,
    CONSTRAINT [PK_universidades] PRIMARY KEY ([idUnversidad]),
    CONSTRAINT [Relacion_usuarioUniversidad_universidad] FOREIGN KEY ([idUnversidad]) REFERENCES [universidadesUsuario] ([idUniversidad]) ON DELETE CASCADE
);

GO

CREATE TABLE [catalogoCarrerasT] (
    [carreTecnicaId] int NOT NULL IDENTITY,
    [carreraTecnica] nvarchar(max) NULL,
    [relcart_Catnoderelcat] int NULL,
    CONSTRAINT [PK_catalogoCarrerasT] PRIMARY KEY ([carreTecnicaId]),
    CONSTRAINT [Relacion_carreratecnica_catalogodecarrerastecnicas] FOREIGN KEY ([relcart_Catnoderelcat]) REFERENCES [carreraTecnicas] ([noderelcat]) ON DELETE NO ACTION
);

GO

CREATE TABLE [carrerasDeseadas] (
    [noDeCarreraDeseada] int NOT NULL IDENTITY,
    [idAlumno] uniqueidentifier NOT NULL,
    [idCarrera] int NOT NULL,
    CONSTRAINT [PK_carrerasDeseadas] PRIMARY KEY ([noDeCarreraDeseada]),
    CONSTRAINT [Relacion_usuario_carreraDeseada] FOREIGN KEY ([idAlumno]) REFERENCES [alumnosUsuarios] ([idAlumno]) ON DELETE CASCADE,
    CONSTRAINT [FK_carrerasDeseadas_catCarreras_idCarrera] FOREIGN KEY ([idCarrera]) REFERENCES [catCarreras] ([idCarrera]) ON DELETE CASCADE
);

GO

CREATE TABLE [carrerasImpartadas] (
    [noderelacion] int NOT NULL IDENTITY,
    [usuarioUniversidad] uniqueidentifier NOT NULL,
    [catCarrerasId] int NOT NULL,
    CONSTRAINT [PK_carrerasImpartadas] PRIMARY KEY ([noderelacion]),
    CONSTRAINT [relacion_cat_imp] FOREIGN KEY ([catCarrerasId]) REFERENCES [catCarreras] ([idCarrera]) ON DELETE CASCADE,
    CONSTRAINT [Relacion_Usuario_carrerasimpartidas] FOREIGN KEY ([usuarioUniversidad]) REFERENCES [universidadesUsuario] ([idUniversidad]) ON DELETE CASCADE
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'areaTestId', N'areaDelTest') AND [object_id] = OBJECT_ID(N'[AreasTests]'))
    SET IDENTITY_INSERT [AreasTests] ON;
INSERT INTO [AreasTests] ([areaTestId], [areaDelTest])
VALUES (10, N'Aire Libre'),
(9, N'Mecanico Constructivo'),
(8, N'Calculo'),
(7, N'Cientifica'),
(1, N'Servicio Social'),
(2, N'Ejecutiva Persuasiva'),
(3, N'Verbal'),
(4, N'Artistico Plastico'),
(5, N'Musical'),
(6, N'Organizacion');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'areaTestId', N'areaDelTest') AND [object_id] = OBJECT_ID(N'[AreasTests]'))
    SET IDENTITY_INSERT [AreasTests] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'idArea', N'area') AND [object_id] = OBJECT_ID(N'[catAreasCarrera]'))
    SET IDENTITY_INSERT [catAreasCarrera] ON;
INSERT INTO [catAreasCarrera] ([idArea], [area])
VALUES (1, N'Fisico matematicas'),
(3, N'Ciencias Sociales y administrativas'),
(2, N'Ciencias Biologicas, Quimicas de la salud'),
(4, N'Humanidades y de las Artes');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'idArea', N'area') AND [object_id] = OBJECT_ID(N'[catAreasCarrera]'))
    SET IDENTITY_INSERT [catAreasCarrera] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'carreTecnicaId', N'carreraTecnica', N'relcart_Catnoderelcat') AND [object_id] = OBJECT_ID(N'[catalogoCarrerasT]'))
    SET IDENTITY_INSERT [catalogoCarrerasT] ON;
INSERT INTO [catalogoCarrerasT] ([carreTecnicaId], [carreraTecnica], [relcart_Catnoderelcat])
VALUES (1, N'Tecnico en informatica', NULL),
(2, N'Tecnico en enfermeria', NULL),
(3, N'Técnico en Diseño Gráfico', NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'carreTecnicaId', N'carreraTecnica', N'relcart_Catnoderelcat') AND [object_id] = OBJECT_ID(N'[catalogoCarrerasT]'))
    SET IDENTITY_INSERT [catalogoCarrerasT] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PregunataId', N'pregunta') AND [object_id] = OBJECT_ID(N'[preguntasDelTestVocacional]'))
    SET IDENTITY_INSERT [preguntasDelTestVocacional] ON;
INSERT INTO [preguntasDelTestVocacional] ([PregunataId], [pregunta])
VALUES (35, N'Interprete musical del genero de tu preferencia'),
(34, N'Realizar una actividad artistica'),
(33, N'Escribir mensajes electronicos o chatear con tus amigos por Internet'),
(32, N'Ver a tus amigos organizar una fiesta con ellos'),
(29, N'Construir objeto o muebles'),
(30, N'Trabajar al aire libre, fuera de la ciudad'),
(28, N'LLevar las cuentas de una institucion'),
(27, N'Investigar el origen de las costumbres de los pueblos'),
(36, N'Diseñador de software'),
(31, N'Cuidar a niños pequeños'),
(37, N'Asistir a una conferencia cientifica a un museo'),
(45, N'Tocar en la orquesta de tu ciudad'),
(39, N'Reparar algun aparato descompuesto'),
(40, N'Ir de paseo al campo o a la playa'),
(41, N'Ser miembro especial de la Cruz Roja Internacional para casos de desastre'),
(42, N'Gerente de mercadotecnia de una compañia'),
(43, N'Articulista de un periodico'),
(44, N'Dieseñador de las portadas de una revista'),
(26, N'Aprender a usar programas de programacion'),
(46, N'Poner en orden tu coleccion favorita'),
(47, N'Coordinador de un grupo cientifico de vanguardia'),
(48, N'Contador general de una empresa'),
(38, N'Calcular el presupuesto de una familia'),
(25, N'Aprender a tocar un instrumento musical'),
(17, N'Hacer experimentos en un laboratorio'),
(23, N'Hacer versos para una publicacion'),
(1, N'Atender y cuidar enfermos'),
(2, N'Intervenir activamente en las discuciones de la clase'),
(3, N'Escribir cuentos, cronicas o articulos'),
(4, N'Dibujar y pintar'),
(5, N'Cantar o tocar un instrumento en publico'),
(6, N'Llevar en orden tus libros y cuadernos'),
(7, N'Conocer y estudiar la estructura de las plantas y de los animales'),
(8, N'Resolver cuestionarios de matematicas'),
(9, N'Armar y desarmar objetos mecanicos'),
(10, N'Salir de excursion'),
(24, N'Encargarte del decorado del lugar para un festival'),
(11, N'Proteger a los muchachos menores del grupo'),
(13, N'Leer obras literarios'),
(14, N'Moldear el barrio, plastilina o cualquier otro material'),
(15, N'Escuchar musica clasica'),
(16, N'Ordenar y clasificar los libro de una biblioteca'),
(49, N'Autoridad en la construccion de ciertas estructuras arquitectonocas'),
(18, N'Resolver problemas de aritmetica'),
(19, N'Manejar herramientas y maquinaria'),
(20, N'Pertenecer a un grupo de exploradores'),
(21, N'Ser miembro de una sociedad de ayuda y asistencia'),
(22, N'Dirigir la campaña politica para un candidato estudiantil'),
(12, N'Ser jefe de un grupo'),
(50, N'Encargado de la ampliacion de la red carretera del pais');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PregunataId', N'pregunta') AND [object_id] = OBJECT_ID(N'[preguntasDelTestVocacional]'))
    SET IDENTITY_INSERT [preguntasDelTestVocacional] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'idCarrera', N'Carrera', N'areasCarreraId', N'areasTestId') AND [object_id] = OBJECT_ID(N'[catCarreras]'))
    SET IDENTITY_INSERT [catCarreras] ON;
INSERT INTO [catCarreras] ([idCarrera], [Carrera], [areasCarreraId], [areasTestId])
VALUES (3, N'Licenciado en Nutrición', 2, 1),
(4, N'Licenciado en Optometría', 2, 1),
(5, N'Contador Público', 3, 1),
(6, N'Licenciatura en Administración Industrial', 3, 1),
(8, N'Arte y Diseño', 4, 4),
(1, N'Ingeniería en Comunicaciones y Electrónica', 1, 6),
(7, N'Administración de Archivos y Gestión Documental', 4, 6),
(2, N'Ingeniería Aeronáutica', 1, 10);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'idCarrera', N'Carrera', N'areasCarreraId', N'areasTestId') AND [object_id] = OBJECT_ID(N'[catCarreras]'))
    SET IDENTITY_INSERT [catCarreras] OFF;

GO

CREATE INDEX [IX_carBecas_idUniversidad] ON [carBecas] ([idUniversidad]);

GO

CREATE INDEX [IX_carrerasDeseadas_idAlumno] ON [carrerasDeseadas] ([idAlumno]);

GO

CREATE INDEX [IX_carrerasDeseadas_idCarrera] ON [carrerasDeseadas] ([idCarrera]);

GO

CREATE INDEX [IX_carrerasImpartadas_catCarrerasId] ON [carrerasImpartadas] ([catCarrerasId]);

GO

CREATE INDEX [IX_carrerasImpartadas_usuarioUniversidad] ON [carrerasImpartadas] ([usuarioUniversidad]);

GO

CREATE INDEX [IX_carreraTecnicas_idAlumno] ON [carreraTecnicas] ([idAlumno]);

GO

CREATE INDEX [IX_catalogoCarrerasT_relcart_Catnoderelcat] ON [catalogoCarrerasT] ([relcart_Catnoderelcat]);

GO

CREATE INDEX [IX_catalogoDeMapasCuarriculares_idUniversidad] ON [catalogoDeMapasCuarriculares] ([idUniversidad]);

GO

CREATE INDEX [IX_catCarreras_areasCarreraId] ON [catCarreras] ([areasCarreraId]);

GO

CREATE INDEX [IX_catCarreras_areasTestId] ON [catCarreras] ([areasTestId]);

GO

CREATE INDEX [IX_contactos_idUniversidad] ON [contactos] ([idUniversidad]);

GO

CREATE INDEX [IX_empresaAsociadas_idUniversidad] ON [empresaAsociadas] ([idUniversidad]);

GO

CREATE INDEX [IX_informaciones_idAlumno] ON [informaciones] ([idAlumno]);

GO

CREATE INDEX [IX_Relaciones_idAlumno] ON [Relaciones] ([idAlumno]);

GO

CREATE INDEX [IX_Relaciones_idUniversidad] ON [Relaciones] ([idUniversidad]);

GO

CREATE INDEX [IX_valorPreguntas_areasTestID] ON [valorPreguntas] ([areasTestID]);

GO

CREATE INDEX [IX_valorPreguntas_idAlumno] ON [valorPreguntas] ([idAlumno]);

GO

CREATE INDEX [IX_valorPreguntas_idPregunta] ON [valorPreguntas] ([idPregunta]);

GO


-- =============================================
-- Author:		Daniel Gonzalez Martinez
-- Create date: 25/05/2020
-- Description:	select * from alumnosUsuarios
--select* from carrerasDeseadas
--select*from Relaciones
--delete from alumnosUsuarios
--	Este store procedure realiza la accion de buscar el test del alumno que se necesita
-- =============================================
CREATE PROCEDURE Realizarcalculodeltest
@idalumno uniqueidentifier
AS
BEGIN
declare @query table (ida uniqueidentifier ,area int )
insert into  @query  select top 3 val.idAlumno ,art.areaTestId  from valorPreguntas val inner join AreasTests art on val.areasTestID = art.areaTestId where val.idAlumno = @idalumno group by art.areaTestId, val.idAlumno order by SUM(val.valor) desc
select*from @query
insert into carrerasDeseadas select*from @query
insert into  Relaciones select distinct car.idAlumno, ci.usuarioUniversidad from carrerasDeseadas car join carrerasImpartadas ci on car.idCarrera = ci.catCarrerasId where car.idAlumno = @idalumno
END

GO

-- =============================================
-- Author:		Daniel
-- Create date: 26/05/2020
-- Description:	SOLO REALIZA LA RELACION
-- =============================================
CREATE PROCEDURE Realizarlarelacion
@idalumno uniqueidentifier
AS
BEGIN
insert into  Relaciones select distinct car.idAlumno, ci.usuarioUniversidad from carrerasDeseadas car join carrerasImpartadas ci on car.idCarrera = ci.catCarrerasId where car.idAlumno = @idalumno
END

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200526184156_1', N'2.1.14-servicing-32113');

GO

