namespace CopilotTest2.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel_p)
    {
        InitializeComponent();
        BindingContext = viewModel_p;
    }
}
