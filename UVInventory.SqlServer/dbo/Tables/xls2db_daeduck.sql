CREATE TABLE [dbo].[xls2db_daeduck] (
    [create_time]      DATETIME       NULL,
    [trackin_time]     DATETIME       NULL,
    [trackout_time]    DATETIME       NULL,
    [txid]             NVARCHAR (255) NULL,
    [trackin_user_id]  NVARCHAR (255) NULL,
    [trackout_user_id] NVARCHAR (255) NULL,
    [lotid]            NVARCHAR (255) NULL,
    [pnlqty]           FLOAT (53)     NULL,
    [sample_order]     NVARCHAR (255) NULL,
    [product_id]       NVARCHAR (255) NULL,
    [lot_notes]        NVARCHAR (255) NULL,
    [machine_cs]       NVARCHAR (255) NULL,
    [machine_ss]       NVARCHAR (255) NULL,
    [isDone]           FLOAT (53)     NULL,
    [isPrinted]        FLOAT (53)     NULL,
    [cust_id]          NVARCHAR (255) NULL,
    [id]               NVARCHAR (255) NULL,
    [lot_type]         NVARCHAR (255) NULL
);

