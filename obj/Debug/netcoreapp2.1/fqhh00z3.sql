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

CREATE TABLE [catAreasCarrera] (
    [idArea] int NOT NULL IDENTITY,
    [area] nvarchar(max) NULL,
    CONSTRAINT [PK_catAreasCarrera] PRIMARY KEY ([idArea])
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
    CONSTRAINT [relacion_Areas_Con_Carrera] FOREIGN KEY ([areasCarreraId]) REFERENCES [catAreasCarrera] ([idArea]) ON DELETE CASCADE
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

CREATE TABLE [catalogoCarrerasT] (
    [carreTecnicaId] int NOT NULL IDENTITY,
    [carreraTecnica] nvarchar(max) NULL,
    [relcart_Catnoderelcat] int NULL,
    CONSTRAINT [PK_catalogoCarrerasT] PRIMARY KEY ([carreTecnicaId]),
    CONSTRAINT [Relacion_carreratecnica_catalogodecarrerastecnicas] FOREIGN KEY ([relcart_Catnoderelcat]) REFERENCES [carreraTecnicas] ([noderelcat]) ON DELETE NO ACTION
);

GO

CREATE TABLE [AreasTests] (
    [areaTestId] int NOT NULL,
    [areaDelTest] nvarchar(max) NULL,
    CONSTRAINT [PK_AreasTests] PRIMARY KEY ([areaTestId]),
    CONSTRAINT [Relacion_carrera_areaTest] FOREIGN KEY ([areaTestId]) REFERENCES [catCarreras] ([idCarrera]) ON DELETE CASCADE
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

CREATE INDEX [IX_carBecas_idUniversidad] ON [carBecas] ([idUniversidad]);

GO

CREATE INDEX [IX_carrerasDeseadas_idAlumno] ON [carrerasDeseadas] ([idAlumno]);

GO

CREATE INDEX [IX_carrerasDeseadas_idCarrera] ON [carrerasDeseadas] ([idCarrera]);

GO

CREATE INDEX [IX_carreraTecnicas_idAlumno] ON [carreraTecnicas] ([idAlumno]);

GO

CREATE INDEX [IX_catalogoCarrerasT_relcart_Catnoderelcat] ON [catalogoCarrerasT] ([relcart_Catnoderelcat]);

GO

CREATE INDEX [IX_catalogoDeMapasCuarriculares_idUniversidad] ON [catalogoDeMapasCuarriculares] ([idUniversidad]);

GO

CREATE INDEX [IX_catCarreras_areasCarreraId] ON [catCarreras] ([areasCarreraId]);

GO

CREATE INDEX [IX_contactos_idUniversidad] ON [contactos] ([idUniversidad]);

GO

CREATE INDEX [IX_empresaAsociadas_idUniversidad] ON [empresaAsociadas] ([idUniversidad]);

GO

CREATE INDEX [IX_informaciones_idAlumno] ON [informaciones] ([idAlumno]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200518173346_1', N'2.1.14-servicing-32113');

GO

