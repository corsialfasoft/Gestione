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