using System.IO;
using Stride.Engine;

namespace GameWindow
{
    class GameApp
    {
        static void Main(string[] args)
        {
            // using (var game = new Game())
            // {
            //     game.Run();
            // }
            string[] paths =
                    [
                    "E:\\Stride\\Projects\\ET-Stride\\Client",
                    "E:\\Stride\\Projects\\ET-Stride\\DotNet", ] ;
                    
                    foreach (string path in paths)
                    {
                        foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles("*.meta",SearchOption.AllDirectories))
                        {
                            fileInfo.Delete();
                        }
                    }
        }
    }
}
