using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskManager.Data
{
    public enum TaskStatus
    {
        NotTaken,
        Taken, 
        Completed,
        [NotMapped]
        TakenByThisUser
}
}
