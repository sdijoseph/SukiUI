using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;

namespace SukiUI.Controls;

public partial class WaveProgress : UserControl
{
    public static readonly DirectProperty<WaveProgress, int> ValueProperty =
        AvaloniaProperty.RegisterDirect<WaveProgress, int>(
            nameof(Value),
            o => o.Value,
            (o, v) => o.Value = v,
            defaultBindingMode: BindingMode.TwoWay,
            enableDataValidation: true);

    public static readonly StyledProperty<bool> IsTextVisibleProperty =
        AvaloniaProperty.Register<WaveProgress, bool>(nameof(IsTextVisible), true);

    private int _value = 50;

    public WaveProgress()
    {
        InitializeComponent();
    }

    public int Value
    {
        get => _value;
        set
        {
            if (value is < 0 or > 100)
                return;

            SetAndRaise(ValueProperty, ref _value, value);
        }
    }

    public bool IsTextVisible
    {
        get => GetValue(IsTextVisibleProperty);
        set => SetValue(IsTextVisibleProperty, value);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}