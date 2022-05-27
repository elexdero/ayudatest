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
Contraseña varchar (20) NOT NULL)

 USE [PA6to]
  INSERT INTO Administrador(Usuario,Contraseña) VALUES ('2020131596', '2020131596')
    INSERT INTO Administrador(Usuario,Contraseña) VALUES ('20201315057', '202013151057')

SELECT TOP (1000) [Usuario]
      ,[Contraseña]
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
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (7, 'Con latidos del corazón fuertes y acelerados')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (8, 'Inestable')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (9, 'Atemorizado o asustado')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (10, 'Nervioso')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (11, 'Con sensación de bloqueo')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (12, 'Con temblores en las manos')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (13, 'Inquieto, inseguro')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (14, 'Con miedo a perder el control')
  INSERT INTO TestAns(No_Pregunta,Pregunta) VALUES (15, 'Con sensación de ahogo')
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
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (4, 'Pérdidad de Placer')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (5, 'Sentimientos de Culpa')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (6, 'Sentimientos de Castigo')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (7, 'Disconformidad con uno mismo')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (8, 'Autocrítica')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (9, 'Pensamientos o Deseos Suicidas')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (10, 'Llanto')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (11, 'Agitación ')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (12, 'Pérdida de Interés')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (13, 'Indecisión')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (14, 'Desvalorización')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (15, 'Pérdida de Energía')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (16, 'Cambios en los Hábitos de Sueño')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (17, 'Irritabilidad')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (18, 'Cambios en el Apetito')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (19, 'Dificultad en Concentración')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (20, 'Cansancio o Fatiga')
  INSERT INTO TestDep(No_Pregunta,Pregunta) VALUES (21, 'Pérdida de Interés en el Sexo')

	SELECT TOP (1000) [No_Pregunta]
      ,[Pregunta]
  FROM [PA6to].[dbo].[TestAns]

  SELECT TOP (1000) [No_Pregunta]
      ,[Pregunta]
  FROM [PA6to].[dbo].[TestDep]

  USE [PA6to]
Delete FROM TestAns
Delete FROM TestDep