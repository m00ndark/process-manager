using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using APITaskDialog = Microsoft.WindowsAPICodePack.Dialogs.TaskDialog;

namespace ProcessManagerUI.Support
{
	public static class TaskDialog
	{
		private static readonly IDictionary<Guid, APITaskDialog> _taskDialogs;

		static TaskDialog()
		{
			_taskDialogs = new Dictionary<Guid, APITaskDialog>();
		}

		#region Properties

		public static bool IsPlatformSupported { get { return APITaskDialog.IsPlatformSupported; } }

		#endregion

		public static DialogResult Show(string title, string instruction, string message,
			string details = null, int progressBarMaxValue = -1, Action<int> tickEventHandler = null)
		{
			return Show(Guid.Empty, title, instruction, message, TaskDialogStandardButtons.None,
				TaskDialogStandardIcon.None, details, progressBarMaxValue, tickEventHandler);
		}

		public static DialogResult Show(string title, string instruction, string message, MessageBoxIcon icon,
			string details = null, int progressBarMaxValue = -1, Action<int> tickEventHandler = null)
		{
			return Show(Guid.Empty, title, instruction, message, TaskDialogStandardButtons.None,
				ConvertToTaskDialogStandardIcon(icon), details, progressBarMaxValue, tickEventHandler);
		}

		public static DialogResult Show(string title, string instruction, string message, MessageBoxButtons buttons,
			bool showCloseButtonInsteadOfOK = false, string details = null, int progressBarMaxValue = -1,
			Action<int> tickEventHandler = null)
		{
			return Show(Guid.Empty, title, instruction, message, ConvertToTaskDialogStandardButtons(buttons, showCloseButtonInsteadOfOK),
				TaskDialogStandardIcon.None, details, progressBarMaxValue, tickEventHandler);
		}

		public static DialogResult Show(string title, string instruction, string message, MessageBoxButtons buttons, MessageBoxIcon icon,
			bool showCloseButtonInsteadOfOK = false, string details = null, int progressBarMaxValue = -1,
			Action<int> tickEventHandler = null)
		{
			return Show(Guid.Empty, title, instruction, message, ConvertToTaskDialogStandardButtons(buttons, showCloseButtonInsteadOfOK),
				ConvertToTaskDialogStandardIcon(icon), details, progressBarMaxValue, tickEventHandler);
		}

		public static DialogResult Show(Guid id, string title, string instruction, string message,
			string details = null, int progressBarMaxValue = -1, Action<int> tickEventHandler = null)
		{
			return Show(id, title, instruction, message, TaskDialogStandardButtons.None,
				TaskDialogStandardIcon.None, details, progressBarMaxValue, tickEventHandler);
		}

		public static DialogResult Show(Guid id, string title, string instruction, string message, MessageBoxIcon icon,
			string details = null, int progressBarMaxValue = -1, Action<int> tickEventHandler = null)
		{
			return Show(id, title, instruction, message, TaskDialogStandardButtons.None,
				ConvertToTaskDialogStandardIcon(icon), details, progressBarMaxValue, tickEventHandler);
		}

		public static DialogResult Show(Guid id, string title, string instruction, string message, MessageBoxButtons buttons,
			bool showCloseButtonInsteadOfOK = false, string details = null, int progressBarMaxValue = -1,
			Action<int> tickEventHandler = null)
		{
			return Show(id, title, instruction, message, ConvertToTaskDialogStandardButtons(buttons, showCloseButtonInsteadOfOK),
				TaskDialogStandardIcon.None, details, progressBarMaxValue, tickEventHandler);
		}

		public static DialogResult Show(Guid id, string title, string instruction, string message, MessageBoxButtons buttons, MessageBoxIcon icon,
			bool showCloseButtonInsteadOfOK = false, string details = null, int progressBarMaxValue = -1,
			Action<int> tickEventHandler = null)
		{
			return Show(id, title, instruction, message, ConvertToTaskDialogStandardButtons(buttons, showCloseButtonInsteadOfOK),
				ConvertToTaskDialogStandardIcon(icon), details, progressBarMaxValue, tickEventHandler);
		}

		private static DialogResult Show(Guid id, string title, string instruction, string message, TaskDialogStandardButtons buttons, TaskDialogStandardIcon icon,
			string details, int progressBarMaxValue, Action<int> tickEventHandler)
		{
			TaskDialogProgressBar progressBar = (progressBarMaxValue < 0 ? null
				: new TaskDialogProgressBar(0, progressBarMaxValue, 0)
					{
						State = (progressBarMaxValue == 0 ? TaskDialogProgressBarState.Marquee : TaskDialogProgressBarState.Normal)
					});

			APITaskDialog taskDialog = new APITaskDialog()
				{
					Caption = title,
					InstructionText = instruction,
					Text = message,
					Icon = icon,
					StandardButtons = buttons,
					Cancelable = true,
					ExpansionMode = TaskDialogExpandedDetailsLocation.ExpandFooter,
					DetailsExpanded = false,
					DetailsCollapsedLabel = "Show Details",
					DetailsExpandedLabel = "Hide Details",
					DetailsExpandedText = details,
					ProgressBar = progressBar,
					StartupLocation = TaskDialogStartupLocation.CenterOwner,
					OwnerWindowHandle = NativeMethods.GetActiveWindow()
				};

			Action<object, TaskDialogTickEventArgs> internalTickEventHandler = null;
			if (tickEventHandler != null)
			{
				internalTickEventHandler = (sender, e) => tickEventHandler(e.Ticks);
				taskDialog.Tick += new EventHandler<TaskDialogTickEventArgs>(internalTickEventHandler);
			}

			if (id != Guid.Empty)
			{
				lock (_taskDialogs)
					_taskDialogs[id] = taskDialog;
			}

			DialogResult result = ConvertFromTaskDialogResult(taskDialog.Show());

			if (tickEventHandler != null)
				taskDialog.Tick -= new EventHandler<TaskDialogTickEventArgs>(internalTickEventHandler);

			if (id != Guid.Empty)
			{
				lock (_taskDialogs)
					_taskDialogs.Remove(id);
			}

			return result;
		}

		public static void Close(Guid id)
		{
			lock (_taskDialogs)
			{
				if (_taskDialogs.ContainsKey(id))
				{
					_taskDialogs[id].Close();
					_taskDialogs.Remove(id);
				}
			}
		}

		#region Helpers

		private static TaskDialogStandardButtons ConvertToTaskDialogStandardButtons(MessageBoxButtons buttons, bool showCloseButtonInsteadOfOK)
		{
			TaskDialogStandardButtons taskDialogButtons;
			switch (buttons)
			{
				case MessageBoxButtons.OK:
					taskDialogButtons = (showCloseButtonInsteadOfOK ? TaskDialogStandardButtons.Close : TaskDialogStandardButtons.Ok);
					break;
				case MessageBoxButtons.OKCancel:
					taskDialogButtons = TaskDialogStandardButtons.Ok | TaskDialogStandardButtons.Cancel;
					break;
				case MessageBoxButtons.YesNoCancel:
					taskDialogButtons = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No | TaskDialogStandardButtons.Cancel;
					break;
				case MessageBoxButtons.YesNo:
					taskDialogButtons = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No;
					break;
				case MessageBoxButtons.RetryCancel:
					taskDialogButtons = TaskDialogStandardButtons.Retry | TaskDialogStandardButtons.Cancel;
					break;
				default:
					taskDialogButtons = TaskDialogStandardButtons.Close;
					break;
			}
			return taskDialogButtons;
		}

		private static TaskDialogStandardIcon ConvertToTaskDialogStandardIcon(MessageBoxIcon icon)
		{
			TaskDialogStandardIcon taskDialogIcon;
			switch (icon)
			{
				case MessageBoxIcon.Information:
					taskDialogIcon = TaskDialogStandardIcon.Information;
					break;
				case MessageBoxIcon.Warning: // same value as MessageBoxIcon.Exclamation
					taskDialogIcon = TaskDialogStandardIcon.Warning;
					break;
				case MessageBoxIcon.Error: // same value as MessageBoxIcon.Stop
					taskDialogIcon = TaskDialogStandardIcon.Error;
					break;
				default:
					taskDialogIcon = TaskDialogStandardIcon.None;
					break;
			}
			return taskDialogIcon;
		}

		private static DialogResult ConvertFromTaskDialogResult(TaskDialogResult result)
		{
			switch (result)
			{
				case TaskDialogResult.Cancel:
					return DialogResult.Cancel;
				case TaskDialogResult.No:
					return DialogResult.No;
				case TaskDialogResult.None:
					return DialogResult.None;
				case TaskDialogResult.Ok:
					return DialogResult.OK;
				case TaskDialogResult.Retry:
					return DialogResult.Retry;
				case TaskDialogResult.Yes:
					return DialogResult.Yes;
				default:
					return DialogResult.None;
			}
		}

		#endregion
	}
}
