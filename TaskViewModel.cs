using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace WpfApp1
{
    class TaskViewModel : INotifyPropertyChanged
    {
        private string? _taskButtonText = "ADD TASK";
        public string? TaskButtonText
        {
            get => _taskButtonText;
            set
            {
                _taskButtonText = value;
                OnPropertyChanged(nameof(TaskButtonText));

            }
        }


        private string? _taskName = "";
        public string? TaskName { get => _taskName; set { _taskName = value; OnPropertyChanged(nameof(TaskName)); } }

        private int? _taskHour = DateTime.Now.Hour;
        public int? TaskHour { get => _taskHour; set { _taskHour = value; OnPropertyChanged(nameof(TaskHour)); } }
        private int? _taskMinute = DateTime.Now.Minute;
        public int? TaskMinute { get => _taskMinute; set { _taskMinute = value; OnPropertyChanged(nameof(TaskMinute)); } }

        private int? _taskSecond = DateTime.Now.Second;
        public int? TaskSecond { get => _taskSecond; set { _taskSecond = value; OnPropertyChanged(nameof(TaskSecond)); } }

        
        
        private string? _taskDescription;

        public string? TaskDescription { get => _taskDescription; set { _taskDescription = value; OnPropertyChanged(nameof(TaskDescription)); } }

        private DateTime _taskDate;
        public DateTime TaskDate { get => _taskDate;  set { _taskDate = value; OnPropertyChanged(nameof(TaskDate)); } }

        private string? _taskStatus = "Pending";
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


        public event PropertyChangedEventHandler? PropertyChanged;
       
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
      
        
    }

   
}
