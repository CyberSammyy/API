﻿CREATE TABLE [dbo].[UserRoles]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[RoleId] UNIQUEIDENTIFIER NOT NULL,
	FOREIGN KEY (UserId) REFERENCES Users(Id),
	FOREIGN KEY (RoleId) REFERENCES Roles(Id),
	CONSTRAINT PK_UserRoles PRIMARY KEY CLUSTERED (UserId, RoleId)
)
