
--SQL QUERY
DECLARE @sku NVARCHAR(max) = 'Heart'

SELECT a.Id AS AnimalId, a.Name AS AnimalName, a.Deceased As AnimalStatus, cl.Id AS ClinetId, CONCAT(cl.Forename, ' ', cl.Surname) AS ClinetName, cl.Active AS ClinetStatus
FROM Animal (NOLOCK) a
INNER JOIN ClientAnimal (NOLOCK) ca on ca.AnimalId = a.Id
INNER JOIN Client (NOLOCK) cl on cl.Id = ca.ClientId
INNER JOIN Treatment (NOLOCK) tr on tr.AnimalId = a.Id
INNER JOIN Product (NOLOCK) p on p.Id = tr.ProductId
WHERE p.SKU = @sku
-- I couldn't not connect to you database server so I am sure about the schema structure. So, I just added to make sure that if there is null data and consider it as well.
AND ISNULL(a.Deceased, 0) = 0
AND ISNULL(cl.Active, 0) = 1


-- FOR IMPORVEMENT
CREATE NONCLUSTERED INDEX [IX_dbo.ClientAnimal_AnimalId] ON ClientAnimal (AnimalId)
CREATE NONCLUSTERED INDEX [IX_dbo.ClientAnimal_ClientId] ON ClientAnimal (ClientId)
CREATE NONCLUSTERED INDEX [IX_dbo.Product_Sku] ON Product (SkU)

-- FOR FURTHER IMPORVMENT
-- I would suggest that we should have Clinet Id refrence in Animals table only if an animal is not owned by more than clients
-- This is just to avoid multiple joins we can say it as database denormalization