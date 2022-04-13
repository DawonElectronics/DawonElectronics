using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using XN.Manager;
using XN.Manager.MsSql.Data;
using BhService;
using CommandType = XN.Manager.CommandType;


namespace ConnectorBHE
{
    public class BheHelper
    {
        public DataSet GetWip(DateTime datefrom, DateTime dateto)
        {
            var client = new XNDataManagerServiceClient(XNDataManagerServiceClient.EndpointConfiguration.DataServiceBinding);

            var exInfo = new ExecuteInfo();
            exInfo.StoredProcedureName = "PKG_M08_F04.SSP_M08_F04_001_01";
            exInfo.CommandType = CommandType.StoredProcedure;
            exInfo.CallerInfo = new CallerInfo() { Plant_Code = "100", Screen_Path = "M08_F04_001", Session_ID = "LK202204130000000084", UserGroup_Code = "SCM_GROUP", User_ID = "000147" };
            exInfo.ConnectionType = ConnectionType.Default;
            exInfo.ExecuteDBType = DataBaseType.Oracle;
            exInfo.ExecuteService = ExecuteService.NoneTransaction;
            exInfo.ExecuteType = ExecuteType.Return;
            exInfo.GMT = false;
            exInfo.XNTransactionScope = XNTransactionScope.None;

            exInfo.Parameters = new List<MsSqlParameter>();
            exInfo.Parameters.Add(new MsSqlParameter() { Name = "P_PLANT_CODE", Value = "100", Direction = ParamDirection.Input, IsString = true });
            exInfo.Parameters.Add(new MsSqlParameter() { Name = "P_BUSI_CODE", Value = "000147", Direction = ParamDirection.Input, IsString = true });
            exInfo.Parameters.Add(new MsSqlParameter() { Name = "P_FROM_DATE", Value = datefrom.ToString("yyyy-MM-dd"), Direction = ParamDirection.Input, IsString = true });
            exInfo.Parameters.Add(new MsSqlParameter() { Name = "P_TO_DATE", Value = dateto.ToString("yyyy-MM-dd"), Direction = ParamDirection.Input, IsString = true });
            exInfo.Parameters.Add(new MsSqlParameter() { Name = "P_PROD_CODE", Value = "", Direction = ParamDirection.Input, IsString = true });
            exInfo.Parameters.Add(new MsSqlParameter() { Name = "P_LOT_NO", Value = "", Direction = ParamDirection.Input, IsString = true });
            exInfo.Parameters.Add(new MsSqlParameter() { Name = "P_SCM_FLAG", Value = "", Direction = ParamDirection.Input, IsString = true });
            exInfo.Parameters.Add(new MsSqlParameter() { Name = "P_CURSOR", Direction = ParamDirection.Output, ReturnValueType = DatabaseReturnType.Cursor, IsString = true });


            var result = client.ExecuteQuery(exInfo);

            return result.ResultDataSet;
        }
    }
}
