﻿CREATE TABLE [dbo].[tblUserInterested]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [EventId] UNIQUEIDENTIFIER NOT NULL
)
