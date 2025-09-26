using StackExchange.Redis;

// Connect to localhost on port 6379.
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();

SimpleListSample(db);
SimpleListMoveSample(db);

void SimpleListSample(IDatabase db)
{
    db.ListLeftPush("users", "user1");
    db.ListLeftPush("users", "user2");

    Console.WriteLine("Users[0] = " + db.ListGetByIndex("users", 0));
    Console.WriteLine("Users[1] = " + db.ListGetByIndex("users", 1));

    db.ListRightPush("users", "user3");
    Console.WriteLine("Users[2] = " + db.ListGetByIndex("users", 2));

    Console.WriteLine("LPOP = " + db.ListLeftPop("users"));
    Console.WriteLine("RPOP = " + db.ListRightPop("users"));

    Console.WriteLine("LLEN = " + db.ListLength("users"));

    Console.ReadLine();
}

void SimpleListMoveSample(IDatabase db)
{
    db.ListLeftPush("list1", "a");
    db.ListLeftPush("list1", "b");
    db.ListLeftPush("list1", "c");

    db.ListLeftPush("list2", "x");
    db.ListLeftPush("list2", "y");
    db.ListLeftPush("list2", "z");

    db.ListMove("list1", "list2", ListSide.Right, ListSide.Left);

    Console.WriteLine("===== List 1 =====");
    var list1 = db.ListRange("list1", 0, -1);
    foreach (var item in list1)
    {
        Console.WriteLine(item.ToString());
    }

    Console.WriteLine("===== List 2 =====");
    var list2 = db.ListRange("list2", 0, -1);
    foreach (var item in list2)
    {
        Console.WriteLine(item.ToString());
    }

    Console.ReadLine();
}
