using System.Threading;
using System.Windows;

namespace DAWON_UV_INVENTORY_PROTO
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTU1NTY5QDMxMzkyZTM0MmUzMEwxOFpCV1hCRms2YzlKUmgrNm1RQ244K0FXRmNyZnBMeTlIWmJNK2NYU2s9;NTU1NTcwQDMxMzkyZTM0MmUzMFpKRjlvU1ZKa1IrYjU1NjIyYXZGV3ZlU1Nzb3FycE1qdFp0R2ZYL0JuS0U9;NTU1NTcxQDMxMzkyZTM0MmUzMFk0NWhpdGZKV0E5OXk1bnh4bFAxMEk4TldEQnNnR3EydVEwcGZRVmdmMTA9;NTU1NTcyQDMxMzkyZTM0MmUzMFpBRzR2ZTFPQ1FldlRJaFNyWmRYQ3hOMktNbFRrRUp5V1BnRm14Z2haSGM9;NTU1NTczQDMxMzkyZTM0MmUzMFNPQmtPNFNCL05hZnhKMGhORXNSNmlUOEpJWU0rNXgxVkF4aVcxRXlsNTA9;NTU1NTc0QDMxMzkyZTM0MmUzMFViRXYrdXpESGUzckI0TlROcXpoaFJmN2JpMEJrQ3ZxRXhoNnMzWVJHeGM9;NTU1NTc1QDMxMzkyZTM0MmUzMGdYNnpSQmJpTXdqWDUzMWhhT3RyVkJ4Vy9tSUpOT2hNSzVjQUs1ZjIybEk9;NTU1NTc2QDMxMzkyZTM0MmUzMEYxdFpneURuL05tVzRlK0o3MDFTRXhQOENiVTI4TU5MWXh4MDlqZWd1S2c9;NTU1NTc3QDMxMzkyZTM0MmUzMGp5L01XVWZHMDRsdXhXZEt6aG1malZNNUVMdHh6cUJqTGhqQ0JpNTl4MzA9;NTU1NTc4QDMxMzkyZTM0MmUzME5PaFlFQlBZSldOUmcvaVVOejBJVWFsQ1BWbmVEQWFTOFZkelNXb2ZibVE9;NTU1NTc5QDMxMzkyZTM0MmUzMGVTZWJmdHZZVDZ0LzJ3ZDg4MXlwNDhQelNzOXpvMUV3NXBrcGhWYjdGa1k9");
        }

        /// <summary>
        /// 중복 실행 방지 코드
        /// </summary>
        /// <param name="e"></param>
        /// 
        Mutex mutex;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string mutexName = "UvInventory";
            bool createNew;

            mutex = new Mutex(true, mutexName, out createNew);

            if (!createNew)
            {
                Shutdown();
            }
        }
    }
}
