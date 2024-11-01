namespace JoinsInLINQ;

internal class Pet
{
    public string Name { get; set; }
    public int Age { get; set; }

    public static void DefaultIfEmptyEx()
    {
        var defaultPet = new Pet { Name = "Default Pet", Age = 0 };

        var pets1 =
            new List<Pet>
            {
                new() { Name = "Barley", Age = 8 },
                new() { Name = "Boots", Age = 4 },
                new() { Name = "Whiskers", Age = 1 }
            };

        foreach (var pet in pets1.DefaultIfEmpty(defaultPet)) Console.WriteLine("Name: {0}", pet.Name);

        var pets2 = new List<Pet>();

        foreach (var pet in pets2.DefaultIfEmpty(defaultPet)) Console.WriteLine("\nName: {0}", pet.Name);

        foreach (var pet in pets2.DefaultIfEmpty()) Console.WriteLine("\nName: {0}", pet.Name);
    }
}