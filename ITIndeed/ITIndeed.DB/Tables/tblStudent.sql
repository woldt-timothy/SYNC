CREATE TABLE [dbo].[tblStudent]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [StudentFirstName] VARCHAR(50) NOT NULL, 
    [StudentLastName] VARCHAR(50) NOT NULL, 
    [Phone] VARCHAR(50) NULL, 
    [Email] VARCHAR(50) NOT NULL, 
    [School] VARCHAR(50) NOT NULL, 
    [Field] VARCHAR(50) NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [ProfilePicture] IMAGE NULL
)
