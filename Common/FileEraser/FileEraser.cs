namespace HumanResourcesWebApi.Common.FileEraser
{
    public static class FileEraser
    {
        public static void DeleteImage(string imageName)
        {
            var path = $@"wwwroot/content/{imageName}";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
