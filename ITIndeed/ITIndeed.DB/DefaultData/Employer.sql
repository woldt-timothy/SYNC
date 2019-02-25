BEGIN
	INSERT INTO [dbo].[tblEmployer] (Id, FirstName,LastName,Phone,Email,UserId)
	VALUES
	(1, 'Alex', 'Smith','(920)111-1111','alex@email.com',1),
	(2, 'Jess', 'Jones','(920)222-2222','jess@email.com',2),
	(3, 'Abby', 'Williams','(920)333-3333','abby@email.com',3)
END