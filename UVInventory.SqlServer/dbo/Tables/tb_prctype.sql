CREATE TABLE [dbo].[tb_prctype] (
    [prc_code] NVARCHAR (20) NOT NULL,
    [prc_name] NVARCHAR (20) NULL,
    CONSTRAINT [PK_tb_prctype] PRIMARY KEY CLUSTERED ([prc_code] ASC)
);

