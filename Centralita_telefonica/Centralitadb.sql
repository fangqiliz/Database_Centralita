CREATE TABLE Llamadass
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TipoLlamada NVARCHAR(20),
    -- "Local" o "Provincial"
    NumOrigen NVARCHAR(50),
    NumDestino NVARCHAR(50),
    Duracion FLOAT,
    Franja INT NULL,
    -- Solo para provinciales
    Precio FLOAT
);
