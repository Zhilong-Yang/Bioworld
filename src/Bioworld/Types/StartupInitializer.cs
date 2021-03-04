namespace Bioworld.Types
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class StartupInitializer : IStartupInitializer
    {
        private readonly IList<IInitializer> _initializers = new List<IInitializer>();


        public async Task InitializeAsync()
        {
            foreach (var initializer in _initializers)
            {
                await initializer.InitializeAsync();
            }
        }

        public void AddInitializer(IInitializer initializer)
        {
            if (initializer is null || _initializers.Contains(initializer))
            {
                return;
            }

            _initializers.Add(initializer);
        }
    }
}