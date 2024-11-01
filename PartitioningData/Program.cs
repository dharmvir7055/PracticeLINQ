using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Buffers.Text;
using System.Drawing;

namespace PartitioningData
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Partitioning in LINQ refers to the operation of dividing an input sequence into two sections, without rearranging the elements, and then returning one of the sections.
            //Method names    Description
            //Skip            Skips elements up to a specified position in a sequence.

            //SkipWhile       Skips elements based on a predicate function until an element doesn't satisfy the condition.

            //Take            Takes elements up to a specified position in a sequence.

            //TakeWhile       Takes elements based on a predicate function until an element doesn't satisfy the condition.

            //Chunk       Splits the elements of a sequence into chunks of a specified maximum size

            foreach (var item in Enumerable.Range(1, 10).Take(3))
            {
                Console.WriteLine(item);
            }  
            
            foreach (var item in Enumerable.Range(1, 10).Skip(4).Take(3))
            {
                Console.WriteLine(item);
            }

            foreach(var item in Enumerable.Range(1, 10).SkipWhile(i => i < 4).Take(3))
            {
                Console.WriteLine(item);
            }

            foreach (var chunk in Enumerable.Range(1,20).Chunk(4))
            {
                Console.WriteLine(string.Join(",",chunk));
            }

        }

    }
}