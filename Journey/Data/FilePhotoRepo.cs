using Journey.Entities;

namespace Journey.Data
{
    public class FilePhotoRepo
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHost;
        public FilePhotoRepo(UnitOfWork unitOfWork, IWebHostEnvironment webHost)
        {
            this.unitOfWork = unitOfWork;
            this.webHost = webHost;
        }
        public string UniquePhotoName(string fileName)
        {
            string[] splitName = fileName.Split('.');
            return splitName.First() + '_' + DateTime.Now.Ticks + '.' + splitName.Last();
        }
        public void UploadFile(IFormFile file, string name)
        {
            string filePath = ImagePath(name, "images");
            if (file == null)
            {
                throw new Exception("Image == null");
            }
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
        }
        public void DeleteFile(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("name == empty");
            }
            string path = ImagePath(name, "images");
            File.Delete(path);
        }
        public void UpdateFile(Photo photo)
        {
            if (photo.Image == null)
            {
                return;
            }
            var name = unitOfWork.PhotoRepo.FindName(photo.Id);
            if (name == null)
            {
                return;
            }
            DeleteFile(name);
            string[] splitName = photo.Image.FileName.Split('.');
            photo.PhotoName = splitName.First() + '_' + DateTime.Now.Ticks + '.' + splitName.Last();
            UploadFile(photo.Image, ImagePath(photo.PhotoName, "images"));
        }
        public string ImagePath(string photoName, string directoryMame)
        {
            if (photoName == null)
            {
                return "";
            }
            string uploadsFolder = Path.Combine(webHost.WebRootPath, directoryMame);
            string filePath = Path.Combine(uploadsFolder, photoName);
            return filePath;
        }
        public string CurrentPath(string directoryMame)
        {
            return Path.Combine(webHost.WebRootPath, directoryMame); ;
        }
    }
}

