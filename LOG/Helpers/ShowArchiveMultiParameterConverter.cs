using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace LOG.Helpers;

public class ShowArchiveMultiParameterConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count != 2)
            return null;

        if (values[0] is not int LogId || values[1] is not string Content)
            return null;

        return new object[] { LogId, Content };
    }
}