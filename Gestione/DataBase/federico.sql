CREATE PROCEDURE AddCorso
 @nome NVARCHAR(15),
 @descrizione NVARCHAR(100),
 @dInizio NVARCHAR(15),
 @dFine NVARCHAR(15)
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
   SELECT IDENT_CURRENT('Corsi') 
go

CREATE PROCEDURE AddLezione
@idCorsi int,
@descrizione NVARCHAR(100),
@durata NVARCHAR(15)

as
BEGIN TRANSACTION
BEGIN TRY
INSERT INTO Lezioni(idCorsi,descrizione,durata)
				VALUES(@idCorsi,@descrizione,@durata)
END TRY
BEGIN CATCH
  SELECT 
		ERROR_NUMBER() AS ErrorNumber
		, ERROR_MESSAGE() AS ErrorMessage;
  ROLLBACK TRANSACTION;
END CATCH
		SELECT IDENT_CURRENT('Lezioni') 
go