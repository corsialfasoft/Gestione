﻿CREATE PROCEDURE AddCv
    @Nome VARCHAR(50), 
  	@Cognome VARCHAR(50),
	@Eta Int, 
  	@Matricola VARCHAR(50), 
  	@Email VARCHAR(50),
	@Residenza VARCHAR(50), 
  	@Telefono VARCHAR(50)
	as
    SET IMPLICIT_TRANSACTIONS ON;
	INSERT INTO Curriculum(Nome,Cognome ,Eta ,Matricola,Email,Residenza ,Telefono)
				VALUES(@Nome,@Cognome,@Eta,@Matricola,@Email,@Residenza,@Telefono)
    COMMIT TRANSACTION 
	go

alter PROCEDURE AddCvStudi
    @AnnoI Int, 
  	@AnnoF Int,
	@Titolo VARCHAR(50), 
  	@Descrizione VARCHAR(50), 
  	@MatrCv NVARCHAR(10)
	
	as
    SET IMPLICIT_TRANSACTIONS ON;
	
	declare @IdControl int;
	set @IdControl = (select top 1 IdCv from Curriculum where Matricola = @MatrCv )

	if @IdControl is null
		begin
			print 'Warning! ID non trovato';
            ROLLBACK TRANSACTION;
			THROW 51000,'Warning! ID non trovato',@IdControl;
			
		end
	 else 	
		begin
			print 'Warning! ID trovato';	
			INSERT INTO PercorsoStudi (AnnoI, AnnoF, Titolo, Descrizione, IdCv )
				VALUES(@AnnoI,@AnnoF,@Titolo,@Descrizione,@IdControl);				 
		end
		COMMIT TRANSACTION 
	go
	


alter PROCEDURE AddEspLav
    @AnnoI Int, 
  	@AnnoF Int,
	@Qualifica NVARCHAR(50), 
  	@Descrizione NVARCHAR(50), 
  	@matr nvarchar(10)
as
	begin transaction ;			
 	declare @IdControl int;
	set @IdControl = (select IdCv from Curriculum where Matricola = @matr )

	if @IdControl is null
		begin
			print 'Warning! ID non trovato';
            ROLLBACK TRANSACTION;
			THROW 51000,'Warning! ID non trovato',@IdControl;			
		end
	 else 	
		begin
			INSERT INTO EspLav (AnnoI, AnnoF, Qualifica, Descrizione, IdCv) 
			VALUES (@AnnoI, @AnnoF, @Qualifica,@Descrizione,@IdControl);
		end
		COMMIT TRANSACTION 
	go

alter PROCEDURE AddCompetenze
	@Tipo NVARCHAR(50),
    @Livello Int,
    @MatrCv nvarchar(10)
as
   SET IMPLICIT_TRANSACTIONS ON;
					
	declare @IdControl int;
	set @IdControl = (select IdCv from Curriculum where Matricola = @MatrCv )

	if @IdControl is null
		begin
			print 'Warning! ID non trovato';
            ROLLBACK TRANSACTION;
			THROW 51000,'Warning! ID non trovato',@IdControl;
			
		end
	 else 	
		begin
			print 'Warning! ID trovato';	
			INSERT INTO Competenze (Tipo, Livello, IdCv)
						VALUES (@Tipo,@Livello,@IdControl)

					 
		end
		COMMIT TRANSACTION 
	
	go
