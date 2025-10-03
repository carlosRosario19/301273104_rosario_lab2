
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace _301273104_rosario_lab2.Services.Impl
{
    public class DynamoDBRepository : IRepository
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly string _tableName = "Bookshelf";

        public DynamoDBRepository(IAmazonDynamoDB dynamoDb)
        {
            _dynamoDb = dynamoDb;
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var request = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "PK", new AttributeValue { S = $"USER#{username}" } },
                    { "SK", new AttributeValue { S = "PROFILE" } }
                }
            };

            var response = await _dynamoDb.GetItemAsync(request);

            if (!response.IsItemSet)
                return false;

            var storedPassword = response.Item["password"].S;

            return storedPassword == password;
        }

        public async Task<List<Dictionary<string, AttributeValue>>> GetUserBooksOrderedAsync(string username)
        {
            var request = new QueryRequest
            {
                TableName = _tableName,
                IndexName = "PK-lastAccessed-index",
                KeyConditionExpression = "PK = :pk",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
        {
            { ":pk", new AttributeValue { S = $"USER#{username}" } }
        },
                ScanIndexForward = false // descending (latest first)
            };

            var response = await _dynamoDb.QueryAsync(request);

            return response.Items;
        }


    }
}
