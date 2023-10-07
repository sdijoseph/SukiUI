using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;

namespace SukiUI.Controls;

[TemplatePart("PART_SplitView", typeof(SplitView))]
public class SideMenuTemplated : TemplatedControl
{
    private SplitView? _splitView;
    public SideMenuTemplated()
    {
        
    }

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

    private IEnumerable<SideMenuItemTemplated> _footerMenuItems;

    public static readonly DirectProperty<SideMenuTemplated, IEnumerable<SideMenuItemTemplated>> FooterMenuItemsProperty = AvaloniaProperty.RegisterDirect<SideMenuTemplated, IEnumerable<SideMenuItemTemplated>>(
        nameof(FooterMenuItems), o => o.FooterMenuItems, (o, v) => o.FooterMenuItems = v);

    public IEnumerable<SideMenuItemTemplated> FooterMenuItems
    {
        get => _footerMenuItems;
        set => SetAndRaise(FooterMenuItemsProperty, ref _footerMenuItems, value);
    }
    
    public delegate void MenuItemChangedEventHandler(object sender, string header);
    public event MenuItemChangedEventHandler? MenuItemChanged;

    private void OnPaneClosing(object sender, CancelRoutedEventArgs ev)
    {
        IsMenuVisible = false;
    }

    /// <inheritdoc />
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        _splitView = e.NameScope.Find<SplitView>("PART_SplitView") ?? throw new InvalidOperationException("Cannot find PART_SplitView");
    }

    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        if (_splitView is not null)
            _splitView.PaneClosing += OnPaneClosing;
        base.OnAttachedToLogicalTree(e);
    }

    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        if (_splitView is not null)
            _splitView.PaneClosing -= OnPaneClosing;
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