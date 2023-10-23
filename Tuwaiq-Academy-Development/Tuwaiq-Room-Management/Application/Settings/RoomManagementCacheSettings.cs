using System.ComponentModel.DataAnnotations;

namespace Application.Settings;

public class RoomManagementCacheSettings
{
    public bool UseCache { get; set; } = true;

    [Range(0, 1000000,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int ExpireSeconds { get; set; } = TimeSpan.FromMinutes(10).Seconds;
}