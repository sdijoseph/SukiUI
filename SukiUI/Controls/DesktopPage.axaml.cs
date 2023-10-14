using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.VisualTree;
using Material.Icons;
using Material.Icons.Avalonia;

namespace SukiUI.Controls;

public partial class DesktopPage : UserControl
{
    public static readonly StyledProperty<HorizontalAlignment> TitleHorizontalAlignmentProperty =
        AvaloniaProperty.Register<DesktopPage, HorizontalAlignment>(nameof(TitleHorizontalAlignment),
            HorizontalAlignment.Left);

    public static readonly StyledProperty<double> TitleFontSizeProperty =
        AvaloniaProperty.Register<DesktopPage, double>(nameof(TitleFontSize), 14);


    public static readonly StyledProperty<FontWeight> TitleFontWeightProperty =
        AvaloniaProperty.Register<DesktopPage, FontWeight>(nameof(TitleFontWeight), FontWeight.Medium);

    public static readonly StyledProperty<IBrush> LogoColorProperty =
        AvaloniaProperty.Register<DesktopPage, IBrush>(nameof(LogoColor), Brushes.DarkSlateBlue);

    public static readonly StyledProperty<MaterialIconKind> LogoKindProperty =
        AvaloniaProperty.Register<DesktopPage, MaterialIconKind>(nameof(LogoKind), MaterialIconKind.DotNet);

    public static readonly StyledProperty<List<MenuItem>> MenuItemsProperty =
        AvaloniaProperty.Register<DesktopPage, List<MenuItem>>(nameof(MenuItems), new List<MenuItem>());

    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<DesktopPage, string>(nameof(Title), "Avalonia UI");

    public static readonly StyledProperty<bool> ShowBottomBorderProperty =
        AvaloniaProperty.Register<DesktopPage, bool>(nameof(ShowBottomBorder), true);

    public static readonly StyledProperty<bool> MenuVisibilityProperty =
        AvaloniaProperty.Register<DesktopPage, bool>(nameof(MenuVisibility), false);


    public static readonly StyledProperty<bool> IsMinimizeButtonEnabledProperty =
        AvaloniaProperty.Register<DesktopPage, bool>(nameof(IsMinimizeButtonEnabled), true);

    public static readonly StyledProperty<bool> IsMaximizeButtonEnabledProperty =
        AvaloniaProperty.Register<DesktopPage, bool>(nameof(IsMaximizeButtonEnabled), true);

    public DesktopPage()
    {
        InitializeComponent();


        //   DataContext = ViewModel;
    }

    public HorizontalAlignment TitleHorizontalAlignment
    {
        get => GetValue(TitleHorizontalAlignmentProperty);
        set => SetValue(TitleHorizontalAlignmentProperty, value);
    }

    public double TitleFontSize
    {
        get => GetValue(TitleFontSizeProperty);
        set => SetValue(TitleFontSizeProperty, value);
    }

    public FontWeight TitleFontWeight
    {
        get => GetValue(TitleFontWeightProperty);
        set => SetValue(TitleFontWeightProperty, value);
    }

    public IBrush LogoColor
    {
        get => GetValue(LogoColorProperty);
        set => SetValue(LogoColorProperty, value);
    }

    public MaterialIconKind LogoKind
    {
        get => GetValue(LogoKindProperty);
        set => SetValue(LogoKindProperty, value);
    }

    public List<MenuItem> MenuItems
    {
        get => GetValue(MenuItemsProperty);
        set => SetValue(MenuItemsProperty, value);
    }

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public bool ShowBottomBorder
    {
        get => GetValue(ShowBottomBorderProperty);
        set => SetValue(ShowBottomBorderProperty, value);
    }

    public bool MenuVisibility
    {
        get => GetValue(MenuVisibilityProperty);
        set => SetValue(MenuVisibilityProperty, value);
    }

    public bool IsMinimizeButtonEnabled
    {
        get => GetValue(IsMinimizeButtonEnabledProperty);
        set => SetValue(IsMinimizeButtonEnabledProperty, value);
    }

    public bool IsMaximizeButtonEnabled
    {
        get => GetValue(IsMaximizeButtonEnabledProperty);
        set => SetValue(IsMaximizeButtonEnabledProperty, value);
    }

    // private DesktopPageViewModel ViewModel = new DesktopPageViewModel();

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    ///     Minimizes Avalonia window
    /// </summary>
    private void MinimizeHandler(object sender, RoutedEventArgs e)
    {
        var hostWindow = (Window)VisualRoot;
        hostWindow.WindowState = WindowState.Minimized;
    }

    /// <summary>
    ///     Maximizes Avalonia window or sets its size to original depending on window state
    /// </summary>
    private void MaximizeHandler(object sender, RoutedEventArgs e)
    {
        var hostWindow = (Window)VisualRoot;
        var icon = this.GetVisualDescendants().OfType<MaterialIcon>()
            .FirstOrDefault(x => (x.Name ?? "").Equals("MaximizeMaterialIcon"));

        if (hostWindow.WindowState != WindowState.Maximized)
        {
            hostWindow.WindowState = WindowState.Maximized;
            icon?.Classes.Remove("WindowMaximize");
            icon?.Classes.Add("WindowRestore");
        }
        else
        {
            hostWindow.WindowState = WindowState.Normal;
            icon?.Classes.Remove("WindowRestore");
            icon?.Classes.Add("WindowMaximize");
        }
    }

    /// <summary>
    ///     Closes Avalonia window
    /// </summary>
    private void CloseHandler(object sender, RoutedEventArgs e)
    {
        var hostWindow = (Window)VisualRoot;
        hostWindow.Close();
    }


    public void SetPage(Control page)
    {
        Content = page;
    }
}