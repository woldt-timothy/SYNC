CREATE TABLE [dbo].[tblEmployer]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [RepresentativeFirstName] VARCHAR(50) NOT NULL, 
    [RepresentativeLastName] VARCHAR(50) NOT NULL, 
    [Phone] VARCHAR(50) NULL, 
    [Email] VARCHAR(50) NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [OrganizationName] VARCHAR(100) NOT NULL, 
    [Industry] VARCHAR(100) NOT NULL, 
    [ProfilePicture] VARBINARY(MAX) NULL
)
