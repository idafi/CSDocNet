using System;
using System.Collections.Generic;
using CSDocNet.Logging;

namespace CSDocNet
{
	public static class Log
	{
		static Dictionary<ILogger, LogLevel> loggers;
		
		static Log()
		{
			loggers = new Dictionary<ILogger, LogLevel>();
		}

		public static void Debug(string msg) => Write(LogLevel.Debug, msg);
		public static void Note(string msg) => Write(LogLevel.Note, msg);
		public static void Warning(string msg) => Write(LogLevel.Warning, msg);
		public static void Error(string msg) => Write(LogLevel.Error, msg);
		public static void Failure(string msg) => Write(LogLevel.Failure, msg);
		public static void Write(LogLevel level, string msg)
		{
			Check.Ref(msg);
			
			DateTime time = DateTime.Now;
			string timestamp = time.ToString("HH:mm:ss.fff: ");
			msg = timestamp + msg;
			
			foreach(var pair in loggers)
			{
				ILogger logger = pair.Key;
				LogLevel minLevel = pair.Value;
				
				if(level >= minLevel)
				{ logger.Write(level, msg); }
			}
		}
		
		public static void AddLogger(ILogger logger, LogLevel minLevel)
		{
			Check.Ref(logger);
			
			if(!loggers.ContainsKey(logger))
			{ loggers.Add(logger, minLevel); }
			else
			{ Warning("tried to add a logger that's already in use"); }
		}
		
		public static void RemoveLogger(ILogger logger)
		{
            if(!loggers.ContainsKey(logger))
            { Warning("tried to remove a logger that isn't in use"); }

            loggers.Remove(logger);
		}

		public static void ChangeMinLevel(ILogger logger, LogLevel minLevel)
		{
			Check.Ref(logger);
			
			if(loggers.ContainsKey(logger))
			{ loggers[logger] = minLevel; }
			else
			{ Error("couldn't change min log level: logger isn't in use"); }
		}
	};
}