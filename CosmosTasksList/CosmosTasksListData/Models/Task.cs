using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosTasksListData.Models
{
    public class Task
    {
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
    }
}
