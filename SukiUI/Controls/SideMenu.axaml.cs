using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;

namespace SukiUI.Controls;

[TemplatePart("PART_FooterMenuItemsBox", typeof(ListBox))]
[TemplatePart("PART_MenuItemsBox", typeof(ListBox))]
public class SideMenu : TemplatedControl
{
    private IDisposable? _currentMenuItemChanged;
    
    private ListBox? _footerMenuItemBox;
    private ListBox? _menuItemBox;

    private bool _isMenuVisible;

    public static readonly DirectProperty<SideMenu, bool> IsMenuVisibleProperty = AvaloniaProperty.RegisterDirect<SideMenu, bool>(
        nameof(IsMenuVisible), o => o.IsMenuVisible, (o, v) => o.IsMenuVisible = v, defaultBindingMode: BindingMode.TwoWay);

    public bool IsMenuVisible
    { 
        get => _isMenuVisible;
        set
        {
            var oldSpacerEnabled = IsSpacerEnabled;
            SetAndRaise(IsMenuVisibleProperty, ref _isMenuVisible, value);
            RaisePropertyChanged(IsSpacerEnabledProperty, oldSpacerEnabled, IsSpacerEnabled);
        }
    }
    
    public static readonly DirectProperty<SideMenu, bool> IsSpacerEnabledProperty = AvaloniaProperty.RegisterDirect<SideMenu, bool>(
        nameof(IsSpacerEnabled), o => o.IsSpacerEnabled);

    public bool IsSpacerEnabled => CanHeaderContentOverlapToggleSidebarButton && !IsMenuVisible;

    public static readonly StyledProperty<bool> CanHeaderContentOverlapToggleSidebarButtonProperty = AvaloniaProperty.Register<SideMenu, bool>(
        nameof(CanHeaderContentOverlapToggleSidebarButton));

    public bool CanHeaderContentOverlapToggleSidebarButton
    {
        get => GetValue(CanHeaderContentOverlapToggleSidebarButtonProperty);
        set => SetValue(CanHeaderContentOverlapToggleSidebarButtonProperty, value);
    }
    
    public static readonly DirectProperty<SideMenu, int> HeaderMinHeightProperty = AvaloniaProperty.RegisterDirect<SideMenu, int>(
        nameof(HeaderMinHeight), o => o.HeaderMinHeight);

    public int HeaderMinHeight => CanHeaderContentOverlapToggleSidebarButton ? 40 : 0;

    public static readonly StyledProperty<object?> HeaderContentProperty = AvaloniaProperty.Register<SideMenu, object?>(
        nameof(HeaderContent));

    public object? HeaderContent
    {
        get => GetValue(HeaderContentProperty);
        set => SetValue(HeaderContentProperty, value);
    }

    public static readonly StyledProperty<object?> CurrentPageProperty = AvaloniaProperty.Register<SideMenu, object?>(
        nameof(CurrentPage));

    public object? CurrentPage
    {
        get => GetValue(CurrentPageProperty);
        set => SetValue(CurrentPageProperty, value);
    }

    private IEnumerable<SideMenuItem> _footerMenuItems = new List<SideMenuItem>();

    public static readonly DirectProperty<SideMenu, IEnumerable<SideMenuItem>> FooterMenuItemsProperty = AvaloniaProperty.RegisterDirect<SideMenu, IEnumerable<SideMenuItem>>(
        nameof(FooterMenuItems), o => o.FooterMenuItems, (o, v) => o.FooterMenuItems = v);

    public IEnumerable<SideMenuItem> FooterMenuItems
    {
        get => _footerMenuItems;
        set => SetAndRaise(FooterMenuItemsProperty, ref _footerMenuItems, value);
    }

    private IEnumerable<SideMenuItem> _menuItems = new List<SideMenuItem>();

    public static readonly DirectProperty<SideMenu, IEnumerable<SideMenuItem>> MenuItemsProperty = AvaloniaProperty.RegisterDirect<SideMenu, IEnumerable<SideMenuItem>>(
        nameof(MenuItems), o => o.MenuItems, (o, v) => o.MenuItems = v);

    public IEnumerable<SideMenuItem> MenuItems
    {
        get => _menuItems;
        set => SetAndRaise(MenuItemsProperty, ref _menuItems, value);
    }

    private SideMenuItem? _currentMenuItem;

    public static readonly DirectProperty<SideMenu, SideMenuItem?> CurrentMenuItemProperty = AvaloniaProperty.RegisterDirect<SideMenu, SideMenuItem?>(
        nameof(CurrentMenuItem), o => o.CurrentMenuItem, (o, v) => o.CurrentMenuItem = v);

    public SideMenuItem? CurrentMenuItem
    {
        get => _currentMenuItem;
        set => SetAndRaise(CurrentMenuItemProperty, ref _currentMenuItem, value);
    }
    
    public delegate void MenuItemChangedEventHandler(object sender, string header);
    public event MenuItemChangedEventHandler? MenuItemChanged;

    /// <inheritdoc />
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        _footerMenuItemBox = e.NameScope.Find<ListBox>("PART_FooterMenuItemsBox") ?? throw new InvalidOperationException("Cannot find PART_FooterMenuItemsBox");
        _menuItemBox = e.NameScope.Find<ListBox>("PART_MenuItemsBox") ?? throw new InvalidOperationException("Cannot find PART_MenuItemsBox");
        _footerMenuItemBox.SelectionChanged += SideMenuItemSelectionChanged;
        _menuItemBox.SelectionChanged += SideMenuItemSelectionChanged;
    }

    [MemberNotNull(nameof(_footerMenuItemBox))]
    [MemberNotNull(nameof(_menuItemBox))]
    private void EnsureTemplateParts()
    {
        if (_footerMenuItemBox is null)
            throw new ArgumentNullException(nameof(_footerMenuItemBox));
        if (_menuItemBox is null)
            throw new ArgumentNullException(nameof(_menuItemBox));
    }

    private void SideMenuItemSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0 || e.AddedItems[0] is not SideMenuItem sideMenuItem)
            return;

        CurrentMenuItem = sideMenuItem;
        CurrentPage = sideMenuItem.PageContent;
    }

    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        _currentMenuItemChanged = CurrentMenuItemProperty.Changed.Subscribe(change =>
        {
            if (change.OldValue.Value is { } oldMenuItem)
                oldMenuItem.IsSelected = false;
            if (change.NewValue.Value is { } newMenuItem)
                newMenuItem.IsSelected = true;
        });
        
        base.OnAttachedToLogicalTree(e);
    }

    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        EnsureTemplateParts();
        _footerMenuItemBox.SelectionChanged -= SideMenuItemSelectionChanged;
        _menuItemBox.SelectionChanged -= SideMenuItemSelectionChanged;
        _currentMenuItemChanged?.Dispose();
        base.OnDetachedFromLogicalTree(e);
    }

    private void MenuItemSelectedChanged(object sender, RoutedEventArgs e)
    {
        RadioButton rButton = (RadioButton)sender;
        if (rButton.IsChecked != true)
            return;
        string header = ((TextBlock)((DockPanel)((Grid)rButton.Content).Children.First()).Children.Last()).Text;
        MenuItemChanged?.Invoke(this, header);
    }
}