namespace CopilotTest2.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    /// <summary>
    /// This is what <see cref="MainPage"/>.xaml will bind to to display the image.
    /// </summary>
    [ObservableProperty]
    private ImageSource _imageSource;
    /// <summary>
    /// <see cref="_flowerService">_flowerService</see> will return null if the service is not registered. 
    /// See <see cref="MauiProgram"/> for service registration.
    /// </summary>
    private FlowerService _flowerService;

    /// <summary>
    /// Not sure why this needs to be an ObservableCollection, but James Montemagno's tutorial says it does.
    /// </summary>
    public ObservableCollection<Flower> Flowers { get; } = new();

    // Constructor for the MainPageViewModel, passing it the FlowerService so we don't get null reference exceptions
    public MainPageViewModel(FlowerService flowerService_p)
    {
        // Set the title of the page through Binding
        Title = "Flower Finder";
        // Again, James Montemagno's tutorial says this is necessary
        this._flowerService = flowerService_p;
    }

    /// <summary>
    /// This is the command that will be called when the button is pressed.
    /// <para>
    /// It is supposed to call the service to retrieve the list of <see cref="Flower"/> objects asynchronously,
    /// then display them in the <see cref="MainPage"/>.xaml.
    /// </para>
    /// </summary>
    [RelayCommand]
    private async Task FindFlowersAsync()
    {
        // If the Image is loading, don't do anything
        if (IsLoading)
        { return; }

        try
        {
            IsLoading = true;


            // This calls the service to get the list of Flower objects, since the MVVM demands that
            // the ViewModel should not know about the View
            List<Flower> _flowerList = _flowerService.GetFlowers();

            // If the ObservableCollection is not empty, clear it
            if (Flowers.Count != 0)
            { Flowers.Clear(); }

            // Check if the array is empty and throw an exception if it is
            if (_flowerList.Count == 0)
            { throw new Exception("NoImagesFound"); }
            // If there is only one image, add it to the ObservableCollection and load it
            else if (_flowerList.Count == 1)
            {
                // Add the only Flower to the ObservableCollection
                Flowers.Add(_flowerList[0]);

                // Load the only image available
                ImageSource = ImageSource.FromFile(Flowers.ElementAt(0).Image);

                // Successfully loaded the image
                IsImageLoaded = true;
            }
            // Otherwise, there are multiple images to choose from so, choose one at random
            else
            {
                // Add all the Flower objects to the ObservableCollection
                foreach (Flower f in _flowerList)
                { Flowers.Add(f); }

                // Create a new Random object
                Random _random = new Random();
                // Select a random index from the list of Flower objects
                int _rnmIndex = _random.Next(Flowers.Count);

                // Load the image selected at the random index
                ImageSource = ImageSource.FromFile(Flowers[_rnmIndex].Image);

                // Successfully loaded the image
                IsImageLoaded = true;
            }
            
        }
        catch (Exception ex)
        {
            if (ex.Message == "NoImagesFound")
            {
                // Using MAUI controls instead of Xamarin.Forms or .NET standard stuff
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

    #region Ignore This
    /// <summary>
    /// This is the command that will be called when the "Download" button is pressed.
    /// <para> Ignore this for now, it's not what I need help with yet. </para>
    /// </summary>
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
    #endregion
}
