BEGIN

DECLARE @eUserId1 uniqueidentifier;
SELECT @eUserId1 = Id from tblUser where [UserName] = 'alex1'
DECLARE @eUserId2 uniqueidentifier;
SELECT @eUserId2 = Id from tblUser where [UserName] = 'jess2'
DECLARE @eUserId3 uniqueidentifier;
SELECT @eUserId3 = Id from tblUser where [UserName] = 'abby3'

	INSERT INTO [dbo].[tblEmployer] (Id, RepresentativeFirstName,RepresentativeLastName,Phone,Email,UserId, OrganizationName, Industry, ProfilePicture)
	VALUES
	(NEWID(), 'Alex', 'Smith','(920)111-1111','alex@bemis.com',@eUserId1,'Bemis Company','Manufacturing', 'employerprofilepicture.jpg'),
	(NEWID(), 'Jess', 'Jones','(920)222-2222','jess@oshkoshtruck.com',@eUserId2, 'Oshkosh Truck', 'Manufacturing','employerprofilepicture.jpg'),
	(NEWID(), 'Abby', 'Williams','(920)333-3333','abby@thrivent.com',@eUserId3, 'Thrivent Financial', 'Finance', 'employerprofilepicture.jpg')
END