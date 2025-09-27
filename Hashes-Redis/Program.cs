using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;


var redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();

IJsonCommands json = db.JSON();

HSetSample(db);
HGetAllSample(db);
HMGETSample(db);
HINCRBYSample(db);

void HSetSample(IDatabase db)
{
    // HSET user:3 Name "ahmed" Age 25
    db.HashSet("user:3", new HashEntry[]
    {
        new HashEntry("Name", "ahmed"),
        new HashEntry("Age", 25)
    });

    // HGET user:3 Name
    var name = db.HashGet("user:3", "Name");

    Console.WriteLine($"Name: {name}");
    Console.ReadLine();
}
void HGetAllSample(IDatabase db)
{
    var values = db.HashGetAll("user:3");

    Console.WriteLine("---- HGETALL ----");
    foreach (var entry in values)
    {
        Console.WriteLine($"{entry.Name} = {entry.Value}");
    }

    Console.ReadLine();
}
void HMGETSample(IDatabase db)
{
    var values = db.HashGet("user:3", new RedisValue[] { "Name", "Age" });

    Console.WriteLine("---- HMGET ----");
    Console.WriteLine($"Name = {values[0]}");
    Console.WriteLine($"Age = {values[1]}");

    Console.ReadLine();
}
void HINCRBYSample(IDatabase db)
{
    var newAge = db.HashIncrement("user:3", "Age", 5);
    Console.WriteLine(newAge);
    Console.ReadLine();
}