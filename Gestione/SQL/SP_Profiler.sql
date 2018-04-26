create procedure GetProfile
as
	SELECT utenti.matricola,utenti.nome,utenti.cognome,funzioni.nome FROM utenti
	INNER JOIN ruoli on utenti.fkruolo = codice.ruoli INNER JOIN funzioni on funzioni.codice = funzioniAssociate.fkFunzione
	INNER JOIN funzioniAssociate on funzioniAssociate.fkRuolo = ruoli.codice INNER JOIN sistemi on sistemi.codice = funzioni.fksistema
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