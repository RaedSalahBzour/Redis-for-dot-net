using StackExchange.Redis;
using System;

// Connect to Redis on localhost
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();

// Run the samples
ZADDSample(db);
ZREVRANGESample(db);
ZRANGEBYSCORESample(db);
ZRANKSample(db);
ZREVRANKSample(db);

// ---------------------- Methods ----------------------

void ZADDSample(IDatabase db)
{
    db.SortedSetAdd("Leaders10", new SortedSetEntry[]
    {
        new SortedSetEntry("mohamed", 5),
        new SortedSetEntry("ali", 6),
        new SortedSetEntry("khaled", 9),
        new SortedSetEntry("hany", 7)
    });

    var members = db.SortedSetRangeByRank("Leaders10", 0, -1);
    Console.WriteLine("ZADD + ZRANGE:");
    foreach (var member in members)
    {
        Console.WriteLine(member);
    }
}

void ZREVRANGESample(IDatabase db)
{
    var members = db.SortedSetRangeByRank("Leaders10", 0, -1, Order.Descending);
    Console.WriteLine("\nZREVRANGE:");
    foreach (var member in members)
    {
        Console.WriteLine(member);
    }
}

void ZRANGEBYSCORESample(IDatabase db)
{
    var members = db.SortedSetRangeByScore("Leaders10", 6, 9);
    Console.WriteLine("\nZRANGEBYSCORE 6 - 9:");
    foreach (var member in members)
    {
        Console.WriteLine(member);
    }
}

void ZRANKSample(IDatabase db)
{
    var rank = db.SortedSetRank("Leaders10", "hany");
    Console.WriteLine($"\nZRANK of hany: {rank}");
}

void ZREVRANKSample(IDatabase db)
{
    var rank = db.SortedSetRank("Leaders10", "hany", Order.Descending);
    Console.WriteLine($"\nZREVRANK of hany: {rank}");
}