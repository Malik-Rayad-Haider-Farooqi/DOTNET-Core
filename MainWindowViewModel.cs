
﻿using System;
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
        private DateTime _filterStartDate = DateTime.Now;
        public DateTime FilterStartDate { get => _filterStartDate; set { _filterStartDate = value; OnPropertyChanged(nameof(FilterStartDate)); } }
        private DateTime _filterEndDate = DateTime.Now;
        public DateTime FilterEndDate { get => _filterEndDate; set { _filterEndDate = value; OnPropertyChanged(nameof(FilterEndDate)); } }
        private String filterStatus = "Pending";

        public String FilterStatus { get => filterStatus; set { filterStatus = value; OnPropertyChanged(nameof(FilterStatus)); } }
        private String filterContains = "";
        public String FilterContains { get => filterContains; set { filterContains = value; OnPropertyChanged(nameof(FilterContains)); } }
        public String FilterBy { get => _filterBy; set { _filterBy = value; OnPropertyChanged(nameof(FilterBy)); } }
        private String _filterBy = "None";
        public String SortBy { get => _sortBy; set { _sortBy = value; OnPropertyChanged(nameof(SortBy)); } }
        private String _sortBy = "None";
        public String OrderBy { get => _orderBy; set { _orderBy = value; OnPropertyChanged(nameof(OrderBy)); } }
        private String _orderBy = "Ascending";
        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<String> _filterItems = new ObservableCollection<string>() { "None", "Date", "Name", "Status" };
        public ObservableCollection<String> FilterItems { get => _filterItems; set { _filterItems = value; OnPropertyChanged(nameof(FilterItems)); } }
        private ObservableCollection<String> _filterOrderItems = new ObservableCollection<string>() { "Ascending", "Descending" };
        public ObservableCollection<String> FilterOrderItems { get => _filterOrderItems; set { _filterOrderItems = value; OnPropertyChanged(nameof(FilterOrderItems)); } }
        private ObservableCollection<String> _filterStatusItems = new ObservableCollection<string>() { "Pending", "Completed" , "Missed"};
        public ObservableCollection<String> FilterStatusItems { get => _filterStatusItems; set { _filterStatusItems = value; OnPropertyChanged(nameof(FilterStatusItems)); } }
        protected virtual void OnPropertyChanged(string propertyName )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        
    }
}
