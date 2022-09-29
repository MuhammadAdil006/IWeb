CREATE TABLE [dbo].[Likes] (
    [PostId] INT NOT NULL,
    [UserId] INT NOT NULL,
    [Id] INT NOT NULL, 
    CONSTRAINT [FK_Table_ToTableofLike] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Post] ([PostId]),
    CONSTRAINT [FK_Table_ToTableofLike1] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserData] ([Id]), 
    CONSTRAINT [PK_Likes] PRIMARY KEY ([Id])
);

