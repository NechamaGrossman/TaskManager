using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskManager.Data
{
    public class UserTask
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public TaskStatus Status { get; set; }
        public int TakenUserId { get; set; }
        
    }
}
