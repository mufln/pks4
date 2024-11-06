namespace pks4;

public class Dish
{
    private static int _idCounter;
    public int Id;
    public string Name;
    public string Ingredients;
    public double Price;
    public int CompleteTime;
    public Category Category;
    public List<Tag> Tags;

    public Dish(string name, string ingredients, double price, int completeTime, Category category, List<Tag> tags)
    {
        _idCounter += 1;
        Id = _idCounter;
        Name = name;
        Ingredients = ingredients;
        Price = price;
        CompleteTime = completeTime;
        Category = category;
        Tags = tags;
    }
    
    public void Delete(ref List<Dish> dishes)
    {
        dishes.Remove(this);
    }

    public void Update(string? name, string? ingredients, double? price, int? completeTime,
        Category? category, List<Tag>? tags)
    {
        if (name != null)
        {
            Name = name;
        }
        if (ingredients != null)
        {
            Ingredients = ingredients;
        }
        if (price != null)
        {
            Price = price.Value;
        }
        if (completeTime != null)
        {
            CompleteTime = completeTime.Value;
        }
        if (category != null)
        {
            Category = category.Value;
        }
        if (tags != null)
        {
            Tags = tags;
        }
    }

    public string GetFormattedData()
    {
        string tags = string.Join(", ", Tags);
        return $"{Name} - {Price}p - {CompleteTime} minutes - {Category}\n{Ingredients}\nTags: {tags}";
    }

    public static Dish GenerateDish()
    {
        List<string> names = ["Coke", "Caesar", "Meat medalions", "Chicken", "Beef", "Fish", "Tomatoes", "Apples"];
        List<string> ingredients = ["vegetables", "meat", "fish", "fruits", "grains", "nuts", "dairy", "oils", "spices", "sweets"];
        Random random = new Random();
        int categoryIndex = random.Next(0, Category.GetValues(typeof(Category)).Length);
        Category category = (Category)categoryIndex;
        string IngredientList = "";
        for (int i = 0; i < random.Next(1, 5); i++)
        {
            int ingredientIndex = random.Next(0, ingredients.Count);
            IngredientList += ingredients[ingredientIndex] + ", ";
        }
        double price = random.Next(1, 100);
        int completeTime = random.Next(1, 100);
        return new Dish(names[random.Next(0, names.Count)], IngredientList, price, completeTime, category, new List<Tag>());
        
    }
}