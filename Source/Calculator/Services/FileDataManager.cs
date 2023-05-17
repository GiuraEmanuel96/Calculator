using System.Text.Json;
using Calculator.Models;
using Calculator.ViewModels.Services;
using Singulink.IO;

namespace Calculator.Services
{
    public class FileDataManager : IDataManager
    {
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