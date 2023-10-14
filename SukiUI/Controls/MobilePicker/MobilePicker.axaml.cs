using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace SukiUI.Controls.MobilePicker;

public partial class MobilePicker : UserControl
{
    public static readonly DirectProperty<MobilePicker, string> SelectedItemProperty =
        AvaloniaProperty.RegisterDirect<MobilePicker, string>(
            nameof(SelectedItem),
            o => o.SelectedItem,
            (o, v) => o.SelectedItem = v,
            defaultBindingMode: BindingMode.TwoWay,
            enableDataValidation: true);


    public static readonly StyledProperty<ObservableCollection<string>> ItemsProperty =
        AvaloniaProperty.Register<MobilePicker, ObservableCollection<string>>(nameof(Items),
            new ObservableCollection<string>());


    public static readonly StyledProperty<ScaleTransform> PopupScaleProperty =
        AvaloniaProperty.Register<MobilePicker, ScaleTransform>(nameof(MobilePicker), new ScaleTransform());

    public static readonly StyledProperty<int> PopupHeightProperty =
        AvaloniaProperty.Register<MobilePicker, int>(nameof(MobilePicker), 200);

    public static readonly StyledProperty<int> PopupWidthProperty =
        AvaloniaProperty.Register<MobilePicker, int>(nameof(MobilePicker), 300);


    private string _selectedItem;

    public MobilePicker()
    {
        InitializeComponent();
    }

    public string SelectedItem
    {
        get => _selectedItem;
        set => SetAndRaise(SelectedItemProperty, ref _selectedItem, value);
    }

    public ObservableCollection<string> Items
    {
        get => GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public ScaleTransform PopupScale
    {
        get => GetValue(PopupScaleProperty);
        set => SetValue(PopupScaleProperty, value);
    }

    public int PopupHeight
    {
        get => GetValue(PopupHeightProperty);
        set => SetValue(PopupHeightProperty, value);
    }

    public int PopupWidth
    {
        get => GetValue(PopupWidthProperty);
        set => SetValue(PopupWidthProperty, value);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OpenPopup(object sender, RoutedEventArgs e)
    {
        var control = new MobilePickerPopUp();

        var vm = (MobilePickerPopUpVM)control.DataContext;
        vm.Items = Items;
        vm.SelectedItem = SelectedItem;
        vm.mobilePicker = this;

        control.Height = PopupHeight;
        control.Width = PopupWidth;
        control.FindControl<Border>("rootBorder").RenderTransform = PopupScale;

        InteractiveContainer.ShowDialog(control, true);
    }
}