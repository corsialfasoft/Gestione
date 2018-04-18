--search corso per id
create procedure SearchCorso
@IdCorso int
as
begin transaction;
begin try
	select * from Corsi where @IdCorso = id;
end try
begin catch  
	select   
        ERROR_NUMBER() AS ErrorNumber, 
		ERROR_MESSAGE() AS ErrorMessage;  
	rollback transaction;
end catch
commit transaction;
go

create procedure Iscrizione
@IdCorso int,
@matr nvarchar(10)
as
begin transaction;
begin try
	insert into StudentiCorsi values (@IdCorso,@matr);
end try
begin catch  
	select   
        ERROR_NUMBER() AS ErrorNumber, 
		ERROR_MESSAGE() AS ErrorMessage;  
	rollback transaction;
end catch
commit transaction;
go

create procedure ListaLezioni
@IdCorso int
as
	select * from Lezioni l where @IdCorso = l.idCorsi;
go 

 exec ListaLezioni 1