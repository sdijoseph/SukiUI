using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using System;
using System.Collections.Generic;

namespace SukiUI.Theme;

/// <summary>
/// Includes the fluent theme in an application.
/// </summary>
public class SukiTheme : Styles
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SukiTheme"/> class.
    /// </summary>
    /// <param name="sp">The parent's service provider.</param>
    public SukiTheme(IServiceProvider? sp = null)
    {
        AvaloniaXamlLoader.Load(sp, this);
        
        object GetAndRemove(string key)
        {
            var val = Resources[key]
                      ?? throw new KeyNotFoundException($"Key {key} was not found in the resources");
            Resources.Remove(key);
            return val;
        }
    }
    
}