namespace Framework.DataLoader
{
    using System.Threading.Tasks;

    public interface IDataUnit
    {
        Task LoadSeedDataAsync();

        Task LoadDemoDataAsync();
    }
}
