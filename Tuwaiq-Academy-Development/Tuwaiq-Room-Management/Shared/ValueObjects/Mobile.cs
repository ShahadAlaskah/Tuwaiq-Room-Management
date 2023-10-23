// using System.Diagnostics.CodeAnalysis;
// using API.Shared.Domain;
//
// namespace API.Shared.ValueObjects;
//
// public class Mobile : ValueObject
// {
//
//     [SetsRequiredMembers]
//     public Mobile(string value)
//     {
//         Value = value;
//     }
//
//     public required string Value { get;init; }
//
//     protected override IEnumerable<object> GetEqualityComponents()
//     {
//         yield return Value ;
//     }
// }