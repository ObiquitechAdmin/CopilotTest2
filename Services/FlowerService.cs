namespace CopilotTest2.Services;

public class FlowerService
{
    public FlowerService()
    {
    }

    // Local list of Flower objects to be returned
    List<Flower> _flowerList = new();

    /// <summary>
    /// Ideally this method should be asynchronous, but I don't know how to make it so.
    /// </summary>
    public List<Flower> GetFlowers()
    {
        // ----- Get all image file names in the "Images" folder -----

        #region Attempt 1
        /*
        // Get all the files in the "Images" folder, returning as type "FileInfo".
        DirectoryInfo directory = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CopilotTest2", "Resources", "Images"));

        // For each file in the directory, add the file name to the list of flowers;
        // Where "FullName" is the full path of the file;
        // And "Path.GetFileName" returns only the file name, including the extension.
        foreach (var file in directory.GetFiles())
        {
            _flowerList.Add(new Flower
            {
                // "Image" and "ImagePath" are string Properties of the "Flower" class.
                Image = Path.GetFileName(file.FullName),
                ImagePath = file.FullName
            });
        }*/
        #endregion

        #region Attempt 2
        ///<summary> 
        /// Note: I think at some point I remember intellisense telling me the <see cref="Directory"/> 
        /// class methods can't be called asynchronously, which is a shame but I don't know what else to 
        /// use to make it asynchronous.
        /// </summary>
        // Get all the files in the "Images" folder, returning as type "string".
        string[] _images = Directory.GetFiles("/CopilotTest2/Resources/Images");
        if (_images.Length > 0)
        {
            /* For each file in the directory, add a Flower object and add the FileName
             * to the current Flower;
             * Where "file" is the full path of the file;
             * And "Path.GetFileName" returns only the FileName, including the extension. */
            foreach (string file in _images)
            {
                _flowerList.Add(new Flower
                {
                    // "Image" and "ImagePath" are string Properties of the "Flower" class.
                    Image = Path.GetFileName(file),
                    ImagePath = file
                });
            }
        }
        //else
        //{
        //    return null;
        //}
        #endregion

        #region Copilot AI's Suggestion
        // This is the code that Copilot AI suggested to use, but it does not work either.
        //string[] _images = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CopilotTest2", "Resources", "Images"));
        #endregion

        // Send local list of Flower objects back to the MainPageViewModel
        return _flowerList;
    }
}

