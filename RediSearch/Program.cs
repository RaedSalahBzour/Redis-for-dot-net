using NRedisStack;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using NRedisStack.Search.Aggregation;
using NRedisStack.Search.Literals.Enums;
using StackExchange.Redis;

// Connect to localhost on port 6379
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost,allowAdmin=true");
var endpoints = redis.GetEndPoints();
var server = redis.GetServer(endpoints.FirstOrDefault());
server.FlushDatabase();

var db = redis.GetDatabase();
var ft = db.FT();
var json = db.JSON();

FillData(db);
SearchByNameAndAge(db);
SearchByNameAndReturnCity(db);
GroupByCity(db);

Console.ReadLine();

void FillData(IDatabase db)
{
    // Test data
    var user1 = new
    {
        name = "ahmed mohamed",
        email = "ahmed.mohamed@example.com",
        age = 42,
        city = "cairo"
    };

    var user2 = new
    {
        name = "ali mohamed",
        email = "ali.mohamed@example.com",
        age = 29,
        city = "alex"
    };

    var user3 = new
    {
        name = "hany ali",
        email = "hany.ali@example.com",
        age = 35,
        city = "alex"
    };

    // Define a RediSearch schema
    var schema = new Schema()
        .AddTextField(new FieldName("$.name", "name"))
        .AddTagField(new FieldName("$.city", "city"))
        .AddNumericField(new FieldName("$.age", "age"));

    // Create an index for JSON documents with prefix "user:"
    ft.Create("idx:users", new FTCreateParams().On(IndexDataType.JSON).Prefix("user:"), schema);

    // Store users in Redis as JSON
    json.Set("user:1", "$", user1);
    json.Set("user:2", "$", user2);
    json.Set("user:3", "$", user3);
}
void SearchByNameAndAge(IDatabase db)
{
    Console.WriteLine("**********SearchByNameAndAge*************");
    var res =
        ft.Search("idx:users",
        new Query("hany @age:[30 40]")).Documents.Select(x => x["json"]);
    Console.WriteLine(string.Join("\n"), res);
}

void SearchByNameAndReturnCity(IDatabase db)
{
    Console.WriteLine("**********SearchByNameAndReturnCity*************");
    var res_cities = ft.Search(
    "idx:users",
    new Query("hany").ReturnFields(new FieldName("$.city", "city"))).Documents
    .Select(x => x["city"]).ToList();

    // Print the results
    Console.WriteLine(string.Join(", ", res_cities));
}
void GroupByCity(IDatabase db)
{
    Console.WriteLine("************ GroupByCity ***************");

    var request = new AggregationRequest("*")
        .GroupBy("@city", Reducers.Count().As("count"));
    var result = ft.Aggregate("idx:users", request);

    for (var i = 0; i < result.TotalResults; i++)
    {
        var row = result.GetRow(i);
        Console.WriteLine($"{row["city"]} - {row["count"]}");
    }
}
