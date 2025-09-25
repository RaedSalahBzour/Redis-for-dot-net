using StackExchange.Redis;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Connect to localhost on port 6379.
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        IDatabase db = redis.GetDatabase();

        SimpleStringSample(db);
    }

    static void SimpleStringSample(IDatabase db)
    {
        // Store and retrieve a simple string.
        db.StringSet("ProductName", "Smart phone");
        Console.WriteLine(db.StringGet("ProductName")); // prints: Smart phone

        Console.ReadLine();
    }
}
