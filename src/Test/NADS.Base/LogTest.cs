using NUnit.Framework;
using NSubstitute;
using NADS.Logging;

namespace NADS
{
    [TestFixture]
    public class LogTest
    {
        ILogger logger;

        [SetUp]
        public void SetUp()
        {
            logger = Substitute.For<ILogger>();
            Log.AddLogger(logger, LogLevel.Note);
        }

        [TearDown]
        public void TearDown()
        {
            Log.RemoveLogger(logger);
        }

        [Test]
        public void TestLogWrite()
        {
            Log.Note("hello");
            logger.Received().Write(LogLevel.Note, Arg.Is<string>(s => s.EndsWith("hello")));

            Log.Warning("oh no");
            logger.Received().Write(LogLevel.Warning, Arg.Is<string>(s => s.EndsWith("oh no")));

            Log.Error("aaa");
            logger.Received().Write(LogLevel.Error, Arg.Is<string>(s => s.EndsWith("aaa")));

            Log.Failure("...");
            logger.Received().Write(LogLevel.Failure, Arg.Is<string>(s => s.EndsWith("...")));
        }

        [Test]
        public void TestLogWriteBelowMinLevel()
        {
            Log.Debug("psst");
            logger.DidNotReceive().Write(LogLevel.Debug, Arg.Is<string>(s => s.EndsWith("psst")));
        }

        [Test]
        public void TestLogWriteAfterLevelChange()
        {
            Log.Debug("psst");
            logger.DidNotReceive().Write(LogLevel.Debug, Arg.Is<string>(s => s.EndsWith("psst")));

            Log.ChangeMinLevel(logger, LogLevel.Debug);
            Log.Debug("psst");
            logger.Received().Write(LogLevel.Debug, Arg.Is<string>(s => s.EndsWith("psst")));
        }

        [Test]
        public void TestLogWriteAfterRemove()
        {
            Log.RemoveLogger(logger);
            Log.Note("hello");
            logger.DidNotReceive().Write(LogLevel.Note, Arg.Any<string>());
        }
    };
}