using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace SukiUI.Controls.TouchInput.TouchNumericPad;

public partial class TouchNumericPad : UserControl
{
    public static readonly DirectProperty<TouchNumericPad, double> ValueProperty =
        AvaloniaProperty.RegisterDirect<TouchNumericPad, double>(nameof(Value), numpicker => numpicker.Value,
            (numpicker, v) => numpicker.Value = v, defaultBindingMode: BindingMode.TwoWay, enableDataValidation: true);


    public static readonly StyledProperty<ScaleTransform> PopupScaleProperty =
        AvaloniaProperty.Register<TouchNumericPad, ScaleTransform>(nameof(TouchNumericPad), new ScaleTransform());

    public static readonly StyledProperty<int> PopupHeightProperty =
        AvaloniaProperty.Register<TouchNumericPad, int>(nameof(TouchNumericPad), 405);

    public static readonly StyledProperty<int> PopupWidthProperty =
        AvaloniaProperty.Register<TouchNumericPad, int>(nameof(TouchNumericPad), 300);

    private double _value;

    public TouchNumericPad()
    {
        InitializeComponent();
    }

    public double Value
    {
        get => _value;
        set
        {
            SetAndRaise(ValueProperty, ref _value, value);
            this.FindControl<TextBlock>("textValue").Text = Value.ToString();
        }
    }

    public ScaleTransform PopupScale
    {
        get => GetValue(PopupScaleProperty);
        set => SetValue(PopupScaleProperty, value);
    }

    public int PopupHeight
    {
        get => GetValue(PopupHeightProperty);
        set => SetValue(PopupHeightProperty, value);
    }

    public int PopupWidth
    {
        get => GetValue(PopupWidthProperty);
        set => SetValue(PopupWidthProperty, value);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OpenPopup(object sender, RoutedEventArgs e)
    {
        var dialog = new NumericPadPopUp
        {
            rootControl = this
        };

        dialog.RenderTransform = PopupScale;
        dialog.Height = PopupHeight;
        dialog.Width = PopupWidth;

        InteractiveContainer.ShowDialog(dialog, true);
    }
}