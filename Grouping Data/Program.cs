using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace Grouping_Data
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Grouping refers to the operation of putting data into groups so that the elements in each group share a common attribute. The following illustration shows the results of grouping a sequence of characters. The key for each group is the character.

            //Method Name     Description                                                                                             C# Query Expression Syntax	More Information
            //GroupBy         Groups elements that share a common attribute. An IGrouping<TKey, TElement> object represents each group.	group … by -or -group … by … into …
            //ToLookup        Inserts elements into a Lookup<TKey, TElement>(a one - to - many dictionary) based on a key selector function.

            #region Data Sources

            var departments = Sources.Departments;
            var students = Sources.Students;
            var teachers = Sources.Teachers;

            #endregion Data Sources

            #region GroupBy

            //Group even and odd numbers

            List<int> numbers = [35, 44, 200, 84, 3987, 4, 199, 329, 446, 208];

            IEnumerable<IGrouping<int, int>> grouped = numbers.GroupBy(number => number % 2);//group
            var groupedAndDouble = numbers.GroupBy(s => s % 2, g => g * 2);//group and double each element
            var query = numbers.GroupBy(number => number % 2, (key, items) => (key, string.Join(",", items)));
            foreach (var group in groupedAndDouble)
            {
                Console.WriteLine(group.Key);
                Console.WriteLine(string.Join(",", group));
            }
            foreach (var group in grouped)
            {
                Console.WriteLine(group.Key);
                Console.WriteLine(string.Join(",", group));
            }

            #endregion GroupBy

            #region Group by single property example

            students.GroupBy(student => student.Year).OrderBy(group => group.Key);
            #endregion

            #region Group by value example
            //In this example, the key is the first letter of the student's family name.

            students.GroupBy(student => student.LastName[0]);
            #endregion

            #region Group by a range example
            //The following example shows how to group source elements by using a numeric range as a group key. The query then projects the results into an anonymous type that contains only the first and family name and the percentile range to which the student belongs.An anonymous type is used because it isn't necessary to use the complete Student object to display the results. GetPercentile is a helper function that calculates a percentile based on the student's average score.The method returns an integer between 0 and 10.

            static int GetPercentile(Student s)
            {
                double avg=s.Scores.Average();
                return avg > 0 ? (int)avg / 10 : 0;
            }

            var gr=students.GroupBy(s => GetPercentile(s));

            #endregion



        }
    }
}