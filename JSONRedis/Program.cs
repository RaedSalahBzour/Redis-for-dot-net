using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();

IJsonCommands json = db.JSON();

SampleJSON(db);
void SampleJSON(IDatabase db)
{
    json.Set("animal", "$", "\"dog\"");

    Console.WriteLine("animal value = " + json.Get("animal"));
    Console.WriteLine("animal type = " + json.Type("animal").FirstOrDefault());
    Console.WriteLine("animal length = " + json.StrLen("animal").FirstOrDefault());

    Console.ReadLine();
}