using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace XplatStorageSpace.Skia.Tizen
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new XplatStorageSpace.App(), args);
            host.Run();
        }
    }
}
