using StackExchange.Redis;
using System;

// Connect to Redis on localhost
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();

SETBITSample(db);
BITCOUNTSample(db);
void SETBITSample(IDatabase db)
{
    db.StringSetBit("bitkey", 12, false);
    db.StringSetBit("bitkey", 13, true);
    db.StringSetBit("bitkey", 14, false);
    db.StringSetBit("bitkey", 25, true);

    Console.WriteLine($"Offset = 12, Value = {db.StringGetBit("bitkey", 12)}");
    Console.WriteLine($"Offset = 13, Value = {db.StringGetBit("bitkey", 13)}");
    Console.WriteLine($"Offset = 14, Value = {db.StringGetBit("bitkey", 14)}");
    Console.WriteLine($"Offset = 25, Value = {db.StringGetBit("bitkey", 25)}");

    Console.ReadLine();
}


void BITCOUNTSample(IDatabase db)
{
    db.StringSetBit("bitkey2", 12, false);
    db.StringSetBit("bitkey2", 13, true);
    db.StringSetBit("bitkey2", 14, false);
    db.StringSetBit("bitkey2", 25, true);

    long bitCount = db.StringBitCount("bitkey2");
    Console.WriteLine($"BITCOUNT = {bitCount}");

    Console.ReadLine();
}
