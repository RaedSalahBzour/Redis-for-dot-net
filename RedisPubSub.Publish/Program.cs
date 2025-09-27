using StackExchange.Redis;
using System;

// Connect to Redis on localhost
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();


PublishSample(db);

void PublishSample(IDatabase db)
{
    while (true)
    {
        Console.WriteLine("Enter your message here:");
        string message = Console.ReadLine();

        if (string.IsNullOrEmpty(message))
        {
            Console.WriteLine("Empty message, exiting...");
            break;
        }

        db.Publish("channel2", message);
        Console.WriteLine($"Message published: {message}");
    }
}