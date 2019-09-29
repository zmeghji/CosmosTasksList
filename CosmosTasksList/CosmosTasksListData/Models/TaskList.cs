using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosTasksListData.Models
{
    public class TaskList
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public DateTime Date { get; set;}
        public int ProfileId { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
