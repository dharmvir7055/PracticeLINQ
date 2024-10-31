﻿//From ms documentation
//https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/join-operations

//Method    Name	Description	                                                                                        C# Query Expression Syntax
//Join	    Joins   two sequences based on key selector functions and extracts pairs of values.	                        join … in … on … equals …

//GroupJoin	Joins   two sequences based on key selector functions and groups the resulting matches for each element.	join … in … on … equals … into …

//inner join- The inner join return records that have matching values in both collections.



//Classes and Data
public enum GradeLevel
{
    FirstYear = 1,
    SecondYear,
    ThirdYear,
    FourthYear
};
