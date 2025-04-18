using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    /// 
    public partial class AddTask : Window
    {
        public Task? task { get; set; }
        public string? taskButtonText { get; set; }
        public int Id;
        TaskViewModel? taskViewModel;
        public AddTask() { 
            InitializeComponent();
        }
        public AddTask(Task task, string taskButtonText)
        {
            
            InitializeComponent();
                       
            
            taskViewModel = new TaskViewModel();
         
            this.DataContext = taskViewModel;
            taskViewModel.TaskButtonText = taskButtonText;
            taskViewModel.TaskName = task.taskName;
            taskViewModel.TaskDescription = task.taskDescription;
            taskViewModel.TaskDate = task.taskDate;
            taskViewModel.TaskHour = task.taskTime.Hours;
            taskViewModel.TaskMinute = task.taskTime.Minutes;
            taskViewModel.TaskSecond = task.taskTime.Seconds;
            taskViewModel.TaskStatus = task.taskStatus;
            




        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan taskTime = new TimeSpan(taskViewModel!.TaskHour!.Value, taskViewModel!.TaskMinute!.Value, taskViewModel!.TaskSecond!.Value);
            


            
            if (string.IsNullOrEmpty(taskViewModel.TaskName))
            {
                MessageBox.Show("The  Task Name Cannot be empty.", "Alert", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if ((taskViewModel.TaskDate >=  DateTime.Today)  &&( taskViewModel.TaskStatus == "Completed" || taskViewModel.TaskStatus == "Missed"))
            { 
                if(taskTime >= DateTime.Now.TimeOfDay)
                {
                    MessageBox.Show("A Future Task cannot be marked as Completed or Missed", "Alert", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                
            }
            else if(taskViewModel.TaskStatus == "Pending" && taskViewModel.TaskDate < DateTime.Today)
            {
                MessageBox.Show("Please select a future date.", "Alert", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else if (taskViewModel.TaskStatus == "Pending" && ( taskViewModel.TaskDate == DateTime.Today && taskTime < DateTime.Now.TimeOfDay))
            {
                MessageBox.Show("Please select a future time.", "Alert", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            task = new Task
            {
                Id = Id,                
                taskName = taskViewModel.TaskName,
                taskDate = taskViewModel.TaskDate,
                taskTime = taskTime,
                taskDescription = taskViewModel.TaskDescription,
                taskStatus = taskViewModel.TaskStatus
            };
            if(taskButtonText == "Update Task")
            {
                SQLiteDB.updateTask(task);
            }
            else
            {
                SQLiteDB.insertTask(task);
            }
            
            DialogResult = true;
                
            


        }
        


    }
}
