using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;

namespace SukiUI.Controls;

public partial class CircleProgressBar : UserControl
{
    /// <summary>
    ///     Defines the <see cref="Value" /> property.
    /// </summary>
    public static readonly DirectProperty<CircleProgressBar, int> ValueProperty =
        AvaloniaProperty.RegisterDirect<CircleProgressBar, int>(
            nameof(Value),
            o => o.Value,
            (o, v) => o.Value = v,
            defaultBindingMode: BindingMode.OneWay,
            enableDataValidation: true);


    public static readonly StyledProperty<int> HeightProperty =
        AvaloniaProperty.Register<CircleProgressBar, int>(nameof(Height), 150);

    public static readonly StyledProperty<int> WidthProperty =
        AvaloniaProperty.Register<CircleProgressBar, int>(nameof(Width), 150);

    public static readonly StyledProperty<int> StrokeWidthProperty =
        AvaloniaProperty.Register<CircleProgressBar, int>(nameof(StrokeWidth), 10);

    private int _value = 50;

    public CircleProgressBar()
    {
        InitializeComponent();
    }

    public int Value
    {
        get => _value;
        set => SetAndRaise(ValueProperty, ref _value, (int)(value * 3.6));
    }

    public int Height
    {
        get => GetValue(HeightProperty);
        set => SetValue(HeightProperty, value);
    }

    public int Width
    {
        get => GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }

    public int StrokeWidth
    {
        get => GetValue(StrokeWidthProperty);
        set => SetValue(StrokeWidthProperty, value);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}