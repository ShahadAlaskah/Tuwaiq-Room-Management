// using System.Text.RegularExpressions;
// using API.Shared.Domain;
//
// namespace API.Shared.ValueObjects;
//
// public class Email : ValueObject
// {
//     public Email(string emailAddress)
//     {
//         EmailAddress = emailAddress;
//     }
//
//     public string EmailAddress { get; }
//
//     public bool IsValid => ValidateEmail(EmailAddress);
//
//     protected override IEnumerable<object> GetEqualityComponents()
//     {
//         yield return EmailAddress.ToUpper();
//     }
//
//     public static Email Create(string email)
//     {
//         if (ValidateEmail(email))
//             return new Email(email);
//
//         throw new Exception("InvalidEmail");
//     }
//
//     public static bool ValidateEmail(string email)
//     {
//         var patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
//                             + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
//                             + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
//                             + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
//                             + @"[a-zA-Z]{2,}))$";
//
//         var regexStrict = new Regex(patternStrict);
//         return regexStrict.IsMatch(email);
//     }
// }