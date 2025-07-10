namespace Finance.Config;

public class AppOptions
{
    public const string AppOptionsSection = "AppOptions";
    public string[] SupportedCultures { get; set; } = [];
}
