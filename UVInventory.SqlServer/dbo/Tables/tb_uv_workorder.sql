CREATE TABLE [dbo].[tb_uv_workorder] (
    [create_time]      DATETIME         NULL,
    [trackin_time]     DATETIME         NULL,
    [trackout_time]    DATETIME         NULL,
    [txid]             UNIQUEIDENTIFIER NOT NULL,
    [trackin_user_id]  NVARCHAR (20)    NULL,
    [trackout_user_id] NVARCHAR (20)    NULL,
    [lotid]            NVARCHAR (100)   NULL,
    [pnlqty]           SMALLINT         NULL,
    [sample_order]     BIT              NULL,
    [product_id]       NVARCHAR (100)   NULL,
    [lot_notes]        NTEXT            NULL,
    [machine_cs]       NVARCHAR (300)   NULL,
    [machine_ss]       NVARCHAR (300)   NULL,
    [isDone]           BIT              DEFAULT ((0)) NULL,
    [isPrinted]        BIT              DEFAULT ((0)) NULL,
    [cust_id]          NVARCHAR (10)    NULL,
    [id]               BIGINT           IDENTITY (1, 1) NOT NULL,
    [lot_type]         NVARCHAR (20)    NULL,
    [WaitTrackout]     BIT              DEFAULT ((0)) NULL,
    CONSTRAINT [PK_tb_uv_workorder] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [tb_workorder_FK_custid] FOREIGN KEY ([cust_id]) REFERENCES [dbo].[tb_customer] ([cust_id]),
    CONSTRAINT [tb_workorder_FK_toolinfo] FOREIGN KEY ([product_id]) REFERENCES [dbo].[tb_uv_toolinfo] ([product_id]),
    CONSTRAINT [tb_workorder_FK_trackinuserid] FOREIGN KEY ([trackin_user_id]) REFERENCES [dbo].[tb_users] ([user_id]),
    CONSTRAINT [tb_workorder_FK_trackoutuserid] FOREIGN KEY ([trackout_user_id]) REFERENCES [dbo].[tb_users] ([user_id])
);


GO

                CREATE TRIGGER [tr_Listener_1]
                ON dbo.[tb_uv_workorder]
                AFTER INSERT, UPDATE, DELETE 
                AS
                SET NOCOUNT ON;
                --Trigger tb_uv_workorder is rising...
                IF EXISTS (SELECT * FROM sys.services WHERE name = 'ListenerService_1')
                BEGIN
                    DECLARE @message NVARCHAR(MAX)
                    SET @message = N'<root/>'
                    IF ( EXISTS(SELECT 1))
                    BEGIN
                        DECLARE @retvalOUT NVARCHAR(MAX)
                        SET @retvalOUT = (SELECT [create_time],[trackin_time],[trackout_time],[txid],[trackin_user_id],[trackout_user_id],[lotid],[pnlqty],[sample_order],[product_id],[machine_cs],[machine_ss],[isDone],[isPrinted],[cust_id],[id] 
                                                     FROM INSERTED 
                                                     FOR XML PATH('row'), ROOT ('inserted'))
                        IF (@retvalOUT IS NOT NULL)
                        BEGIN SET @message = N'<root>' + @retvalOUT END                        
                        SET @retvalOUT = (SELECT [create_time],[trackin_time],[trackout_time],[txid],[trackin_user_id],[trackout_user_id],[lotid],[pnlqty],[sample_order],[product_id],[machine_cs],[machine_ss],[isDone],[isPrinted],[cust_id],[id] 
                                                     FROM DELETED 
                                                     FOR XML PATH('row'), ROOT ('deleted'))
                        IF (@retvalOUT IS NOT NULL)
                        BEGIN
                            IF (@message = N'<root/>') BEGIN SET @message = N'<root>' + @retvalOUT END
                            ELSE BEGIN SET @message = @message + @retvalOUT END
                        END 
                        IF (@message != N'<root/>') BEGIN SET @message = @message + N'</root>' END
                    END
                	--Beginning of dialog...
                	DECLARE @ConvHandle UNIQUEIDENTIFIER
                	--Determine the Initiator Service, Target Service and the Contract 
                	BEGIN DIALOG @ConvHandle 
                        FROM SERVICE [ListenerService_1] TO SERVICE 'ListenerService_1' ON CONTRACT [DEFAULT] WITH ENCRYPTION=OFF, LIFETIME = 60; 
	                --Send the Message
	                SEND ON CONVERSATION @ConvHandle MESSAGE TYPE [DEFAULT] (@message);
	                --End conversation
	                END CONVERSATION @ConvHandle;
                END
            