namespace Framework.DataLoader
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IDataUnit" />.
    /// </summary>
    public interface IDataUnit
    {
        /// <summary>
        /// The LoadSeedDataAsync.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        Task LoadSeedDataAsync();

        /// <summary>
        /// The LoadDemoDataAsync.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        Task LoadDemoDataAsync();
    }
}
