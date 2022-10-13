using Scan.BL;
using Scan.DI;
using SimpleInjector;

namespace Scan.Settings
{
    /// <summary>
    /// Настройка DI контейнера
    /// </summary>
    public class Configuration
    {
        public Container Container { get; }

        public Configuration()
        {
            Container = new Container();

            Setup();

        }

        public virtual void Setup()
        {
            Container.Register<IScanFile, ScanFileModel>(Lifestyle.Singleton);
        }
    }
}
