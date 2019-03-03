BEGIN
	INSERT INTO [dbo].tblEvent(Id,Name,Type,StartDate,EndDate)
	VALUES
	(NEWID(), 'Coffee at Starbucks', 'meetup','05/29/2015 05:00','05/29/2015 06:00'),
	(NEWID(), 'X Networking Event','networking event','04/29/2015 02:00','04/29/2015 05:00'),
	(NEWID(), 'Lunch with x Employer', 'meetup','03/29/2015 01:00','03/29/2015 02:00')
END