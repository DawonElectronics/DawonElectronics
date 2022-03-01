CREATE TABLE [dbo].[tb_machine] (
    [machine_id]        NVARCHAR (10) NOT NULL,
    [machine_name]      NVARCHAR (10) NOT NULL,
    [department]        NVARCHAR (3)  NOT NULL,
    [machine_modelname] NVARCHAR (50) NULL,
    [machine_maker]     NVARCHAR (50) NULL,
    [machine_shortname] NVARCHAR (3)  NULL,
    CONSTRAINT [tb_machine_PK] PRIMARY KEY CLUSTERED ([machine_id] ASC)
);

