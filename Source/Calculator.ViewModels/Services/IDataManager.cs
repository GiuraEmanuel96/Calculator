using Calculator.Models;

namespace Calculator.ViewModels.Services
{
    public interface IDataManager
    {
        Task Save(Calculation calculation);
        Task<Calculation?> Load();
    }
}