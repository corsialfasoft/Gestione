create procedure SP_AddCommessa
	@nome nvarchar (50),
	@descrizione nvarchar (200),
	@capienza int
as
	begin try
		insert into dbo.Commesse(nome, descrizione, stimaOre) values (@nome, @descrizione, @capienza);
	end try
	begin catch
		if(@@ERROR = 2627)
			return 0
	end catch
	return 1
go

exec SP_AddCommessa 'Commessa', 'Per provare la SP', 9;