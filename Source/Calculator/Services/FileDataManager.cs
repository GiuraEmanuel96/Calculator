using System.Text.Json;
using Calculator.Models;
using Calculator.ViewModels.Services;
using Singulink.IO;

namespace Calculator.Services
{
    public sealed class FileDataManager : IDataManager
    {
        public static FileDataManager Instance { get; } = new();

        private FileDataManager()
        {
        }

        public IAbsoluteFilePath FilePath
        {
            get {
                return DirectoryPath
                .GetSpecialFolder(Environment.SpecialFolder.LocalApplicationData)
                .CombineDirectory("WinUICalculator")
                .CombineFile("data.json");
            }
        }

        public async Task<Calculation?> Load()
        {
            await Task.Delay(5000);
            var filePath = FilePath;

            if (!filePath.Exists)
            {
                return null;
            }

            await using var fileStream = await filePath.OpenStreamAsync(FileMode.Open, FileAccess.Read);
            var calculation = await JsonSerializer.DeserializeAsync<Calculation>(fileStream);

            return calculation;
        }

        public async Task Save(Calculation calculation)
        {
            await using var fileStream = await FilePath.OpenStreamAsync(FileMode.Create, FileAccess.Write);
            await JsonSerializer.SerializeAsync(fileStream, calculation);
        }
    }
}