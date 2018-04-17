create procedure CreateMatricola
	@Nome varchar(20),
	@Cognome varchar(20),
	@Users varchar(20),
	@Passwd nvarchar(20),
	@fkTipo int
as
	declare @randomString varchar(6);
	SELECT @randomString = CONVERT(varchar(255), NEWID()); --Guid
	INSERT INTO Utente(Matricola,Nome,Cognome,Users,Passwd,fkTipo) VALUES(@randomString,@Nome,@Cognome,@Users,@Passwd,@fkTipo);
go

use Profilatura;