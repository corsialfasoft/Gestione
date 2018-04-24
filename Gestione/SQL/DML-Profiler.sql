INSERT INTO ruoli(nome) VALUES('admin');
INSERT INTO ruoli(nome) VALUES('sconosciuto');
INSERT INTO ruoli(nome) VALUES('studente');
INSERT INTO ruoli(nome) VALUES('professore');

INSERT INTO sistemi(nome) VALUES('GeTime');
INSERT INTO sistemi(nome) VALUES('GeCo');
INSERT INTO sistemi(nome) VALUES('GeCv');

--Funzioni GeCo
INSERT INTO funzioni(nome,fksistema) VALUES('ElencoCorsi',2); --1
INSERT INTO funzioni(nome,fksistema) VALUES('RicercaCorso',2); --2
INSERT INTO funzioni(nome,fksistema) VALUES('VisualizzaCorso',2); --3
INSERT INTO funzioni(nome,fksistema) VALUES('AggiungiCorso',2); --4
INSERT INTO funzioni(nome,fksistema) VALUES('ElencoLezioni',2); --5
INSERT INTO funzioni(nome,fksistema) VALUES('AggiungiLezione',2); --6
INSERT INTO funzioni(nome,fksistema) VALUES('ModificaLezione',2); --7
INSERT INTO funzioni(nome,fksistema) VALUES('IscrivitiCorso',2); --8
--Funzioni GeTime
INSERT INTO funzioni(nome,fksistema) VALUES('VisuallaCommessa',1); --9
INSERT INTO funzioni(nome,fksistema) VALUES('VisualizzaMese',1); --10
INSERT INTO funzioni(nome,fksistema) VALUES('VisualizzaGiorno',1); --11
INSERT INTO funzioni(nome,fksistema) VALUES('AggiungiOre',1); --12
--Funzioni GeCV
INSERT INTO funzioni(nome,fksistema) VALUES('AggiungiCV',3); --13
INSERT INTO funzioni(nome,fksistema) VALUES('ModificaAnag',3); --14
INSERT INTO funzioni(nome,fksistema) VALUES('ModificaEsperienza',3); --15
INSERT INTO funzioni(nome,fksistema) VALUES('ModificaStudio',3); --16
INSERT INTO funzioni(nome,fksistema) VALUES('ModificaCompetenza',3); --17
INSERT INTO funzioni(nome,fksistema) VALUES('AggiungiEsperienza',3); --18
INSERT INTO funzioni(nome,fksistema) VALUES('AggiungiCompetenza',3); --19
INSERT INTO funzioni(nome,fksistema) VALUES('AggiungiStudio',3); --20
INSERT INTO funzioni(nome,fksistema) VALUES('EliminaCv',3); --21
INSERT INTO funzioni(nome,fksistema) VALUES('RicercaCv',3); --22
--Funzioni Admin
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,1);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,2);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,3);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,5);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,8);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,9);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,10);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,11);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,12);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,14);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,15);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,16);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,17);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,18);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,19);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,20);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,21);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(1,22);
--Funzioni Sconosciuto
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(2,1);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(2,2);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(2,3);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(2,13);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(2,14);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(2,15);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(2,16);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(2,17);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(2,18);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(2,19);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(2,20);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(2,21);
--Funzioni Studente
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(3,1);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(3,2);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(3,3);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(3,5);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(3,8);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(3,13);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(3,14);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(3,15);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(3,16);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(3,17);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(3,18);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(3,19);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(3,20);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(3,21);
--Funzioni Professore
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(4,1);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(4,2);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(4,3);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(4,4);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(4,5);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(4,6);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(4,7);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(4,9);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(4,10);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(4,11);
INSERT INTO funzioniAssociate(fkRuolo,fkFunzione) VALUES(4,12);


INSERT INTO utenti(matricola,nome,cognome,username,passwd,fkruolo) VALUES('admin','admin','admin','admin','admin',1);