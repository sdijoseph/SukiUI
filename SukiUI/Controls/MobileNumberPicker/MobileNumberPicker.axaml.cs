using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SukiUI.Controls.MobileNumberPicker;

public partial class MobileNumberPicker : UserControl
{
    /// <summary>
    ///     Defines the <see cref="Value" /> property.
    /// </summary>
    public static readonly DirectProperty<MobileNumberPicker, int> ValueProperty =
        AvaloniaProperty.RegisterDirect<MobileNumberPicker, int>(
            nameof(Value),
            o => o.Value,
            (o, v) => o.Value = v,
            defaultBindingMode: BindingMode.TwoWay,
            enableDataValidation: true);

    /// <summary>
    ///     Defines the <see cref="Minimum" /> property.
    /// </summary>
    public static readonly StyledProperty<int> MinimumProperty =
        AvaloniaProperty.Register<MobileNumberPicker, int>(nameof(Minimum), 0);

    /// <summary>
    ///     Defines the <see cref="Maximum" /> property.
    /// </summary>
    public static readonly StyledProperty<int> MaximumProperty =
        AvaloniaProperty.Register<MobileNumberPicker, int>(nameof(Maximum), 100);

    private int _value;

    public MobileNumberPicker()
    {
        InitializeComponent();
    }

    /// <summary>
    ///     Gets the current value.
    /// </summary>
    public int Value
    {
        get => _value;
        set => SetAndRaise(ValueProperty, ref _value, value);
    }

    /// <summary>
    ///     Gets or sets the minimum allowed value.
    /// </summary>
    public int Minimum
    {
        get => GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    /// <summary>
    ///     Gets or sets the maximum allowed value.
    /// </summary>
    public int Maximum
    {
        get => GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OpenPopup(object sender, RoutedEventArgs e)
    {
        var control = new MobileNumberPickerPopup(this);

        InteractiveContainer.ShowDialog(control, true);
    }
}