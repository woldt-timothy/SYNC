BEGIN
	INSERT INTO [dbo].tblStudent(Id, FirstName,LastName,Phone,Email,School,Field,UserId)
	VALUES
	(1, 'Amy', 'Adams','(920)444-4444','amy@email.com',4),
	(2, 'Bob', 'Ross','(920)555-5555','bob@email.com',5),
	(3, 'Daryll', 'Kay','(920)666-6666','daryll@email.com',6)
END