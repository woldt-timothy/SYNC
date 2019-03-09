BEGIN
	INSERT INTO [dbo].[tblEmployer] (Id, RepresentativeFirstName,RepresentativeLastName,Phone,Email,UserId, OrganizationName, Industry)
	VALUES
	(NEWID(), 'Alex', 'Smith','(920)111-1111','alex@bemis.com',NEWID(),'Bemis Company','Manufacturing'),
	(NEWID(), 'Jess', 'Jones','(920)222-2222','jess@oshkoshtruck.com',NEWID(), 'Oshkosh Truck', 'Manufacturing'),
	(NEWID(), 'Abby', 'Williams','(920)333-3333','abby@thrivent.com',NEWID(), 'Thrivent Financial', 'Finance')
END