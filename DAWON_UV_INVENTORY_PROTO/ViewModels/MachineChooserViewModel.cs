using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAWON_UV_INVENTORY_PROTO.Models;
using Syncfusion.Data.Extensions;

namespace DAWON_UV_INVENTORY_PROTO.ViewModels
{
    public class MachineChooserViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<object> _selectedMachine = new ObservableCollection<object>();
        public MachineChooserViewModel()
        {
            //Machines = MainWindow.mainwindowViewModel.Machines.Select(x=>x.MachineShortname).ToObservableCollection();
            Machines = MainWindow._mainwindowViewModel.Machines;
        }
        private ObservableCollection<TbMachine>? _machines;
        public ObservableCollection<TbMachine>? Machines
        {
            get { return _machines; }
            set
            {
                _machines = value;
                OnPropertyChanged(nameof(Machines));
            }
        }

        private ViewUvWorkorder _workorderRecord;
        public ViewUvWorkorder WorkorderRecord
        {
            get { return _workorderRecord; }
            set
            {
                _workorderRecord = value;
                OnPropertyChanged(nameof(WorkorderRecord));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public ObservableCollection<object> SelectedMachine
        {
            get { return _selectedMachine;}
            set
            {
                _selectedMachine = value;
                OnPropertyChanged(nameof(SelectedMachine));
            }

        }

        public string DbMachineString
        {
            get
            {
                var templist= new List<string>(); 
                foreach (var item in SelectedMachine)
                {
                    templist.Add(((TbMachine) item).MachineShortname);
                }

                return string.Join(",", templist);
            }
            set
            {
                if (value != null&& value.Length>2)
                {
                    var templist = new ObservableCollection<object>();

                    foreach (var item in value.Split(',').ToList())
                {
                    templist.Add(Machines.Where(x => x.MachineShortname == item).First());
                }

                SelectedMachine.Clear();
                SelectedMachine = templist;
                OnPropertyChanged(nameof(SelectedMachine));
            }
        }
        }

    }
}
