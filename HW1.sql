USE AdventureWorks2019;
GO
--1.Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, with no filter
SELECT p.ProductID, p.Name, p.Color, p.ListPrice
FROM Production.Product AS p;

--2.Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, excludes the rows that ListPrice is 0
SELECT p.ProductID, p.Name, p.Color, p.ListPrice
FROM Production.Product AS p
where p.ListPrice != 0;


--3.Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, the rows that are NULL for the Color column
SELECT p.ProductID, p.Name, p.Color, p.ListPrice
FROM Production.Product AS p
where p.Color is null;


--4.Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, the rows that are not NULL for the Color column.
SELECT p.ProductID, p.Name, p.Color, p.ListPrice
FROM Production.Product AS p
where p.Color is not null;


--5.Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, the rows that are not NULL for the column Color, 
--and the column ListPrice has a value greater than zero
SELECT p.ProductID, p.Name, p.Color, p.ListPrice
FROM Production.Product AS p
where p.Color is not null and p.ListPrice > 0;

--6.Write a query that concatenates the columns Name and Color like 'LL Crankarm: Black' from the Production.Product table by excluding the rows that are null for color.
SELECT  p.Name + ': '+ p.Color as ProductNameAndColor
FROM Production.Product AS p
where p.Color is not null ;


--7.Write a query that generates the following result set  from Production.Product:
--<single column>
--NAME: LL Crankarm -- COLOR: Black
--NAME: ML Crankarm -- COLOR: Black
--NAME: HL Crankarm -- COLOR: Black
--NAME: Chainring Bolts -- COLOR: Silver
--NAME: Chainring Nut -- COLOR: Silver
--NAME: Chainring -- COLOR: Black
--NAME: Freewheel -- COLOR: Silver
--NAME: Front Derailleur Cage -- COLOR: Silver
--...
SELECT  'NAME: '+p.Name + '-- COLOR: '+ p.Color as ProductNameAndColor
FROM Production.Product AS p
where p.Color is not null ;


--8.Write a query to retrieve the columns ProductID and Name from the Production.Product table filtered by ProductID from 400 to 500
SELECT  p.ProductID, p.Name
FROM Production.Product AS p
where 400 <= p.ProductID and p.ProductID <= 500 ;


--9.Write a query to retrieve the to the columns  ProductID, Name and color from the Production.Product table restricted to the colors black and blue



--10.Write a query to get a result set on products that begins with the letter S. 



--11.Write a query that retrieves the columns Name and ListPrice from the Production.Product table. 
--Order the result set by the Name column.


--12. Write a query that retrieves the columns Name and ListPrice from the Production.Product table. 
--Order the result set by the Name column. The products name should start with either 'A' or 'S'


--13.Write a query so you retrieve rows that have a Name that begins with the letters SPO, but is then not followed by the letter K. 
--After this zero or more letters can exists. Order the result set by the Name column.


--14.Write a query that retrieves unique colors from the table Production.Product. Order the results in descending  manner


--15.Write a query that retrieves the unique combination of columns ProductSubcategoryID and Color from the Production.Product table. 
--We do not want any rows that are NULL.in any of the two columns in the result.