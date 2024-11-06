namespace pks4;

public class Order
{
    private int _idCounter;
    public int TableId;
    public int Id;
    public List<Dish> Dishes;
    public string Comment;
    public int Waiter;
    public string CreatedAt;
    public string ClosedAt;
    public double Total;

    public Order(int tableId, List<Dish> dishes, string comment, int waiter, string createdAt, string closedAt)
    {
        _idCounter++;
        TableId = tableId;
        Id = _idCounter;
        Dishes = dishes;
        Comment = comment;
        Waiter = waiter;
        CreatedAt = createdAt;
        ClosedAt = closedAt;
        Total = (Dishes).Sum(dish => dish.Price);
    }

    public void Delete(ref List<Order> orders)
    {
        orders.Remove(this);
    }

    public void Update(List<Dish>? dishes, string? comment, int? waiter, string? createdAt, string? closedAt)
    {
        if (dishes != null)
        {
            Dishes = dishes;
            Total = (Dishes).Sum(dish => dish.Price);
        }

        if (comment != null)
        {
            Comment = comment;
        }

        if (waiter != null)
        {
            Waiter = waiter.Value;
        }

        if (createdAt != null)
        {
            CreatedAt = createdAt;
        }

        if (closedAt != null)
        {
            ClosedAt = closedAt;
        }
    }

    public string GetReceipt()
    {
        if (ClosedAt == String.Empty) { return "no receipt"; }
        string res = $"Table:{TableId}\nWaiter:{Waiter}\nfrom: {CreatedAt} to: {ClosedAt} - {Comment}\n Total:{Total}";
        Dishes
            .GroupBy(entry => entry.Category)
            .ToList()
            .ForEach(
                category =>
                {
                    res += $"\n{category.Key} - {category.Sum(dish => dish.Price)}p";
                    category
                        .GroupBy(dish => dish.Name).ToList()
                        .ForEach(dish =>
                            res += $"\n{dish.Key} - {dish.Count()}*{dish.ToList()[0]}={dish.Sum(d => d.Price)}p");
                }
            );
        return res;
    }

    public string GetFormattedData()
    {
        string res = $"Table:{TableId}\nWaiter:{Waiter}\nfrom: {CreatedAt} to: {ClosedAt} - {Comment}\n Total:{Total}";
        Dishes.ForEach(dish => res += $"\n{dish.GetFormattedData()}");
        return res;
    }
    
    public void Close(string closedAt)
    {
        ClosedAt = closedAt;
    }
    
    public static void GenerateOrder(ref List<Order> orders, in List<Dish> dishes)
    {
        Random random = new Random();
        int waiter = random.Next(0, 100);
        string createdAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string closedAt = String.Empty;
        Order order = new Order(random.Next(0, 100), dishes, "test", waiter, createdAt, closedAt);
        orders.Add(order);
    }
}