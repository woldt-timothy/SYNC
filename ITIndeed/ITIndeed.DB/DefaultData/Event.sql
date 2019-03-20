BEGIN

DECLARE @evUserId1 uniqueidentifier;
SELECT @evUserId1 = Id from tblUser where [UserName] = 'alex1'
DECLARE @evUserId2 uniqueidentifier;
SELECT @evUserId2 = Id from tblUser where [UserName] = 'jess2'
DECLARE @evUserId3 uniqueidentifier;
SELECT @evUserId3 = Id from tblUser where [UserName] = 'daryll6'

	INSERT INTO [dbo].tblEvent(Id,[Name],[Type],StartDate,EndDate,UserId)
	VALUES
	(NEWID(), 'Coffee at Starbucks', 'meetup','05/29/2015 05:00','05/29/2015 06:00', @evUserId1),
	(NEWID(), 'X Networking Event','networking event','04/29/2015 02:00','04/29/2015 05:00', @evUserId2),
	(NEWID(), 'Lunch with Employer', 'meetup','03/29/2015 01:00','03/29/2015 02:00', @evUserId3)
END