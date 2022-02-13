using System.Windows.Input;
using Syncfusion.UI.Xaml.Grid;

namespace DAWON_UV_INVENTORY_PROTO
{
    public class GridCellSelectionControllerExt : GridCellSelectionController
    {
        public GridCellSelectionControllerExt(SfDataGrid dataGrid)
        : base(dataGrid)
        {
        }
        protected override void ProcessKeyDown(KeyEventArgs args)
        {
            if (args.Key == Key.Enter)
            {
                args.Handled = false;
                return;
            }
            base.ProcessKeyDown(args);
        }
    }
}
