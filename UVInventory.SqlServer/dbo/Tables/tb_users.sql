CREATE TABLE [dbo].[tb_users] (
    [user_id]    NVARCHAR (20) NOT NULL,
    [user_name]  NVARCHAR (20) NULL,
    [user_group] NVARCHAR (20) NULL,
    [user_role]  NVARCHAR (20) NULL,
    [isRetired]  BIT           DEFAULT ((0)) NULL,
    CONSTRAINT [PK_tb_users] PRIMARY KEY CLUSTERED ([user_id] ASC),
    CONSTRAINT [FK_tb_users_tb_users] FOREIGN KEY ([user_id]) REFERENCES [dbo].[tb_users] ([user_id]),
    CONSTRAINT [UNIQUE_user_id] UNIQUE NONCLUSTERED ([user_id] ASC)
);

