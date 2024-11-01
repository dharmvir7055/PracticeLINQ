using System.Runtime.CompilerServices;

namespace QuantifierOperations;

public class Program
{
    public static void Main(string[] args)
    {
        //Quantifier operations return a Boolean value that indicates whether some or all of the elements in a sequence satisfy a condition.
        // Method Name	Description	C# Query Expression Syntax	More Information
        // All	        Determines whether all the elements in a sequence satisfy a condition.
        // Any	        Determines whether any elements in a sequence satisfy a condition.	Not applicable.	Enumerable.Any
        // Contains	    Determines whether a sequence contains a specified element.

        #region DataSources

        var teachers = Sources.Teachers;
        var students = Sources.Students;
        var departments = Sources.Departments;

        #endregion DataSources

        #region All

        // The following example uses the All to find students that scored above 70 on all exams.

        var studentWithMerit = students.Where(student => student.Scores.All(score => score > 70)).Select(student => (student.ID, marks: string.Join(",", student.Scores)));

        foreach (var student in studentWithMerit)
        {
            Console.WriteLine($"ID-{student.ID}|Marks:{student.marks}");
        }

        #endregion All

        #region Any

        var studentWith95Score = students.Where(student => student.Scores.Any(score => score > 95)).Select(student => (student.ID, marks: string.Join(",", student.Scores)));

        foreach (var student in studentWithMerit)
        {
            Console.WriteLine($"ID-{student.ID}|Marks:{student.marks}");
        }

        #endregion Any

        #region Contains

        IEnumerable<string> names = from student in students
                                    where student.Scores.Contains(95)
                                    select $"{student.FirstName} {student.LastName}: {string.Join(", ", student.Scores.Select(s => s.ToString()))}";

        foreach (string name in names)
        {
            Console.WriteLine($"{name}");
        }

        // This code produces the following output:
        //
        // Claire O'Donnell: 56, 78, 95, 95
        // Donald Urquhart: 92, 90, 95, 57

        #endregion Contains
    }
}