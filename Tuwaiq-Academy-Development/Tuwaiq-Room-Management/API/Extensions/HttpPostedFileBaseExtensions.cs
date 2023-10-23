namespace API.Extensions;

public static class HttpPostedFileBaseExtensions
{
    private static readonly IDictionary<string, string> ImageMimeDictionary = new Dictionary<string, string>
    {
        { ".bmp", "image/bmp" },
        { ".dib", "image/bmp" },
        { ".gif", "image/gif" },
        { ".svg", "image/svg+xml" },
        { ".jpe", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".jpg", "image/jpeg" },
        { ".png", "image/png" },
        { ".pnz", "image/png" }
    };
    
     public const int ImageMinimumBytes = 512;

     public static bool IsImage(this IFormFile file)
     {
         if (string.IsNullOrEmpty(file.FileName))
         {
             throw new ArgumentNullException(nameof(file));
         }

         var extension = Path.GetExtension(file.FileName);
         return ImageMimeDictionary.ContainsKey(extension.ToLower());
     }
     public static bool IsImage(this string file)
     {
         if (string.IsNullOrEmpty(file))
         {
             throw new ArgumentNullException(nameof(file));
         }

         var extension = Path.GetExtension(file);
         return ImageMimeDictionary.ContainsKey(extension.ToLower());
     }
    //  
    // public static bool IsImage(this IFormFile postedFile)
    // {
    //     //-------------------------------------------
    //     //  Check the image mime types
    //     //-------------------------------------------
    //     if (postedFile.ContentType.ToLower() != "image/jpg" &&
    //                 postedFile.ContentType.ToLower() != "image/jpeg" &&
    //                 postedFile.ContentType.ToLower() != "image/pjpeg" &&
    //                 postedFile.ContentType.ToLower() != "image/gif" &&
    //                 postedFile.ContentType.ToLower() != "image/x-png" &&
    //                 postedFile.ContentType.ToLower() != "image/png")
    //     {
    //         return false;
    //     }
    //
    //     //-------------------------------------------
    //     //  Check the image extension
    //     //-------------------------------------------
    //     if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
    //         && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
    //         && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
    //         && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
    //     {
    //         return false;
    //     }
    //
    //     //-------------------------------------------
    //     //  Attempt to read the file and check the first bytes
    //     //-------------------------------------------
    //     try
    //     {
    //         if (!postedFile.OpenReadStream().CanRead)
    //         {
    //             return false;
    //         }
    //         //------------------------------------------
    //         //check whether the image size exceeding the limit or not
    //         //------------------------------------------ 
    //         if (postedFile.Length < ImageMinimumBytes)
    //         {
    //             return false;
    //         }
    //
    //         byte[] buffer = new byte[ImageMinimumBytes];
    //         postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
    //         string content = System.Text.Encoding.UTF8.GetString(buffer);
    //         if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
    //             RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
    //         {
    //             return false;
    //         }
    //     }
    //     catch (Exception)
    //     {
    //         return false;
    //     }
    //
    //     //-------------------------------------------
    //     //  Try to instantiate new Bitmap, if .NET will throw exception
    //     //  we can assume that it's not a valid image
    //     //-------------------------------------------
    //
    //     try
    //     {
    //         using (var bitmap = new System.Drawing.Bitmap(postedFile.OpenReadStream()))
    //         {
    //         }
    //     }
    //     catch (Exception)
    //     {
    //         return false;
    //     }
    //     finally
    //     {
    //         postedFile.OpenReadStream().Position = 0;
    //     }
    //
    //     return true;
    // }
}