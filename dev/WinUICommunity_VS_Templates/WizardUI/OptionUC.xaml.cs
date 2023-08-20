using System.Windows;
using System.Windows.Controls;

namespace WinUICommunity_VS_Templates;

public partial class OptionUC : UserControl
{
    public Visibility ExpanderVisibility
    {
        get { return (Visibility)GetValue(ExpanderVisibilityProperty); }
        set { SetValue(ExpanderVisibilityProperty, value); }
    }

    public static readonly DependencyProperty ExpanderVisibilityProperty =
        DependencyProperty.Register("ExpanderVisibility", typeof(Visibility), typeof(OptionUC), new PropertyMetadata(Visibility.Visible));

    public string ExpanderHeader
    {
        get { return (string)GetValue(ExpanderHeaderProperty); }
        set { SetValue(ExpanderHeaderProperty, value); }
    }

    public static readonly DependencyProperty ExpanderHeaderProperty =
        DependencyProperty.Register("ExpanderHeader", typeof(string), typeof(OptionUC), new PropertyMetadata("See More"));

    public string InfoBarMessage
    {
        get { return (string)GetValue(InfoBarMessageProperty); }
        set { SetValue(InfoBarMessageProperty, value); }
    }

    public static readonly DependencyProperty InfoBarMessageProperty =
        DependencyProperty.Register("InfoBarMessage", typeof(string), typeof(OptionUC), new PropertyMetadata(default(string)));

    public string InfoBarTitle
    {
        get { return (string)GetValue(InfoBarTitleProperty); }
        set { SetValue(InfoBarTitleProperty, value); }
    }

    public static readonly DependencyProperty InfoBarTitleProperty =
        DependencyProperty.Register("InfoBarTitle", typeof(string), typeof(OptionUC), new PropertyMetadata(default(string)));

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(OptionUC), new PropertyMetadata(default(string)));

    public bool IsOn
    {
        get { return (bool)GetValue(IsOnProperty); }
        set { SetValue(IsOnProperty, value); }
    }

    public static readonly DependencyProperty IsOnProperty =
        DependencyProperty.Register("IsOn", typeof(bool), typeof(OptionUC), new PropertyMetadata(false));

    public OptionUC()
    {
        InitializeComponent();
    }
}
