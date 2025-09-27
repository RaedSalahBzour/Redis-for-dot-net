using StackExchange.Redis;
using System;

// Connect to Redis on localhost
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();

HyperLogLogSample(db);
void HyperLogLogSample(IDatabase db)
{
    bool ahmedResult = db.HyperLogLogAdd("user:2023:12:20", "ahmed");
    bool aliResult = db.HyperLogLogAdd("user:2023:12:20", "ali");
    bool ahmed2Result = db.HyperLogLogAdd("user:2023:12:20", "ahmed");

    Console.WriteLine($"PFADD user:2023:12:20 ahmed = {ahmedResult}");
    Console.WriteLine($"PFADD user:2023:12:20 ali = {aliResult}");
    Console.WriteLine($"PFADD user:2023:12:20 ahmed (duplicate) = {ahmed2Result}");

    long count = db.HyperLogLogLength("user:2023:12:20");
    Console.WriteLine($"PFCOUNT user:2023:12:20 = {count}");

    Console.ReadLine();
}