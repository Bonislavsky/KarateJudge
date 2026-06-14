using Avalonia.Controls;

namespace KarateMVVMApp;

public partial class MessageBox : Window
{
    public bool Confirmed { get; private set; }
    public bool? CheckBoxResult { get; private set; }

    public MessageBox(string message, string? checkBoxText = null, bool checkBoxDefault = false)
    {
        InitializeComponent();

        this.FindControl<TextBlock>("Message")!.Text = message;

        var checkBox = this.FindControl<CheckBox>("OptionalCheckBox")!;
        if (checkBoxText != null)
        {
            checkBox.Content = checkBoxText;
            checkBox.IsChecked = checkBoxDefault;
            checkBox.IsVisible = true;
        }
        else
        {
            checkBox.IsVisible = false;
        }

        this.FindControl<Button>("YesButton")!.Click += (_, _) =>
        {
            Confirmed = true;
            CheckBoxResult = checkBox.IsChecked;
            Close();
        };
        this.FindControl<Button>("NoButton")!.Click += (_, _) =>
        {
            Confirmed = false;
            Close();
        };
    }
}