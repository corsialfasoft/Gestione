select * from Curriculum

Insert into Curriculum (nome,Cognome,Eta,Matricola,email,telefono,residenza) values ('Pino','Panino',22,'AAAA','adsf@dfads.it',302446500,'Casa Mia')
Insert into Curriculum (nome,Cognome,Eta,Matricola,email,telefono,residenza) values ('Pino','Panino',22,'BBBB','adsf@dfads.it',302446500,'Casa Mia')
Insert into Curriculum (nome,Cognome,Eta,Matricola,email,telefono,residenza) values ('Pino','Panino',22,'CCCC','adsf@dfads.it',302446500,'Casa Mia')
Insert into Curriculum (nome,Cognome,Eta,Matricola,email,telefono,residenza) values ('Pino','Panino',22,'DDDD','adsf@dfads.it',302446500,'Casa Mia')
Insert into Curriculum (nome,Cognome,Eta,Matricola,email,telefono,residenza) values ('Pino','Panino',22,'EEEE','adsf@dfads.it',302446500,'Casa Mia')
Insert into Curriculum (nome,Cognome,Eta,Matricola,email,telefono,residenza) values ('Pino','Franzoso',23,'FFFF','adsf@dfads.it',302446500,'Casa Mia')
Insert into Curriculum (nome,Cognome,Eta,Matricola,email,telefono,residenza) values ('Pino','Urdue',29,'GGGGG','adsf@dfads.it',302446500,'Casa Mia')

sELECT * FROM EspLav

Insert into EspLav (DESCRIZIONE,idCV) values ('binzinaro',1)
Insert into EspLav (DESCRIZIONE,idCV) values ('panettiere',2)
Insert into EspLav (DESCRIZIONE,idCV) values ('parcheggiatore',3)
Insert into EspLav (DESCRIZIONE,idCV) values ('birdwatcher',4)
Insert into EspLav (DESCRIZIONE,idCV) values ('bagnigno',5)

Select * from Competenze
Insert into Competenze (tipo,IdCv) values ('Primo tipo',1)
Insert into Competenze (tipo,IdCv) values ('Secondo tipo',2)
Insert into Competenze (tipo,IdCv) values ('Terzo tipo',3)
Insert into Competenze (tipo,IdCv) values ('Quarto tipo',4)
Insert into Competenze (tipo,IdCv) values ('Quinto tipo',5)

Select * from PercorsoStudi

iNSERT INTO PercorsoStudi (Descrizione,IdCv) values ('Primina',1)
iNSERT INTO PercorsoStudi (Descrizione,IdCv) values ('Seconda',2)
iNSERT INTO PercorsoStudi (Descrizione,IdCv) values ('Terza',3)
iNSERT INTO PercorsoStudi (Descrizione,IdCv) values ('Quarta',4)
iNSERT INTO PercorsoStudi (Descrizione,IdCv) values ('Quinta',5)

sELECT * FROM Curriculum c inner join EspLav el on c.IdCv=el.IdCv
							inner join Competenze co on c.IdCv =co.IdCv
							inner join PercorsoStudi ps on c.IdCv=ps.IdCv
							where c.Matricola = 'BBBB'