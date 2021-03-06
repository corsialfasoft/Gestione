﻿SELECT * FROM Curriculum
INSERT INTO Curriculum(nome,Cognome,Eta,Matricola,Email,Residenza,Telefono) VALUES('Nauman','Aziz',21,'AAAA','nauman@aziz.com','Via Aziz 1','111111')
INSERT INTO Curriculum(nome,Cognome,Eta,Matricola,Email,Residenza,Telefono) VALUES('Edoardo','PogaTanto',26,'BBBB','edo@pogo.com','Via Mano 2','22222')
INSERT INTO Curriculum(nome,Cognome,Eta,Matricola,Email,Residenza,Telefono) VALUES('Massimo','Franzoso',22,'CCCC','max@franzoso.com','Via Moto 3','333333')
INSERT INTO Curriculum(nome,Cognome,Eta,Matricola,Email,Residenza,Telefono) VALUES('Luca','Gentilesca',25,'DDDD','luca@gent.com','Via Cuneo 4','444444')
INSERT INTO Curriculum(nome,Cognome,Eta,Matricola,Email,Residenza,Telefono) VALUES('Dragos','Brinzila',23,'EEEE','dra@brin.com','Via Moldavia 5','55555')
INSERT INTO Curriculum(nome,Cognome,Eta,Matricola,Email,Residenza,Telefono) VALUES('Federico','Marras',21,'FFFF','fede@marras.com','Via Mito 6','66666')
INSERT INTO Curriculum(nome,Cognome,Eta,Matricola,Email,Residenza,Telefono) VALUES('Florin','Gheliuc',23,'GGGG','flo@gheliuc.com','Via Romagna 7','77777')
INSERT INTO Curriculum(nome,Cognome,Eta,Matricola,Email,Residenza,Telefono) VALUES('Carmen','Capobianco',34,'HHHH','Carmen@capob.com','Via Kinder 8','888888')
INSERT INTO Curriculum(nome,Cognome,Eta,Matricola,Email,Residenza,Telefono) VALUES('Ale','Siculo',20,'IIII','ale@siculo.com','Via Sicily 9','99999')
INSERT INTO Curriculum(nome,Cognome,Eta,Matricola,Email,Residenza,Telefono) VALUES('Matteo','Tani',22,'JJJJ','matteo@tani.com','Via Matrix 10','101010')
INSERT INTO Curriculum(nome,Cognome,Eta,Matricola,Email,Residenza,Telefono) VALUES('Alessandro','Qwerty',23,'KKKK','alessandro@qwerty.com','Via MSI 11','1121212')

SELECT * FROM EspLav
INSERT INTO EspLav(AnnoI,AnnoF,DESCRIZIONE,Qualifica,idCV) VALUES(2010,2015,'Assitense socio sanitario','Diploma',1)
INSERT INTO EspLav(AnnoI,AnnoF,DESCRIZIONE,Qualifica,idCV) VALUES(2004,2009,'Liceo Scientifico','Diploma',2)
INSERT INTO EspLav(AnnoI,AnnoF,DESCRIZIONE,Qualifica,idCV) VALUES(2011,2016,'Scientifico tecnologico','Diploma',3)
INSERT INTO EspLav(AnnoI,AnnoF,DESCRIZIONE,Qualifica,idCV) VALUES(2008,2013,'Geometra','Diploma',4)
INSERT INTO EspLav(AnnoI,AnnoF,DESCRIZIONE,Qualifica,idCV) VALUES(2011,2016,'Ragioniere Programmatore','Diploma',5)
INSERT INTO EspLav(AnnoI,AnnoF,DESCRIZIONE,Qualifica,idCV) VALUES(2012,2017,'Scientifico tecnologico','Diploma',6)
INSERT INTO EspLav(AnnoI,AnnoF,DESCRIZIONE,Qualifica,idCV) VALUES(2010,2016,'Scientifico tecnologico','Diploma',7)
INSERT INTO EspLav(AnnoI,AnnoF,DESCRIZIONE,Qualifica,idCV) VALUES(2002,2007,'Scientifico','Diploma',8)
INSERT INTO EspLav(AnnoI,AnnoF,DESCRIZIONE,Qualifica,idCV) VALUES(2012,2017,'Ragioniere Programmatore','Diploma',9)
INSERT INTO EspLav(AnnoI,AnnoF,DESCRIZIONE,Qualifica,idCV) VALUES(2010,2015,'Scientifico Matematico','Diploma',10)
INSERT INTO EspLav(AnnoI,AnnoF,DESCRIZIONE,Qualifica,idCV) VALUES(2010,2017,'Designer','Diploma',11)

SELECT * FROM Competenze
INSERT INTO Competenze(tipo,Livello,IdCv) VALUES('Java',10,1)
INSERT INTO Competenze(tipo,Livello,IdCv) VALUES('Pogo',8,2)
INSERT INTO Competenze(tipo,Livello,IdCv) VALUES('Mangiare',9,3)
INSERT INTO Competenze(tipo,Livello,IdCv) VALUES('Cunese',10,4)
INSERT INTO Competenze(tipo,Livello,IdCv) VALUES('Boh',7,5)
INSERT INTO Competenze(tipo,Livello,IdCv) VALUES('Camicia Bianca',8,6)
INSERT INTO Competenze(tipo,Livello,IdCv) VALUES('Albanese',4,7)
INSERT INTO Competenze(tipo,Livello,IdCv) VALUES('DataBase',10,8)
INSERT INTO Competenze(tipo,Livello,IdCv) VALUES('Siculo',10,9)
INSERT INTO Competenze(tipo,Livello,IdCv) VALUES('Matrici',8,10)
INSERT INTO Competenze(tipo,Livello,IdCv) VALUES('Puntualita',6,11)

SELECT * FROM PercorsoStudi
INSERT INTO PercorsoStudi(AnnoI,AnnoF,Titolo,Descrizione,IdCv) VALUES(2013,2014,'Rosario','Ciao amico prendi rosa',1)
INSERT INTO PercorsoStudi(AnnoI,AnnoF,Titolo,Descrizione,IdCv) VALUES(2000,2018,'Pogataro','Ho pogato un sacco',2)
INSERT INTO PercorsoStudi(AnnoI,AnnoF,Titolo,Descrizione,IdCv) VALUES(2011,2014,'Motociclista','Una volta ho fatto un incidente',3)
INSERT INTO PercorsoStudi(AnnoI,AnnoF,Titolo,Descrizione,IdCv) VALUES(2011,2012,'Motociclista','Uso la moto solo a cuneo',4)
INSERT INTO PercorsoStudi(AnnoI,AnnoF,Titolo,Descrizione,IdCv) VALUES(2009,2017,'Niente','Non sapevo cosa scrivere',5)
INSERT INTO PercorsoStudi(AnnoI,AnnoF,Titolo,Descrizione,IdCv) VALUES(2013,2014,'Motociclista','Ho il motorino',6)
INSERT INTO PercorsoStudi(AnnoI,AnnoF,Titolo,Descrizione,IdCv) VALUES(2010,2016,'Giocatore','Forza Giaguari',7)
INSERT INTO PercorsoStudi(AnnoI,AnnoF,Titolo,Descrizione,IdCv) VALUES(2003,2017,'Impiegata','L azienda è fallita',8)
INSERT INTO PercorsoStudi(AnnoI,AnnoF,Titolo,Descrizione,IdCv) VALUES(2016,2018,'Programmatore','Ho fatto tutte cose',9)
INSERT INTO PercorsoStudi(AnnoI,AnnoF,Titolo,Descrizione,IdCv) VALUES(2013,2018,'Matematico','Amo le matrici',10)
INSERT INTO PercorsoStudi(AnnoI,AnnoF,Titolo,Descrizione,IdCv) VALUES(2013,2014,'CollezionaDiplomi','Ho gia lavorato',11)