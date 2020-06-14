using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.Data
{
    public class TaskRepository
    {
        string _connectionString;
        public TaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<UserTask> GetTasks(User u)
        {
            using(var context = new UserTaskContext(_connectionString))
            {
                var userTasks = context.UserTasks.Where(ut=> ut.Status !=TaskStatus.Completed).ToList();
                    foreach (UserTask ut in userTasks)
                    {
                        if (ut.Status == TaskStatus.Taken && ut.TakenUserId == u.Id)
                        {
                            ut.Status = TaskStatus.TakenByThisUser;
                        }
                    }
                return userTasks;
            }
        }
        public void AddTask(string task)
        {
            UserTask userTask = new UserTask
            {
                Task=task,
                Status= TaskStatus.NotTaken,
                Id=0
            };
            using (var context = new UserTaskContext(_connectionString))
            {
                context.UserTasks.Add(userTask);
                context.SaveChanges();
            }
        }
        public void ChangeStatus(int TaskId, TaskStatus status, User user)
        {
            var task = GetTaskById(TaskId);
            task.Status = status;
            task.TakenUserId = user.Id;
            using (var context = new UserTaskContext(_connectionString))
            {
                context.UserTasks.Attach(task);
                context.Entry(task).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public UserTask GetTaskById(int Id)
        {
            using(var context = new  UserTaskContext(_connectionString))
            {
                return context.UserTasks.FirstOrDefault(u => u.Id == Id);
            }
        }
    }
}
