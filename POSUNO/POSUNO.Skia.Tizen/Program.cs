using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace POSUNO.Skia.Tizen
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new POSUNO.App(), args);
            host.Run();
        }
    }
}
