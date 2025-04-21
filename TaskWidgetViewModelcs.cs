using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class TaskWidgetViewModelcs : INotifyPropertyChanged
    {
        private string? _taskID;
        public string? TaskID
        {
            get => _taskID;
            set
            {
                _taskID = value;
                OnPropertyChanged(nameof(TaskID));
            }
        }

        private string? _taskName;
        public string? TaskName { get => _taskName; set { _taskName = value; OnPropertyChanged(nameof(TaskName)); } }
        private string? _taskTime;
        public string? TaskTime { get => _taskTime; set { _taskTime = value; OnPropertyChanged(nameof(TaskTime)); } }

        private string? _taskDate;
        public string? TaskDate { get => _taskDate; set { _taskDate = value; OnPropertyChanged(nameof(TaskDate)); } }
        private string? _taskStatus;
        public string? TaskStatus
        {
            get => _taskStatus;
            set
            {
                _taskStatus = value;
                OnPropertyChanged(nameof(TaskStatus));
            }
        }
        public ObservableCollection<string> _statusOptions = new ObservableCollection<string> { "Pending", "Missed", "Completed" };
        public ObservableCollection<string> StatusOptions
        {
            get => _statusOptions;
            set
            {
                _statusOptions = value;
                OnPropertyChanged(nameof(StatusOptions));
            }
        }
        private string? _taskDescription;
        public string? TaskDescription { get => _taskDescription; set { _taskDescription = value; OnPropertyChanged(nameof(TaskDescription)); } }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
