BEGIN
	INSERT INTO [dbo].[tblTemp] (Id, UserName)
	VALUES
	(NEWID(), 'Charlie'),
	(NEWID(), 'Bravo'),
	(NEWID(), 'Alpha')
END