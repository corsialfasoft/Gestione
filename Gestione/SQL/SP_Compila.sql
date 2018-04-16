CREATE PROCEDURE SP_Compila
	@giorno as date,
	@idUtente as nvarchar(10),
	@ore as int,
	@TipoOre as int
AS
DECLARE @idGiorno int = (
SELECT TOP 1 id FROM Giorni
WHERE giorno=@giorno AND idUtente=@idUtente);

IF @idGiorno IS NOT NULL
	BEGIN
		INSERT INTO Giorni (giorno, idUtente) VALUES (@giorno, @idUtente);
		SET @idGiorno = (SELECT IDENT_CURRENT ('Giorni'))
	END

DECLARE @TotOreLav int = 
(SELECT SUM(ore) FROM OreLavorative WHERE idGiorno=@idGiorno)

SET @TotOreLav += (
SELECT SUM(ore) FROM OreNonLavorative WHERE idGiorno=@idGiorno)

IF @TotOreLav+@ore>8 
	BEGIN
		RETURN 0
	END
ELSE
	BEGIN
		INSERT INTO OreNonLavorative (tipoOre, ore, idGiorno) VALUES (@TipoOre, @ore, @idGiorno)
		RETURN 1
	END
GO