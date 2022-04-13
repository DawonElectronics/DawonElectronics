using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bh_test.BHDataService;
using CommandType = bh_test.BHDataService.CommandType;

namespace bh_test
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = new XNDataManagerServiceClient("DataServiceBinding");

            var exInfo = new ExecuteInfo();
            exInfo.StoredProcedureName = "PKG_M08_F04.SSP_M08_F04_001_01";
            exInfo.CommandType = CommandType.StoredProcedure;
            exInfo.CallerInfo = new CallerInfo() {Plant_Code = "100",Screen_Path = "M08_F04_001",Session_ID = "LK202204130000000084",UserGroup_Code = "SCM_GROUP",User_ID = "000147"};
            exInfo.ConnectionType = ConnectionType.Default;
            exInfo.ExecuteDBType = DataBaseType.Oracle;
            exInfo.ExecuteService = ExecuteService.NoneTransaction;
            exInfo.ExecuteType = ExecuteType.Return;
            exInfo.GMT = false;
            
            exInfo.XNTransactionScope = XNTransactionScope.None;
            //exInfo.Parameters = new MsSqlParameter[]
            //{
            //    new MsSqlParameter() {Name = "P_PLANT_CODE",Value = "100",Direction = ParamDirection.Input,AutoRollback = AutoRollback.None, ReturnValueType = DatabaseReturnType.None,IsString = true},
            //    new MsSqlParameter() {Name = "P_BUSI_CODE",Value = "000147",Direction = ParamDirection.Input,AutoRollback = AutoRollback.None, ReturnValueType = DatabaseReturnType.None,IsString = true},
            //    new MsSqlParameter() {Name = "P_FROM_DATE",Value = "2022-04-01",Direction = ParamDirection.Input,AutoRollback = AutoRollback.None, ReturnValueType = DatabaseReturnType.None,IsString = true},
            //    new MsSqlParameter() {Name = "P_TO_DATE",Value = "2022-04-13",Direction = ParamDirection.Input,AutoRollback = AutoRollback.None, ReturnValueType = DatabaseReturnType.None,IsString = true},
            //    new MsSqlParameter() {Name = "P_PROD_CODE",Value = "",Direction = ParamDirection.Input,AutoRollback = AutoRollback.None, ReturnValueType = DatabaseReturnType.None,IsString = true},
            //    new MsSqlParameter() {Name = "P_LOT_NO",Value = "",Direction = ParamDirection.Input,AutoRollback = AutoRollback.None, ReturnValueType = DatabaseReturnType.None,IsString = true},
            //    new MsSqlParameter() {Name = "P_SCM_FLAG",Value = "",Direction = ParamDirection.Input,AutoRollback = AutoRollback.None, ReturnValueType = DatabaseReturnType.None,IsString = true},
            //    new MsSqlParameter() {Name = "P_CURSOR",Direction = ParamDirection.Output,AutoRollback = AutoRollback.None, ReturnValueType = DatabaseReturnType.Cursor,IsString = true}
            //};

            exInfo.Parameters = new MsSqlParameter[]
            {
                new MsSqlParameter() {Name = "P_PLANT_CODE",Value = "100",Direction = ParamDirection.Input,IsString = true},
                new MsSqlParameter() {Name = "P_BUSI_CODE",Value = "000147",Direction = ParamDirection.Input,IsString = true},
                new MsSqlParameter() {Name = "P_FROM_DATE",Value = "2022-04-01",Direction = ParamDirection.Input,IsString = true},
                new MsSqlParameter() {Name = "P_TO_DATE",Value = "2022-04-13",Direction = ParamDirection.Input,IsString = true},
                new MsSqlParameter() {Name = "P_PROD_CODE",Value = "",Direction = ParamDirection.Input,IsString = true},
                new MsSqlParameter() {Name = "P_LOT_NO",Value = "",Direction = ParamDirection.Input,IsString = true},
                new MsSqlParameter() {Name = "P_SCM_FLAG",Value = "",Direction = ParamDirection.Input,IsString = true},
                new MsSqlParameter() {Name = "P_CURSOR",Direction = ParamDirection.Output, ReturnValueType = DatabaseReturnType.Cursor,IsString = true}
            };


            var result = client.ExecuteQuery(exInfo);
            Console.WriteLine(result.ExecuteSuccess);
            Console.ReadKey();
        }

        public static async Task<DataSet> GetWip(DateTime datefrom, DateTime dateto)
        {
            var client = new XNDataManagerServiceClient("DataServiceBinding");

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

            exInfo.Parameters = new MsSqlParameter[]
            {
                new MsSqlParameter() {Name = "P_PLANT_CODE",Value = "100",Direction = ParamDirection.Input,IsString = true},
                new MsSqlParameter() {Name = "P_BUSI_CODE",Value = "000147",Direction = ParamDirection.Input,IsString = true},
                new MsSqlParameter() {Name = "P_FROM_DATE",Value = datefrom.ToString("yyyy-MM-dd"),Direction = ParamDirection.Input,IsString = true},
                new MsSqlParameter() {Name = "P_TO_DATE",Value = dateto.ToString("yyyy-MM-dd"),Direction = ParamDirection.Input,IsString = true},
                new MsSqlParameter() {Name = "P_PROD_CODE",Value = "",Direction = ParamDirection.Input,IsString = true},
                new MsSqlParameter() {Name = "P_LOT_NO",Value = "",Direction = ParamDirection.Input,IsString = true},
                new MsSqlParameter() {Name = "P_SCM_FLAG",Value = "",Direction = ParamDirection.Input,IsString = true},
                new MsSqlParameter() {Name = "P_CURSOR",Direction = ParamDirection.Output, ReturnValueType = DatabaseReturnType.Cursor,IsString = true}
            };


            var result = await client.ExecuteQueryAsync(exInfo);

            return result.ResultDataSet;
        }
    }
}
