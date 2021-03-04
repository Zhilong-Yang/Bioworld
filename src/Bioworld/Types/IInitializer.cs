namespace Bioworld.Types
{
    using System.Threading.Tasks;

    public interface IInitializer
    {
        Task InitializeAsync();
    }
}