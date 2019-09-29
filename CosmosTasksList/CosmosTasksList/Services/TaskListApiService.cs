using CosmosTasksListData.Models;
using CosmosTasksListData.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosTasksList.Services
{
    public interface ITaskListApiService
    {
        Task<TaskList> Create(TaskList taskList);
        Task<List<TaskList>> Get();
        Task<TaskList> Get(int profileId, DateTime date);
    }
    public class TaskListApiService : ITaskListApiService
    {
        private readonly IConfiguration _configuration;
        private readonly ITaskListRepository _taskListRepository;
        public TaskListApiService(IConfiguration configuration)
        {
            this._configuration = configuration;
            _taskListRepository = new TaskListRepository(
                _configuration.GetSection("CosmosServiceUri").Value,
                _configuration.GetSection("CosmosAuthKey").Value,
                _configuration.GetSection("CosmosDatabaseId").Value,
                _configuration.GetSection("CosmosContainerId").Value
                );
        }
        public async Task<TaskList> Create(TaskList taskList)
        {
            return await _taskListRepository.Create(taskList);
        }

        public async Task<List<TaskList>> Get()
        {
            return await _taskListRepository.Get();
        }

        public Task<TaskList> Get(int profileId, DateTime date)
        {
            return _taskListRepository.Get(profileId, date);
        }
    }
}
