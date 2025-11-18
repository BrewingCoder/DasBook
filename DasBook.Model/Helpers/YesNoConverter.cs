using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DasBook.Model.Helpers;

public class YesNoConverter() :
    ValueConverter<bool, string>(v => v ? "Yes" : "No",
        v => v == "Yes");