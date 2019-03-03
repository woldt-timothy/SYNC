BEGIN
	INSERT INTO [dbo].[tblEmployer] (Id, FirstName,LastName,Phone,Email,UserId)
	VALUES
	(NEWID(), 'Alex', 'Smith','(920)111-1111','alex@email.com',NEWID()),
	(NEWID(), 'Jess', 'Jones','(920)222-2222','jess@email.com',NEWID()),
	(NEWID(), 'Abby', 'Williams','(920)333-3333','abby@email.com',NEWID())
END