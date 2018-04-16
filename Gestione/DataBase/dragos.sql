create procedure SearchCorsi
	@descrizione nvarchar(20)
as 
	begin transaction
	begin try 
		select * from Corsi c where c.nome like '%'+@descrizione+'%' or c.descrizione like '%'+@descrizione+'%'
		commit transaction;
	end try
	begin catch
		select 
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage;
		rollback transaction;
	end catch
go


create procedure SearchCorsiStud
	@idStudente nvarchar (10),
	@descrizione nvarchar(20)
as 
	begin transaction
	begin try 
		select c.id,c.nome,c.descrizione,c.dInizio,c.dFine from Corsi c 
			inner join StudentiCorsi sc
				on c.id = sc.idCorsi
			inner join Studenti s
				on sc.idStudenti = s.matr
			where c.nome like '%'+@descrizione+'%' or c.descrizione like '%'+@descrizione+'%' and @idStudente=s.matr;
		commit transaction;
	end try
	begin catch
		select 
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage;
		rollback transaction;
	end catch
go
