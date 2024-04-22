namespace CopilotTest2.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ImageSource _imageSource;
    [ObservableProperty]
    private bool _isLoading;
    [ObservableProperty]
    bool _isImageLoaded;

    public MainPageViewModel()
    {
        Title = "Flower Finder";
    }

    [RelayCommand]
    private async void FindFlower()
    {
        IsLoading = true;
        try
        {
            // Replace with your actual logic to find a flower image
            ImageSource = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Flower_jtca001.jpg/1280px-Flower_jtca001.jpg"));
            IsImageLoaded = true;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async void DownloadImage()
    {
        IsLoading = true;
        try
        {
            // Replace with your actual logic to download the image
            var httpClient = new HttpClient();
            var bytes = await httpClient.GetByteArrayAsync(ImageSource.ToString());
            var filePath = Path.Combine(FileSystem.CacheDirectory, "downloaded.jpg");
            await File.WriteAllBytesAsync(filePath, bytes);
            await Application.Current.MainPage.DisplayAlert("Success", "Image downloaded successfully", "OK");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }
}
