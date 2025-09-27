using StackExchange.Redis;
using System;

// Connect to Redis on localhost
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();

SubscribeSample(db);

void SubscribeSample(IDatabase db)
{
    var subscriber = redis.GetSubscriber();

    subscriber.Subscribe("channel2", (channel, message) =>
    {
        Console.WriteLine($"message recieved = {message}");
    });

    Console.ReadLine();
}