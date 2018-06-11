using System;
using CSDocNet.Debug;

namespace CSDocNet.Logging
{
	public class ConsoleLogger : ILogger
	{
		/// <inheritdoc />
		public void Write(LogLevel level, string msg)
		{
			Assert.Ref(msg);

			// remember old color
			ConsoleColor fg = Console.ForegroundColor;
			ConsoleColor bg = Console.BackgroundColor;

			// set new color and write
			SetColor(level);
			Console.WriteLine(msg.ToString());

			// restore old color
			Console.ForegroundColor = fg;
			Console.BackgroundColor = bg;
		}

		void SetColor(LogLevel level)
		{
			switch(level)
			{
				case LogLevel.Debug:
					Console.ForegroundColor = ConsoleColor.DarkGray;
					Console.BackgroundColor = ConsoleColor.Black;
					break;
				case LogLevel.Note:
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.BackgroundColor = ConsoleColor.Black;
					break;
				case LogLevel.Warning:
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.BackgroundColor = ConsoleColor.Black;
					break;
				case LogLevel.Error:
					Console.ForegroundColor = ConsoleColor.Red;
					Console.BackgroundColor = ConsoleColor.Black;
					break;
				case LogLevel.Failure:
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.BackgroundColor = ConsoleColor.Red;
					break;
			}
		}
	};
}