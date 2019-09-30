using CosmosTasksList.Services;
using CosmosTasksListData.Models;
using CosmosTasksListData.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosTasksList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskListController: ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITaskListApiService _taskListApiService;

        public TaskListController(IConfiguration configuration, ITaskListApiService taskListApiService)
        {
            _configuration = configuration;
            _taskListApiService = taskListApiService;
        }

        [HttpGet]
        [Route("/profile/{profileId}/tasklist/{date}")]
        public async Task<TaskList> Get(int profileId, DateTime date)
        { 
            date = new DateTime(date.Year, date.Month, date.Day);
            return await _taskListApiService.Get(profileId, date);
        }

        [HttpGet]
        public async Task<List<TaskList>> Get()
        {
            return await _taskListApiService.Get();
        }
        [HttpPost]
        public async Task<TaskList> Post(TaskList taskList)
        {
            return await _taskListApiService.Create(taskList);
        }
    }
}
