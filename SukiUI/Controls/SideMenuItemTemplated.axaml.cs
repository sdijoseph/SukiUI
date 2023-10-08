using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Material.Icons;

namespace SukiUI.Controls;

public class SideMenuItemTemplated : ListBoxItem
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

    public static readonly StyledProperty<object?> ContentToDisplayProperty = AvaloniaProperty.Register<SideMenuItemTemplated, object?>(
        nameof(ContentToDisplay));

    public object? ContentToDisplay
    {
        get => GetValue(ContentToDisplayProperty);
        set => SetValue(ContentToDisplayProperty, value);
    }

    // public static readonly StyledProperty<bool> IsSelectedProperty = SelectingItemsControl.IsSelectedProperty.AddOwner<SideMenuItemTemplated>();
    //
    // public bool IsSelected
    // {
    //     get => GetValue(IsSelectedProperty);
    //     set => SetValue(IsSelectedProperty, value);
    // }
}