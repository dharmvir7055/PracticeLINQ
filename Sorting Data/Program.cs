namespace Sorting_Data;

internal class Program
{
    private static void Main(string[] args)
    {
        //A sorting operation orders the elements of a sequence based on one or more attributes.The first sort criterion performs a primary sort on the elements.By specifying a second sort criterion, you can sort the elements within each primary sort group.
        //Method Name         Description                                                           C# Query Expression Syntax	More Information
        //OrderBy             orts values in ascending order.	                                    orderby
        //OrderByDescending   Sorts values in descending order.	                                    orderby … descending
        //ThenBy              Performs a secondary sort in ascending order.	                        orderby …, …
        //ThenByDescending    Performs a secondary sort in descending order.	                    orderby …, … descending
        //Reverse             Reverses the order of the elements in a collection.                 	Not applicable.

        #region Data Sources

        var departments = Sources.Departments;
        var students = Sources.Students;
        var teachers = Sources.Teachers;

        //Primary Ascending Sort
        var sortedTeachers = from teacher in teachers
            orderby teacher.Last
            select teacher.Last;
        foreach (var teacher in sortedTeachers) Console.WriteLine(teacher);

        sortedTeachers = teachers.OrderBy(teacher => teacher.Last).Select(teacher => teacher.Last);

        #endregion

        // Primary Descending Sort

        sortedTeachers = from teacher in teachers
            orderby teacher.Last descending
            select teacher.Last;

        teachers.OrderByDescending(teacher => teacher.Last).Select(teacher => teacher.Last);

        #region Secondary Descending Sort

        var secondarySortedTeachers = from teacher in teachers
            orderby teacher.City, teacher.Last descending
            select (teacher.City, teacher.Last);


        secondarySortedTeachers = teachers.OrderByDescending(teacher => teacher.City)
            .ThenByDescending(teacher => teacher.Last).Select(teacher => (teacher.City, teacher.Last));

        #endregion
    }
}