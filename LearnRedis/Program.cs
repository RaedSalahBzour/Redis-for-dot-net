using StackExchange.Redis;
using System;



// Connect to localhost on port 6379.
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();

SimpleStringSample(db);
IncrementSample(db);
IncrementBySample(db);
GETSETSample(db);
MSETSample(db);

void SimpleStringSample(IDatabase db)
{
    // Store a simple string.
    db.StringSet("ProductName", "Smart phone");
    //Retrieve a simple string
    Console.WriteLine(db.StringGet("ProductName"));

    Console.ReadLine();
}
void IncrementSample(IDatabase db)
{
    // Store a simple string.
    db.StringSet("Count", 10);
    //Retrieve a simple string
    Console.WriteLine(db.StringIncrement("Count"));

    Console.ReadLine();
}
void IncrementBySample(IDatabase db)
{
    // Store a simple string.
    db.StringSet("Count2", 10);
    //Retrieve a simple string
    Console.WriteLine(db.StringIncrement("Count2", 4));

    Console.ReadLine();
}

void GETSETSample(IDatabase db)
{
    Console.WriteLine(db.StringGetSet("name", "ahmed"));
    Console.WriteLine(db.StringGetSet("name", "ali"));
    Console.ReadLine();
}

// MSET  key1 value1  key2 value2  key3 value3
void MSETSample(IDatabase db)
{
    db.StringSet(new KeyValuePair<RedisKey, RedisValue>[]
    {
        new("key:1", "value1"),
        new("key:2", "value2"),
        new("key:3", "value3")
    });

    var redisValues = db.StringGet(new RedisKey[] { "key:1", "key:2", "key:3" });
    foreach (var val in redisValues)
    {
        Console.WriteLine(val.ToString());
    }
}
