using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Avalonia.Collections;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using SukiUI.Controls;
using SukiUI.Demo.Common;
using SukiUI.Demo.Features;
using SukiUI.Demo.Features.CustomTheme;
using SukiUI.Models;

namespace SukiUI.Demo;

public partial class SukiUIDemoViewModel : ViewAwareObservableObject
{
    public AvaloniaList<DemoPageBase> DemoPages { get; } = [];

    public IAvaloniaReadOnlyList<SukiColorTheme> Themes { get; }

    [ObservableProperty] private ThemeVariant _baseTheme;
    [ObservableProperty] private bool _animationsEnabled;

    private readonly SukiTheme _theme;

    public SukiUIDemoViewModel(IEnumerable<DemoPageBase> demoPages)
    {
        DemoPages.AddRange(demoPages.OrderBy(x => x.Index).ThenBy(x => x.DisplayName));
        _theme = SukiTheme.GetInstance();
        Themes = _theme.ColorThemes;
        BaseTheme = _theme.ActiveBaseTheme;
        _theme.OnBaseThemeChanged += variant =>
        {
            BaseTheme = variant;
            SukiHost.ShowToast("Successfully Changed Theme", $"Changed Theme To {variant}");
        };
        _theme.OnColorThemeChanged += theme =>
        {
            SukiHost.ShowToast("Successfully Changed Color", $"Changed Color To {theme.DisplayName}.");
        };
    }

    public void ToggleAnimations()
    {
        AnimationsEnabled = !AnimationsEnabled;
        var title = AnimationsEnabled ? "Animation Enabled" : "Animation Disabled";
        var content = AnimationsEnabled
            ? "Background animations are now enabled."
            : "Background animations are now disabled.";
        SukiHost.ShowToast(title, content);
    }

    public void ToggleBaseTheme() =>
        _theme.SwitchBaseTheme();

    public void ChangeTheme(SukiColorTheme theme) =>
        _theme.ChangeColorTheme(theme);

    public void CreateCustomTheme()
    {
        SukiHost.ShowDialog(new CustomThemeDialogViewModel(_theme), allowBackgroundClose: true);
    }

    public void OpenURL(string url)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            Process.Start(new ProcessStartInfo(url.Replace("&", "^&")) { UseShellExecute = true });
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            Process.Start("xdg-open", url);
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            Process.Start("open", url);
    }
}