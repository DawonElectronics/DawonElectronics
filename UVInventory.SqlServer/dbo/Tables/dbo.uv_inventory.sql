CREATE TABLE [dbo].[dbo.uv_inventory] (
    [id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [input_time]          DATETIME       NOT NULL,
    [modfied_time]        DATETIME       NULL,
    [completed_time]      DATETIME       NULL,
    [customer_code]       TINYINT        NOT NULL,
    [customer_name]       NVARCHAR (50)  NOT NULL,
    [input_username]      NCHAR (20)     NOT NULL,
    [lastmodify_username] NCHAR (20)     NULL,
    [complete_username]   NCHAR (20)     NULL,
    [lotid]               NVARCHAR (100) NULL,
    [order_type]          NCHAR (10)     NULL,
    [pnlqty]              SMALLINT       NOT NULL,
    [lotinfo_mesprcname]  NCHAR (30)     NULL,
    [lotinfo_mesprcid]    NVARCHAR (10)  NULL,
    [lotinfo_customer]    NVARCHAR (100) NULL,
    [lotinfo_layer]       NCHAR (20)     NULL,
    [lotinfo_prctype]     NCHAR (10)     NULL,
    [lotinfo_toolno]      NVARCHAR (50)  NULL,
    [lotinfo_modelname]   NVARCHAR (200) NULL,
    [lotinfo_rev]         NCHAR (20)     NULL,
    [lotinfo_pnlpcsqty]   INT            NULL,
    [lotinfo_worksizex]   DECIMAL (6, 2) NULL,
    [lotinfo_worksizey]   DECIMAL (6, 2) NULL,
    [lotinfo_holecount]   INT            NULL,
    [note]                NTEXT          NULL,
    [machine_cs]          NCHAR (10)     NULL,
    [machine_ss]          NCHAR (10)     NULL,
    [isDone]              BIT            NULL,
    [isPrinted]           BIT            NULL,
    CONSTRAINT [PK_dbo.uv_inventory] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_dbo.uv_inventory_Column]
    ON [dbo].[dbo.uv_inventory]([input_time] ASC, [lotinfo_prctype] ASC);

