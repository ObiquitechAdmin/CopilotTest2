namespace CopilotTest2.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {
        
    }

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotLoading))]
    private bool _isLoading;
    [ObservableProperty]
    private bool _isImageLoaded;

    public bool IsNotLoading => !IsLoading;


}
