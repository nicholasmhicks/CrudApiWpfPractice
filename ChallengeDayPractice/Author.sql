﻿CREATE TABLE [dbo].[Author]
(
	[AuthorId] NVARCHAR(5) NOT NULL,
	[FirstName] NVARCHAR(50) NOT NULL,
	[SurName] NVARCHAR(50) NOT NULL,
	[TaxFileNumber] NVARCHAR(9) NOT NULL,
	CONSTRAINT PK_AUTHOR PRIMARY KEY (AuthorId)
)
