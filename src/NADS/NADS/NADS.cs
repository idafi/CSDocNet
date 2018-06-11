using System.Diagnostics;
using System.Reflection;
using System.IO;
using NADS.Comments;
using NADS.Logging;
using NADS.Reflection.Generation;

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
                var gen = new AssemblyDocGenerator();

                Stopwatch sw = new Stopwatch();

                foreach(string arg in args)
                {
                    sw.Start();

                    if(File.Exists(arg))
                    {
                        using(FileStream stream = new FileStream(arg, FileMode.Open))
                        {
                            var comments = parser.Parse(stream);

                            sw.Stop();
                            Log.Note($"parsed {comments.Members.Count} members from {comments.Name} assembly in {sw.ElapsedMilliseconds} ms");
                            
                            sw.Restart();
                            string assName = $"{comments.Name}.dll";
                            assName = Path.GetFullPath(assName);
                            if(File.Exists(assName))
                            {
                                Assembly ass = Assembly.LoadFile(assName);
                                var doc = gen.GenerateAssemblyDoc(ass);

                                sw.Stop();
                                Log.Note($"generated reflected doc in {sw.ElapsedMilliseconds} ms");
                            }
                        }
                    }

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