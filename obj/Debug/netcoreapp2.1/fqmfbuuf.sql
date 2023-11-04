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
    CONSTRAINT [Relacion_usuario_informacion] FOREIGN KEY ([idAlumno]) REFERENCES [alumnosUsuarios] ([idAlumno]) ON DELETE CASCADE
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
VALUES (N'20200426200519_1', N'2.1.14-servicing-32113');

GO

