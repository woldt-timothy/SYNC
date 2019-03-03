BEGIN
	INSERT INTO [dbo].tblEventShowing(Id,UserId,EventId)
	VALUES
	(NEWID(), NEWID(), NEWID()),
	(NEWID(), NEWID(), NEWID()),
	(NEWID(), NEWID(), NEWID())
END