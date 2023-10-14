using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SukiUI.Controls;

public partial class MobileNumericUpDown : UserControl
{
    public static readonly DirectProperty<MobileNumericUpDown, int> ValueProperty =
        AvaloniaProperty.RegisterDirect<MobileNumericUpDown, int>(nameof(Value), numpicker => numpicker.Value,
            (numpicker, v) => numpicker.Value = v, defaultBindingMode: BindingMode.TwoWay, enableDataValidation: true);

    private int _value;

    public MobileNumericUpDown()
    {
        InitializeComponent();
    }

    public int Value
    {
        get => _value;
        set => SetAndRaise(ValueProperty, ref _value, value);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void ButtonPlus(object sender, RoutedEventArgs e)
    {
        Value = Value + 1;
    }

    private void ButtonMinus(object sender, RoutedEventArgs e)
    {
        Value = Value - 1;
    }
}

public class IntToStringConverter : IValueConverter
{
    public static readonly IntToStringConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value.ToString();
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}