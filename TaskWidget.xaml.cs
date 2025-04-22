using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for TaskWidget.xaml
    /// </summary>
    public partial class TaskWidget : UserControl
    {
        
        TaskWidgetViewModelcs taskWidgetViewModel = new TaskWidgetViewModelcs();
        public Task task { get; set; }
        public TaskWidget( Task t )
        {
            task = t;            
            this.DataContext = taskWidgetViewModel;
            

            InitializeComponent();
            Initializer();
            this.Name = "Task_" + task.Id.ToString();
        }
        private void Initializer()
        {
            taskWidgetViewModel.TaskDate = task.taskDate.ToLongDateString();
            taskWidgetViewModel.TaskTime = task.taskTime.ToString();
            taskWidgetViewModel.TaskName = task.taskName;
            taskWidgetViewModel.TaskDescription = task.taskDescription;
            taskWidgetViewModel.TaskStatus = task.taskStatus;
            taskWidgetViewModel.TaskID = task.Id.ToString();
        }
        
        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            var parent = FindParent<UserControl>((sender as DependencyObject)!);

            int Id = Int32.Parse(parent!.Name.Split('_')[1]);
            
            AddTask addTask = new AddTask(task, "Update Task") { task = task, taskButtonText ="Update Task"};
            task = addTask.task;
            addTask.Id = Id;
            Initializer();
            addTask.ShowDialog();
            task = addTask.task;
            Initializer();
            MainWindow.tasks![(MainWindow.tasks.FindIndex(t => t.Id == Id)!) ]= task;
                    

            


            


        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var parent = FindParent<UserControl>((sender as DependencyObject)!);
            int Id = Int32.Parse(parent!.Name.Split('_')[1]);
            SQLiteDB.deleteTask(Id);
            MainWindow.tasks!.Remove(task);
            MainWindow.TaskWidgets.Remove(MainWindow.TaskWidgets.First(x => x.task.Id == Id)!);
            
            
            MessageBox.Show("Task Deleted" , "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static T? FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            while (parent != null)
            {
                if (parent is T correctlyTyped)
                    return correctlyTyped;

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindow.statusBarControl(task.taskStatus!, task.taskTime.ToString(), task.taskDate.ToLongDateString(), MainWindow.TaskWidgets.Count);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            MainWindow.statusBarControl("No Task Selected", "No Task Selected", "No Task Selected", MainWindow.TaskWidgets.Count);
        }

       
    }
}
