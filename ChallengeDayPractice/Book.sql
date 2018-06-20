CREATE TABLE [dbo].[Book]
(
	[ISBN] NVARCHAR(50) NOT NULL,
	[Title] NVARCHAR(100) NOT NULL,
	[YearPublished] INT NOT NULL,
	[StudentId] NVARCHAR(9) NULL,
	[AuthorId] NVARCHAR(5) NOT NULL,
	CONSTRAINT PK_Book PRIMARY KEY (ISBN),
	CONSTRAINT FK_Student FOREIGN KEY (StudentId) REFERENCES Student(StudentId),
	CONSTRAINT FK_Author FOREIGN KEY (AuthorId) REFERENCES Author(AuthorId),
	check (YearPublished<=YEAR( getdate()))
)
