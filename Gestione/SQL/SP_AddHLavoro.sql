create procedure SP_AddHLavoro
	--DateTime data, int ore, int idCommessa, string idUtente

	--idGiorno int foreign key references Giorni not null,
	--idCommessa int foreign key references Commesse not null,
	--ore int not null,
	--primary key(idGiorno,idCommessa)

	@data Date,
	@ore int,
	@idCommessa int,
	@idUtente as nvarchar(10)
as
	DECLARE @idGiorno int = (
	SELECT TOP 1 id FROM Giorni
	WHERE giorno=@data AND idUtente=@idUtente);

	IF @idGiorno IS NULL
		BEGIN
			INSERT INTO Giorni (giorno, idUtente) VALUES (@data, @idUtente);
			SET @idGiorno = (SELECT IDENT_CURRENT ('Giorni'))
		end;
		declare @oreNonLav int = 0;
		set @oreNonLav = (select sum(ore) from OreNonLavorative where idGiorno = @idGiorno);

		declare @oreLav int = 0;
		set @oreLav = (select sum(ore) from OreLavorative where idGiorno = @idGiorno);
			
		if (@ore + @oreLav + @oreNonLav <= 8)
			begin
				insert into OreLavorative(idGiorno, idCommessa, ore) values(@idGiorno, @idCommessa, @ore);
				if(@@Error >0)
					begin
						declare @oreDellaComm int = (select ore from OreLavorative where idGiorno=@idGiorno and idCommessa = @idCommessa);
						update OreLavorative set ore=@ore+@oreDellaComm where idGiorno=@idGiorno and idCommessa = @idCommessa;
					end
			end;
		else
			return 0;
	
go
