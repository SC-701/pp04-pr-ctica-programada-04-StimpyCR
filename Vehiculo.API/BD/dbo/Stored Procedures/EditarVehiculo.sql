CREATE PROCEDURE EditarVehiculo
    @id uniqueidentifier,
    @idModelo uniqueidentifier,
    @placa varchar(max),
    @color varchar(max),
    @anio int,
    @precio decimal(18,2),
    @correoPropietario varchar(max),
    @telefonoPropietario varchar(max)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar modelo exista (simple)
    IF NOT EXISTS (SELECT 1 FROM modelos WHERE id = @idModelo)
        RETURN;

    UPDATE Vehiculo
    SET
        idModelo = @idModelo,
        placa = @placa,
        color = @color,
        anio = @anio,
        precio = @precio,
        correoPropietario = @correoPropietario,
        telefonoPropietario = @telefonoPropietario
    WHERE id = @id;

    SELECT @id AS idVehiculo;
END