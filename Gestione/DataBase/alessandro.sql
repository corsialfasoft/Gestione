create procedure dbo.ListaCorsi
as
BEGIN TRANSACTION   
	BEGIN TRY  
		select * from Corsi
	COMMIT TRANSACTION;  
 END TRY  
	BEGIN CATCH  
		select
			ERROR_NUMBER() as ErrorNuber,
			ERROR_MESSAGE() as ErrorMessage;
		rollback transaction
	end catch
go

exec dbo.ListaCorsi;
drop procedure dbo.ListaCorsi
drop procedure  dbo.ListaCorsiStudendi

create procedure dbo.ListaCorsiStudendi
@idStudente nvarchar(10)
as
   BEGIN TRANSACTION   
		BEGIN TRY  
		select c.nome, c.descrizione, c.dInizio, c.dFine
		from Corsi c
		inner join StudentiCorsi sc on c.id = sc.idCorsi 
		inner join Studenti s on sc.idStudenti = s.matr 
		where s.matr = @idStudente
 COMMIT TRANSACTION;  
 END TRY  
BEGIN CATCH  
	select
		ERROR_NUMBER() as ErrorNuber,
		ERROR_MESSAGE() as ErrorMessage;
		rollback transaction
end catch
go

exec dbo.ListaCorsiStudendi 'wqw'
select * from studenti
insert into studenti (nome,cognome, matr) values ('pollo','scotto','aqwe')
select * from Corsi
select * from StudentiCorsi
insert into Corsi (nome, descrizione, dInizio, dFine) values('C#','programmatore c#', '22/01/2000', '01/03/2000')