using System.Collections.Generic;
using Syncfusion.UI.Xaml.Grid;

namespace DAWON_UV_INVENTORY_PROTO.Helper
{
    public class GridSelectionControllerExt : GridSelectionController
    {
        public GridSelectionControllerExt(SfDataGrid sfgrid)
            : base(sfgrid)
        {

        }
        protected override void ProcessOnGroupChanged(GridGroupingEventArgs args)
        {
            this.SuspendUpdates();
            var removedItems = new List<object>();
            //Removes the items which are not in View
            this.RefreshSelectedItems(ref removedItems);
            // Resets the selected indexes based on SelectedItems
            this.RefreshSelectedRows();
            // Update the current RowIndex 
            this.UpdateCurrentRowIndex();
            this.ResumeUpdates();
        }
    }
}
