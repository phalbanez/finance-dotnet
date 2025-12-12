namespace Lib.ResultApp;

public class ResultFailureApp(string title, int status = 0)
{
    private readonly List<ResultErrorApp> _errors = [];

    public string Title { get; set; } = title;
    public int Status { get; set; } = status;

    public IDictionary<string, string[]> Errors { get => ToDictionary(); }

    public void AddError(string propertyName, string errorMessage)
    {
        _errors.Add(new ResultErrorApp(propertyName, errorMessage));
    }

    private Dictionary<string, string[]> ToDictionary()
    {
        return _errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            );
    }
}
