using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Syncfusion.Data.Extensions;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Cells;
using Syncfusion.Windows.Tools.Controls;

namespace DAWON_UV_INVENTORY_PROTO.Helper
{
    public class DisplayConverter : IValueConverter
    {
        GridColumn _cachedColumn;

        public DisplayConverter()
        {

        }

        public DisplayConverter(GridColumn column)
        {
            _cachedColumn = column;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var selectedItems = value as IEnumerable;
            var displayMemberPath = string.Empty;

            var column = _cachedColumn as GridComboBoxColumn;
            displayMemberPath = column.DisplayMemberPath;

            if (selectedItems == null)
                return null;

            string selectedItem = string.Empty;
            PropertyDescriptorCollection pdc = null;
            var enumerator = selectedItems.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var type = enumerator.Current.GetType();

                pdc = pdc ?? TypeDescriptor.GetProperties(type);

                if (!string.IsNullOrEmpty(displayMemberPath))
                    selectedItem += pdc.GetValue(enumerator.Current, displayMemberPath) + " - ";
            }
            return selectedItem.Substring(0, selectedItem.Length - 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
    public class GridComboBoxRenderer : GridVirtualizingCellRenderer<TextBlock, ComboBoxAdv>
    {

        public GridComboBoxRenderer()
        {
        }
        /// <summary>
        /// Create new display element.
        /// </summary>
        /// <returns></returns>

        protected override TextBlock OnCreateDisplayUIElement()
        {
            return new TextBlock();
        }
        /// <summary>
        /// Create new edit element.
        /// </summary>
        /// <returns></returns>

        protected override ComboBoxAdv OnCreateEditUIElement()
        {
            return new ComboBoxAdv();
        }
        /// <summary>
        /// Initialize binding for display element.
        /// </summary>
        /// <param name="dataColumn"></param>
        /// <param name="uiElement"></param>
        /// <param name="dataContext"></param>

        public override void OnInitializeDisplayElement(Syncfusion.UI.Xaml.Grid.DataColumnBase dataColumn, TextBlock uiElement, object dataContext)
        {
            base.OnInitializeDisplayElement(dataColumn, uiElement, dataContext);
            SetDisplayBinding(uiElement, dataColumn.GridColumn, dataContext);
        }

        /// <summary>
        /// custom binding for display element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="column"></param>
        /// <param name="dataContext"></param>

        private static void SetDisplayBinding(TextBlock element, GridColumn column, object dataContext)
        {
            var comboBoxColumn = (GridComboBoxColumn)column;
            var binding = new Binding
            {
                Path = new PropertyPath(comboBoxColumn.MappingName),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = new DisplayConverter(comboBoxColumn),
            };
            element.SetBinding(TextBlock.TextProperty, binding);
        }
        /// <summary>
        /// Update binding for display element.
        /// </summary>
        /// <param name="dataColumn"></param>
        /// <param name="uiElement"></param>
        /// <param name="dataContext"></param>

        public override void OnUpdateDisplayBinding(DataColumnBase dataColumn, TextBlock uiElement, object dataContext)
        {
            base.OnUpdateDisplayBinding(dataColumn, uiElement, dataContext);
            SetDisplayBinding(uiElement, dataColumn.GridColumn, dataContext);
        }
        /// <summary>
        /// Initialize binding for edit element.
        /// </summary>
        /// <param name="dataColumn"></param>
        /// <param name="uiElement"></param>
        /// <param name="dataContext"></param>

        public override void OnInitializeEditElement(DataColumnBase dataColumn, ComboBoxAdv uiElement, object dataContext)
        {
            base.OnInitializeEditElement(dataColumn, uiElement, dataContext);
            SetEditBinding(uiElement, dataColumn.GridColumn, dataContext);
        }

        /// <summary>
        /// Update binding for edit element.
        /// </summary>
        /// <param name="dataColumn"></param>
        /// <param name="uiElement"></param>
        /// <param name="dataContext"></param>

        public override void OnUpdateEditBinding(DataColumnBase dataColumn, ComboBoxAdv element, object dataContext)
        {
            base.OnUpdateEditBinding(dataColumn, element, dataContext);
            SetEditBinding(element, dataColumn.GridColumn, dataContext);
        }

        /// <summary>
        /// custom binding for display element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="column"></param>
        /// <param name="dataContext"></param>

        private static void SetEditBinding(ComboBoxAdv element, GridColumn column, object dataContext)
        {
            var comboboxColumn = (GridComboBoxColumn)column;
            var binding = new Binding
            {
                Source = dataContext,
                Path = new PropertyPath(comboboxColumn.MappingName),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };
            element.SetBinding(ComboBoxAdv.SelectedItemsProperty, binding);

            var itemsSourceBinding = new Binding { Path = new PropertyPath("ItemsSource"), Mode = BindingMode.TwoWay, Source = comboboxColumn };
            element.SetBinding(ComboBoxAdv.ItemsSourceProperty, itemsSourceBinding);

            var displayMemberBinding = new Binding { Path = new PropertyPath("DisplayMemberPath"), Mode = BindingMode.TwoWay, Source = comboboxColumn };
            element.SetBinding(ComboBoxAdv.DisplayMemberPathProperty, displayMemberBinding);

            var selectedValuePathBinding = new Binding { Path = new PropertyPath("SelectedValuePath"), Mode = BindingMode.TwoWay, Source = comboboxColumn };
            element.SetBinding(ComboBoxAdv.SelectedValuePathProperty, selectedValuePathBinding);

            element.AllowMultiSelect = true;
        }

        /// <summary>
        /// Let Renderer decide whether the parent grid should be allowed to handle keys and prevent
        /// the key event from being handled by the visual UIElement for this renderer. 
        /// </summary>
        /// <param name="e">A <see cref="KeyEventArgs" /> object.</param>
        /// <returns>
        /// True if the parent grid should be allowed to handle keys; false otherwise.
        /// </returns>

        protected override bool ShouldGridTryToHandleKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (!HasCurrentCellState || !IsInEditing)
                return true;

            switch (e.Key)
            {
                case Key.End:
                case Key.Home:
                case Key.Enter:
                case Key.Escape:
                    return !((ComboBoxAdv)CurrentCellRendererElement).IsDropDownOpen;

                case Key.Down:
                case Key.Up:
                case Key.Left:
                case Key.Right:
                    return !((ComboBoxAdv)CurrentCellRendererElement).IsDropDownOpen;
            }
            return base.ShouldGridTryToHandleKeyDown(e);
        }
        /// <summary>
        /// Gets the control value.
        /// </summary>

        public override object GetControlValue()
        {

            if (!HasCurrentCellState)
                return base.GetControlValue();

            return CurrentCellRendererElement.GetValue(IsInEditing ? ComboBoxAdv.SelectedValueProperty : TextBlock.TextProperty);
        }

        /// <summary>
        /// Sets the control value.
        /// </summary>
        /// <param name="value">The value.</param>

        public override void SetControlValue(object value)
        {

            if (!HasCurrentCellState)
                return;

            if (IsInEditing)
                ((ComboBoxAdv)CurrentCellRendererElement).SelectedValue = value;

            else
                throw new Exception("Value cannot be Set for Unloaded Editor");
        }
    }
}

    