using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using ReactiveUI;

//using DialogHostAvalonia.Positioners;

namespace SukiUI.Controls;

public class MobileMenuPageViewModel : INotifyPropertyChanged
{
    private object currentPage = new Grid();


    /* private AlignmentDialogPopupPositioner dialogPosition = new AlignmentDialogPopupPositioner();

     public AlignmentDialogPopupPositioner DialogPosition
     {
         get => dialogPosition;
         set => this.RaiseAndSetIfChanged(ref dialogPosition, value);
     } */


    private Control dialogchild = new Grid();


    private object headerContent = new Grid();

    private string headertext = "Home";

    private bool isdialogopen;

    private List<SideMenuItem> menuItems = new();

    private bool menuvisibility;

    private Thickness toastMargin = new(0, 125, 0, 0);

    private Control toastMessage = new Grid();


    private double toastOpacity;

    public MobileMenuPageViewModel()
    {
        DoTheThing = ReactiveCommand.Create<SideMenuItem>(ChangePage);
    }

    public string HeaderText
    {
        get => headertext;
        set
        {
            headertext = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HeaderText)));
        }
    }

    public bool IsDialogOpen
    {
        get => isdialogopen;
        set
        {
            isdialogopen = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsDialogOpen)));
        }
    }

    public double ToastOpacity
    {
        get => toastOpacity;
        set
        {
            toastOpacity = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToastOpacity)));
        }
    }

    public Control ContentToast
    {
        get => toastMessage;
        set
        {
            toastMessage = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContentToast)));
        }
    }

    public Thickness ToastMargin
    {
        get => toastMargin;
        set
        {
            toastMargin = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToastMargin)));
        }
    }

    public Control DialogChild
    {
        get => dialogchild;
        set
        {
            dialogchild = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DialogChild)));
        }
    }

    public bool MenuVisibility
    {
        get => menuvisibility;
        set
        {
            menuvisibility = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MenuVisibility)));
        }
    }

    public object HeaderContent
    {
        get => headerContent;
        set
        {
            headerContent = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HeaderContent)));
        }
    }

    public List<SideMenuItem> MenuItems
    {
        get => menuItems;
        set
        {
            menuItems = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MenuItems)));
        }
    }

    public object CurrentPage
    {
        get => currentPage;
        set
        {
            currentPage = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
        }
    }

    public ReactiveCommand<SideMenuItem, Unit> DoTheThing { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;


    public void ShowMenu()
    {
        MenuVisibility = false;
        MenuVisibility = true;
    }

    public void HideMenu()
    {
        MenuVisibility = false;
    }

    public void ChangeMenuVisibility()
    {
        MenuVisibility = !MenuVisibility;
    }

    public void ChangePage(SideMenuItem o)
    {
        Console.WriteLine(o);
        HeaderText = o.Header;
        CurrentPage = o.Content;

        Task.Run(() =>
        {
            Thread.Sleep(50);
            MenuVisibility = false;
        });
    }
}