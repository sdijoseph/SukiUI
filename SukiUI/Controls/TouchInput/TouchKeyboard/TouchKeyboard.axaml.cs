using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace SukiUI.Controls.TouchInput.TouchKeyboard;

public partial class TouchKeyboard : UserControl
{
    public static readonly StyledProperty<ScaleTransform> PopupScaleProperty =
        AvaloniaProperty.Register<TouchKeyboard, ScaleTransform>(nameof(TouchKeyboard), new ScaleTransform());

    public static readonly StyledProperty<int> PopupHeightProperty =
        AvaloniaProperty.Register<TouchKeyboard, int>(nameof(TouchKeyboard), 300);

    public static readonly StyledProperty<int> PopupWidthProperty =
        AvaloniaProperty.Register<TouchKeyboard, int>(nameof(TouchKeyboard), 900);

    public static readonly DirectProperty<TouchKeyboard, string> TextProperty =
        AvaloniaProperty.RegisterDirect<TouchKeyboard, string>(nameof(Text), numpicker => numpicker.Text,
            (numpicker, v) => numpicker.Text = v, defaultBindingMode: BindingMode.TwoWay, enableDataValidation: true);

    private string _text;

    public TouchKeyboard()
    {
        InitializeComponent();
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

    public string Text
    {
        get => _text;
        set
        {
            SetAndRaise(TextProperty, ref _text, value);
            this.FindControl<TextBlock>("textValue").Text = Text;
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OpenPopup(object sender, RoutedEventArgs e)
    {
        var dialog = new TouchKeyboardPopUp(this, Text);

        dialog.RenderTransform = PopupScale;
        dialog.Height = PopupHeight;
        dialog.Width = PopupWidth;

        InteractiveContainer.ShowDialog(dialog, true);
    }
}