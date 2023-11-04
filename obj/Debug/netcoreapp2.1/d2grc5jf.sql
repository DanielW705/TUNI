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

CREATE TABLE [catalogoCarrerasT] (
    [idCarreTecnicas] int NOT NULL IDENTITY,
    [carreraTecnica] nvarchar(max) NULL,
    CONSTRAINT [PK_catalogoCarrerasT] PRIMARY KEY ([idCarreTecnicas])
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

CREATE TABLE [datosAcademicos] (
    [idAlumno] uniqueidentifier NOT NULL,
    [boletaGlobal] nvarchar(max) NOT NULL,
    [doc] varbinary(max) NOT NULL,
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

CREATE TABLE [carreraTecnicas] (
    [idCart] int NOT NULL IDENTITY,
    [idAlumno] uniqueidentifier NOT NULL,
    [idCatT] int NOT NULL,
    CONSTRAINT [PK_carreraTecnicas] PRIMARY KEY ([idCart]),
    CONSTRAINT [Relacion_usuario_carreraTecnica] FOREIGN KEY ([idAlumno]) REFERENCES [alumnosUsuarios] ([idAlumno]) ON DELETE CASCADE,
    CONSTRAINT [Relacion_carreratecnica_catalogodecarrerastecnicas] FOREIGN KEY ([idCatT]) REFERENCES [catalogoCarrerasT] ([idCarreTecnicas]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_carreraTecnicas_idAlumno] ON [carreraTecnicas] ([idAlumno]);

GO

CREATE UNIQUE INDEX [IX_carreraTecnicas_idCatT] ON [carreraTecnicas] ([idCatT]);

GO

CREATE INDEX [IX_informaciones_idAlumno] ON [informaciones] ([idAlumno]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200429034252_f', N'2.1.14-servicing-32113');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[datosAcademicos]') AND [c].[name] = N'doc');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [datosAcademicos] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [datosAcademicos] ALTER COLUMN [doc] varbinary(max) NULL;

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[datosAcademicos]') AND [c].[name] = N'boletaGlobal');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [datosAcademicos] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [datosAcademicos] ALTER COLUMN [boletaGlobal] nvarchar(max) NULL;

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[alumnos]') AND [c].[name] = N'nombre');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [alumnos] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [alumnos] ALTER COLUMN [nombre] nvarchar(max) NULL;

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[alumnos]') AND [c].[name] = N'apPaterno');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [alumnos] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [alumnos] ALTER COLUMN [apPaterno] nvarchar(max) NULL;

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[alumnos]') AND [c].[name] = N'apMaterno');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [alumnos] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [alumnos] ALTER COLUMN [apMaterno] nvarchar(max) NULL;

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
    CONSTRAINT [Rellacion_usuario_contacto] FOREIGN KEY ([idUniversidad]) REFERENCES [universidadesUsuario] ([idUniversidad]) ON DELETE CASCADE
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

CREATE TABLE [universidades] (
    [idUnversidad] uniqueidentifier NOT NULL,
    [nombre] nvarchar(max) NULL,
    [direccion] nvarchar(max) NULL,
    CONSTRAINT [PK_universidades] PRIMARY KEY ([idUnversidad]),
    CONSTRAINT [Relacion_usuarioUniversidad_universidad] FOREIGN KEY ([idUnversidad]) REFERENCES [universidadesUsuario] ([idUniversidad]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_carBecas_idUniversidad] ON [carBecas] ([idUniversidad]);

GO

CREATE INDEX [IX_catalogoDeMapasCuarriculares_idUniversidad] ON [catalogoDeMapasCuarriculares] ([idUniversidad]);

GO

CREATE INDEX [IX_contactos_idUniversidad] ON [contactos] ([idUniversidad]);

GO

CREATE INDEX [IX_empresaAsociadas_idUniversidad] ON [empresaAsociadas] ([idUniversidad]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200507015214_1', N'2.1.14-servicing-32113');

GO

