
                        CREATE PROCEDURE dbo.sp_InstallListenerNotification_1
                        AS
                        BEGIN
                            -- Service Broker configuration statement.
                            
                -- Setup Service Broker
                IF EXISTS (SELECT * FROM sys.databases 
                                    WHERE name = 'db_uv_inventory' AND is_broker_enabled = 0) 
                BEGIN
                    ALTER DATABASE [db_uv_inventory] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
                    ALTER DATABASE [db_uv_inventory] SET ENABLE_BROKER; 
                    ALTER DATABASE [db_uv_inventory] SET MULTI_USER WITH ROLLBACK IMMEDIATE
                    -- FOR SQL Express
                    ALTER AUTHORIZATION ON DATABASE::[db_uv_inventory] TO [sa]
                END
                -- Create a queue which will hold the tracked information 
                IF NOT EXISTS (SELECT * FROM sys.service_queues WHERE name = 'ListenerQueue_1')
	                CREATE QUEUE dbo.[ListenerQueue_1]
                -- Create a service on which tracked information will be sent 
                IF NOT EXISTS(SELECT * FROM sys.services WHERE name = 'ListenerService_1')
	                CREATE SERVICE [ListenerService_1] ON QUEUE dbo.[ListenerQueue_1] ([DEFAULT]) 
            
                            -- Notification Trigger check statement.
                            
                IF OBJECT_ID ('dbo.tr_Listener_1', 'TR') IS NOT NULL
                    RETURN;
            
                            -- Notification Trigger configuration statement.
                            DECLARE @triggerStatement NVARCHAR(MAX)
                            DECLARE @select NVARCHAR(MAX)
                            DECLARE @sqlInserted NVARCHAR(MAX)
                            DECLARE @sqlDeleted NVARCHAR(MAX)
                            
                            SET @triggerStatement = N'
                CREATE TRIGGER [tr_Listener_1]
                ON dbo.[tb_uv_workorder]
                AFTER INSERT, UPDATE, DELETE 
                AS
                SET NOCOUNT ON;
                --Trigger tb_uv_workorder is rising...
                IF EXISTS (SELECT * FROM sys.services WHERE name = ''ListenerService_1'')
                BEGIN
                    DECLARE @message NVARCHAR(MAX)
                    SET @message = N''<root/>''
                    IF ( EXISTS(SELECT 1))
                    BEGIN
                        DECLARE @retvalOUT NVARCHAR(MAX)
                        %inserted_select_statement%
                        IF (@retvalOUT IS NOT NULL)
                        BEGIN SET @message = N''<root>'' + @retvalOUT END                        
                        %deleted_select_statement%
                        IF (@retvalOUT IS NOT NULL)
                        BEGIN
                            IF (@message = N''<root/>'') BEGIN SET @message = N''<root>'' + @retvalOUT END
                            ELSE BEGIN SET @message = @message + @retvalOUT END
                        END 
                        IF (@message != N''<root/>'') BEGIN SET @message = @message + N''</root>'' END
                    END
                	--Beginning of dialog...
                	DECLARE @ConvHandle UNIQUEIDENTIFIER
                	--Determine the Initiator Service, Target Service and the Contract 
                	BEGIN DIALOG @ConvHandle 
                        FROM SERVICE [ListenerService_1] TO SERVICE ''ListenerService_1'' ON CONTRACT [DEFAULT] WITH ENCRYPTION=OFF, LIFETIME = 60; 
	                --Send the Message
	                SEND ON CONVERSATION @ConvHandle MESSAGE TYPE [DEFAULT] (@message);
	                --End conversation
	                END CONVERSATION @ConvHandle;
                END
            '
                            
                            SET @select = STUFF((SELECT ',' + '[' + COLUMN_NAME + ']'
						                         FROM INFORMATION_SCHEMA.COLUMNS
						                         WHERE DATA_TYPE NOT IN  ('text','ntext','image','geometry','geography') AND TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'tb_uv_workorder' AND TABLE_CATALOG = 'db_uv_inventory'
						                         FOR XML PATH ('')
						                         ), 1, 1, '')
                            SET @sqlInserted = 
                                N'SET @retvalOUT = (SELECT ' + @select + N' 
                                                     FROM INSERTED 
                                                     FOR XML PATH(''row''), ROOT (''inserted''))'
                            SET @sqlDeleted = 
                                N'SET @retvalOUT = (SELECT ' + @select + N' 
                                                     FROM DELETED 
                                                     FOR XML PATH(''row''), ROOT (''deleted''))'                            
                            SET @triggerStatement = REPLACE(@triggerStatement
                                                     , '%inserted_select_statement%', @sqlInserted)
                            SET @triggerStatement = REPLACE(@triggerStatement
                                                     , '%deleted_select_statement%', @sqlDeleted)
                            EXEC sp_executesql @triggerStatement
                        END
                        