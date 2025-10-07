using MotorcycleFlow.Application.Interfaces;
using System.Text;

namespace MotorcycleFlow.Infrastructure.Services
{
    public class LocalImageStorageService : IImageStorageService
    {
        private readonly string _imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        private readonly string _baseUrl = "/images";

        public LocalImageStorageService()
        {
            // Criar diretório se não existir
            if (!Directory.Exists(_imagesPath))
            {
                Directory.CreateDirectory(_imagesPath);
            }
        }

        public async Task<string> UploadImageAsync(string base64Image, string fileName)
        {
            try
            {
                // Remover header data URL se existir
                if (base64Image.Contains("base64,"))
                {
                    base64Image = base64Image.Split(',')[1];
                }

                // Decodificar base64 para bytes
                var imageBytes = Convert.FromBase64String(base64Image);

                // Validar se é PNG ou BMP
                if (!IsValidImageFormat(imageBytes))
                {
                    throw new ArgumentException("Invalid image format. Only PNG and BMP are allowed.");
                }

                // Gerar nome único do arquivo
                var uniqueFileName = $"{fileName}_{Guid.NewGuid():N}.png";
                var filePath = Path.Combine(_imagesPath, uniqueFileName);

                // Salvar arquivo
                await File.WriteAllBytesAsync(filePath, imageBytes);

                // Retornar URL relativa
                return $"{_baseUrl}/{uniqueFileName}";
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to upload image: {ex.Message}", ex);
            }
        }

        public Task<bool> DeleteImageAsync(string imageUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(imageUrl))
                    return Task.FromResult(false);

                // Extrair nome do arquivo da URL
                var fileName = Path.GetFileName(imageUrl);
                var filePath = Path.Combine(_imagesPath, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return Task.FromResult(true);
                }

                return Task.FromResult(false);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        private bool IsValidImageFormat(byte[] imageBytes)
        {
            if (imageBytes.Length < 4)
                return false;

            // PNG: 89 50 4E 47
            if (imageBytes[0] == 0x89 && imageBytes[1] == 0x50 &&
                imageBytes[2] == 0x4E && imageBytes[3] == 0x47)
                return true;

            // BMP: 42 4D
            if (imageBytes[0] == 0x42 && imageBytes[1] == 0x4D)
                return true;

            return false;
        }
    }
}
