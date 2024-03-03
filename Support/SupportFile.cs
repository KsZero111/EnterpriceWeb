namespace EnterpriceWeb
{
    public class SupportFile
    {
        private static SupportFile _instance;
        public static SupportFile Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SupportFile();
                }
                return _instance;
            }
        }
        private SupportFile() { }

        public async Task<string> SaveFileAsync(IFormFile file, string folder)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                    string currentDirectory = Directory.GetCurrentDirectory();
                    string filePath = Path.Combine(currentDirectory, "wwwroot", folder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return fileName;
                }
                else
                {
                    return "null";
                }
            }
            catch (Exception)
            {
                return "null";
            }
        }

        public void DeleteFileAsync(string fileName, string folder)
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string filePath = Path.Combine(currentDirectory, "wwwroot", folder, fileName);

                File.Delete(filePath);
            }
            catch (Exception)
            {
            }
        }


    }
}
