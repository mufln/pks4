// See https://aka.ms/new-console-template for more information

using pks4;

void Main()
{
    List<Dish> dishes = new List<Dish>();
    
    List<Order> orders = new List<Order>();
    
    Console.WriteLine("1. Add dishes");
    Console.WriteLine("2. Add orders");
    Console.WriteLine("3. Show menu");
    Console.WriteLine("4. Show total of closed orders");
    Console.WriteLine("5. Show orders closed by waiter");
    Console.WriteLine("6. Show statistics");
    Console.WriteLine("7. Exit");
    while (true)
    {
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                for (int i = 0; i < 10; i++)
                {
                    dishes.Add(Dish.GenerateDish());
                }
                break;
            case "2":
                for (int i = 0; i < 10; i++)
                {
                    Random random = new Random();
                    int dishesInOrder = random.Next(1, dishes.Count);
                    List<Dish> dishesInOrderList = new List<Dish>();
                    for (int j = 0; j < dishesInOrder; j++)
                    {
                        int dishIndex = random.Next(0, dishes.Count);
                        dishesInOrderList.Add(dishes[dishIndex]);
                    }
                    Order.GenerateOrder(ref orders, dishesInOrderList);
                }
                break;
            case "3":
                dishes.ForEach(dish =>
                {
                    Console.WriteLine(dish.GetFormattedData());
                    Console.WriteLine("");
                });
                break;
            case "4":
                Console.WriteLine($"Total: {orders.
                    Where(order => order.ClosedAt != String.Empty)
                    .ToList()
                    .Sum(order => order.Total)}");
                break;
            case "5":
                int waiter = Int32.Parse(Console.ReadLine());
                Console.WriteLine($"Waiter: {orders.Where(order => order.ClosedAt != String.Empty)
                    .ToList()
                    .Where(order => order.Waiter == waiter)
                    .Sum(order => order.Total)}");
                break;
            case "6":
                Console.WriteLine("Statistics");
                Console.WriteLine($"Total orders = {orders.Count}");
                Dictionary<int, int> dishCount = new Dictionary<int, int>();
                foreach (var item in dishes)
                {
                    dishCount.Add(item.Id, 0);
                }
                orders.ForEach(order => order.Dishes.ForEach(dish => dishCount[dish.Id] += 1));
                foreach (var item in dishCount)
                {
                    string dishName = dishes.Where(dish => dish.Id == item.Key).First().Name;
                    Console.WriteLine($"{dishName}: {item.Value}");
                }
                break;
            case "7":
                break;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }
}

Main();