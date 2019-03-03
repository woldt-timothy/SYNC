BEGIN
	INSERT INTO [dbo].tblStudent(Id, FirstName,LastName,Phone,Email,School,Field,UserId)
	VALUES
	(NEWID(), 'Amy', 'Adams','(920)444-4444','amy@email.com','FVTC', 'Networking', NEWID()),
	(NEWID(), 'Bob', 'Ross','(920)555-5555','bob@email.com','FVTC', 'User Support',NEWID()),
	(NEWID(), 'Daryll', 'Kay','(920)666-6666','daryll@email.com','FVTC', 'UI/UX', NEWID())
END