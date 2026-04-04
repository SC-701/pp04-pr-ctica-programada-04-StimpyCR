CREATE PROCEDURE ObtenerVehiculo
	-- Add the parameters for the stored procedure here
	@id uniqueidentifier

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT        Vehiculo.id, Vehiculo.idModelo, Vehiculo.placa, Vehiculo.color, Vehiculo.anio, Vehiculo.precio, Vehiculo.correoPropietario, Vehiculo.telefonoPropietario, modelos.nombre AS Modelo, marcas.nombre AS Marca
FROM            Vehiculo INNER JOIN
                         modelos ON Vehiculo.idModelo = modelos.id INNER JOIN
                         marcas ON modelos.idMarca = marcas.id
WHERE        (Vehiculo.id = @id)
END