namespace ApiMiniApp.Extensions;

public static class FileExtension
{
    public static string SaveFile(this IFormFile file,  string rootPath)
    {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(),rootPath, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        return fileName;
    }
}