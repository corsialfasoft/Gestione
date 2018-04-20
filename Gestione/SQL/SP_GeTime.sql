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

	DECLARE @OreLav int = ISNULL((SELECT top 1 SUM(ore) FROM OreLavorative WHERE idGiorno=@idGiorno),0)
	DECLARE @OreNLav int =  ISNULL((SELECT top 1 SUM(ore) FROM OreNonLavorative WHERE idGiorno=@idGiorno),0)
	IF @OreNLav+ @OreLav +@ore>8 
		throw 111133,'non si puo inserire il record',22;
	ELSE
		BEGIN
			begin try
					INSERT INTO OreNonLavorative (tipoOre, ore, idGiorno) VALUES (@TipoOre, @ore, @idGiorno);
				end try
				begin catch			
					if(@@Error = 2627)
						begin
							declare @oreNL int = (select top 1 ore from OreNonLavorative where idGiorno=@idGiorno and tipoOre = @TipoOre);
							update OreNonLavorative set ore=@ore+@oreNL where idGiorno=@idGiorno and tipoOre = @TipoOre;
						end
				end catch
		
		END
GO
create procedure SP_AddHLavoro
	@data Date,
	@ore int,
	@idCommessa int,
	@idUtente as nvarchar(10)
as
	DECLARE @idGiorno int = (SELECT id FROM Giorni WHERE giorno=@data AND idUtente=@idUtente);
	IF @idGiorno IS NULL
		BEGIN
			INSERT INTO Giorni (giorno, idUtente) VALUES (@data, @idUtente);
			SET @idGiorno = (SELECT IDENT_CURRENT ('Giorni'));
		end;

	declare @oreNonLav int =  ISNULL((select sum(ore) from OreNonLavorative where idGiorno = @idGiorno),0);
	declare @oreLav int = ISNULL((select sum(ore) from OreLavorative where idGiorno = @idGiorno),0);

	if (@ore + @oreLav + @oreNonLav <= 8)
		begin
			begin try
				insert into OreLavorative(idGiorno, idCommessa, ore) values(@idGiorno, @idCommessa, @ore);
			end try
			begin catch			
				if(@@Error = 2627)
					begin
						declare @oreDellaComm int = (select top 1 ore from OreLavorative where idGiorno=@idGiorno and idCommessa = @idCommessa);
						update OreLavorative set ore=@ore+@oreDellaComm where idGiorno=@idGiorno and idCommessa = @idCommessa;
					end
			end catch
		end
	else
		throw 111133,'non si puo inserire il record',22;
go
create procedure SP_VisualizzaCommessa
	@idC int,
	@idU nvarchar(10)
as 
	select G.id,G.giorno,OL.ore,C.id,C.nome,C.descrizione
	from Giorni G inner join OreLavorative OL on G.id=OL.idGiorno inner join Commesse C on OL.idCommessa=C.id
	where G.idUtente=@idU and C.id=@idC;
go
create procedure SP_CercaCommessa
	@nomeCommessa nvarchar(50)
as	
	select id,nome,descrizione,stimaOre, (select ISNULL(SUM(OL.ore),0)
										  from OreLavorative OL inner join Commesse C1 on OL.idCommessa=C1.id
										  where C.id=C1.id) as OreTotLavorate
	from Commesse C
	where nome = @nomeCommessa;
go
create procedure SP_VisualizzaGiorno
	@Data date,
	@IdUtente nvarchar(20)
as
	select 4, ol.ore, c.nome, c.descrizione, c.id
		from Commesse c inner join OreLavorative ol on c.id = ol.idCommessa
						inner join Giorni g on ol.idGiorno = g.id
		where g.giorno = @Data and g.idUtente = @IdUtente
		union all
	select  onl.tipoOre, onl.ore, '','', 1
		from OreNonLavorative onl inner join Giorni g on onl.idGiorno = g.id
		where g.giorno = @Data and g.idUtente = @IdUtente;
go