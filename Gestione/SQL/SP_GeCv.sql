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
	@id int,
	@annoIMod int ,
	@annoFMod int ,
	@qualificaMod nvarchar(50),
	@descrMod nvarchar(200)
as
	update EspLav set AnnoI = @annoIMod , AnnoF= @annoFMod, Qualifica = @qualificaMod , Descrizione=@descrMod
		where IdEl = @id;
go

create procedure ModPerStud
	@id int,
	@annoIMod int , 
	@annoFMod int ,
	@titoloMod nvarchar(50),
	@descrMod nvarchar(200)
as
	update PercorsoStudi  set AnnoI = @annoIMod , AnnoF= @annoFMod, Titolo = @titoloMod , Descrizione=@descrMod
		where IdPs = @id;
go

create procedure ModComp
	@id int,
	@titoloMod nvarchar(50),
	@livMod int
as
	UPDATE Competenze set Tipo= @titoloMod , Livello= @livMod where IdCs=@id;
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
create PROCEDURE CercaParolaChiava
	@parola nvarchar(20)
AS
	SELECT DISTINCT C.Matricola 
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
	select c.IdCs,c.Livello,c.Tipo from Competenze c where c.IdCv=@idc;
go
Create procedure GetPerStudi
	@Matricola nvarchar(10)
as
	declare @idc int  ;
	set @idc = (select top 1 c.IdCv from Curriculum c where c.Matricola=@Matricola);
	select p.IdPs,p.AnnoI,p.AnnoF,p.Titolo,p.Descrizione from PercorsoStudi p where p.IdCv=@idc;
go
Create procedure GetEspLav
	@Matricola nvarchar(10)
as
	declare @idc int  ;
	set @idc = (select top 1 c.IdCv from Curriculum c where c.Matricola=@Matricola);
	select e.idEl, e.AnnoI,e.AnnoF,e.Qualifica,e.Descrizione from EspLav e where e.IdCv=@idc;
go

Create procedure DelComp
	@id int
as
	Delete Competenze where idCs=@id
go

Create procedure DelEspLav
	@id int
as
	Delete EspLav where idEl =@id
go

Alter procedure DelPerStud
	@id int
as
	delete PercorsoStudi from PercorsoStudi where IdPs = @id;
go

Create Procedure GetPercorso 
	@id int
as
	select ps.IdPs,ps.AnnoI,ps.AnnoF,ps.Titolo,ps.Descrizione from PercorsoStudi ps where ps.IdPs=@id;
go
Create Procedure GetEsperienza 
	@id int
as
	select el.IdEl,el.AnnoI,el.AnnoF,el.Qualifica, el.Descrizione from EspLav el where el.IdEl=@id;
go

Create Procedure GetCompetenza
	@id int
as
	select c.IdCs,c.Tipo, c.Livello from Competenze c where c.IdCs=@id;
go