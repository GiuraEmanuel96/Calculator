using Calculator.Models;

namespace Calculator.ViewModels.Services
{
    public interface IDataManager
    {
        /// <summary>
        /// Saves the state.
        /// </summary>
        /// <exception cref="IOException">An I/O error happened during state saving.</exception>
        Task Save(Calculation calculation);

        /// <summary>
        /// Loads the saved state.
        /// </summary>
        /// <exception cref="IOException">An I/O error happened during state loading.</exception>
        /// <exception cref="InvalidDataException">The saved state is invalid or corrupt.</exception>
        Task<Calculation?> Load();

        /// <summary>
        /// Deletes the saved state.
        /// </summary>
        /// <exception cref="IOException">An I/O error happened during state delete.</exception>
        Task DeleteAsync();
    }
}