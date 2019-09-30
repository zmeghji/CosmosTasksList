using CosmosTasksList.Exceptions;
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
        Task<TaskList> Update(int profileId, DateTime date, TaskList taskList);
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

        public async Task<TaskList> Get(int profileId, DateTime date)
        {
            var taskList= await _taskListRepository.Get(profileId, date);
            if (taskList == null)
            {
                throw new ApiException("Task List Not Found", 404);
            }
            return taskList;
        }

        public async Task<TaskList> Update(int profileId, DateTime date, TaskList taskList)
        {
            var taskListFromDb = await _taskListRepository.Get(profileId, date);
            if (taskListFromDb == null)
            {
                throw new ApiException("Task List Not Found", 404);
            }
            return await _taskListRepository.Update(profileId, date, taskList);
        }
    }
}
