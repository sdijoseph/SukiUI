using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;

namespace SukiUI.Controls;

public partial class GroupBox : UserControl
{
    public static readonly StyledProperty<string> HeaderProperty =
        AvaloniaProperty.Register<GroupBox, string>(nameof(Header), "Header");

    public static readonly StyledProperty<string?> TextProperty =
        TextBlock.TextProperty.AddOwner<GroupBox>(new StyledPropertyMetadata<string?>(
            defaultBindingMode: BindingMode.TwoWay,
            enableDataValidation: true));

    public GroupBox()
    {
        InitializeComponent();
    }

    public string Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}