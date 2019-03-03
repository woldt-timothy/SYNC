BEGIN
	INSERT INTO [dbo].tblUser(Id,Username,Password)
	VALUES
	(NEWID(), 'alex1', 'password1'),
	(NEWID(), 'jess2', 'password2'),
	(NEWID(), 'abby3', 'password3'),
	(NEWID(), 'amy4', 'password4'),
	(NEWID(), 'bob5', 'password5'),
	(NEWID(), 'daryll6', 'password6')
END