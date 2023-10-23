// using System.Diagnostics.CodeAnalysis;
// using API.Shared.Domain;
//
// namespace API.Shared.ValueObjects;
//
// // public record Name(string NameFirst,string NameFather,string NameGrandFather,string NameFamily);
//
// public class Name : ValueObject
// {
//     [SetsRequiredMembers]
//     public Name(string firstName, string familyName)
//     {
//         NameFirst = firstName;
//         NameFamily = familyName;
//     }
//
//     [SetsRequiredMembers]
//     public Name(string firstName, string fatherName, string familyName)
//     {
//         NameFirst = firstName;
//         NameFather = fatherName;
//         NameFamily = familyName;
//     }
//
//     [SetsRequiredMembers]
//     public Name(string firstName, string fatherName, string grandNameFather, string familyName)
//     {
//         NameFirst = firstName;
//         NameFather = fatherName;
//         NameGrandFather = grandNameFather;
//         NameFamily = familyName;
//     }
//
//     public required string NameFirst { get; init; }
//     public string NameFather { get; init; }
//     public string NameGrandFather { get; init; }
//     public required string NameFamily { get; init; }
//
//     protected override IEnumerable<object> GetEqualityComponents()
//     {
//         yield return NameFirst;
//         yield return NameFather;
//         yield return NameGrandFather;
//         yield return NameFamily;
//     }
// }