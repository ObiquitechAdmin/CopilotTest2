namespace CopilotTest2.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ImageSource _imageSource;
    private FlowerService _flowerService;

    public ObservableCollection<Flower> Flowers { get; } = new();


    public MainPageViewModel()
    {
        Title = "Flower Finder";
    }

    [RelayCommand]
    private async Task FindFlowersAsync()
    {
        if (IsLoading)
        { return; }

        try
        {
            IsLoading = true;
            List<Flower> _flowerList = new();

// ERROR: 
//      _flowerService is null and doesn't seem to run the GetFlowers method
            _flowerList = _flowerService.GetFlowers();

            // If the observable collection is not empty, clear it
            if (Flowers.Count != 0)
            { Flowers.Clear(); }

            // Check if the array is null or empty
            if (_flowerList.Count == 0)
            { throw new Exception("NoImagesFound"); }
            else if (_flowerList.Count == 1)
            {
                // Add the only flower to the observable collection
                Flowers.Add(_flowerList[0]);

                // Load the only image available
                ImageSource = ImageSource.FromFile(Flowers.ElementAt(0).Image);

                // Successfully loaded the image
                IsImageLoaded = true;
            }
            else
            {
                // Add all the flowers to the observable collection
                foreach (Flower f in _flowerList)
                { Flowers.Add(f); }

                Random _random = new Random();
                // Select a random image file
                int _rnmIndex = _random.Next(Flowers.Count);

                // Load the image selected
                ImageSource = ImageSource.FromFile(Flowers[_rnmIndex].Image);

                // Successfully loaded the image
                IsImageLoaded = true;
            }
            
        }
        catch (Exception ex)
        {
            if (ex.Message == "NoImagesFound")
            {
                await Shell.Current.DisplayAlert(
                    "Retrieval Error", 
                    $"No images found in the Images folder." +
                    $"\n{ex.Message}", 
                    "OK");
                // Something has gone wrong so the image couldn't be loaded
                IsImageLoaded = false;
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                // Something has gone wrong so the image probably isn't loaded
                IsImageLoaded = false;
            }
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
