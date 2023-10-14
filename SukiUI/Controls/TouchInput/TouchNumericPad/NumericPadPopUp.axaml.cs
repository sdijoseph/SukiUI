using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SukiUI.Controls.TouchInput.TouchNumericPad;

public partial class NumericPadPopUp : UserControl
{
    private readonly TextBlock _textBlock;

    public string CurrentText = "";

    public TouchNumericPad rootControl = null;

    public NumericPadPopUp()
    {
        InitializeComponent();
        _textBlock = this.FindControl<TextBlock>("TextNombre");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void AddNumber(object sender, RoutedEventArgs e)
    {
        CurrentText += ((TextBlock)((Button)sender).Content).Text;
        _textBlock.Text = CurrentText;
    }

    private void RemoveChar(object sender, RoutedEventArgs e)
    {
        try
        {
            CurrentText = CurrentText.Remove(CurrentText.Length - 1);
            _textBlock.Text = CurrentText;
        }
        catch (Exception exc)
        {
        }
    }

    private void Close(object sender, RoutedEventArgs e)
    {
        try
        {
            if (rootControl != null)
                rootControl.Value = double.Parse(CurrentText, CultureInfo.InvariantCulture);
        }
        catch
        {
        }

        InteractiveContainer.CloseDialog();
    }
}