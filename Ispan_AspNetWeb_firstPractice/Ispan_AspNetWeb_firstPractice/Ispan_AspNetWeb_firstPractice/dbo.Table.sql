CREATE TABLE [dbo].[tToDo]
(
	[fId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [fTitle] NCHAR(10) NULL, 
    [fLevel] NCHAR(10) NULL, 
    [fDate] DATE NOT NULL
)
