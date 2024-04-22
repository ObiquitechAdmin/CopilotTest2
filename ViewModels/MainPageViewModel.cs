namespace CopilotTest2.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ImageSource _imageSource;

    public MainPageViewModel()
    {
        Title = "Flower Finder";
    }

    [RelayCommand]
    private async Task FindFlower()
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
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task DownloadImage()
    {
        IsLoading = true;
        try
        {
            // Replace with your actual logic to download the image
            HttpClient _httpClient = new HttpClient();
            byte[] _bytes = await _httpClient.GetByteArrayAsync(ImageSource.ToString());
            string _filePath = Path.Combine(FileSystem.CacheDirectory, "downloaded.jpg");
            await File.WriteAllBytesAsync(_filePath, _bytes);
            await Shell.Current.DisplayAlert("Success", "Image downloaded successfully", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }
}
