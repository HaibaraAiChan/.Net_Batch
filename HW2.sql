USE AdventureWorks2019;
GO
--1.How many products can you find in the Production.Product table?
select count(distinct ProductID) as NumberOfProducts
from Production.Product;

--2.Write a query that retrieves the number of products in the Production.Product table that are included in a subcategory. 
--The rows that have NULL in column ProductSubcategoryID are considered to not be a part of any subcategory.
--select top 3 * from Production.Product
select count(distinct ProductID) as NumberOfProducts
from Production.Product as p
where p.ProductSubcategoryID is not null;


--3.Count how many products belong to each product subcategory.
	--Write a query that displays the result with two columns:
	--ProductSubcategoryID (the subcategory ID)， CountedProducts (the number of products in that subcategory).
select p.ProductSubcategoryID, count(distinct p.ProductID) as CountedProducts
from Production.Product as p
where p.ProductSubcategoryID is not null
group by p.ProductSubcategoryID;


--4.How many products that do not have a product subcategory.
select p.ProductSubcategoryID, count(distinct p.ProductID) as CountedProducts
from Production.Product as p
where p.ProductSubcategoryID is  null
group by p.ProductSubcategoryID;


--5.Write a query to list the sum of products quantity in the Production.ProductInventory table.
--select p.quantity 
--from Production.ProductInventory as p;
select sum(p.quantity ) as sum_of_total_quantity
from Production.ProductInventory as p;



--6.Write a query to list the sum of products in the Production.ProductInventory table 
--and LocationID set to 40 and limit the result to include just summarized quantities less than 100.
select sum(p.quantity)  as LocationID_40_sum_of_total_quantity
from Production.ProductInventory as p
where p.LocationID = 40 and p.quantity < 100;



--7. Write a query to list the sum of products with the shelf information in the Production.ProductInventory 
--table and LocationID set to 40 and limit the result to include just summarized quantities less than 100
--select * 
--from Production.ProductInventory ;

--select Shelf, quantity as sum_of_products_with_shelf
--from Production.ProductInventory 
--where shelf is not null  and LocationID = 40;

--select Shelf, quantity as sum_of_products_with_shelf
--from Production.ProductInventory 
--where shelf !='N/A'  and LocationID = 40;

select sum(quantity) as sum_of_products_with_shelf
from Production.ProductInventory 
where shelf !='N/A'  and LocationID = 40 and quantity < 100;



--8.Write the query to list the average quantity for products where column LocationID has the value of 10 from the table Production.ProductInventory table.

select LocationID, avg(quantity) as avg_of_products_LocationID_10
from Production.ProductInventory 
where LocationID=10
group by LocationID;




--9.Write query  to see the average quantity  of  products by shelf  from the table Production.ProductInventory
select shelf, avg(quantity) as avg_of_products_with_shelf
from Production.ProductInventory 
group by shelf;


--10.Write query  to see the average quantity  of  products by shelf excluding rows that has the value of N/A in the column Shelf from the table Production.ProductInventory

select shelf, avg(quantity) as avg_of_products_with_shelf
from Production.ProductInventory 
where shelf !='N/A'
group by shelf;

--11.List the members (rows) and average list price in the Production.Product table. This should be grouped independently over the Color and the Class column. 
--Exclude the rows where Color or Class are null.

select  color, avg(ListPrice)
from Production.Product
 where color is not null
 group by color;

select  class, avg(ListPrice)
from Production.Product
 where class is not null
 group by class;

--12.Write a query that lists the country and province names from person. CountryRegion and person. StateProvince tables. 
--Join them and produce a result set similar to the following
select c.CountryRegionCode ,p.name 
from person.CountryRegion c
join person.StateProvince p
on c.CountryRegionCode = p.CountryRegionCode;



--13.Write a query that lists the country and province names from person. CountryRegion and person.StateProvince
-- tables and list the countries filter them by Germany and Canada. Join them and produce a result set similar to the following.
select * from person.CountryRegion;
select * from person.StateProvince;

select c.CountryRegionCode ,p.name 
from person.CountryRegion c
join person.StateProvince p
on c.CountryRegionCode = p.CountryRegionCode
where c.CountryRegionCode = 'CA' or  c.CountryRegionCode = 'DE'
;



USE Northwind;
GO
-- Using Northwnd Database: (Use aliases for all the Joins)
--14.List all Products that has been sold at least once in last 25 years.
--select * from dbo.orders;
--select * from dbo.products;
--select * from dbo.[Order Details];

select p.ProductName, p.productID, sum(od.quantity) as num_of_sold, o.OrderDate
from products as p
left join [Order Details] as od
on p.ProductID = od.ProductID
left join orders as o
on o.OrderID = od.OrderID
group by p.ProductID, p.ProductName,o.OrderDate
having o.OrderDate >'2000-01-01 00:00:00.000'
;


--15.List top 5 locations (Zip Code) where the products sold most.
--select * from dbo.products;
--select * from dbo.Orders ;
--select * from dbo.[Order Details];

--select p.ProductName, p.productID, o.OrderID,  o.ShipPostalCode
--from orders as o 
--left join [Order Details] as od
--on o.OrderID = od.OrderID
--left join products as p
--on p.ProductID = od.ProductID;

select top 5 num_of_products, ShipPostalCode from (
	select  sum(od.Quantity) as num_of_products,  o.ShipPostalCode
		from orders as o 
		left join [Order Details] as od
		on o.OrderID = od.OrderID
		left join products as p
		on p.ProductID = od.ProductID
		group by o.ShipPostalCode
) as subquery
order by num_of_products desc;



--16.List top 5 locations (Zip Code) where the products sold most in last 25 years.
select top 5  
	sum(od.Quantity) as num_of_products, 
	o.ShipPostalCode
from 
	orders as o 
left join 
	[Order Details] as od
	on o.OrderID = od.OrderID
left join 
	products as p
	on p.ProductID = od.ProductID
where 
	o.OrderDate >'1995-01-01 00:00:00.000'
group by 
	o.ShipPostalCode
order by 
	num_of_products desc;



--17.List all city names and number of customers in that city.    
select * from Customers;

select c.city , 
	count(c.customerid ) as num_of_customers
from 
	Customers as c
group by 
	c.City
	;


--18.List city names which have more than 2 customers, and number of customers in that city

select c.city , 
	count(c.customerid ) as num_of_customers
from 
	Customers as c
group by 
	c.City
having 
	count(c.customerid) >2
	;


--19.List the names of customers who placed orders after 1/1/98 with order date.
select c.ContactName, 
	o.orderdate
from 
	Customers as c
left join
	orders as o on o.CustomerID = c.CustomerID
where o.OrderDate >'1998-01-01 00:00:00.000';


--20.List the names of all customers with most recent order dates

select  c.ContactName, 
	max(o.orderdate) as MostRecentOrderDate
from 
	Customers as c
left join
	orders as o on o.CustomerID = c.CustomerID
group by c.ContactName
;



--21.Display the names of all customers  along with the  count of products they bought
--select c.contactname, 
--	od.productid
--,	od.quantity
--from 
--	Customers as c
--left join 
--	Orders as o
--on c.CustomerID = o.CustomerID
--left join 
--	[Order Details] as od
--on o.OrderID = od.OrderID
--order by c.contactname;

select c.contactname, 
	sum(od.quantity) as num_of_products
from 
	Customers as c
left join 
	Orders as o
on c.CustomerID = o.CustomerID
left join 
	[Order Details] as od
on o.OrderID = od.OrderID
group by c.contactname
order by c.contactname
;


--22.Display the customer ids who bought more than 100 Products with count of products.
select c.contactname, 
	sum(od.quantity) as num_of_products
from 
	Customers as c
left join 
	Orders as o
on c.CustomerID = o.CustomerID
left join 
	[Order Details] as od
on o.OrderID = od.OrderID
group by c.contactname
having sum(od.quantity)>100
order by c.contactname
;




--23.Show all the possible combinations of suppliers and shippers, representing every way a supplier can ship its products.
	--The result should display two columns:
	--Supplier CompanyName， Shipper CompanyName
	--select * from shippers;
	--select * from Suppliers;
	select su.CompanyName as 'Supplier CompanyName',
		sh.CompanyName as 'Shipper CompanyName'
	from 
		suppliers su
	cross join 
		shippers sh
	;



--24.Display the products order each day. Show Order date and Product Name.
select 
	o.orderdate, 
	p.productname
from orders as o
left join 
	[Order Details] as od
	on o.OrderID = od.OrderID
left join 
	Products as p
	on od.ProductID = p.ProductID
order by o.orderdate;



--25.Displays pairs of employees who have the same job title.

--select * from Employees;
--select distinct title from Employees;
select 
	e1.lastname+ ' '+ e1.firstname as employee1, 
	e1.title, 
	e2.lastname+ ' '+  e2.lastname as employee2
from 
	Employees e1
left join 
	Employees e2
on 
	e1.title = e2.title 
where
	e1.EmployeeID < e2.EmployeeID;


--26.Display all the Managers who have more than 2 employees reporting to them.
select m.lastname, m.firstname 
from Employees m
where title like '%Manager%'
	and ( 
	select count(*)
	from Employees e
	where e.ReportsTo = m.EmployeeID
	) > 2
;



--27.List all customers and suppliers together, grouped by city.
--The result should display the following columns:
--City，CompanyName，ContactName，Type (indicating whether the record is a Customer or a Supplier).
select * from Suppliers;

select city, companyname, contactname, 'customer' as type
from 
	customers 

UNION ALL
	
select
	City, companyname, contactname, 'supplier' as type
from 
	Suppliers
ORDER BY 
    City, Type;
	;
