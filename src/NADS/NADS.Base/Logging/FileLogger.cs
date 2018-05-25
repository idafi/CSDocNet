using System;
using System.IO;
using NADS.Debug;

namespace NADS.Logging
{
	public class FileLogger : IDisposable, ILogger
	{
		readonly StreamWriter writer;

		public FileLogger(string filePath)
		{
			Check.Ref(filePath);

			FileStream stream = null;

			try
			{
				stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read);
				writer = new StreamWriter(stream);
			}
			catch(Exception e)
			{
				Log.Error("failed to open FileLogger\n" + e.ToString());
				
                writer?.Dispose();
                stream?.Dispose();
			}
		}

		public void Dispose()
		{
			writer?.Dispose();
		}

		public void Write(LogLevel level, string msg)
		{
			Assert.Ref(msg);

			if(writer != null)
			{
				try
				{ writer.WriteLine(msg); }
				catch(Exception e)
				{ Log.Error("couldn't log to file\n" + e.ToString()); }
			}
			else
			{ Log.Error("couldn't log to file: no stream is open"); }
		}
	};
}