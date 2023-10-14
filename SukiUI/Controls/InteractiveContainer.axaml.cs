﻿using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Avalonia.VisualTree;

namespace SukiUI.Controls;

public partial class InteractiveContainer : UserControl
{
    public static readonly StyledProperty<bool> ShowAtBottomProperty =
        AvaloniaProperty.Register<InteractiveContainer, bool>(nameof(InteractiveContainer), false);

    public static readonly StyledProperty<bool> IsDialogOpenProperty =
        AvaloniaProperty.Register<InteractiveContainer, bool>(nameof(InteractiveContainer), false);

    public static readonly StyledProperty<bool> IsToastOpenProperty =
        AvaloniaProperty.Register<InteractiveContainer, bool>(nameof(InteractiveContainer), false);

    public static readonly StyledProperty<Control> DialogContentProperty =
        AvaloniaProperty.Register<InteractiveContainer, Control>(nameof(InteractiveContainer), new Grid());

    public static readonly StyledProperty<Control> ToastContentProperty =
        AvaloniaProperty.Register<InteractiveContainer, Control>(nameof(InteractiveContainer), new Grid());

    public InteractiveContainer()
    {
        InitializeComponent();
    }

    public bool ShowAtBottom
    {
        get => GetValue(ShowAtBottomProperty);
        set => SetValue(ShowAtBottomProperty, value);
    }

    public bool IsDialogOpen
    {
        get => GetValue(IsDialogOpenProperty);
        set => SetValue(IsDialogOpenProperty, value);
    }

    public bool IsToastOpen
    {
        get => GetValue(IsToastOpenProperty);
        set => SetValue(IsToastOpenProperty, value);
    }

    public Control DialogContent
    {
        get => GetValue(DialogContentProperty);
        set => SetValue(DialogContentProperty, value);
    }

    public Control ToastContent
    {
        get => GetValue(ToastContentProperty);
        set => SetValue(ToastContentProperty, value);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    ///     Fired when the container is closed.
    /// </summary>
    public static event EventHandler? DialogClosed;

    private static InteractiveContainer GetInteractiveContainerInstance()
    {
        var container = Application.Current?.ApplicationLifetime switch
        {
            ISingleViewApplicationLifetime { MainView: not null } singleViewApplicationLifetime =>
                singleViewApplicationLifetime.MainView.GetVisualDescendants().OfType<InteractiveContainer>()
                    .FirstOrDefault(),
            IClassicDesktopStyleApplicationLifetime { MainWindow: not null } desktopStyleApplicationLifetime =>
                desktopStyleApplicationLifetime.MainWindow.GetVisualDescendants()
                    .OfType<InteractiveContainer>()
                    .FirstOrDefault(),
            _ => null
        };

        if (container is null)
            throw new InvalidOperationException(
                "You are trying to use a InteractiveContainer functionality without declaring one !");

        return container;
    }

    public static void ShowToast(Control Message, int seconds)
    {
        var container = GetInteractiveContainerInstance();

        container.ToastContent = Message;
        container.IsToastOpen = true;


        Task.Run(() =>
        {
            Thread.Sleep(seconds * 1000);
            Dispatcher.UIThread.InvokeAsync(() => { container.IsToastOpen = false; });
        });
    }

    public static void CloseDialog()
    {
        DialogClosed?.Invoke(null, EventArgs.Empty);
        GetInteractiveContainerInstance().IsDialogOpen = false;
    }

    public static void WaitUntilDialogIsClosed()
    {
        InteractiveContainer container = null;

        Dispatcher.UIThread.InvokeAsync(() => { container = GetInteractiveContainerInstance(); });
        var flag = true;

        do
        {
            Dispatcher.UIThread.InvokeAsync(() => { flag = container.IsDialogOpen; });

            Thread.Sleep(200);
        } while (flag);
    }

    public static async Task<Unit> ShowDialogAsync(Control content, bool showAtBottom = false)
    {
        var container = GetInteractiveContainerInstance();

        container.IsDialogOpen = true;
        container.DialogContent = content;
        container.ShowAtBottom = showAtBottom;

        var result = new TaskCompletionSource<Unit>();

        Observable.FromEventPattern(
                x => DialogClosed += x,
                x => DialogClosed -= x)
            .Take(1)
            .Subscribe(_ => { result.SetResult(Unit.Default); });

        return await result.Task;
    }

    public static void ShowDialog(Control content, bool showAtBottom = false)
    {
        var container = GetInteractiveContainerInstance();

        container.IsDialogOpen = true;
        container.DialogContent = content;
        container.ShowAtBottom = showAtBottom;
    }
}