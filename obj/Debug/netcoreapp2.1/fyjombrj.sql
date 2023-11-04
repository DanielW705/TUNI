IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [aceptados] (
    [nodeaceptado] int NOT NULL IDENTITY,
    [iduniversidad] uniqueidentifier NOT NULL,
    [idalumno] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_aceptados] PRIMARY KEY ([nodeaceptado])
);

GO

CREATE TABLE [Administradores] (
    [idAmon] uniqueidentifier NOT NULL,
    [username] nvarchar(max) NOT NULL,
    [contraseña] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Administradores] PRIMARY KEY ([idAmon])
);

GO

CREATE TABLE [alumnosUsuarios] (
    [idAlumno] uniqueidentifier NOT NULL,
    [usuario] nvarchar(max) NOT NULL,
    [contraseña] nvarchar(max) NOT NULL,
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

CREATE TABLE [rechazos] (
    [noderechazo] int NOT NULL IDENTITY,
    [idUniversidad] uniqueidentifier NOT NULL,
    [idAlumno] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_rechazos] PRIMARY KEY ([noderechazo])
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
    [nombre] nvarchar(max) NOT NULL,
    [apPaterno] nvarchar(max) NOT NULL,
    [apMaterno] nvarchar(max) NOT NULL,
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

CREATE TABLE [solicitar] (
    [nodesolicitud] int NOT NULL IDENTITY,
    [idAlumno] uniqueidentifier NOT NULL,
    [idUniversidad] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_solicitar] PRIMARY KEY ([nodesolicitud]),
    CONSTRAINT [RelacionsolicitudAlumno] FOREIGN KEY ([idAlumno]) REFERENCES [alumnosUsuarios] ([idAlumno]) ON DELETE CASCADE,
    CONSTRAINT [RelacionsolicutudUni] FOREIGN KEY ([idUniversidad]) REFERENCES [universidadesUsuario] ([idUniversidad]) ON DELETE CASCADE
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

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'idAmon', N'contraseña', N'username') AND [object_id] = OBJECT_ID(N'[Administradores]'))
    SET IDENTITY_INSERT [Administradores] ON;
INSERT INTO [Administradores] ([idAmon], [contraseña], [username])
VALUES ('712304d3-691b-4650-a978-42ef3d070a51', N'123aEFGJnfsa', N'ADMINISTRADOR 1'),
('1bfbfc54-f670-4257-83dc-be4e84689f4f', N'aggvkKBQ5hp', N'ADMINISTRADOR 2'),
('ad84fb9d-a17d-4475-b9bc-8e1c5cc97bc5', N'HzmJlaLKU1f', N'ADMINISTRADOR 3'),
('af35d7d3-773c-4fc6-81ad-a197f4b27fbb', N'Xmxg82RTiuQV', N'ADMINISTRADOR 4'),
('e409d6ae-cc4d-4450-a907-42a09b3aa9a9', N'bgTR1apIK1ye', N'ADMINISTRADOR 5');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'idAmon', N'contraseña', N'username') AND [object_id] = OBJECT_ID(N'[Administradores]'))
    SET IDENTITY_INSERT [Administradores] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'areaTestId', N'areaDelTest') AND [object_id] = OBJECT_ID(N'[AreasTests]'))
    SET IDENTITY_INSERT [AreasTests] ON;
INSERT INTO [AreasTests] ([areaTestId], [areaDelTest])
VALUES (11, N'Destreza manual'),
(10, N'Aire Libre'),
(9, N'Mecanico Constructivo'),
(7, N'Cientifica'),
(6, N'Organizacion'),
(5, N'Musical'),
(4, N'Artistico Plastico'),
(8, N'Calculo'),
(2, N'Ejecutiva Persuasiva'),
(1, N'Servicio Social'),
(3, N'Verbal');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'areaTestId', N'areaDelTest') AND [object_id] = OBJECT_ID(N'[AreasTests]'))
    SET IDENTITY_INSERT [AreasTests] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'idArea', N'area') AND [object_id] = OBJECT_ID(N'[catAreasCarrera]'))
    SET IDENTITY_INSERT [catAreasCarrera] ON;
INSERT INTO [catAreasCarrera] ([idArea], [area])
VALUES (4, N'Humanidades y de las Artes'),
(3, N'Ciencias Sociales y administrativas'),
(2, N'Ciencias Biologicas, Quimicas de la salud'),
(1, N'Fisico matematicas');
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
(27, N'Investigar el origen de las costumbres de los pueblos'),
(30, N'Trabajar al aire libre, fuera de la ciudad'),
(29, N'Construir objeto o muebles'),
(28, N'LLevar las cuentas de una institucion'),
(36, N'Diseñador de software'),
(31, N'Cuidar a niños pequeños'),
(37, N'Asistir a una conferencia cientifica a un museo'),
(48, N'Contador general de una empresa'),
(39, N'Reparar algun aparato descompuesto'),
(40, N'Ir de paseo al campo o a la playa'),
(41, N'Ser miembro especial de la Cruz Roja Internacional para casos de desastre'),
(42, N'Gerente de mercadotecnia de una compañia'),
(43, N'Articulista de un periodico'),
(44, N'Dieseñador de las portadas de una revista'),
(45, N'Tocar en la orquesta de tu ciudad'),
(46, N'Poner en orden tu coleccion favorita'),
(47, N'Coordinador de un grupo cientifico de vanguardia'),
(26, N'Aprender a usar programas de programacion'),
(38, N'Calcular el presupuesto de una familia'),
(25, N'Aprender a tocar un instrumento musical'),
(14, N'Moldear el barrio, plastilina o cualquier otro material'),
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
(49, N'Autoridad en la construccion de ciertas estructuras arquitectonocas'),
(15, N'Escuchar musica clasica'),
(16, N'Ordenar y clasificar los libro de una biblioteca'),
(17, N'Hacer experimentos en un laboratorio'),
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
VALUES (21, N'Ingenieria Topografica y Fotogrametrica', 1, 1),
(88, N'Investigacion Biomedica Basica', 2, 7),
(91, N'Medicina Veterinaria y Zootecnia', 2, 7),
(93, N'Neurociencias', 2, 7),
(99, N'Quimica', 2, 7),
(100, N'Quimica de Alimentos', 2, 7),
(101, N'Quimica e Ingenieria en Materiales', 2, 7),
(102, N'Quimica Farmaceuticao Biologica', 2, 7),
(71, N'Quimico Farmaceutico Indisctrial', 2, 7),
(103, N'Quimica Industrial', 2, 7),
(144, N'Filosofia', 4, 7),
(145, N'Geohistoria', 4, 7),
(146, N'Historia', 4, 7),
(147, N'Historia del Arte', 4, 7),
(4, N'Ingenieria en informatica', 1, 8),
(11, N'Ingenieria en sistemas Energeticos y Redes Inteligentes', 1, 8),
(19, N'Ingenieria en Transporte', 1, 8),
(108, N'Antropologia', 3, 7),
(25, N'Ingenieria Biomedica', 1, 8),
(69, N'Licenciado en Biologia', 2, 7),
(64, N'Tecnologia', 1, 7),
(9, N'Ingenieria en Sistemas Automotrices', 1, 7),
(12, N'Ingenieria Quimica Industrial', 1, 7),
(13, N'Ingenieria Quimica Petrolera', 1, 7),
(14, N'Ingenieria Telemeatica', 1, 7),
(23, N'Licenciatura en Ciencias de la Informatica', 1, 7),
(24, N'Licenciatura en Fisica y Matematicas', 1, 7),
(26, N'Ingenieria Biomedica', 1, 7),
(65, N'Tecnologias para la informacion en Cinecias', 1, 7),
(36, N'Ciencias de la Computacion', 1, 7),
(39, N'Fisica', 1, 7),
(40, N'Fisica Biomedica', 1, 7),
(49, N'Ingenieria en Telecomunicaciones, Sistemas y Electronica', 1, 7),
(56, N'Ingenieria Mecatronica', 1, 7),
(57, N'Ingenieria Petrolera', 1, 7),
(58, N'Ingenieria Quimica', 1, 7),
(63, N'Nanotecnologia', 1, 7),
(37, N'Ciencias de la Tierra', 1, 7),
(31, N'Actuaria', 1, 8),
(34, N'Ciencias de Datos', 1, 8),
(42, N'Ingenieria Ambiental', 1, 8),
(76, N'Ciencias Agroforestales', 2, 10),
(77, N'Ciencias Agrogenomicas', 2, 10),
(78, N'Ciencias Ambientales', 2, 10),
(79, N'Ciencias Genomicas', 2, 10),
(86, N'Ingenieria Agricola', 2, 10),
(6, N'Ingeniería en Metalurgia y Materiales', 1, 11),
(16, N'Ingeniero Arquitecto', 1, 11),
(52, N'Ingenieria Geomatica', 1, 10),
(18, N'Ingenieria Matematica', 1, 11),
(27, N'Ingenieria Bionica', 1, 11),
(28, N'Ingenieria Bioquimica', 1, 11),
(38, N'Diseño Industrial', 1, 11),
(44, N'Ingenieria Electrica Electronica', 1, 11),
(45, N'Ingenieria en Computacion', 1, 11),
(46, N'Ingenieria en Energias Renovables', 1, 11),
(73, N'Medico Cirujano Partero', 2, 11),
(20, N'Ingenieria Farmaceutica', 1, 11),
(51, N'Ingenieria Geologica', 1, 10),
(50, N'Ingenieria Geofisica', 1, 10),
(43, N'Ingenieria de Minas y Metalurgia', 1, 10),
(47, N'Ingenieria en Sistemas Biomedicos', 1, 8),
(48, N'Ingenieria en Telecomunicaicones', 1, 8),
(53, N'Ingenieria Industrial', 1, 8),
(54, N'Ingenieria Mecanica', 1, 8),
(55, N'Ingenieria Mecanica Electrica', 1, 8),
(104, N'Contador Publico', 3, 8),
(125, N'Geografia', 3, 8),
(126, N'Geografia Aplicada', 3, 8),
(2, N'Ingeniería Aeronáutica', 1, 9),
(10, N'Ingenieria en Sistemas Computacionales', 1, 9),
(29, N'Ingenieria Biotecnologica', 1, 9),
(30, N'Ingenieria Civil', 1, 9),
(35, N'Ciencias de Materiales Sustentables', 1, 9),
(59, N'Ingenieria Quimica Metalurgica', 1, 9),
(97, N'Ortesis y protesis', 2, 9),
(22, N'Licenciatura en ciencia de datos', 1, 10),
(41, N'Geociencias', 1, 10),
(8, N'Ingenieria en Robotica Industrial', 1, 7),
(5, N'Ingeniería en Inteligencia Artificial', 1, 7),
(137, N'Desarrollo y Gestion Interculturales', 4, 6),
(135, N'Bibliotecologia y Estudios de la informacion', 4, 6),
(130, N'Sociologia', 3, 1),
(131, N'Licenciatura en Tabajo Social', 3, 1),
(140, N'Enseñanza de Lengua Extranjera', 4, 1),
(141, N'Enseñansa de Ingles', 4, 1),
(142, N'Estudios Latinoamericanos', 4, 1),
(156, N'Musica Educacion Musical', 4, 1),
(160, N'Pedagogia', 4, 1),
(128, N'Planificacion para el Desarrollo Agropecuario', 3, 1),
(7, N'Ingenieria en Negocios Energeticos Sustentables', 1, 2),
(89, N'Manejo Sustentable de Zonas Costeras', 2, 2),
(92, N'Licenciatura en Medicina', 2, 2),
(96, N'Optometria', 2, 2),
(105, N'Licenciatura en Administracion Industrial', 3, 2),
(106, N'Administracion', 3, 2),
(107, N'Administracion Agropecuaria', 3, 2),
(110, N'Ciencias Politicas y Administracion Publica', 3, 2),
(87, N'Ingenieria en Alimentos', 2, 2),
(124, N'Estudios Sociales y Gestion Local', 3, 1),
(120, N'Desarrollo Comunitario para el Envejecimiento', 3, 1),
(119, N'Derecho', 3, 1),
(66, N'Urbanismo', 1, 1),
(67, N'Licenciado en Nutricion', 2, 1),
(68, N'Licenciado en Optometria', 2, 1),
(70, N'Licenciado en Diagnostica', 2, 1),
(72, N'Medico Cirujano Homeopata', 2, 1),
(80, N'Cirujano Dentista', 2, 1),
(81, N'Ecologia', 2, 1),
(82, N'Licenciatura en Enfermeria', 2, 1),
(83, N'Licenciatura en enfermeria y Obstetricia', 2, 1),
(84, N'Farmacia', 2, 1),
(85, N'Fisioterapia', 2, 1),
(90, N'Medico Cirujano', 2, 1),
(94, N'Nutriologia', 2, 1),
(95, N'Licenciado en Odontologia', 2, 1),
(98, N'Licenciado en Psicologia', 2, 1),
(117, N'Licenciado en Relaciones Comerciales', 3, 1),
(118, N'Licenciado en Turismo', 3, 1),
(121, N'Desarrollo Territorial', 3, 2),
(74, N'Quimico Bacteriologo Parasitologo', 2, 11),
(122, N'Licenciatura en Economia', 3, 2),
(109, N'Ciencias de la Comunicacion', 3, 3),
(154, N'Musica Canto', 4, 5),
(155, N'Musica Composicion', 4, 5),
(157, N'Musica Instrumentista', 4, 5),
(158, N'Musica Piano', 4, 5),
(159, N'Musica y Tecnologia Artistica', 4, 5),
(1, N'Ingeniería en Comunicaciones y Electrónica', 1, 6),
(3, N'Ingeniería en Control y Automatización', 1, 6),
(143, N'Etnomusicologia', 4, 5),
(15, N'Ingenieria Textil', 1, 6),
(61, N'Matematicas Aplicadas', 1, 6),
(62, N'Matematicas Aplicadas y Computacion', 1, 6),
(111, N'Licenciatura en Administracion y Desarrollo Empresarial', 3, 6),
(112, N'Licenciatura en Archivonomia', 3, 6),
(113, N'Licencitura en Bibliotecnomia', 3, 6),
(116, N'Contaduria', 3, 6),
(132, N'Administracion de Arcivos y Gestion Documental', 4, 6),
(60, N'Matematicas', 1, 6),
(161, N'Teatro y actuacion', 4, 4),
(152, N'Literatura Dramatica y Teatro', 4, 4),
(139, N'Diseño y Comunicacion Visual', 4, 4),
(114, N'Comunicacion', 3, 3),
(115, N'Comunicacion y Periodismo', 3, 3),
(127, N'Licenciatura en Negocios Internacionales', 3, 3),
(129, N'Relaciones Internacionales', 3, 3),
(148, N'Lengua y literaturas Hispanicas', 4, 3),
(149, N'Lengua y literaturas Modernas', 4, 3),
(150, N'lenguas Clasicas', 4, 3),
(151, N'Linguistica Aplicada', 4, 3),
(153, N'Literatura Intercultural', 4, 3),
(162, N'Traduccion', 4, 3),
(17, N'Ingenieria Metalirgica', 1, 4),
(32, N'Arquitectura', 1, 4),
(33, N'Arquitectura de Paisajes', 1, 4),
(133, N'Arte y Diseño', 4, 4),
(134, N'Artes Visuales', 4, 4),
(136, N'Cinematografia', 4, 4),
(138, N'Diseño Grafico', 4, 4),
(123, N'Economia Industrial', 3, 2),
(75, N'Ciencia Forense', 2, 11);
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

CREATE UNIQUE INDEX [IX_solicitar_idAlumno] ON [solicitar] ([idAlumno]);

GO

CREATE UNIQUE INDEX [IX_solicitar_idUniversidad] ON [solicitar] ([idUniversidad]);

GO

CREATE INDEX [IX_valorPreguntas_areasTestID] ON [valorPreguntas] ([areasTestID]);

GO

CREATE INDEX [IX_valorPreguntas_idAlumno] ON [valorPreguntas] ([idAlumno]);

GO

CREATE INDEX [IX_valorPreguntas_idPregunta] ON [valorPreguntas] ([idPregunta]);

GO


---=======================================
--Author:		Daniel Gonzalez Martinez
-- Create date: 25 / 05 / 2020
-- Description: select* from alumnosUsuarios
--select* from carrerasDeseadas
--select* from Relaciones
--delete from alumnosUsuarios
--  Este store procedure realiza la accion de buscar el test del alumno que se necesita
-- =============================================
CREATE PROCEDURE Realizarcalculodeltest
@idalumno uniqueidentifier
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
END
ALTER PROCEDURE Realizarlarelacion
@idalumno uniqueidentifier
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
END

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200610195426_1', N'2.1.14-servicing-32113');

GO

