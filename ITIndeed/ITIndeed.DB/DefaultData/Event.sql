﻿BEGIN
	INSERT INTO [dbo].tblEvent(Id,Name,Type,StartDate,EndDate)
	VALUES
	(1, 'Coffee at Starbucks', 'meetup','05/29/2015 05:00','05/29/2015 06:00'),
	(2, 'X Networking Event','networking event','04/29/2015 02:00','04/29/2015 05:00'),
	(3, 'Lunch with x Employer', 'meetup','03/29/2015 01:00','03/29/2015 02:00')
END