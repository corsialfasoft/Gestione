--Alessandro
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
create procedure dbo.ListaCorsiStudenti
@idStudente nvarchar(10)
as
   BEGIN TRANSACTION   
		BEGIN TRY  
		select c.id, c.nome, c.descrizione, c.dInizio, c.dFine
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
--dragos
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
--federico
CREATE PROCEDURE AddCorso
 @nome NVARCHAR(15),
 @descrizione NVARCHAR(100),
 @dInizio DATE,
 @dFine DATE
as
BEGIN TRANSACTION
BEGIN TRY
INSERT INTO Corsi(Nome,Descrizione,dInizio,dFine)
				VALUES(@nome,@descrizione,@dInizio,@dFine)
END TRY
BEGIN CATCH
  SELECT 
		ERROR_NUMBER() AS ErrorNumber
		, ERROR_MESSAGE() AS ErrorMessage;
  ROLLBACK TRANSACTION;
END CATCH  
COMMIT TRANSACTION
go
CREATE PROCEDURE AddLezione
@idCorsi int,
@nome nvarchar(200),
@descrizione NVARCHAR(100),
@durata NVARCHAR(15)
as
BEGIN TRANSACTION
BEGIN TRY
INSERT INTO Lezioni(nome, durata, descrizione, idCorsi)
				VALUES(@nome,@durata,@descrizione,@idCorsi)
END TRY
BEGIN CATCH
  SELECT 
		ERROR_NUMBER() AS ErrorNumber
		, ERROR_MESSAGE() AS ErrorMessage;
  ROLLBACK TRANSACTION;
END CATCH		 
COMMIT TRANSACTION
go
--matteo
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

