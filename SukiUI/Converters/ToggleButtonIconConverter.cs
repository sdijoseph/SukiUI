using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Material.Icons;

namespace SukiUI.Converters;

public class ToggleButtonIconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isChecked &&
            targetType.IsAssignableTo(typeof(MaterialIconKind)))
            return isChecked ? MaterialIconKind.ChevronLeft : MaterialIconKind.ChevronRight;

        throw new NotSupportedException("Invalid use of {nameof(ToggleButtonIconConverter)}");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}