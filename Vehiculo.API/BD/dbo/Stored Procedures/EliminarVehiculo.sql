CREATE PROCEDURE EliminarVehiculo
	-- Add the parameters for the stored procedure here
	@id uniqueidentifier

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	begin transaction
delete
FROM            Vehiculo 
WHERE        (id = @id)
select @id
commit transaction
END