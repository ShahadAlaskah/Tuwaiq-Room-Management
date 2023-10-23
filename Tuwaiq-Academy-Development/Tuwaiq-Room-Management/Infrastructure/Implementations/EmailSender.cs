// using FluentEmail.Core;
// using Microsoft.AspNetCore.Identity.UI.Services;
//
// namespace API.Infrastructure.Implementations;
//
// public class EmailSender: IEmailSender
// {
//     private readonly IFluentEmail _fluentEmail;
//
//     public EmailSender(IFluentEmail fluentEmail)
//     {
//         _fluentEmail = fluentEmail;
//     }
//     
//     public Task SendEmailAsync(string email, string subject, string htmlMessage)
//     {  
//         return _fluentEmail!.To(email)
//         .Subject(subject)
//         .Body(htmlMessage).SendAsync();
//     }
// }