using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DasBook.Model.Helpers;

public class EnumToStringConverter<T>() :
    ValueConverter<T, string>(
        v => v.ToString(),
        v => Enum.Parse<T>(v))
    where T : struct, Enum { }