--search corso per id
create procedure SearchCorso
@IdCorso int
as
begin transaction;
begin try
	select * from Corsi where @IdCorso = id;
	commit transaction;
end try
begin catch  
	select   
        ERROR_NUMBER() AS ErrorNumber, 
		ERROR_MESSAGE() AS ErrorMessage;  
	rollback transaction;
end catch
go

create procedure Iscrizione
@IdCorso int,
@matr nvarchar(10)
as
begin transaction;
begin try
	insert into StudentiCorsi values (@IdCorso,@matr);
	commit transaction;
end try
begin catch  
	select   
        ERROR_NUMBER() AS ErrorNumber, 
		ERROR_MESSAGE() AS ErrorMessage;  
	rollback transaction;
end catch
go


