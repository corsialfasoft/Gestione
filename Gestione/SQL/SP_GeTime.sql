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
go
create procedure SP_VisualizzaCommessa
	@idC int,
	@idU nvarchar(10)
as 
	select G.id,G.giorno,OL.ore,C.id,C.nome,C.descrizione
	from Giorni G inner join OreLavorative OL on G.id=OL.idGiorno inner join Commesse C on OL.idCommessa=C.id
	where G.idUtente=@idU and C.id=@idC
	order by G.giorno;
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
create procedure SP_CercaCommesse
	@nomeCommessa nvarchar(50)
as	
	select id,nome,descrizione,stimaOre, (select ISNULL(SUM(OL.ore),0)
										  from OreLavorative OL inner join Commesse C1 on OL.idCommessa=C1.id
										  where C.id=C1.id) as OreTotLavorate
	from Commesse C
	where nome like '%'+@nomeCommessa+'%';
go
create procedure SP_VisualizzaGiorno
	@Data date,
	@IdUtente nvarchar(10)
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
create procedure SP_VisualizzaMese
	@Anno int,
	@Mese int,
	@IdUtente nvarchar(10)
as
	select 4 as tipoOre, sum(ol.ore) ore, g.giorno
		from OreLavorative ol
		inner join Giorni g on ol.idGiorno = g.id
		where (year(g.giorno) = @Anno and month(g.giorno) = @Mese) and g.idUtente = @IdUtente
		group by g.giorno
	union all
	select onl.tipoOre, onl.ore, g.giorno
		from OreNonLavorative onl inner join Giorni g on onl.idGiorno = g.id
		where (year(g.giorno) = @Anno and month(g.giorno) = @Mese) and g.idUtente = @IdUtente
		order by g.giorno
go