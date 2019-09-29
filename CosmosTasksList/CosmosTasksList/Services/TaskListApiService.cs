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
    }
    public class TaskListApiService : ITaskListApiService
    {
        private readonly IConfiguration _configuration;

        public TaskListApiService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task<TaskList> Create(TaskList taskList)
        {
            return await new TaskListRepository(
                _configuration.GetSection("CosmosServiceUri").Value,
                _configuration.GetSection("CosmosAuthKey").Value,
                _configuration.GetSection("CosmosDatabaseId").Value,
                _configuration.GetSection("CosmosContainerId").Value
                ).Create(taskList);
        }

        public async Task<List<TaskList>> Get()
        {
            return await new TaskListRepository(
                _configuration.GetSection("CosmosServiceUri").Value,
                _configuration.GetSection("CosmosAuthKey").Value,
                _configuration.GetSection("CosmosDatabaseId").Value,
                _configuration.GetSection("CosmosContainerId").Value
                ).Get();
        }
    }
}
