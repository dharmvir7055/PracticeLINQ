namespace ProjectionOperations;

internal partial class Program
{
    private static void Main(string[] args)
    {
        //Projection: refers to the operation of transforming an object into a new form that often consists only of those properties subsequently used.By using projection, you can construct a new type that is built from each object.You can project a property and perform a mathematical function on it.You can also project the original object without changing it.

        //Method names    Description                                                                                                               C# query expression syntax	                                                More information
        //Select          Projects values that are based on a transform function.	                                                                select                                                                      Enumerable.Select Queryable.Select
        //SelectMany      Projects sequences of values that are based on a transform function and then flattens them into one sequence.         	Use multiple from clauses                                                   Enumerable.SelectMany Queryable.SelectMany
        //Zip             roduces a sequence of tuples with elements from 2 - 3 specified sequences.	                                            Not applicable.	                                                            numerable.Zip Queryable.Zip
        Console.WriteLine("Hello, World!");

        //Select
        List<string> words = ["an", "apple", "a", "day"];
        var substrings = words.Select(word => word.Substring(0, 1));
        foreach (var item in substrings) Console.WriteLine(item);

        substrings = from word in words
            select word.Substring(0, 1);
        foreach (var item in substrings) Console.WriteLine(item);

        //SelectMany
        List<string> phrases = ["an apple a day", "the quick brown fox"];

        var query = from phrase in phrases
            from word in phrase.Split(' ')
            select word;

        foreach (var s in query) Console.WriteLine(s);

        // An int array with 7 elements.
        IEnumerable<int> numbers = [1, 2, 3, 4, 5, 6, 7];
        // A char array with 6 elements.
        IEnumerable<char> letters = ['A', 'B', 'C', 'D', 'E', 'F', 'G'];
        // form the combination using select many

        var numbersLettersCombination = from number in numbers
            from letter in letters
            select (number, letter);

        foreach (var item in numbersLettersCombination) Console.WriteLine($"{item.number}-{item.letter}");

        numbersLettersCombination = numbers.SelectMany(number => letters, (number, letter) => (number, letter));

        foreach (var item in numbersLettersCombination) Console.WriteLine($"{item.number}-{item.letter}");

        List<Bouquet> bouquets =
        [
            new() { Flowers = ["sunflower", "daisy", "daffodil", "larkspur"] },
            new() { Flowers = ["tulip", "rose", "orchid"] },
            new() { Flowers = ["gladiolis", "lily", "snapdragon", "aster", "protea"] },
            new() { Flowers = ["larkspur", "lilac", "iris", "dahlia"] }
        ];

        var query1 = bouquets.Select(bq => bq.Flowers);

        var query2 = bouquets.SelectMany(bq => bq.Flowers);

        Console.WriteLine("Results by using Select():");
        // Note the extra foreach loop here.
        foreach (IEnumerable<string> collection in query1)
        foreach (var item in collection)
            Console.WriteLine(item);

        Console.WriteLine("\nResults by using SelectMany():");
        foreach (var item in query2) Console.WriteLine(item);

        //Zip
        //The resulting sequence from a zip operation is never longer in length than the shortest sequence.The numbers and letters collections differ in length, and the resulting sequence omits the last element from the numbers collection, as it has nothing to zip with.
        // A string array with 8 elements.
        IEnumerable<string> emoji = ["🤓", "🔥", "🎉", "👀", "⭐", "💜", "✔", "💯"];
        IEnumerable<char> letters2 = ['A', 'B', 'C', 'D', 'E', 'F'];

        numbers.Zip(emoji);
        numbers.Zip(emoji, letters2);

        foreach (var result in
                 numbers.Zip(letters, (number, letter) => $"{number} = {letter} ({(int)letter})"))
            Console.WriteLine(result);
    }
}