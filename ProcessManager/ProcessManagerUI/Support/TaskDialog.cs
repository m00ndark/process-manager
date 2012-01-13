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
		private static IDictionary<int, APITaskDialog> _taskDialogs;

		static TaskDialog()
		{
			_taskDialogs = new Dictionary<int, APITaskDialog>();
		}

		#region Properties

		public static bool IsPlatformSupported { get { return APITaskDialog.IsPlatformSupported; } }

		#endregion

		public static DialogResult Show(string title, string instruction, string message,
			string details = null, int progressBarMaxValue = -1, Action<int> tickEventHandler = null)
		{
			return Show(title, instruction, message, TaskDialogStandardButtons.None,
				TaskDialogStandardIcon.None, details, progressBarMaxValue, tickEventHandler);
		}

		public static DialogResult Show(string title, string instruction, string message, MessageBoxIcon icon,
			string details = null, int progressBarMaxValue = -1, Action<int> tickEventHandler = null)
		{
			return Show(title, instruction, message, TaskDialogStandardButtons.None,
				ConvertToTaskDialogStandardIcon(icon), details, progressBarMaxValue, tickEventHandler);
		}

		public static DialogResult Show(string title, string instruction, string message, MessageBoxButtons buttons,
			bool showCloseButtonInsteadOfOK = false, string details = null, int progressBarMaxValue = -1,
			Action<int> tickEventHandler = null)
		{
			return Show(title, instruction, message, ConvertToTaskDialogStandardButtons(buttons, showCloseButtonInsteadOfOK),
				TaskDialogStandardIcon.None, details, progressBarMaxValue, tickEventHandler);
		}

		public static DialogResult Show(string title, string instruction, string message, MessageBoxButtons buttons, MessageBoxIcon icon,
			bool showCloseButtonInsteadOfOK = false, string details = null, int progressBarMaxValue = -1,
			Action<int> tickEventHandler = null)
		{
			return Show(title, instruction, message, ConvertToTaskDialogStandardButtons(buttons, showCloseButtonInsteadOfOK),
				ConvertToTaskDialogStandardIcon(icon), details, progressBarMaxValue, tickEventHandler);
		}

		private static DialogResult Show(string title, string instruction, string message, TaskDialogStandardButtons buttons, TaskDialogStandardIcon icon,
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

			lock (_taskDialogs)
				_taskDialogs[Thread.CurrentThread.ManagedThreadId] = taskDialog;

			DialogResult result = ConvertFromTaskDialogResult(taskDialog.Show());

			if (tickEventHandler != null)
				taskDialog.Tick -= new EventHandler<TaskDialogTickEventArgs>(internalTickEventHandler);

			lock (_taskDialogs)
				_taskDialogs.Remove(Thread.CurrentThread.ManagedThreadId);

			return result;
		}

		public static void Close()
		{
			Close(Thread.CurrentThread.ManagedThreadId);
		}

		public static void Close(int threadID)
		{
			lock (_taskDialogs)
			{
				if (_taskDialogs.ContainsKey(threadID))
				{
					_taskDialogs[threadID].Close();
					_taskDialogs.Remove(threadID);
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

		//public static DialogResult Show(string title, string instruction, bool showCloseButtonInsteadOfOK = false)
		//{
		//    return InternalShow(title, instruction, null, MessageBoxButtons.OK, MessageBoxIcon.None, showCloseButtonInsteadOfOK);
		//}

		//public static DialogResult Show(string title, string instruction, string content, bool showCloseButtonInsteadOfOK = false)
		//{
		//    return InternalShow(title, instruction, content, MessageBoxButtons.OK, MessageBoxIcon.None, showCloseButtonInsteadOfOK);
		//}

		//public static DialogResult Show(string title, string instruction, string content, MessageBoxButtons buttons, bool showCloseButtonInsteadOfOK = false)
		//{
		//    return InternalShow(title, instruction, content, buttons, MessageBoxIcon.None, showCloseButtonInsteadOfOK);
		//}

		//public static DialogResult Show(string title, string instruction, string content, MessageBoxIcon icon, bool showCloseButtonInsteadOfOK = false)
		//{
		//    return InternalShow(title, instruction, content, MessageBoxButtons.OK, icon, showCloseButtonInsteadOfOK);
		//}

		//public static DialogResult Show(string title, string instruction, string content, MessageBoxButtons buttons, MessageBoxIcon icon, bool showCloseButtonInsteadOfOK = false, string details = null)
		//{
		//    return InternalShow(title, instruction, content, buttons, icon, showCloseButtonInsteadOfOK, details);
		//}

		//private static DialogResult InternalShow(string title, string instruction, string content, MessageBoxButtons buttons, MessageBoxIcon icon, bool showCloseButtonInsteadOfOK, string details = null)
		//{
		//    int dialogResult = 0;
		//    bool taskDialogFailed = false;

		//    try
		//    {
		//        IntPtr parent = NativeMethods.GetActiveWindow();
		//        NativeMethods.TaskDialogButtons taskDialogButtons = ConvertToTaskDialogButtons(buttons, showCloseButtonInsteadOfOK);
		//        NativeMethods.TaskDialogIcon taskDialogIcon = ConvertToTaskDialogIcon(icon);
		//        if (details != null)
		//        {
		//            int selectedRadioButton;
		//            bool setVerification;
		//            NativeMethods.TaskDialogConfig taskDialogConfig = CreateTaskDialogConfig(parent, title, instruction, content, details, taskDialogButtons, taskDialogIcon);
		//            if (NativeMethods.TaskDialogIndirect(ref taskDialogConfig, out dialogResult, out selectedRadioButton, out setVerification) != IntPtr.Zero)
		//                throw new Exception();
		//        }
		//        else
		//        {
		//            if (NativeMethods.TaskDialog(parent, IntPtr.Zero, title, instruction, content, (int) taskDialogButtons, new IntPtr((long) taskDialogIcon), out dialogResult) != 0)
		//                throw new Exception();
		//        }
		//    }
		//    catch
		//    {
		//        taskDialogFailed = true;
		//    }

		//    if (taskDialogFailed)
		//        return MessageBox.Show(instruction + Environment.NewLine + Environment.NewLine + content, title, buttons, icon);

		//    if (dialogResult > 0 && dialogResult < 8)
		//        return (DialogResult) dialogResult;

		//    return DialogResult.None;
		//}

		//private static NativeMethods.TaskDialogConfig CreateTaskDialogConfig(IntPtr parent, string title, string instruction, string content,
		//    string details, NativeMethods.TaskDialogButtons buttons, NativeMethods.TaskDialogIcon icon)
		//{
		//    return new NativeMethods.TaskDialogConfig()
		//        {
		//            cbSize = (uint) Marshal.SizeOf(typeof(NativeMethods.TaskDialogConfig)),
		//            hwndParent = parent,
		//            hInstance = IntPtr.Zero,
		//            hMainIcon = (IntPtr) icon,
		//            dwCommonButtons = buttons,
		//            pszWindowTitle = title,
		//            pszMainInstruction = instruction,
		//            pszContent = content,
		//            pszVerificationText = null,
		//            pszExpandedInformation = details,
		//            pszExpandedControlText = "Show Details",
		//            pszCollapsedControlText = "Hide Details",
		//            pszFooter = null,
		//            hFooterIcon = IntPtr.Zero,
		//            cxWidth = 0,
		//            cButtons = 0,
		//            pButtons = IntPtr.Zero,
		//            nDefaultButton = (int) NativeMethods.TaskDialogButtons.OK,
		//            cRadioButtons = 0,
		//            pRadioButtons = IntPtr.Zero,
		//            nDefaultRadioButton = 0,
		//            pfCallback = null,
		//            lpCallbackData = IntPtr.Zero,
		//            dwFlags = NativeMethods.TaskDialogFlags.TDF_POSITION_RELATIVE_TO_WINDOW
		//                | NativeMethods.TaskDialogFlags.TDF_EXPAND_FOOTER_AREA
		//        };
		//}

		//private static NativeMethods.TaskDialogButtons ConvertToTaskDialogButtons(MessageBoxButtons buttons, bool showCloseButtonInsteadOfOK)
		//{
		//    NativeMethods.TaskDialogButtons taskDialogButtons;
		//    switch (buttons)
		//    {
		//        case MessageBoxButtons.OK:
		//            taskDialogButtons = (showCloseButtonInsteadOfOK ? NativeMethods.TaskDialogButtons.Close : NativeMethods.TaskDialogButtons.OK);
		//            break;
		//        case MessageBoxButtons.OKCancel:
		//            taskDialogButtons = NativeMethods.TaskDialogButtons.OK | NativeMethods.TaskDialogButtons.Cancel;
		//            break;
		//        case MessageBoxButtons.YesNoCancel:
		//            taskDialogButtons = NativeMethods.TaskDialogButtons.Yes | NativeMethods.TaskDialogButtons.No | NativeMethods.TaskDialogButtons.Cancel;
		//            break;
		//        case MessageBoxButtons.YesNo:
		//            taskDialogButtons = NativeMethods.TaskDialogButtons.Yes | NativeMethods.TaskDialogButtons.No;
		//            break;
		//        case MessageBoxButtons.RetryCancel:
		//            taskDialogButtons = NativeMethods.TaskDialogButtons.Retry | NativeMethods.TaskDialogButtons.Cancel;
		//            break;
		//        default:
		//            taskDialogButtons = NativeMethods.TaskDialogButtons.Close;
		//            break;
		//    }
		//    return taskDialogButtons;
		//}

		//private static NativeMethods.TaskDialogIcon ConvertToTaskDialogIcon(MessageBoxIcon icon)
		//{
		//    NativeMethods.TaskDialogIcon taskDialogIcon;
		//    switch (icon)
		//    {
		//        case MessageBoxIcon.Information:
		//            taskDialogIcon = NativeMethods.TaskDialogIcon.Information;
		//            break;
		//        case MessageBoxIcon.Warning: // same value as MessageBoxIcon.Exclamation
		//            taskDialogIcon = NativeMethods.TaskDialogIcon.Warning;
		//            break;
		//        case MessageBoxIcon.Stop: // same value as MessageBoxIcon.Error
		//            taskDialogIcon = NativeMethods.TaskDialogIcon.Stop;
		//            break;
		//        default:
		//            taskDialogIcon = NativeMethods.TaskDialogIcon.None;
		//            break;
		//    }
		//    return taskDialogIcon;
		//}
	}
}
