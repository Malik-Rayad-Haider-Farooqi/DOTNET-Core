using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;


namespace WpfApp1
{
    public class SQLiteDB
    {
        private static string connectionString = $"DataSource =ToDoList.db";

        public static int dbInitializer() {
            string tableQuery = "CREATE TABLE IF NOT EXISTS TODOLIST(Id INTEGER PRIMARY KEY,taskName TEXT , taskDate DATE, taskTime TEXT, taskStatus TEXT, taskDescription TEXT)";
            string getCount = "SELECT COUNT(*) FROM TODOLIST";
            int count;
            using (var connection = new SQLiteConnection(connectionString)) {
                connection.Open();
                using (var command = new SQLiteCommand(tableQuery ,connection))
                {
                    command.ExecuteNonQuery();
                    command.CommandText = getCount;
                    count = Convert.ToInt32(command.ExecuteScalar());
                };
                connection.Close();
            };
            return count;
        }

        public static void insertTask(Task task)
        {
            string insertionQuery = "INSERT INTO TODOLIST VALUES(@Id,@taskName,@taskDate,@taskTime, @taskStatus, @taskDescription)";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(insertionQuery, connection))
                {
                    command.Parameters.Add("@Id", DbType.Int32).Value = task.Id;
                    command.Parameters.Add("@taskName", DbType.String).Value = task.taskName;
                    command.Parameters.Add("@taskDate", DbType.Date).Value = task.taskDate;
                    command.Parameters.Add("@taskTime", DbType.Time).Value = task.taskTime.ToString();
                    command.Parameters.Add("@taskStatus", DbType.String).Value = task.taskStatus;
                    command.Parameters.Add("@taskDescription", DbType.String).Value = task.taskDescription;
                    command.ExecuteNonQuery();
                };
                connection.Close();
            };
        }
       
        public static List<Task> SelectAllTasks()
        {
            List<Task> tasks = new List<Task>();
            string selectionAll = "SELECT * FROM TODOLIST";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(selectionAll, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var task = new Task
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                taskName = reader["taskName"].ToString(),
                                taskStatus = reader["taskStatus"].ToString()!,
                                taskDescription = reader["taskDescription"].ToString(),
                                taskDate = DateTime.Parse(reader["taskDate"].ToString()!),
                                taskTime = DateTime.Parse(reader["taskTime"].ToString()!).TimeOfDay
                            };
                            tasks.Add(task);

                        }
                    }
                }
                
            }
            return tasks;
        }
         public static void deleteTask(int id)
        {
            string deletionQuery = "DELETE FROM TODOLIST WHERE Id = @Id";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(deletionQuery, connection))
                {
                    command.Parameters.Add("@Id", DbType.Int32).Value = id;
                    command.ExecuteNonQuery();
                };
                connection.Close();
            };
        }
        
        
        
        public static void updateTask(Task task)
        {
            string updationQuery = "UPDATE TODOLIST set Id = @Id,taskName = @taskName, taskDate = @taskDate, taskTime = @taskTime, taskStatus = @taskStatus, taskDescription = @taskDescription WHERE Id = @Id";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(updationQuery, connection))
                {
                    command.Parameters.Add("@Id", DbType.Int32).Value = task.Id;
                    command.Parameters.Add("@taskName", DbType.String).Value = task.taskName;
                    command.Parameters.Add("@taskDate", DbType.Date).Value = task.taskDate;
                    command.Parameters.Add("@taskTime", DbType.Time).Value = task.taskTime.ToString();
                    command.Parameters.Add("@taskStatus", DbType.String).Value = task.taskStatus;
                    command.Parameters.Add("@taskDescription", DbType.String).Value = task.taskDescription;
                    command.ExecuteNonQuery();
                };
                connection.Close();
            };
        }
        
        
        
        
    }
    public class Task
    {

        public  int Id { get; set; }
        public  string? taskName { get; set; }
        public  DateTime taskDate { get; set; }
        public  TimeSpan taskTime { get; set; }
        public string? taskStatus { get; set; }
        public string? taskDescription { get; set; }

}
}
