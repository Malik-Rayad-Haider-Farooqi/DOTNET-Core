<<<<<<< HEAD
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



namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public static ObservableCollection<TaskWidget> TaskWidgets { get; set; } = new ObservableCollection<TaskWidget>();
        public static  List<Task>? tasks;
        MainWindowViewModel viewModel = new MainWindowViewModel();
        public static MainWindow? CurrentInstance { get; private set; }
        

        public MainWindow()
        {
            InitializeComponent();
           
            this.DataContext = viewModel;
            CurrentInstance = this;
            viewModel.taskCount =  SQLiteDB.dbInitializer();
            
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
            AddTask addTask = new AddTask(task, "Add Task") {task = task,taskButtonText = "Add Task"};
           addTask.Id = viewModel.taskCount;
                
                
            viewModel.taskStatus = "Adding Task";
            addTask.task!.Id = viewModel.taskCount ;

            addTask.ShowDialog();
            if(addTask.DialogResult == true )
            {
                viewModel.taskCount++;
                viewModel.taskStatus = "Task Added";

                tasks!.Add(addTask.task);
                
                addTaskWidget(tasks.Last());
                MessageBox.Show("Task Added" , "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
             
            
            
            
        }
        private void addTaskWidget(Task task)
        {
            TaskWidget taskWidget = new TaskWidget(task) { task = task };
            TaskWidgets.Add(taskWidget);
            
        }
        public static void statusBarControl(string ts, string tt, string td , int count)
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
            if(SortBy.SelectedItem.ToString() == "Date")
            {
                switch (FilterBy.SelectedItem.ToString())
                {
                    case "Date":
                        break;
                    case "Name":
                        break;
                    case "Status":
                        break;
                    default:
                        break;
                }
            }
            FilterPane.Visibility = Visibility.Collapsed;
        }

        private void SaveSetting_Click(object sender, RoutedEventArgs e)
        {
            SettingsPane.Visibility = Visibility.Collapsed;
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
=======
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



namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public static ObservableCollection<TaskWidget> TaskWidgets { get; set; } = new ObservableCollection<TaskWidget>();
        public static  List<Task>? tasks;
        MainWindowViewModel viewModel = new MainWindowViewModel();
        public static MainWindow? CurrentInstance { get; private set; }
        

        public MainWindow()
        {
            InitializeComponent();
           
            this.DataContext = viewModel;
            CurrentInstance = this;
            viewModel.taskCount =  SQLiteDB.dbInitializer();
            
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
            AddTask addTask = new AddTask(task, "Add Task") {task = task,taskButtonText = "Add Task"};
           addTask.Id = viewModel.taskCount;
                
                
            viewModel.taskStatus = "Adding Task";
            addTask.task!.Id = viewModel.taskCount ;

            addTask.ShowDialog();
            if(addTask.DialogResult == true )
            {
                viewModel.taskCount++;
                viewModel.taskStatus = "Task Added";

                tasks!.Add(addTask.task);
                
                addTaskWidget(tasks.Last());
                MessageBox.Show("Task Added" , "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
             
            
            
            
        }
        private void addTaskWidget(Task task)
        {
            TaskWidget taskWidget = new TaskWidget(task) { task = task };
            TaskWidgets.Add(taskWidget);
            
        }
        public static void statusBarControl(string ts, string tt, string td , int count)
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
            if(SortBy.SelectedItem.ToString() == "Date")
            {
                switch (FilterBy.SelectedItem.ToString())
                {
                    case "Date":
                        break;
                    case "Name":
                        break;
                    case "Status":
                        break;
                    default:
                        break;
                }
            }
            FilterPane.Visibility = Visibility.Collapsed;
        }

        private void SaveSetting_Click(object sender, RoutedEventArgs e)
        {
            SettingsPane.Visibility = Visibility.Collapsed;
        }
    }
>>>>>>> 20590a87fc9dcf5ab841490e85afdeb890238359
}