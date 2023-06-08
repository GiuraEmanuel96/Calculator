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

        public static IAbsoluteFilePath FilePath => DirectoryPath
                .GetSpecialFolder(Environment.SpecialFolder.LocalApplicationData)
                .CombineDirectory("WinUICalculator")
                .CombineFile("data.json");

        public async Task<Calculation?> Load()
        {
            await Task.Delay(3000);
            var filePath = FilePath;

            if (!filePath.Exists)
            {
                return null;
            }

            await using var fileStream = await filePath.OpenStreamAsync(FileMode.Open, FileAccess.Read);

            try
            {
                return await JsonSerializer.DeserializeAsync<Calculation>(fileStream);
            }
            catch (JsonException ex)
            {
                throw new InvalidDataException("Invalid or corrupt data file contents.", ex);
            }
        }

        public async Task Save(Calculation calculation)
        {
            // TODO: Save to temp file first, i.e. "data.json.tmp", then "move" temp file to "data.json"
            // var tempFilePath = DirectoryPath
            //             .GetSpecialFolder(Environment.SpecialFolder.LocalApplicationData)
            //             .CombineDirectory("WinUICalculator")
            //             .CombineFile("data.json.tmp");

            var tempFilePath = FilePath.ParentDirectory.CombineFile(FilePath.Name + ".tmp");

            tempFilePath.ParentDirectory.Create();
            await using var fileStream = await tempFilePath.OpenStreamAsync(FileMode.Create, FileAccess.Write);
            await JsonSerializer.SerializeAsync(fileStream, calculation);

            await Task.Run(() => tempFilePath.MoveTo(FilePath, true));
        }

        public async Task DeleteAsync()
        {
            await Task.Run(FilePath.Delete);
        }
    }
}