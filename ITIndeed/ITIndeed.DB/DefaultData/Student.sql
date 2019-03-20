BEGIN

DECLARE @sUserId1 uniqueidentifier;
SELECT @sUserId1 = Id from tblUser where [UserName] = 'amy4'
DECLARE @sUserId2 uniqueidentifier;
SELECT @sUserId2 = Id from tblUser where [UserName] = 'bob5'
DECLARE @sUserId3 uniqueidentifier;
SELECT @sUserId3 = Id from tblUser where [UserName] = 'daryll6'

	INSERT INTO [dbo].tblStudent(Id, StudentFirstName,StudentLastName,Phone,Email,School,Field,UserId)
	VALUES
	(NEWID(), 'Amy', 'Adams','(920)444-4444','amy@email.com','FVTC', 'Networking', @sUserId1),
	(NEWID(), 'Bob', 'Ross','(920)555-5555','bob@email.com','FVTC', 'User Support',@sUserId2),
	(NEWID(), 'Daryll', 'Kay','(920)666-6666','daryll@email.com','FVTC', 'UI/UX', @sUserId3)
END