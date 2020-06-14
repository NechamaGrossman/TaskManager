using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;

namespace TaskManager.Web.Models
{
    public class UserTaskViewModel: UserTask
    {
        public bool TakenByThisUser;
    }
}
