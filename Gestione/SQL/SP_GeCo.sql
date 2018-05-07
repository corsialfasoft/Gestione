--Alessandro
create procedure dbo.ListaCorsi
as
	select * from Corsi
go
create procedure dbo.ListaCorsiStudenti
@idStudente nvarchar(10)
as  
	select c.id, c.nome, c.descrizione, c.dInizio, c.dFine
	from Corsi c
	inner join StudentiCorsi sc on c.id = sc.idCorsi 
	inner join Studenti s on sc.idStudenti = s.matr 
	where s.matr = @idStudente 
go
--dragos
create procedure SearchCorsi
	@descrizione nvarchar(20)
as 
	select * from Corsi c where c.nome like '%'+@descrizione+'%' or c.descrizione like '%'+@descrizione+'%'
go
create procedure SearchCorsiStud
	@idStudente nvarchar (10),
	@descrizione nvarchar(20)
as 
	select c.id,c.nome,c.descrizione,c.dInizio,c.dFine 
	from Corsi c 
		inner join StudentiCorsi sc
			on c.id = sc.idCorsi
		inner join Studenti s
			on sc.idStudenti = s.matr
	where c.nome like '%'+@descrizione+'%' or c.descrizione like '%'+@descrizione+'%' and @idStudente=s.matr;
go
--federico
CREATE PROCEDURE AddCorso
 @nome NVARCHAR(15),
 @descrizione NVARCHAR(100),
 @dInizio DATE,
 @dFine DATE
as
	INSERT INTO Corsi(Nome,Descrizione,dInizio,dFine)
				VALUES(@nome,@descrizione,@dInizio,@dFine)
go
CREATE PROCEDURE AddLezione
@idCorsi int,
@nome nvarchar(200),
@descrizione NVARCHAR(100),
@durata NVARCHAR(15)
as
	INSERT INTO Lezioni(nome, durata, descrizione, idCorsi)
				VALUES(@nome,@durata,@descrizione,@idCorsi)
go
--matteo
create procedure SearchCorso
@IdCorso int
as
	select * from Corsi where @IdCorso = id;
go
create procedure Iscrizione
@IdCorso int,
@matr nvarchar(10)
as
	insert into StudentiCorsi values (@IdCorso,@matr);
go
create procedure ListaLezioni
@IdCorso int
as
	select * from Lezioni l where @IdCorso = l.idCorsi;
go 
-- Max
Create procedure ModLezione
	@idLezione int,
	@nome nvarchar(200),
	@descrizione nvarchar(50),
	@durata nvarchar(50)
as
	Update Lezioni 
	set nome=@nome,descrizione=@descrizione,durata=@durata
	where id=@idLezione;
go

create procedure ModificaCorso
@nomeN NVARCHAR(15),
@descrizioneN NVARCHAR(100),
@dInizioN DATE,
@dFineN DATE,
@IdCorso int
as
	update Corsi
	set nome=@nomeN, descrizione=@descrizioneN, dInizio=@dInizioN, dFine=@dFineN
	where id=@IdCorso;
go

create procedure CancellaCorso
@IdCorso int
as
	delete from Corsi where id= @IdCorso;
go

