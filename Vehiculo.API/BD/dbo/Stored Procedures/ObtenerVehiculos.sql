CREATE PROCEDURE ObtenerVehiculos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Vehiculo.id AS idVehiculo,
        Vehiculo.placa,
        Vehiculo.color,
        Vehiculo.anio,
        Vehiculo.precio,
        Vehiculo.correoPropietario,
        Vehiculo.telefonoPropietario,

        modelos.id AS idModelo,
        modelos.nombre AS modelo,

        marcas.id AS idMarca,
        marcas.nombre AS marca

    FROM Vehiculo
    INNER JOIN modelos 
        ON Vehiculo.idModelo = modelos.id
    INNER JOIN marcas 
        ON modelos.idMarca = marcas.id
END