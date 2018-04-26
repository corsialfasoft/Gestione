create procedure GetProfile
	@usr varchar(50),
	@pass varchar(50)
as
	SELECT utenti.matricola,utenti.nome,utenti.cognome FROM utenti;
go

create procedure GetFunzioni
	@usr varchar(50),
	@pass varchar(50)
as
	declare @fkRuolo int;
	set @fkRuolo = (SELECT utenti.fkruolo FROM utenti WHERE username = @usr and passwd = @pass);
	if @fkRuolo is null
		begin
			print 'Warning! Ruolo non trovato';
			throw 51005,'Warning! Ruolo non trovato',@fkRuolo;
		end
	 else 	
		begin
			SELECT funzioni.nome FROM funzioni
			INNER JOIN funzioniAssociate on funzioni.codice = funzioniAssociate.fkFunzione
			where fkRuolo = @fkRuolo;
		end
go
create procedure IscrizioneAlPortale
@nome varchar(50),
@cognome varchar(50),
@usr varchar(50),
@psw varchar(50)
as
	declare @userTrovato varchar(50);
	set @userTrovato = (select top 1 utenti.username from utenti where @usr = utenti.username);
	if(@userTrovato != '')
		throw 100739, 'user già presente!', 44;
	else
		insert into utenti(nome, cognome,username,passwd,fkruolo) values (@nome, @cognome, @usr, @psw,2);
go

exec	IscrizioneAlPortale 'ciao', 'ciao', 'provaiiii', 'ciao'