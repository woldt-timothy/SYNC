BEGIN
	INSERT INTO [dbo].tblStudent(Id, StudentFirstName,StudentLastName,Phone,Email,School,Field,UserId, ProfilePicture)
	VALUES
	(NEWID(), 'Amy', 'Adams','(920)444-4444','amy@email.com','FVTC', 'Networking', NEWID(), convert(varbinary, 'studentprofilepicture.jpg1')),
	(NEWID(), 'Bob', 'Ross','(920)555-5555','bob@email.com','FVTC', 'User Support',NEWID(), convert(varbinary, 'studentprofilepicture.jpg2')),
	(NEWID(), 'Daryll', 'Kay','(920)666-6666','daryll@email.com','FVTC', 'UI/UX', NEWID(), convert(varbinary, 'studentprofilepicture.jpg3'))
END