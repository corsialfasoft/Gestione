CREATE PROCEDURE SP_Compila
	@giorno date,
	@idUtente nvarchar(10),
	@ore int,
	@TipoOre int
AS
DECLARE @idGiorno int = (
SELECT TOP 1 id FROM Giorni
WHERE giorno=@giorno AND idUtente=@idUtente);

IF @idGiorno IS NULL
	BEGIN
		INSERT INTO Giorni (giorno, idUtente) VALUES (@giorno, @idUtente);
		SET @idGiorno = (SELECT IDENT_CURRENT ('Giorni'))
	END

DECLARE @OreLav int = (SELECT top 1 SUM(ore) FROM OreLavorative WHERE idGiorno=@idGiorno)
IF @OreLav IS NULL
	set @OreLav =0;
DECLARE @OreNLav int =  (SELECT top 1 SUM(ore) FROM OreNonLavorative WHERE idGiorno=@idGiorno)
IF @OreNLav IS NULL
	set @OreNLav =0;
IF @OreNLav+ @OreLav +@ore>8 
	throw 111133,'non si puo inserire il record',22;
ELSE
	BEGIN
		INSERT INTO OreNonLavorative (tipoOre, ore, idGiorno) VALUES (@TipoOre, @ore, @idGiorno);
	END
GO