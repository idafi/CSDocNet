using System.Diagnostics;
using System.IO;
using NADS.Comments;
using NADS.Logging;

namespace NADS
{
    internal static class NADS
    {
        static void Main(string[] args)
        {
            InitLog();

            if(args != null)
            {
                var parser = new AssemblyCommentParser();

                Stopwatch sw = new Stopwatch();

                foreach(string arg in args)
                {
                    sw.Start();

                    if(File.Exists(arg))
                    {
                        using(FileStream stream = new FileStream(arg, FileMode.Open))
                        {
                            var comments = parser.Parse(stream);
                            Log.Note($"parsed {comments.Members.Count} members from {comments.Name} assembly in {sw.ElapsedMilliseconds} ms");
                        }
                    }

                    sw.Stop();
                    sw.Reset();
                }
            }
        }

        static void InitLog()
        {
            Log.AddLogger(new ConsoleLogger(), LogLevel.Debug);
        }
    }
}