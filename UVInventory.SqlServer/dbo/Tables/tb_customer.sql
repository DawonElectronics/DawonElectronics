CREATE TABLE [dbo].[tb_customer] (
    [cust_id]   NVARCHAR (10) NOT NULL,
    [cust_name] NVARCHAR (30) NOT NULL,
    [cust_code] CHAR (3)      NULL,
    CONSTRAINT [PK_tb_customer] PRIMARY KEY CLUSTERED ([cust_id] ASC),
    CONSTRAINT [UNIQUE_customer_code] UNIQUE NONCLUSTERED ([cust_code] ASC)
);

