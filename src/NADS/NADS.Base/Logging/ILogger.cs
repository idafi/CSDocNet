namespace NADS.Logging
{
	public interface ILogger
	{
		void Write(LogLevel level, string msg);
	};
}