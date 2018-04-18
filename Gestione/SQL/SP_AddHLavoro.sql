create procedure SP_AddHLavoro
	@data Date,
	@ore int,
	@idCommessa int,
	@idUtente as nvarchar(10)
as
	DECLARE @idGiorno int = (
	SELECT id FROM Giorni
	WHERE giorno=@data AND idUtente=@idUtente);

	IF @idGiorno IS NULL
		BEGIN
			INSERT INTO Giorni (giorno, idUtente) VALUES (@data, @idUtente);
			SET @idGiorno = (SELECT IDENT_CURRENT ('Giorni'));
		end;

	declare @oreNonLav int;
	set @oreNonLav = (select sum(ore) from OreNonLavorative where idGiorno = @idGiorno);

	declare @oreLav int;
	set @oreLav = (select sum(ore) from OreLavorative where idGiorno = @idGiorno);

	if (@oreNonLav is null)
		begin
			set @oreNonLav = 0;
		end

	if (@oreLav is null)
		begin
			set @oreLav = 0;
		end
	
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

exec SP_AddHLavoro "2018/04/8", 1, 1, "n.1";

select * from OreLavorative
