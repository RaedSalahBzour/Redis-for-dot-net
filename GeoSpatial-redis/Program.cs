using Newtonsoft.Json;
using StackExchange.Redis;
using System;

// Connect to Redis on localhost
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();

GeoAddSample(db);

void GeoAddSample(IDatabase db)
{
    db.GeoAdd("stations", -122.27652, 37.805186, "station:1");
    db.GeoAdd("stations", -122.2674626, 37.8062344, "station:2");
    db.GeoAdd("stations", -122.2469854, 37.8104049, "station:3");

    var results = db.GeoRadius(
        "stations",
        -122.2612767,
        37.7936847,
        5,
        GeoUnit.Kilometers,
        options: GeoRadiusOptions.WithDistance
    );

    Console.WriteLine("---- Nearby Stations ----");
    foreach (var entry in results)
    {
        Console.WriteLine(
            $"Station = {JsonConvert.SerializeObject(entry.Member)}, " +
            $"Distance = {entry.Distance} KM"
        );
    }

    Console.ReadLine();
}
