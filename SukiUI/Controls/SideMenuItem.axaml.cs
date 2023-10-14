using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Material.Icons;

namespace SukiUI.Controls;

[TemplatePart("PART_RadioButton", typeof(RadioButton))]
public class SideMenuItem : ListBoxItem
{
    public static readonly StyledProperty<string?> HeaderProperty = AvaloniaProperty.Register<SideMenuItem, string?>(
        nameof(Header));

    public static readonly StyledProperty<MaterialIconKind> IconProperty =
        AvaloniaProperty.Register<SideMenuItem, MaterialIconKind>(
            nameof(Icon));

    public static readonly StyledProperty<object?> PageContentProperty =
        AvaloniaProperty.Register<SideMenuItem, object?>(
            nameof(PageContent));

    public string? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public MaterialIconKind Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public object? PageContent
    {
        get => GetValue(PageContentProperty);
        set => SetValue(PageContentProperty, value);
    }
}