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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHFqVVhkW1pFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF9jTX9WdkJiX31WcnxQQQ==;Mgo+DSMBPh8sVXJ0S0d+XE9AcVRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS3xTckRmWXhfdnZdRGlZVg==;ORg4AjUWIQA/Gnt2VVhhQlFaclhJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkFiXH9ZcXJXTmNcV0M=;NjE2NzQ5QDMyMzAyZTMxMmUzME9NcmszMTgyRWUyQUpIWkNzRXpuSE42TGZ6bllDaDAzS3BxOXdmT29rWVk9;NjE2NzUwQDMyMzAyZTMxMmUzMGYvNXBDNVN0aDZWWjE2NGRjMGxHNkV1STVWU3ZYQkQ5V2RXU3Y1a1NLQjA9;NRAiBiAaIQQuGjN/V0Z+XU9EaFtFVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdEVnWHped3VTRGhfV0Vx;NjE2NzUyQDMyMzAyZTMxMmUzMGVMdHpLNmtSTnpwWGZTTlEraFR5RkdOY2lmUDlrSkV4T3E0T2VEYmliblU9;NjE2NzUzQDMyMzAyZTMxMmUzMEJ5MnJPVEdKM0lGaitnRmlkaGc3SDZFUjE3TENJdXk4VGZTdXJ2RnhpMnM9;Mgo+DSMBMAY9C3t2VVhhQlFaclhJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkFiXH9ZcXJXTmRdVEM=;NjE2NzU1QDMyMzAyZTMxMmUzMG5tMHRwN1dyWUc0SjdTNk5aSE1ab0NWczk3VEUyK0pZVkZGeUFSMncyTnc9;NjE2NzU2QDMyMzAyZTMxMmUzMEFlUnY0TkRZVHpPa3B6eE02Nk9BYjJYUW5yaFIwcnIxWkFCUmlWVm84UGc9");
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
