﻿CREATE TABLE [dbo].[tblEventShowing]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [EventId] UNIQUEIDENTIFIER NOT NULL
)
