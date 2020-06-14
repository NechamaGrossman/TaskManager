using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;

namespace TaskManager.Web
{

    public class TaskHub : Hub
    {
        string _connectionString;
        public TaskHub(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public void SendRefresh()
        {
            TaskRepository repo = new TaskRepository(_connectionString);
            AccountRepository aRepo = new AccountRepository(_connectionString);
            var userEmail = Context.User.Identity.Name;
            User user = aRepo.GetByEmail(userEmail);
            List<UserTask> userTasks = repo.GetTasks(user);
            Clients.All.SendAsync("refresh", userTasks);
        }

    }
}
