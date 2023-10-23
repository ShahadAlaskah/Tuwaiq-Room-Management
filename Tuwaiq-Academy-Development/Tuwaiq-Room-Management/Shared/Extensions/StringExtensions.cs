using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Shared.Extensions;

public static class StringExtensions
{
    public static string FixMobileNumber(this string number)
    {
        if (number.StartsWith("05")) number = "966" + number.Substring(1);

        return number;
    }

    public static string FixArabicNumber(this string number)
    {
        if (!Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft) return number;
        number = number.Replace("0", "٠");
        number = number.Replace("1", "١");
        number = number.Replace("2", "٢");
        number = number.Replace("3", "٣");
        number = number.Replace("4", "٤");
        number = number.Replace("5", "٥");
        number = number.Replace("6", "٦");
        number = number.Replace("7", "٧");
        number = number.Replace("8", "٨");
        number = number.Replace("9", "٩");

        return number;
    }

    public static string ValidateEmail(this string email)
    {
        var trimmedEmail = email.ToLower().Trim();
    
        if (trimmedEmail.EndsWith(".")) throw new Exception("InvalidEmail");
        try
        {
            var addr = new MailAddress(email);
            if (addr.Address == trimmedEmail) return trimmedEmail;
        }
        catch
        {
            throw new Exception("InvalidEmail");
        }
    
        throw new Exception("InvalidEmail");
    }

    public static bool IsEmail(this string email)
    {
        var trimmedEmail = email.ToLower().Trim();
    
        if (trimmedEmail.EndsWith(".")) throw new Exception("InvalidEmail");
        try
        {
            var addr = new MailAddress(email);
            if (addr.Address == trimmedEmail) return true;
        }
        catch
        {
            //
        }

        return false;
    }
    
    public static bool IsUrl(this string url)
    {
        try
        {
            var uri = new Uri(url);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsPhone(this string phoneNumber)
    {
        var trimmedPhoneNumber = phoneNumber.Trim();
        if (trimmedPhoneNumber.StartsWith("05")) trimmedPhoneNumber = "966" + trimmedPhoneNumber.Substring(1);

        var regix = new Regex(@"^9665\d{8}$");

        return regix.IsMatch(trimmedPhoneNumber);
    }
    
    // public static Result ValidateEmail(this string email)
    // {
    //     var trimmedEmail = email.ToLower().Trim();
    //
    //     if (trimmedEmail.EndsWith(".")) return Result.Fail(Error.Invalid("Email"));
    //     try
    //     {
    //         var addr = new MailAddress(email);
    //         if (addr.Address == trimmedEmail) return Result.Ok(trimmedEmail);
    //     }
    //     catch
    //     {
    //         //return Result.Fail(Error.Invalid("Email"));
    //     }
    //
    //     return Result.Fail(Error.Invalid("Email"));
    // }

    public static string ValidateComplexPassword(this string password)
    {
        var regix = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

        if (regix.IsMatch(password)) return password;

        throw new Exception("InvalidPassword");
    }
}