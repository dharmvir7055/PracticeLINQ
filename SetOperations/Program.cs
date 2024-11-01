namespace SetOperations;

internal class Program
{
    //Set operations in LINQ refer to query operations that produce a result set based on the presence or absence of equivalent elements within the same or separate collections.
    private static void Main(string[] args)
    {
        //Method names                Description
        //Distinct or DistinctBy      Removes duplicate values from a collection.
        //Except or ExceptBy          Returns the set difference, which means the elements of one collection that don't appear in a second collection.
        //Intersect or IntersectBy    Returns the set intersection, which means elements that appear in each of two collections.
        //Union or UnionBy            Returns the set union, which means unique elements that appear in either of two collections.

        var departments = Sources.Departments;
        var students = Sources.Students;
        var teachers = Sources.Teachers;

        string[] words = ["the", "quick", "brown", "fox", "jumped", "over", "the", "lazy", "dog"];

        var distinctWords = from word in words.Distinct()
            select word;

        distinctWords = words.Distinct();
        distinctWords = words.DistinctBy(word => word.Length); // return words with different lengths

        string[] words1 = ["the", "quick", "brown", "fox"];
        string[] words2 = ["jumped", "over", "the", "lazy", "dog"];

        words1.Except(words2); //[ "quick", "brown", "fox"]

        int[] teachersToExclude =
        [
            901, // English
            965, // Mathematics
            932, // Engineering
            945, // Economics
            987, // Physics
            901 // Chemistry
        ];

        teachers.ExceptBy(teachersToExclude, teacher => teacher.ID);

        words1.Intersect(words2); //the

        teachers.IntersectBy(students.Select(st => (st.FirstName, st.LastName)),
            teacher => (teacher.First, teacher.Last)); //select only same names

        words1.Union(words2);
        /* This code produces the following output:
         *
         * the
         * quick
         * brown
         * fox
         * jumped
         * over
         * lazy
         * dog
         */

        var people = teachers.Select(tt => (FirstName: tt.First, LastName: tt.Last)).UnionBy(
            students.Select(st => (st.FirstName, st.LastName)),
            teacher => (teacher.FirstName, teacher.LastName)); //select all name but remove duplicats
    }
}