namespace FilterData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] words = ["the", "quick", "brown", "fox", "jumps"];

            var wordsWith3length=words.Where(word => word.Length == 3);
            foreach (var word in wordsWith3length) {
                Console.WriteLine(word);
            }

            var wordsWith3length2=from word in words
                                  where word.Length == 3
                                  select word;


        }
    }
}
