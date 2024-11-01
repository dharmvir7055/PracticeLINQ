//From ms documentation
//https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/join-operations

//Method    Name	Description	                                                                                        C# Query Expression Syntax
//Join	    Joins   two sequences based on key selector functions and extracts pairs of values.	                        join … in … on … equals …

//GroupJoin	Joins   two sequences based on key selector functions and groups the resulting matches for each element.	join … in … on … equals … into …

//inner join- The inner join return records that have matching values in both collections.

public class Program
{
    public static void Main(string[] args)
    {
        #region Data Sources

        var departments = Sources.Departments;
        var students = Sources.Students;
        var teachers = Sources.Teachers;

        #endregion Data Sources

        #region Basic join

        //basic join

        var query = from student in Sources.Students
            join department in Sources.Departments on student.DepartmentID equals department.ID
            select new { Name = $"{student.FirstName} {student.LastName}", DepartmentName = department.Name };

        foreach (var item in query) Console.WriteLine($"{item.Name} - {item.DepartmentName}");

        //method syntax

        var methodSyn = Sources.Students.Join(Sources.Departments, st => st.DepartmentID, dp => dp.ID,
            (student, department) => new
                { Name = $"{student.FirstName} {student.LastName}", DepartmentName = department.Name });

        foreach (var item in methodSyn) Console.WriteLine($"{item.Name} - {item.DepartmentName}");

        #endregion Basic join

        #region Group join

        IEnumerable<IEnumerable<Student>> studentGroups = from department in Sources.Departments
            join student in Sources.Students on department.ID equals student.DepartmentID into studentGroup
            select studentGroup;

        foreach (var studentGroup in studentGroups)
        {
            Console.WriteLine("Group");
            foreach (var student in studentGroup) Console.WriteLine($"  - {student.FirstName}, {student.LastName}");
        }

        var departmentStudentGroups = Sources.Departments.GroupJoin(Sources.Students,
            department => department.ID, student => student.DepartmentID,
            (deparment, students) => new DeparmentStudentGroup(deparment.Name, students));

        foreach (var deparmentStudentGroup in departmentStudentGroups)
        {
            Console.WriteLine($"\nGroup:{deparmentStudentGroup.Name}\nStudents:\n");
            foreach (var student in deparmentStudentGroup.Students)
                Console.WriteLine($"Id:{student.ID},Name:{student.LastName}");
        }

        #endregion Group join

        #region Inner joins

        //Perform inner joins=>variations of an inner join
        //=>A simple inner join that correlates elements from two data sources based on a simple key.
        //=>An inner join that correlates elements from two data sources based on a composite key.A composite key, which is a key that consists of more than one value, enables you to correlate elements based on more than one property.
        //=>A multiple join in which successive join operations are appended to each other.
        //=>An inner join that is implemented by using a group join.

        //Single key join
        var innerquery = from department in departments
            join teacher in teachers on department.TeacherID equals teacher.ID
            select new
            {
                DepartmentName = department.Name,
                TeacherName = $"{teacher.First} {teacher.Last}"
            };

        foreach (var departmentAndTeacher in innerquery)
            Console.WriteLine(
                $"{departmentAndTeacher.DepartmentName} is managed by {departmentAndTeacher.TeacherName}");

        var methodinnerQuery = teachers
            .Join(departments, teacher => teacher.ID, department => department.TeacherID,
                (teacher, department) =>
                    new { DepartmentName = department.Name, TeacherName = $"{teacher.First} {teacher.Last}" });

        foreach (var departmentAndTeacher in methodinnerQuery)
            Console.WriteLine(
                $"{departmentAndTeacher.DepartmentName} is managed by {departmentAndTeacher.TeacherName}");

        //Composite key join

        var compositeKeyJoinResult = teachers.Join(students,
            teacher => new { FirstName = teacher.First, LastName = teacher.Last },
            student => new { student.FirstName, student.LastName },
            (teacher, student) => $"{teacher.First}|{student.LastName}");
        foreach (var item in compositeKeyJoinResult) Console.WriteLine(item);

        // Join the two data sources based on a composite key consisting of first and last name,
        // to determine which employees are also students.
        IEnumerable<string> compositeKeyJoinResult2 =
            from teacher in teachers
            join student in students on new
            {
                FirstName = teacher.First,
                LastName = teacher.Last
            } equals new
            {
                student.FirstName,
                student.LastName
            }
            select teacher.First + " " + teacher.Last;

        var result = "The following people are both teachers and students:\r\n";
        foreach (var name in compositeKeyJoinResult2) result += $"{name}\r\n";

        Console.Write(result);

        #endregion Inner joins

        #region Multiple join

        //Multiple join-Any number of join operations can be appended to each other to perform a multiple join. Each join clause in C# correlates a specified data source with the results of the previous join.

        //Here:
        // The first join matches Department.ID and Student.DepartmentID from the list of students and
        // departments, based on a common ID. The second join matches teachers who lead departments
        // with the students studying in that department.

        var multijoinquery = from student in students
            join department in departments on student.DepartmentID equals department.ID
            join teacher in teachers on department.TeacherID equals teacher.ID
            select new
            {
                StudentName = $"{student.FirstName} {student.LastName}",
                DepartmentName = department.Name,
                TeacherName = $"{teacher.First} {teacher.Last}"
            };

        foreach (var obj in multijoinquery)
            Console.WriteLine(
                $"""The student "{obj.StudentName}" studies in the {obj.DepartmentName} department run  by "{obj.TeacherName}".""");

        var multijoinquery2 = students
            .Join(departments, s => s.DepartmentID, dep => dep.ID, (stu, dep) => new { stu, dep })
            .Join(teachers, dep => dep.dep.TeacherID, teacher => teacher.ID, (stuDep, teacher) =>
                $"The student {stuDep.stu.FirstName} studies in the {stuDep.dep.Name} department run  by {teacher.First}"
            );

        foreach (var item in multijoinquery2) Console.WriteLine(item);

        #endregion Multiple join

        #region Inner join by using grouped join

        //Inner join by using grouped join

        var query1 =
            from department in departments
            join student in students on department.ID equals student.DepartmentID into gj
            from subStudent in gj
            select new
            {
                DepartmentName = department.Name,
                StudentName = $"{subStudent.FirstName} {subStudent.LastName}"
            };
        Console.WriteLine("Inner join using GroupJoin():");
        foreach (var v in query1) Console.WriteLine($"{v.DepartmentName} - {v.StudentName}");

        var queryMethod1 = departments
            .GroupJoin(students, d => d.ID, s => s.DepartmentID, (department, gj) => new { department, gj })
            //.SelectMany(dsl=>dsl.students );
            //.SelectMany((dsl, i) => dsl.students);
            .SelectMany(departmentAndStudent => departmentAndStudent.gj,
                (departmentAndStudent, subStudent) => new
                {
                    DepartmentName = departmentAndStudent.department.Name,
                    StudentName = $"{subStudent.FirstName} {subStudent.LastName}"
                });

        Console.WriteLine("Inner join using GroupJoin():");
        foreach (var v in queryMethod1) Console.WriteLine($"{v.DepartmentName} - {v.StudentName}");

        #endregion Inner join by using grouped join

        #region Select manyExamples

        List<string> phrases = ["an apple a day", "the quick brown fox"];

        //var words1=from phares in phrases
        //from word in phares.Split(" ")
        //select word;

        var words2 = phrases.SelectMany(phrase =>
            phrase.Split("")
        ).SelectMany(word => word.ToList()).Count(s => s.Equals('a'));

        //foreach (var charItem in words2)
        //{
        //    Console.WriteLine(charItem);
        //}

        #endregion

        #region Perform left outer joins

        var outerJoin = from student in students
            join department in departments on student.DepartmentID equals department.ID into studentgroup
            from subgroup in studentgroup.DefaultIfEmpty()
            select new
            {
                student.FirstName,
                student.LastName,
                Department = subgroup?.Name ?? string.Empty
            };

        foreach (var item in outerJoin) Console.WriteLine($"{item.FirstName}-{item.LastName}-{item.Department}");

        //method syntax

        var outerjoinWithMethod = students.GroupJoin(departments, student => student.DepartmentID,
                department => department.ID,
                (student, departmentList) => new { student, subgroup = departmentList.AsQueryable() })
            .SelectMany(joinedSet => joinedSet.subgroup.DefaultIfEmpty(), (student, department) => new
            {
                student.student.FirstName,
                student.student.LastName,
                Department = department?.Name ?? string.Empty
            });

        foreach (var v in outerjoinWithMethod) Console.WriteLine($"{v.FirstName:-15} {v.LastName:-15}: {v.Department}");

        #endregion Perform left outer joins
    }
}

internal class DeparmentStudentGroup
{
    public DeparmentStudentGroup(string name, IEnumerable<Student> students)
    {
        Name = name;
        Students = students;
    }

    public string Name { get; }
    public IEnumerable<Student> Students { get; }

    public override bool Equals(object? obj)
    {
        return obj is DeparmentStudentGroup other &&
               Name == other.Name &&
               EqualityComparer<IEnumerable<Student>>.Default.Equals(Students, other.Students);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Students);
    }
}