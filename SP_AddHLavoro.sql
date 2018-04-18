create procedure SP_AddHLavoro
	--DateTime data, int ore, int idCommessa, string idUtente

	--idGiorno int foreign key references Giorni not null,
	--idCommessa int foreign key references Commesse not null,
	--ore int not null,
	--primary key(idGiorno,idCommessa)

	@data Date,
	@ore int,
	@idCommessa int,
	@idUtente int
as
	if @data is not null
		begin
			declare @oreNonLav int;
			set @oreNonLav = (select ore from OreNonLavorative where idGiorno = @data);

			declare @oreLav int;
			set @oreLav = (select ore from OreLavorative where idGiorno = @data.DAY);
			
			if (@ore + @oreLav + @oreNonLav <= 8)
				begin
					declare @idGiorno int = (select id from Giorni where giorno = @data);
					insert into OreLavorative(idGiorno, idCommessa, ore) values(@idGiorno, @idCommessa, @ore);
					if(@@Error >0)
						begin
							declare @oreDellaComm int = (select ore from OreLavorative where idGiorno=@idGiorno and idCommessa = @idCommessa);
							update OreLavorative set ore=@ore+@oreDellaComm where idGiorno=@idGiorno and idCommessa = @idCommessa;
						end
				end;
			else
				begin
					rollback transaction;
				end;
		end;
	else
		begin
			rollback transaction;
		end;
go