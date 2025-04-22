
﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;





namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public static ObservableCollection<TaskWidget> TaskWidgets { get; set; } = new ObservableCollection<TaskWidget>();
        public static ObservableCollection<TaskWidget> FilteredTaskWidgets { get; set; } = new ObservableCollection<TaskWidget>();
        public static List<Task>? tasks;
        MainWindowViewModel viewModel = new MainWindowViewModel();
        public static MainWindow? CurrentInstance { get; private set; }


        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = viewModel;
            CurrentInstance = this;
            viewModel.taskCount = SQLiteDB.dbInitializer();

            tasks = SQLiteDB.SelectAllTasks();
            foreach (var task in tasks)
            {
                addTaskWidget(task);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task()
            {
                Id = viewModel.taskCount,
                taskName = "",
                taskDate = DateTime.Now,
                taskTime = DateTime.Now.TimeOfDay,
                taskStatus = "Pending",
                taskDescription = ""
            };
            AddTask addTask = new AddTask(task, "Add Task") { task = task, taskButtonText = "Add Task" };
            addTask.Id = viewModel.taskCount;


            viewModel.taskStatus = "Adding Task";
            addTask.task!.Id = viewModel.taskCount;

            addTask.ShowDialog();
            if (addTask.DialogResult == true)
            {
                viewModel.taskCount++;
                viewModel.taskStatus = "Task Added";

                tasks!.Add(addTask.task);

                addTaskWidget(tasks.Last());
                MessageBox.Show("Task Added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }




        }
        private void addTaskWidget(Task task)
        {
            TaskWidget taskWidget = new TaskWidget(task) { task = task };
            TaskWidgets.Add(taskWidget);

        }
        public static void statusBarControl(string ts, string tt, string td, int count)
        {
            CurrentInstance!.viewModel.taskStatus = ts;
            CurrentInstance!.viewModel.taskTime = tt;
            CurrentInstance!.viewModel.taskDate = td;
            CurrentInstance!.viewModel.taskCount = count;

        }

        private void SettigsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsPane.Visibility = Visibility.Visible;
        }

        private void filter_Click(object sender, RoutedEventArgs e)
        {
            FilterPane.Visibility = Visibility.Visible;

        }

        private void applyFilter_Click(object sender, RoutedEventArgs e)
        {
            switch (viewModel.SortBy)
            {
                case "Date":
                    Filter();
                    FilteredTaskWidgets = FilteredTaskWidgets.OrderByDescending(x => x.task.taskDate).ToObservableCollection();
                    break;
                case "Name":
                    FilteredTaskWidgets = FilteredTaskWidgets.OrderBy(x => x.task.taskName).ToObservableCollection();
                    break;
                case "Status":
                    FilteredTaskWidgets = FilteredTaskWidgets.OrderByDescending(x => x.task.taskStatus).ToObservableCollection();
                    break;
                default:
                    Filter();
                    break;
            }
            if(viewModel.OrderBy == "Descending")
            {
                FilteredTaskWidgets = FilteredTaskWidgets.Reverse().ToObservableCollection();
            }
            viewModel.TaskWidgets = FilteredTaskWidgets;
            FilterPane.Visibility = Visibility.Collapsed;
        }
       

        private void SaveSetting_Click(object sender, RoutedEventArgs e)
        {
            SettingsPane.Visibility = Visibility.Collapsed;
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            viewModel.TaskWidgets = TaskWidgets;
            FilterPane.Visibility = Visibility.Collapsed;
        }
        public void Filter()
        {
            switch (viewModel.FilterBy)
            {
                case "Date":
                    if(viewModel.FilterStartDate <= viewModel.FilterEndDate)
                    {
                        FilteredTaskWidgets = TaskWidgets.Where(x => x.task.taskDate >= viewModel.FilterStartDate && x.task.taskDate <= viewModel.FilterEndDate).ToObservableCollection();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Date Range", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                    break;
                case "Name":
                    if (!string.IsNullOrEmpty(viewModel.FilterContains))
                    {
                        FilteredTaskWidgets = TaskWidgets.Where(x => x.task.taskName!.Contains(viewModel.FilterContains)).ToObservableCollection();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case "Status":
                    if (!string.IsNullOrEmpty(viewModel.FilterStatus))
                    {
                        FilteredTaskWidgets = TaskWidgets.Where(x => x.task.taskStatus == viewModel.FilterStatus).ToObservableCollection();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Status", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                default:
                    return;
                    
            }
        }

        private void FilterBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterStartDate.Visibility = Visibility.Collapsed;
            FilterStartDateLabel.Visibility = Visibility.Collapsed;
            FilterEndDate.Visibility = Visibility.Collapsed;
            FilterEndDateLabel.Visibility = Visibility.Collapsed;
            FilterContains.Visibility = Visibility.Collapsed;
            FilterContainsLabel.Visibility = Visibility.Collapsed;
            FilterStatus.Visibility = Visibility.Collapsed;
            FilterStatusLabel.Visibility = Visibility.Collapsed;
            switch (viewModel.FilterBy)
            {
                case "Date":
                    FilterStartDate.Visibility = Visibility.Visible;
                    FilterStartDateLabel.Visibility = Visibility.Visible;
                    FilterEndDate.Visibility = Visibility.Visible;
                    FilterEndDateLabel.Visibility = Visibility.Visible;
                    break;
                case "Name":
                    FilterContains.Visibility = Visibility.Visible;
                    FilterContainsLabel.Visibility = Visibility.Visible;
                    break;
                case "Status":
                    FilterStatus.Visibility = Visibility.Visible;
                    FilterStatusLabel.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
    }
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source) =>
            new ObservableCollection<T>(source);
    }
}
