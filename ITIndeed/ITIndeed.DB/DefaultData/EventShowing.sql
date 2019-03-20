BEGIN

/* Events */
DECLARE @esEventId1 uniqueidentifier;
SELECT @esEventId1 = Id from tblEvent where [Name] = 'Coffee at Starbucks'
DECLARE @esEventId2 uniqueidentifier;
SELECT @esEventId2 = Id from tblEvent where [Name] = 'X Networking Event'
DECLARE @esEventId3 uniqueidentifier;
SELECT @esEventId3 = Id from tblEvent where [Name] = 'Lunch with Employer'

/* Students */
DECLARE @esUserId1 uniqueidentifier;
SELECT @esUserId1 = Id from tblUser where [UserName] = 'amy4'
DECLARE @esUserId2 uniqueidentifier;
SELECT @esUserId2 = Id from tblUser where [UserName] = 'bob5'
DECLARE @esUserId3 uniqueidentifier;
SELECT @esUserId3 = Id from tblUser where [UserName] = 'daryll6'

/* Employers */
DECLARE @esUserId4 uniqueidentifier;
SELECT @esUserId4 = Id from tblUser where [UserName] = 'alex1'
DECLARE @esUserId5 uniqueidentifier;
SELECT @esUserId5 = Id from tblUser where [UserName] = 'jess2'
DECLARE @esUserId6 uniqueidentifier;
SELECT @esUserId6 = Id from tblUser where [UserName] = 'abby3'

	INSERT INTO [dbo].tblEventShowing(Id,UserId,EventId)
	VALUES
	(NEWID(),@esUserId1,@esEventId1),
	(NEWID(),@esUserId4,@esEventId1),

	(NEWID(),@esUserId1,@esEventId2),
	(NEWID(),@esUserId2,@esEventId2),
	(NEWID(),@esUserId3,@esEventId2),
	(NEWID(),@esUserId4,@esEventId2),
	(NEWID(),@esUserId5,@esEventId2),
	(NEWID(),@esUserId6,@esEventId2),

	(NEWID(),@esUserId3,@esEventId3),
	(NEWID(),@esUserId6,@esEventId3)

END