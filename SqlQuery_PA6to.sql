CREATE DATABASE PA6to

USE [PA6to]
CREATE TABLE Datos(
ID varchar (20) NOT NULL,
Nombre varchar (20) NOT NULL,
ApPaterno varchar (20) NOT NULL,
ApMaterno varchar (20) NOT NULL,
Boleta numeric (15)NOT NULL,
TelUser numeric (15)NOT NULl)

USE [PA6to]
CREATE TABLE Administrador(
Usuario varchar (20) NOT NULL,
Contrase�a varchar (20) NOT NULL)

 USE [PA6to]
  INSERT INTO Administrador(Usuario,Contrase�a) VALUES ('2020131596', '2020131596')
    INSERT INTO Administrador(Usuario,Contrase�a) VALUES ('20201315057', '202013151057')

SELECT TOP (1000) [Usuario]
      ,[Contrase�a]
  FROM [PA6to].[dbo].[Administrador]

SELECT TOP (1000) [ID]
      ,[Nombre]
      ,[ApPaterno]
      ,[ApMaterno]
      ,[Boleta]
      ,[TelUser]
  FROM [PA6to].[dbo].[Datos]

USE [PA6to]
Delete FROM Datos

--CUESTIONARIOS
USE [PA6to]
CREATE TABLE TestAns(
No_Pregunta numeric (30) NOT NULL,
Pregunta varchar (1000)NOT NULl)

USE [PA6to]
CREATE TABLE TestDep(
No_Pregunta numeric (30) NOT NULL,
Pregunta varchar (1000)NOT NULl)

USE [PA6to]
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (1, 'Torpe o entumecido')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (2, 'Acalorado')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (3, 'Con temblor en las piernas')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (4, 'Incapaz de relajarse')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (5, 'Con temor a que ocurra lo peor')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (6, 'Mareado, o que se le va la cabeza')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (7, 'Con latidos del coraz�n fuertes y acelerados')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (8, 'Inestable')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (9, 'Atemorizado o asustado')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (10, 'Nervioso')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (11, 'Con sensaci�n de bloqueo')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (12, 'Con temblores en las manos')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (13, 'Inquieto, inseguro')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (14, 'Con miedo a perder el control')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (15, 'Con sensaci�n de ahogo')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (16, 'Con temor a morir')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (17, 'Con miedo')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (18, 'Con problemas digestivos')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (19, 'Con desvanecimientos')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (20, 'Con rubor facial')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (21, 'Con sudores, frios o calientes')

  USE [PA6to]
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (1, 'Tristeza')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (2, 'Pesimismo')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (3, 'Fracaso')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (4, 'P�rdidad de Placer')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (5, 'Sentimientos de Culpa')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (6, 'Sentimientos de Castigo')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (7, 'Disconformidad con uno mismo')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (8, 'Autocr�tica')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (9, 'Pensamientos o Deseos Suicidas')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (10, 'Llanto')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (11, 'Agitaci�n ')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (12, 'P�rdida de Inter�s')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (13, 'Indecisi�n')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (14, 'Desvalorizaci�n')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (15, 'P�rdida de Energ�a')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (16, 'Cambios en los H�bitos de Sue�o')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (17, 'Irritabilidad')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (18, 'Cambios en el Apetito')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (19, 'Dificultad en Concentraci�n')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (20, 'Cansancio o Fatiga')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (21, 'P�rdida de Inter�s en el Sexo')

	SELECT TOP (1000) [No_Pregunta]
      ,[Pregunta]
  FROM [PA6to].[dbo].[TestAns]

  SELECT TOP (1000) [No_Pregunta]
      ,[Pregunta]
  FROM [PA6to].[dbo].[TestDep]

  USE [PA6to]
Delete FROM TestAns
Delete FROM TestDep