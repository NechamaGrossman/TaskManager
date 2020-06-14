using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Data;

namespace TaskManager.Web.Models
{
    public class ChangeStatusViewModel
    {
        public TaskStatus Status { get; set; }
        public int TaskId { get; set; }
    }
}
