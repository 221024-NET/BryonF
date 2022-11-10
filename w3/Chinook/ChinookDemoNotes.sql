--This is SQL
--This is for comments


--When you don't highlight a specific command before run/executing, everything in the current (open) SQL file runs.
--you can select a specific command, and only that command will run. 


SELECT * FROM Album;
--When you run a query, the results take the form of a table.

SELECT 'Hello World';
--Single quotes denote strings in SQL

SELECT 2 + 2

SELECT SYSUTCDATETIME()

--Usually, nothing in SQL is case sensitive
--Semi colons not required to end a line. They can be useful to denote the end of an operation/thought.
select 2+2; 

--When you make a query to a db, any result will take the form of a table. 
--SELECT (Columns) FROM (Table)
SELECT FirstName FROM CUSTOMER;



--When selecting multiple columns from a table, seperate them with a comma. 
SELECT FirstName, LastName FROM Customer;

SELECT FirstName + ' ' + LastName FROM Customer;

SELECT FirstName + ' ' + LastName AS Fullname FROM CUSTOMER;
SELECT FirstName + ' ' + LastName AS 'Full name' FROM CUSTOMER;
SELECT FirstName + ' ' + LastName AS [Fullname] FROM CUSTOMER;

SELECT * FROM Customer;


--Filtering with WHERE

SELECT * -- Get this/these columns 
FROM CUSTOMER -- From this table
WHERE LEN(FirstName) > 4; -- for entries that meet this condition.

SELECT *
FROM Customer
WHERE Country = 'Chile' AND City = 'Santiago'; -- You can use AND to chain multiple conditions together.

--Aggregate functions

SELECT COUNT(*)
FROM CUSTOMER;

SELECT SUM(Total)
FROM INVOICE;

SELECT MIN(Total)
FROM INVOICE;

--other agg functuions like min, max, average


SELECT CustomerId, SUM(Total)
FROM Invoice
GROUP BY CustomerId; -- Group by gives us a way to group the result of an agg function by entries 



--Here is an example of a complex query with multiple filters
SELECT CustomerId, SUM(Total) AS Sum_Total --Selects the customer ID AND the total sum of all invoices that belong to that customer, aliased as Sum_Total.
FROM INVOICE --Selects the above data, from the invoice table.
WHERE BillingCountry != 'USA' --Filters the data and only keeps data where the BillingCountry ISNT the USA.
GROUP BY CustomerId --Group the data being returned by the CustomerID
HAVING SUM(Total) > 40 --Another filter, this time only keep data where the sum of all a customers invoices total more than 40.
ORDER BY Sum_Total DESC, CustomerId; --Orders the returning data by the Sum_Total in descending order, and display the CustomerID next to that total. 

-- Logical order of operations is:
-- FROM
-- WHERE
-- GROUP BY
-- HAVING
-- SELECT
-- ORDER BY


--Joins
--Accessing multiple tables to return the desired data

--GET every album by artist 
SELECT Album.Title AS 'Album Title', Artist.Name AS 'Artist Name'
FROM Album LEFT JOIN Artist ON Album.ArtistId = Artist.ArtistId;

--Many types of joins exist.
--Cross Join
--Inner Joins
--Left/Right Joins 
--Outer Joins
--Etc

--It can be useful to visualize joins as venn diagrams. 
--https://www.w3schools.com/sql/sql_join.asp

-- Set Operations

--UNION
SELECT FirstName FROM Customer
UNION 
SELECT FirstName FROM Employee;
-- UNION returns all entries that are found on either query, without duplicates.
-- UNION ALL returns with duplicates


-- INTERSECT
SELECT FirstName FROM Customer
INTERSECT
SELECT FirstName FROM Employee;
-- INTERSENT returns entries that are in both queries


-- EXCEPT
SELECT FirstName FROM Customer
EXCEPT 
SELECT FirstName FROM Employee;
-- EXCEPT returns entries that match the first query, and are not matched in the second query.

SELECT FirstName FROM Employee
EXCEPT 
SELECT FirstName FROM Customer;






-- Sub-Query
-- its generally easier to express a complex query as two queries that work sequentially.

SELECT * FROM Album
SELECT * 
	FROM Artist 
	JOIN Album ON Artist.ArtistId = Album.ArtistId
	ORDER BY LEN(Title) DESC


SELECT * FROM Artist
	WHERE ArtistId = (
		SELECT ArtistId
			FROM Album
			WHERE LEN(Title) >= ALL(SELECT LEN(Title) FROM Album)
	)

SELECT * 
	FROM Track 
	WHERE TrackId NOT IN ( 
		SELECT TrackID 
		FROM InvoiceLine
	);

-- Queries operate from the most-nested to the least-nested
-- Sub-Query operators: IN, NOT IN, EXISTS, ANY, ALL



-- SQL Language "Families" - Sub-Languages
-- DQL - Data Query Language - How we phrase/form a query
-- DDL - Data Definition Language - how we define the database - think structurally like the tables and constraints
-- DML - Data Manipulation Language - how we modify the database - think the data in the structures


-- DDL - CREATE, ALTER, DROP

GO 
-- separate commands to different batches
-- the "batch" is the group of commands being sent for execution.


CREATE SCHEMA Pokemon;
-- Schemas are logical "namespaces" in SQL. "dbo" is the default, the one that it assumes if you don't give one.

GO

-- VERB NOUN NAME
CREATE TABLE Pokemon.Trainer
(
	-- name type constraints
	TrainerId INT NOT NULL PRIMARY KEY IDENTITY,
	-- UNIQUE can be used to enforce that every entry in that field is unique
	-- the PRIMARY KEY constraint includes both 'NOT NULL' and 'UNIQUE' constraints
	-- IDENTITY is the SQL Server or T-SQL auto increment functionality
	Name VARCHAR(255) NOT NULL CHECK(LEN(Name)>=5),
	-- CHECK (condition) ensures that the entry meets the condition, otherwise the entry is refused
	Age INT NOT NULL,
	DOB DATE NOT NULL DEFAULT GETDATE(),
	Badges INT NOT NULL DEFAULT 0
);
-- DROP TABLE Poken.Trainer;
GO

CREATE TABLE Pokemon.Pokemons
(
	PokemonId INT PRIMARY KEY IDENTITY,
	DexNumber INT NOT NULL,
	Name VARCHAR(255) NOT NULL,
	Type VARCHAR(32) NOT NULL,
	Health INT NOT NULL,
	Level INT DEFAULT 1,
	Exp INT DEFAULT 1
);
-- DROP TABLE Pokemon.Pokemons;
GO

CREATE TABLE Pokemon.Types
(
	TypeId INT PRIMARY KEY IDENTITY,
	Type VARCHAR(32) UNIQUE NOT NULL
);
-- ALTER TABLE Pokemon.Pokemons
--	DROP CONSTRAINT FK_Pokemons_TypeId;
-- DROP TABLE Pokemon.Types;
GO

ALTER TABLE Pokemon.Pokemons
	ALTER COLUMN Type INT;
	-- change the data type of the Type field to INT

ALTER Table Pokemon.Pokemons
	ADD CONSTRAINT FK_Pokemons_TypeId 
	FOREIGN KEY (Type)
	REFERENCES Pokemon.Types (TypeId);

GO

-- DML - Data Manipulation Language
-- INSERT, UPDATE, DELET, TRUNCATE

-- VERB NOUN
INSERT INTO Pokemon.Types (Type)
VALUES
	('Fire'),
	('Water'),
	('Ground'),
	('Air'),
	('Electric');

GO

SELECT * FROM Pokemon.Types;

DELETE FROM Pokemon.Types;
-- DELETE to remove the entry from a table - it leaves the structure of the table behind

UPDATE Pokemon.Types
	SET Type = 'Grass'
	WHERE TypeId = 6;
-- UPDATE to modify the values of an entry, or table.
-- Use a filter (like WHERE) to focus the update to specific entries
GO


SELECT * FROM Pokemon.Pokemons;

INSERT INTO Pokemon.Pokemons (DexNumber, Name, Type, Health, Level, Exp)
VALUES
	(1, 'Bulbasaur', 6, 25, 5, 4),
	(7, 'Squirtle', (SELECT TypeId FROM Pokemon.Types WHERE Type = 'Water'), 50, 4, 5);
GO



SELECT Pokemons.DexNumber, Pokemons.Name, Types.Type AS Pokes
	FROM Pokemon.Pokemons 
	JOIN Pokemon.Types ON Pokemons.Type = Types.TypeId
	WHERE Types.Type = 'Grass';