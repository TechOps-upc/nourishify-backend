using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace nourishify.api.Shared.Converters;

public class StringListConverter : ValueConverter<List<string>, string>
{
    private const string Delimiter = ",";

    public StringListConverter() 
        : base(
            v => ConvertToString(v),
            v => ConvertToList(v))
    { }

    private static string ConvertToString(List<string> values)
    {
        return values != null ? string.Join(Delimiter, values) : null;
    }

    private static List<string> ConvertToList(string value)
    {
        return value != null ? new List<string>(value.Split(new[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries)) : null;
    }
}