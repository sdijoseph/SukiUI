using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Material.Icons;

namespace SukiUI.Controls;

public class SideMenuItemTemplated : TemplatedControl
{
    public static readonly StyledProperty<string?> HeaderProperty = AvaloniaProperty.Register<SideMenuItemTemplated, string?>(
        nameof(Header));

    public string? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public static readonly StyledProperty<MaterialIconKind> IconProperty = AvaloniaProperty.Register<SideMenuItemTemplated, MaterialIconKind>(
        nameof(Icon));

    public MaterialIconKind Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly StyledProperty<object?> ContentProperty = AvaloniaProperty.Register<SideMenuItemTemplated, object?>(
        nameof(Content));

    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }
}