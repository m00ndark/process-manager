using System;
using System.Windows.Forms;
using ProcessManager;
using ProcessManagerUI.Support;

namespace ProcessManagerUI.Utilities
{
	public static class Messenger
	{
		public static void ShowError(Exception ex, bool showAsDetails)
		{
			if (showAsDetails)
				ShowError("An exception occurred",
					"An unexpected exception occurred, please try again. If this error continues to occur, contact application author.", ex.Message);
			else
				ShowError("An exception occurred", ex.ToString());
		}

		public static void ShowError(string instruction, string message, string details = null)
		{
			ShowMessage(instruction, message, MessageBoxButtons.OK, MessageBoxIcon.Error, details);
		}

		public static void ShowInformation(string instruction, string message, string details = null)
		{
			ShowMessage(instruction, message, MessageBoxButtons.OK, MessageBoxIcon.Information, details);
		}

		public static void ShowWarning(string instruction, string message, string details = null)
		{
			ShowMessage(instruction, message, MessageBoxButtons.OK, MessageBoxIcon.Warning, details);
		}

		public static DialogResult ShowQuestion(string instruction, string message, string details = null)
		{
			return ShowMessage(instruction, message, MessageBoxButtons.YesNo, MessageBoxIcon.Information, details);
		}

		public static DialogResult ShowWarningQuestion(string instruction, string message, string details = null)
		{
			return ShowMessage(instruction, message, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, details);
		}

		public static DialogResult ShowMessage(string instruction, string message, MessageBoxButtons buttons, MessageBoxIcon icon, string details = null)
		{
			if (TaskDialog.IsPlatformSupported)
				return TaskDialog.Show(Settings.Constants.APPLICATION_NAME, instruction, message, buttons, icon, true, details);

			return MessageBox.Show(instruction + Environment.NewLine + Environment.NewLine + message, Settings.Constants.APPLICATION_NAME, buttons, icon);
		}
	}
}
