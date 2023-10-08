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

[TemplatePart("PART_SplitView", typeof(SplitView))]
[TemplatePart("PART_FooterMenuItemsBox", typeof(ListBox))]
[TemplatePart("PART_MenuItemsBox", typeof(ListBox))]
public class SideMenuTemplated : TemplatedControl
{
    private SplitView? _splitView;
    private ListBox? _footerMenuItemBox;
    private ListBox? _menuItemBox;

    private bool _isMenuVisible;

    public static readonly DirectProperty<SideMenuTemplated, bool> IsMenuVisibleProperty = AvaloniaProperty.RegisterDirect<SideMenuTemplated, bool>(
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
    
    public static readonly DirectProperty<SideMenuTemplated, bool> IsSpacerEnabledProperty = AvaloniaProperty.RegisterDirect<SideMenuTemplated, bool>(
        nameof(IsSpacerEnabled), o => o.IsSpacerEnabled);

    public bool IsSpacerEnabled => CanHeaderContentOverlapToggleSidebarButton && !IsMenuVisible;

    public static readonly StyledProperty<bool> CanHeaderContentOverlapToggleSidebarButtonProperty = AvaloniaProperty.Register<SideMenuTemplated, bool>(
        nameof(CanHeaderContentOverlapToggleSidebarButton));

    public bool CanHeaderContentOverlapToggleSidebarButton
    {
        get => GetValue(CanHeaderContentOverlapToggleSidebarButtonProperty);
        set => SetValue(CanHeaderContentOverlapToggleSidebarButtonProperty, value);
    }
    
    public static readonly DirectProperty<SideMenuTemplated, int> HeaderMinHeightProperty = AvaloniaProperty.RegisterDirect<SideMenuTemplated, int>(
        nameof(HeaderMinHeight), o => o.HeaderMinHeight);

    public int HeaderMinHeight => CanHeaderContentOverlapToggleSidebarButton ? 40 : 0;

    public static readonly StyledProperty<object?> HeaderContentProperty = AvaloniaProperty.Register<SideMenuTemplated, object?>(
        nameof(HeaderContent));

    public object? HeaderContent
    {
        get => GetValue(HeaderContentProperty);
        set => SetValue(HeaderContentProperty, value);
    }

    public static readonly StyledProperty<object?> CurrentPageProperty = AvaloniaProperty.Register<SideMenuTemplated, object?>(
        nameof(CurrentPage));

    public object? CurrentPage
    {
        get => GetValue(CurrentPageProperty);
        set => SetValue(CurrentPageProperty, value);
    }

    private IEnumerable<SideMenuItemTemplated> _footerMenuItems = new List<SideMenuItemTemplated>();

    public static readonly DirectProperty<SideMenuTemplated, IEnumerable<SideMenuItemTemplated>> FooterMenuItemsProperty = AvaloniaProperty.RegisterDirect<SideMenuTemplated, IEnumerable<SideMenuItemTemplated>>(
        nameof(FooterMenuItems), o => o.FooterMenuItems, (o, v) => o.FooterMenuItems = v);

    public IEnumerable<SideMenuItemTemplated> FooterMenuItems
    {
        get => _footerMenuItems;
        set => SetAndRaise(FooterMenuItemsProperty, ref _footerMenuItems, value);
    }

    private IEnumerable<SideMenuItemTemplated> _menuItems = new List<SideMenuItemTemplated>();

    public static readonly DirectProperty<SideMenuTemplated, IEnumerable<SideMenuItemTemplated>> MenuItemsProperty = AvaloniaProperty.RegisterDirect<SideMenuTemplated, IEnumerable<SideMenuItemTemplated>>(
        nameof(MenuItems), o => o.MenuItems, (o, v) => o.MenuItems = v);

    public IEnumerable<SideMenuItemTemplated> MenuItems
    {
        get => _menuItems;
        set => SetAndRaise(MenuItemsProperty, ref _menuItems, value);
    }
    
    public delegate void MenuItemChangedEventHandler(object sender, string header);
    public event MenuItemChangedEventHandler? MenuItemChanged;

    private void OnPaneClosing(object? sender, CancelRoutedEventArgs ev)
    {
        IsMenuVisible = false;
    }

    /// <inheritdoc />
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        _splitView = e.NameScope.Find<SplitView>("PART_SplitView") ?? throw new InvalidOperationException("Cannot find PART_SplitView");
        _footerMenuItemBox = e.NameScope.Find<ListBox>("PART_FooterMenuItemsBox") ?? throw new InvalidOperationException("Cannot find PART_FooterMenuItemsBox");
        _menuItemBox = e.NameScope.Find<ListBox>("PART_MenuItemsBox") ?? throw new InvalidOperationException("Cannot find PART_MenuItemsBox");
        _splitView.PaneClosing += OnPaneClosing;
        _footerMenuItemBox.SelectionChanged += SideMenuItemSelectionChanged;
        _menuItemBox.SelectionChanged += SideMenuItemSelectionChanged;
    }

    [MemberNotNull(nameof(_splitView))]
    [MemberNotNull(nameof(_footerMenuItemBox))]
    [MemberNotNull(nameof(_menuItemBox))]
    private void EnsureTemplateParts()
    {
        if (_splitView is null)
            throw new ArgumentNullException(nameof(_splitView));
        if (_footerMenuItemBox is null)
            throw new ArgumentNullException(nameof(_footerMenuItemBox));
        if (_menuItemBox is null)
            throw new ArgumentNullException(nameof(_menuItemBox));
    }

    private void SideMenuItemSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0 || e.AddedItems[0] is not SideMenuItemTemplated sideMenuItem)
            return;

        CurrentPage = sideMenuItem.ContentToDisplay;
    }

    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        EnsureTemplateParts();
        _splitView.PaneClosing -= OnPaneClosing;
        _footerMenuItemBox.SelectionChanged -= SideMenuItemSelectionChanged;
        _menuItemBox.SelectionChanged -= SideMenuItemSelectionChanged;
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