using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using ProcessManager.DataAccess;

namespace ProcessManager.Utilities
{
	public enum LogType
	{
		Debug = 0,
		Flow = 1,
		Warning = 2,
		Error = 3
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
				Settings.Defaults.COMPANY_FOLDER_NAME), Settings.Defaults.APPLICATION_FOLDER_NAME);
			_logFolder = Path.Combine(_appDataFolder, Settings.Read("LogFolderName"));
			_logFileNameTemplate = Settings.Read("LogFileName");

			FileSystemHandler.CreateDirectory(_logFolder);

			_logQueue = new Queue<string>();
			_logEntryAddedEvent = new AutoResetEvent(false);
			SetupLogTypeIndentationDepth();
			DefaultLogType = LogType.Flow;

			new Thread(LogWriterThread) { IsBackground = true, Name = "Log Writer Thread" }.Start();
		}

		#region Properties

		public static LogType DefaultLogType { get; set; }

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
			Add(LogType.Error, logText);
			Add(LogType.Error, exception.ToString());
		}

		public static void Add(LogType logType, string logText)
		{
			lock (_logQueue)
			{
				_logQueue.Enqueue(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
					+ " > " + logType.ToString().PadRight(_logTypeIndentationDepth) + " " + logText);
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
