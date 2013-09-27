using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using ProcessManager.DataAccess;

namespace ProcessManager.Utilities
{
	public enum LogSource
	{
		Undefined = 0,
		Client = 1,
		Server = 2
	}

	public enum LogType
	{
		Debug = 0,
		Verbose = 1,
		Flow = 2,
		Warning = 3,
		Error = 4
	}

	public static class Logger
	{
		private static readonly string _appDataFolder;
		private static readonly string _logFolder;
		private static readonly string _logFileNameTemplate;
		private static readonly Queue<string> _logQueue;
		private static readonly AutoResetEvent _logEntryAddedEvent;
		private static int _logTypeIndentationDepth;

		static Logger()
		{
			_appDataFolder = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
				Settings.Service.Defaults.COMPANY_FOLDER_NAME), Settings.Service.Defaults.APPLICATION_FOLDER_NAME);
			_logFolder = Path.Combine(_appDataFolder, Settings.Service.Read("LogFolderName"));
			_logFileNameTemplate = Settings.Service.Read("LogFileName");

			FileSystemHandler.CreateDirectory(_logFolder);

			_logQueue = new Queue<string>();
			_logEntryAddedEvent = new AutoResetEvent(false);
			SetupLogTypeIndentationDepth();
			LogSource = LogSource.Undefined;
			DefaultLogType = LogType.Flow;
			LogTypeMinLevel = Settings.Service.Defaults.LOG_TYPE_MIN_LEVEL;

			new Thread(LogWriterThread) { IsBackground = true, Name = "Log Writer Thread" }.Start();
		}

		#region Properties

		public static LogSource LogSource { get; set; }
		public static LogType DefaultLogType { get; set; }
		public static LogType LogTypeMinLevel { get; set; }

		private static string LogPathName
		{
			get { return Path.Combine(_logFolder, MakeLogFileName(DateTime.Now)); }
		}

		#endregion

		private static void SetupLogTypeIndentationDepth()
		{
			string[] logTypeNames = Enum.GetNames(typeof(LogType));
			_logTypeIndentationDepth = logTypeNames.Max(logTypeName => logTypeName.Length);
		}

		private static string MakeLogFileName(DateTime date)
		{
			return _logFileNameTemplate.Replace("%date%", date.ToString("yyyy-MM-dd"));
		}

		public static void Add(string logText)
		{
			Add(DefaultLogType, logText);
		}

		public static void Add(string logText, Exception exception)
		{
			if (exception == null)
				Add(logText);
			else
			{
				Add(LogType.Error, logText);
				Add(LogType.Error, exception.ToString());
			}
		}

		public static void Add(LogType logType, string logText)
		{
			if (logType < LogTypeMinLevel)
				return;

			lock (_logQueue)
			{
				_logQueue.Enqueue(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
					+ " > " + logType.ToString().PadRight(_logTypeIndentationDepth) + " "
					+ (LogSource != LogSource.Undefined ? LogSource.ToString().ToUpper() + "/" + Thread.CurrentThread.ManagedThreadId.ToString().PadRight(4) : string.Empty)
					+ ": " + logText);
			}
			_logEntryAddedEvent.Set();
		}

		private static void LogWriterThread()
		{
			try
			{
				while (true)
				{
					try
					{
						_logEntryAddedEvent.WaitOne(1000);
						lock (_logQueue)
						{
							if (_logQueue.Count > 0)
							{
								File.AppendAllLines(LogPathName, _logQueue);
								_logQueue.Clear();
							}
						}
					}
					catch (ThreadAbortException) { throw; }
					catch { ; /* swallow and be happy :) */ }
				}
			}
			catch (ThreadAbortException) { /* exit */ }
			catch { ; /* this is bad, but there's isn't much we can do.. */ }
		}
	}
}
