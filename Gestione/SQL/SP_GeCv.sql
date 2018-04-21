CREATE PROCEDURE AddCv
    @Nome VARCHAR(50), 
  	@Cognome VARCHAR(50),
	@Eta Int, 
  	@Matricola VARCHAR(50), 
  	@Email VARCHAR(50),
	@Residenza VARCHAR(50), 
  	@Telefono VARCHAR(50)
as
	INSERT INTO Curriculum(Nome,Cognome ,Eta ,Matricola,Email,Residenza ,Telefono)
				VALUES(@Nome,@Cognome,@Eta,@Matricola,@Email,@Residenza,@Telefono) 
go
create PROCEDURE AddCvStudi
    @AnnoI Int, 
  	@AnnoF Int,
	@Titolo VARCHAR(50), 
  	@Descrizione VARCHAR(50), 
  	@MatrCv NVARCHAR(10)
as
	declare @IdControl int;
	set @IdControl = (select top 1 IdCv from Curriculum where Matricola = @MatrCv )
	if @IdControl is null
		begin
			print 'Warning! ID non trovato';
			THROW 51000,'Warning! ID non trovato',@IdControl;
		end
	 else 	
		begin
			print 'Warning! ID trovato';	
			INSERT INTO PercorsoStudi (AnnoI, AnnoF, Titolo, Descrizione, IdCv )
				VALUES(@AnnoI,@AnnoF,@Titolo,@Descrizione,@IdControl);				 
		end
go
create PROCEDURE AddEspLav
    @AnnoI Int, 
  	@AnnoF Int,
	@Qualifica NVARCHAR(50), 
  	@Descrizione NVARCHAR(50), 
  	@matr nvarchar(10)
as		
 	declare @IdControl int;
	set @IdControl = (select IdCv from Curriculum where Matricola = @matr )
	if @IdControl is null
		begin
			print 'Warning! ID non trovato';
			THROW 51000,'Warning! ID non trovato',@IdControl;			
		end
	 else 	
		begin
			INSERT INTO EspLav (AnnoI, AnnoF, Qualifica, Descrizione, IdCv) 
			VALUES (@AnnoI, @AnnoF, @Qualifica,@Descrizione,@IdControl);
		end
go
create PROCEDURE AddCompetenze
	@Tipo NVARCHAR(50),
    @Livello Int,
    @MatrCv nvarchar(10)
as
	declare @IdControl int;
	set @IdControl = (select IdCv from Curriculum where Matricola = @MatrCv )

	if @IdControl is null
		begin
			print 'Warning! ID non trovato';
			THROW 51000,'Warning! ID non trovato',@IdControl;			
		end
	 else 	
		begin
			print 'Warning! ID trovato';	
			INSERT INTO Competenze (Tipo, Livello, IdCv)
						VALUES (@Tipo,@Livello,@IdControl)					 
		end	
go
create procedure DeleteCurriculum
	@idcurr nvarchar(50)
as
	declare @test int;
	set @test = (select C.IdCv from Curriculum c where c.Matricola=@idcurr);
	if @test is null
		throw 66666 , 'id errato riprovare!' ,2;
	else
		begin
			delete Competenze from Competenze cs where cs.IdCv = @test;
			delete EspLav from EspLav es where es.IdCv=@test;
			delete PercorsoStudi from PercorsoStudi ps where ps.IdCv=@test;
			delete Curriculum from Curriculum c where c.IdCv=@test;
		end
go
create procedure CercaEtaMinMax
	@e_min int , 
	@e_max int 
as
	Select c.Matricola from Curriculum c  where c.Eta between @e_min and @e_max ;
go
Create procedure CercaCognome
	@cognome nvarchar(40)
as
	Select c.Matricola from Curriculum c where c.cognome= @cognome;
go
create procedure ModificaCurriculum
	@matricolaM nvarchar(10),
	@nomeM nvarchar(50),
	@cognomeM nvarchar(50),
	@etaM int,
	@emailM nvarchar(30),
	@residenzaM nvarchar(100),
	@telefonoM nvarchar(10)
as
	declare @test int;
	set @test = (select c.IdCv from Curriculum c where c.matricola = @matricolaM);
	if	@test is null
		throw 66666 , 'Matricola Errata!!!!!! RIPROVA' ,2;
	else
		begin 
		UPDATE Curriculum SET Nome= @nomeM , Cognome= @cognomeM , Eta= @etaM ,
				Email = @emailM , Residenza = @residenzaM , Telefono= @telefonoM 
				where IdCv = @test;
		end
go
Create procedure CercaEta
	@eta int
as
	Select c.Matricola from Curriculum c Where c.Eta=@eta
go
create procedure CercaCitta
	@citta nvarchar
as
	select c.IdCv from Curriculum c where c.residenza like '%'+@citta+'%';
go
create procedure CercaMatricola
	@matri nvarchar
as
	select c.IdCv From Curriculum c where c.matricola=@matri;
go
create procedure ModEspLav
	@matricola nvarchar(10),
	@annoIdaMod int, @annoFdaMod int,
	@qualificaDaMod nvarchar(50),
	@descrDaMod nvarchar(200),
	@annoIMod int , @annoFMod int ,
	@qualificaMod nvarchar(50),
	@descrMod nvarchar(200)
as
	declare @idcurr int;
	set @idcurr = (select c.IdCv from Curriculum c where c.matricola = @matricola);
	if @idcurr is null
		throw 66666 , 'Matricola Errata!!!!!! RIPROVA' ,2;
	else
		begin
		declare @idEsp int;
		set @idEsp = (select e.IdEl from EspLav e where e.IdCv= @idcurr and e.AnnoF=@annoFdaMod and
						e.AnnoI= @annoIdaMod and e.Qualifica= @qualificaDaMod and e.Descrizione= @descrDaMod );
		if @idEsp is null 
			throw 66666 , 'Id ESP LAV NON TROVATO Errata!!!!!! RIPROVA' ,2;
		else
			begin
			update EspLav set AnnoI = @annoIMod , AnnoF= @annoFMod, Qualifica = @qualificaMod , Descrizione=@descrMod
						where IdEl = @idEsp;
			end
		end
go
create procedure ModPerStud
	@matricola nvarchar(10),
	@annoIdaMod int, @annoFdaMod int,
	@titoloDaMod nvarchar(50),
	@descrDaMod nvarchar(200),
	@annoIMod int , @annoFMod int ,
	@titoloMod nvarchar(50),
	@descrMod nvarchar(200)
as
	declare @idcurr int;
	set @idcurr = (select c.IdCv from Curriculum c where c.matricola = @matricola);
	if @idcurr is null
		throw 66666 , 'Matricola Errata!!!!!! RIPROVA' ,2;
	else
		begin
		declare @idpers int;
		set @idpers = (select e.IdPs from PercorsoStudi e where e.IdCv= @idcurr and e.AnnoF=@annoFdaMod and
						e.AnnoI= @annoIdaMod and e.Titolo= @titoloDaMod and e.Descrizione= @descrDaMod );
		if @idpers is null 
			throw 66666 , 'Id PERCORSO STUDI NON TROVATO Errata!!!!!! RIPROVA' ,2;
		else
			begin
			update PercorsoStudi  set AnnoI = @annoIMod , AnnoF= @annoFMod, Titolo = @titoloMod , Descrizione=@descrMod
						where IdPs = @idpers;
			end
		end
go
create procedure ModComp
	@matricola nvarchar(10),
	@titoloDaMod nvarchar(50),
	@livDaMod int , 
	@titoloMod nvarchar(50),
	@livMod int
as
	declare @idcurr int;
	set @idcurr = (select c.IdCv from Curriculum c where c.matricola = @matricola);
	if @idcurr is null
		throw 66666 , 'Matricola Errata!!!!!! RIPROVA' ,2;
	else
		begin 
			declare @idComp int;
			set @idComp = (select c.IdCs from Competenze c where c.Tipo= @titoloDaMod and c.Livello=@livDaMod and c.IdCv=@idcurr);
			if @idComp is null
				throw 66666 , 'Competenza Errata!!!!!! RIPROVA' ,2;
			else 
				begin
					UPDATE Competenze set Tipo= @titoloMod , Livello= @livMod where IdCs=@idComp;
				end
			end
go
create procedure ModificaCV
	@nome nvarchar(50),
	@cognome nvarchar(50),
	@eta int,
	@matr nvarchar(10),
	@email nvarchar(30),
	@residenza nvarchar(100),
	@telefono nvarchar(10)
as
	update Curriculum  set Nome=@nome,Cognome=@cognome,Eta=@eta,Email=@email,Residenza=@residenza,Telefono=@telefono
		where Matricola=@matr;
go
CREATE PROCEDURE RecuperaIdCv
	@Matricola nvarchar(10)
AS
	SELECT IdCv FROM Curriculum WHERE Matricola=@Matricola;
GO
alter PROCEDURE CercaParolaChiava
	@parola nvarchar(20)
AS
	SELECT C.Matricola 
	FROM Curriculum C
		left JOIN PercorsoStudi PS ON C.IdCv = PS.IdCv 
		left JOIN EspLav EL ON C.IdCv = EL.IdCv 
		left JOIN Competenze CS ON C.IdCv = CS.IdCv 
	WHERE C.Nome like '%'+ @parola +'%'
		OR C.Cognome like '%'+ @parola +'%'
		OR C.email like '%'+ @parola +'%'
		OR C.Residenza like '%'+ @parola +'%'
		OR PS.Titolo like '%'+ @parola +'%'
		OR PS.Descrizione like '%'+ @parola +'%'
		OR EL.Qualifica like '%'+ @parola +'%'
		OR EL.Descrizione like '%'+ @parola +'%'
		OR CS.Tipo like '%'+ @parola +'%'
GO
CREATE PROCEDURE CercaLingua
	@competenza nvarchar(20)
AS
	SELECT C.IdCv 
	FROM Curriculum C INNER JOIN Competenze CS ON C.IdCv = CS.IdCv
	WHERE CS.Tipo like '%'+ @competenza +'%'
GO
Create Procedure GetCV
	@Matricola nvarchar(10)
as
	select top 1 c.nome,c.cognome,c.eta,c.matricola,c.email,c.residenza,c.telefono
	from Curriculum c where c.Matricola=@Matricola;
go
Create Procedure GetComp
	@Matricola nvarchar(10)
as
	declare @idc int  ;
	set @idc = (select top 1 c.IdCv from Curriculum c where c.Matricola=@Matricola);
	select c.Livello,c.Tipo from Competenze c where c.IdCv=@idc;
go
Create procedure GetPerStudi
	@Matricola nvarchar(10)
as
	declare @idc int  ;
	set @idc = (select top 1 c.IdCv from Curriculum c where c.Matricola=@Matricola);
	select p.AnnoI,p.AnnoF,p.Titolo,p.Descrizione from PercorsoStudi p where p.IdCv=@idc;
go
Create procedure GetEspLav
	@Matricola nvarchar(10)
as
	declare @idc int  ;
	set @idc = (select top 1 c.IdCv from Curriculum c where c.Matricola=@Matricola);
	select e.AnnoI,e.AnnoF,e.Qualifica,e.Descrizione from EspLav e where e.IdCv=@idc;
go
