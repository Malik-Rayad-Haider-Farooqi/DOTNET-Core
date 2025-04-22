using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.Core;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _taskStatus = "No Task Selected";
        public string taskStatus
        {
            get => _taskStatus;
            set
            {
                _taskStatus = value;
                OnPropertyChanged(nameof(taskStatus));
            }
            
        }
        private string _taskTime = "No Task Selected";
        public string taskTime
        {
            get => _taskTime;
            set
            {
                _taskTime = value;
                OnPropertyChanged(nameof(taskTime));
            }
        }
        private String _taskDate = "No Task Selected";
        public string taskDate
        {
            get => _taskDate;
            set
            {
                _taskDate = value;
                OnPropertyChanged(nameof(taskDate));
            }
        }

        private static int _taskCount = MainWindow.TaskWidgets.Count;
        public int taskCount
        {
            get => _taskCount;
            set
            {
                _taskCount = value;
                OnPropertyChanged(nameof(taskCount));
            }
        }
        public ObservableCollection<TaskWidget>? _taskWidgets = MainWindow.TaskWidgets;
        public ObservableCollection<TaskWidget>? TaskWidgets
        {
            get => _taskWidgets; set
            {
                _taskWidgets = value;
                OnPropertyChanged(nameof(TaskWidgets));
            }
        }

        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        
    }
}
