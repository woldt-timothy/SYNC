﻿CREATE TABLE [dbo].[tblEmployer]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [Phone] VARCHAR(50) NOT NULL, 
    [Email] VARCHAR(50) NOT NULL, 
    [UserId] INT NOT NULL
)
