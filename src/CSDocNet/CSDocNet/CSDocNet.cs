using System.Diagnostics;
using System.Reflection;
using System.IO;
using CSDocNet.Comments;
using CSDocNet.Logging;
using CSDocNet.Markdown;
using CSDocNet.Reflection.Data;
using CSDocNet.Reflection.Generation;

namespace CSDocNet
{
    internal static class CSDocNet
    {
        static void Main(string[] args)
        {
            InitLog();

            if(args != null)
            {
                var sWriter = new StreamWriter("out.md");
                var parser = new AssemblyCommentParser();
                var gen = new AssemblyDataGenerator();
                var mdWriter = new MDCommentBlockWriter();
                var syntaxWriter = new MDSummarySyntaxWriter(new MDMemberRefUtility());

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
                                var doc = gen.GenerateAssemblyData(ass);

                                sw.Stop();
                                Log.Note($"generated reflected doc in {sw.ElapsedMilliseconds} ms");

                                sw.Restart();
                                foreach(ClassData data in doc.Classes.Values)
                                {
                                    if(comments.Members.TryGetValue(data.Member.CommentID, out var c))
                                    {
                                        sWriter.Write($"# {data.Member.Name}\n\n");
                                        sWriter.Write(mdWriter.WriteCommentBlock(c.Summary));
                                        sWriter.Write("\n\n");
                                        sWriter.Write("## Syntax\n\n");
                                        sWriter.Write(syntaxWriter.WriteClassSyntax(data, doc));
                                        sWriter.Write("\n\n");
                                    }
                                }

                                foreach(ClassData data in doc.Structs.Values)
                                {
                                    if(comments.Members.TryGetValue(data.Member.CommentID, out var c))
                                    {
                                        sWriter.Write($"# {data.Member.Name}\n\n");
                                        sWriter.Write(mdWriter.WriteCommentBlock(c.Summary));
                                        sWriter.Write("\n\n");
                                        sWriter.Write("## Syntax\n\n");
                                        sWriter.Write(syntaxWriter.WriteStructSyntax(data, doc));
                                        sWriter.Write("\n\n");
                                    }
                                }

                                foreach(ClassData data in doc.Interfaces.Values)
                                {
                                    if(comments.Members.TryGetValue(data.Member.CommentID, out var c))
                                    {
                                        sWriter.Write($"# {data.Member.Name}\n\n");
                                        sWriter.Write(mdWriter.WriteCommentBlock(c.Summary));
                                        sWriter.Write("\n\n");
                                        sWriter.Write("## Syntax\n\n");
                                        sWriter.Write(syntaxWriter.WriteInterfaceSyntax(data, doc));
                                        sWriter.Write("\n\n");
                                    }
                                }

                                sw.Stop();
                                Log.Note($"wrote summaries in {sw.ElapsedMilliseconds} ms");
                            }
                        }
                    }

                    sw.Reset();
                }

                sWriter.Dispose();
            }
        }

        static void InitLog()
        {
            Log.AddLogger(new ConsoleLogger(), LogLevel.Debug);
        }
    }
}