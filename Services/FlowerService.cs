namespace CopilotTest2.Services;

public class FlowerService
{
    public FlowerService()
    {
    }

    List<Flower> _flowerList = new();
    public List<Flower> GetFlowers()
    {

        // Get all image file names in the "Images" folder
        string[] _images = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CopilotTest2", "Resources", "Images"));
        if (_images.Length > 0)
        {
            foreach (string _image in _images)
            {
                _flowerList.Add(new Flower
                {
                    Image = Path.GetFileName(_image),
                    ImagePath = _image
                });
            }
        }
        else
        {
            return null;
        }
        return _flowerList;
    }
}

