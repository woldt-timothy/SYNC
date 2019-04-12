BEGIN
	INSERT INTO [dbo].[tblEmployer] (Id, RepresentativeFirstName,RepresentativeLastName,Phone,Email,UserId, OrganizationName, Industry, ProfilePicture)
	VALUES
	(NEWID(), 'Alex', 'Smith','(920)111-1111','alex@email.com',NEWID(), 'Bemis Company', 'Manufacturing', convert(varbinary, NULL) ), 
	(NEWID(), 'Jess', 'Jones','(920)222-2222','jess@email.com',NEWID(), 'Oshkosh Truck', 'Manufacturing', convert(varbinary, NULL)),
	(NEWID(), 'Abby', 'Williams','(920)333-3333','abby@email.com',NEWID(), 'Plexus', 'Manufacturing', convert(varbinary, NULL))
END