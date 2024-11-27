namespace HumanResourcesWebApi.Common.FileUploader
{
    public static class FileUploader
    {
        static public string UploadFile(IFormFile image)
        {
            var imageName = $"{Guid.NewGuid().ToString()}{image.FileName}";
            var path = $@"wwwroot/content/{imageName}";

            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                image.CopyTo(fs);
            }

            return imageName;
        }
    }
}
