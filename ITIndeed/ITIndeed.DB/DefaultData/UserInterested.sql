BEGIN
	INSERT INTO [dbo].tblUserInterested(Id,UserId,EventId)
	VALUES
	(NEWID(), NEWID(), NEWID()),
	(NEWID(), NEWID(), NEWID()),
	(NEWID(), NEWID(), NEWID()),
	(NEWID(), NEWID(), NEWID()),
	(NEWID(), NEWID(), NEWID()),
	(NEWID(), NEWID(), NEWID())
END