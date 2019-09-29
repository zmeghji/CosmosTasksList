using CosmosTasksListData.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosTasksListData.Repositories
{
    public interface ITaskListRepository
    {
        Task<TaskList> Create(TaskList taskList);
        Task<List<TaskList>> Get();
        Task<TaskList> Get(int profileId, DateTime date);
    }
    public class TaskListRepository : ITaskListRepository
    {
        public TaskListRepository(string cosmosServiceUri, string cosmosAuthKey, string cosmosDatabaseId, string cosmosContainerId)
        {
            _cosmosServiceUri = cosmosServiceUri;
            _cosmosAuthKey = cosmosAuthKey;
            _cosmosDatabaseId = cosmosDatabaseId;
            _cosmosContainerId = cosmosContainerId;
        }
        private readonly string _cosmosServiceUri;
        private readonly string _cosmosAuthKey;
        private readonly string _cosmosDatabaseId;
        private readonly string _cosmosContainerId;

        public async Task<TaskList> Create(TaskList taskList)
        {
            taskList.Id = Guid.NewGuid().ToString();
            using (var client = new CosmosClient(_cosmosServiceUri, _cosmosAuthKey))
            {
                var container = client.GetContainer(_cosmosDatabaseId, _cosmosContainerId);
                var resp = await container.CreateItemAsync<TaskList>(taskList);
                return resp.Resource;
            }
        }

        public async Task<List<TaskList>> Get()
        {
            using (var client = new CosmosClient(_cosmosServiceUri, _cosmosAuthKey))
            {
                var container = client.GetContainer(_cosmosDatabaseId, _cosmosContainerId);
                var feed =await container.GetItemQueryIterator<TaskList>("select * from c").ReadNextAsync();
                return feed.ToList();
            }
        }

        public async Task<TaskList> Get(int profileId, DateTime date)
        {
            using (var client = new CosmosClient(_cosmosServiceUri, _cosmosAuthKey))
            {
                var container = client.GetContainer(_cosmosDatabaseId, _cosmosContainerId);
                var feed = await container.GetItemLinqQueryable<TaskList>()
                    .Where(t => t.ProfileId == profileId && t.Date == date)
                    .ToFeedIterator()
                    .ReadNextAsync();
                    
                return feed.FirstOrDefault();
            }
        }
    }
}
