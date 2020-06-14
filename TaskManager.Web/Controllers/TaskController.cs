using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaskManager.Data;
using TaskManager.Web.Models;

namespace TaskManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        string _connectionString;
        public TaskController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [HttpGet]
        [Route("GetTasks")]
        public List<UserTask> GetTasks()
        {
            TaskRepository repo = new TaskRepository(_connectionString);
            AccountRepository aRepo = new AccountRepository(_connectionString);
            User user = aRepo.GetByEmail(User.Identity.Name);
            List<UserTask> userTasks = repo.GetTasks(user);
            return userTasks;
        }
        [HttpPost]
        [Route("AddTask")]
        public void AddTask(TaskViewModel taskViewModel)
        {
            TaskRepository repo = new TaskRepository(_connectionString);
            repo.AddTask(taskViewModel.Task);
        }
        [HttpPost]
        [Route("ChangeTaskStatus")]
        public void ChangeTaskStatus(ChangeStatusViewModel changeStatusViewModel)
        {
            TaskRepository repo = new TaskRepository(_connectionString);
            AccountRepository aRepo = new AccountRepository(_connectionString);
            User user = aRepo.GetByEmail(User.Identity.Name);
            repo.ChangeStatus(changeStatusViewModel.TaskId, changeStatusViewModel.Status, user);
        }
        [HttpGet]
        [Route("GetUserById")]
        public User GetUserById(int Id)
        {
            AccountRepository aRepo = new AccountRepository(_connectionString);
            return aRepo.GetById(Id);

        }
    }
}