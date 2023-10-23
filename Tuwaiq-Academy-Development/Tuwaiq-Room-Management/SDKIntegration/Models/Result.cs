using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;
using System.Text.RegularExpressions;
using SDKIntegration.Models.Enums;

namespace SDKIntegration.Models;


public class Result
{
    protected Result(bool success, Error? error = null)
    {
        Success = success;
        Error = error;
        if (!(success || error != null)) throw new Exception("CannotBeSuccessAndErrorHasValue");

        if (!(!success || error == null)) throw new Exception("CannotBeNotSuccessAndErrorDoesNNotHaveValue");
    }

    protected Result(bool success, List<Error>? errors = null)
    {
        Success = success;
        Errors = errors;
        if (!(success || errors != null)) throw new Exception("CannotBeSuccessAndErrorHasValue");

        if (!(!success || errors == null)) throw new Exception("CannotBeNotSuccessAndErrorDoesNNotHaveValue");
    }

    public bool Success { get; }
    public Error? Error { get; }
    public List<Error>? Errors { get; } = new List<Error>();

    public bool Failure => !Success || Errors?.Any() == true || Error != null;

    public static Result Fail(Error? message)
    {
        return new Result(false, message);
    }

    public static Result Fail(List<Error>? messages)
    {
        return new Result(false, messages);
    }

    public static Result<T> Fail<T>(Error? error)
    {
        return new Result<T>(default, false, error);
    }

    public static Result<T> Fail<T>(List<Error>? errors)
    {
        return new Result<T>(default, false, errors);
    }

    public static Result Ok()
    {
        return new Result(true, error: default!);
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, true, error: default!);
    }

    public static Result Combine(params Result[] results)
    {
        foreach (var result in results)
            if (result.Failure)
                return result;

        return Ok();
    }
}

public class Result<T> : Result
{
    private T _value = default!;

    protected internal Result([AllowNull] T value, bool success, Error? error)
        : base(success, error)
    {
        if (!(value != null || !success)) throw new Exception("ValueMustHaveValueOrNotSuccess");

        Value = value!;
    }

    protected internal Result([AllowNull] T value, bool success, List<Error>? errors)
        : base(success, errors)
    {
        if (!(value != null || !success)) throw new Exception("ValueMustHaveValueOrNotSuccess");

        Value = value!;
    }

    public T Value
    {
        get
        {
            if (!Success) throw new Exception("MustBeSuccess");

            return _value;
        }
        [param: AllowNull] private set => _value = value;
    }
}


public class Error
{
    public Error(string errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public string ErrorCode { get; }
    public string ErrorMessage { get; }
    public string RequestId { get; set; } = null!;

    public static Error Empty()
    {
        return new Error(string.Empty, string.Empty);
    }

    public static Error Create(string errorCode, string errorMessage)
    {
        return new Error(errorCode, errorMessage);
    }

    public static Error Create(string errorMessage)
    {
        return new Error("Error", errorMessage);
    }

    public static Error Invalid(string errorMessage)
    {
        return new Error("Invalid", errorMessage);
    }
    public static Error Required(string errorMessage)
    {
        return new Error("Invalid", errorMessage);
    }
    public static Error Already(string errorMessage)
    {
        return new Error("Already", errorMessage);
    }
    public static Error Exist(string errorMessage)
    {
        return new Error("Exist", errorMessage);
    }

    public static Error NotFound(string errorMessage)
    {
        return new Error("NotFound", errorMessage);
    }
    
    public static int GetStatus(Error error)
    {
        return error.ErrorCode switch
        {
            "Invalid" => 406,
            "NotFound" => 404,
            "Already" => 409,
            "Exist" => 409,
            _ => 400
        };
    }
}


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
    
    public static NationIdType NationalIdCheck(string id)
    {
        id = id.Trim();
        if (!Regex.IsMatch(id, @"[0-9]+"))
        {
            return NationIdType.Invalid;
        }

        if (id.Length != 10)
        {
            return NationIdType.Invalid;
        }

        int type = (int)(id[0] - '0');
        if (type != 2 && type != 1)
        {
            return NationIdType.Invalid;
        }

        int sum = 0;
        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
            {
                string zfOdd = ((int)(id[i] - '0') * 2).ToString().PadLeft(2, '0');
                sum += (int)(zfOdd[0] - '0') + (int)(zfOdd[1] - '0');
            }
            else
            {
                sum += (int)(id[i] - '0');
            }
        }

        return (sum % 10 != 0) ? NationIdType.Invalid : (NationIdType)type;
    }
    
    public static bool IsNationalId(this string id)
    {
        return NationalIdCheck(id) != NationIdType.Invalid;
    }
}