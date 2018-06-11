namespace CSDocNet.Logging
{
	public interface ILogger
	{
		void Write(LogLevel level, string msg);
	};
}