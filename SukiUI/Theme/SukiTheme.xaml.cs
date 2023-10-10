using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace SukiUI.Theme;

 /// <summary>
/// Includes the fluent theme in an application.
/// </summary>
public class SukiTheme : Styles
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentTheme"/> class.
    /// </summary>
    /// <param name="sp">The parent's service provider.</param>
    public SukiTheme(IServiceProvider? sp = null)
    {
        AvaloniaXamlLoader.Load(sp, this);
    }
}