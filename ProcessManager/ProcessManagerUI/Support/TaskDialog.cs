using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProcessManagerUI.Support
{
	public static class TaskDialog
	{
		public static DialogResult Show(string title, string instruction, bool showCloseButtonInsteadOfOK = false)
		{
			return InternalShow(title, instruction, null, MessageBoxButtons.OK, MessageBoxIcon.None, showCloseButtonInsteadOfOK);
		}

		public static DialogResult Show(string title, string instruction, string content, bool showCloseButtonInsteadOfOK = false)
		{
			return InternalShow(title, instruction, content, MessageBoxButtons.OK, MessageBoxIcon.None, showCloseButtonInsteadOfOK);
		}

		public static DialogResult Show(string title, string instruction, string content, MessageBoxButtons buttons, bool showCloseButtonInsteadOfOK = false)
		{
			return InternalShow(title, instruction, content, buttons, MessageBoxIcon.None, showCloseButtonInsteadOfOK);
		}

		public static DialogResult Show(string title, string instruction, string content, MessageBoxIcon icon, bool showCloseButtonInsteadOfOK = false)
		{
			return InternalShow(title, instruction, content, MessageBoxButtons.OK, icon, showCloseButtonInsteadOfOK);
		}

		public static DialogResult Show(string title, string instruction, string content, MessageBoxButtons buttons, MessageBoxIcon icon, bool showCloseButtonInsteadOfOK = false, string details = null)
		{
			return InternalShow(title, instruction, content, buttons, icon, showCloseButtonInsteadOfOK, details);
		}

		private static DialogResult InternalShow(string title, string instruction, string content, MessageBoxButtons buttons, MessageBoxIcon icon, bool showCloseButtonInsteadOfOK, string details = null)
		{
			int dialogResult = 0;
			bool taskDialogFailed = false;

			try
			{
				IntPtr parent = NativeMethods.GetActiveWindow();
				NativeMethods.TaskDialogButtons taskDialogButtons = ConvertToTaskDialogButtons(buttons, showCloseButtonInsteadOfOK);
				NativeMethods.TaskDialogIcon taskDialogIcon = ConvertToTaskDialogIcon(icon);
				if (details != null)
				{
					int selectedRadioButton;
					bool setVerification;
					NativeMethods.TaskDialogConfig taskDialogConfig = CreateTaskDialogConfig(parent, title, instruction, content, details, taskDialogButtons, taskDialogIcon);
					if (NativeMethods.TaskDialogIndirect(ref taskDialogConfig, out dialogResult, out selectedRadioButton, out setVerification) != IntPtr.Zero)
						throw new Exception();
				}
				else
				{
					if (NativeMethods.TaskDialog(parent, IntPtr.Zero, title, instruction, content, (int) taskDialogButtons, new IntPtr((long) taskDialogIcon), out dialogResult) != 0)
						throw new Exception();
				}
			}
			catch
			{
				taskDialogFailed = true;
			}

			if (taskDialogFailed)
				return MessageBox.Show(instruction + Environment.NewLine + Environment.NewLine + content, title, buttons, icon);

			if (dialogResult > 0 && dialogResult < 8)
				return (DialogResult) dialogResult;

			return DialogResult.None;
		}

		private static NativeMethods.TaskDialogConfig CreateTaskDialogConfig(IntPtr parent, string title, string instruction, string content,
			string details, NativeMethods.TaskDialogButtons buttons, NativeMethods.TaskDialogIcon icon)
		{
			return new NativeMethods.TaskDialogConfig()
				{
					cbSize = (uint) Marshal.SizeOf(typeof(NativeMethods.TaskDialogConfig)),
					hwndParent = parent,
					hInstance = IntPtr.Zero,
					hMainIcon = (IntPtr) icon,
					dwCommonButtons = buttons,
					pszWindowTitle = title,
					pszMainInstruction = instruction,
					pszContent = content,
					pszVerificationText = null,
					pszExpandedInformation = details,
					pszExpandedControlText = "Show Details",
					pszCollapsedControlText = "Hide Details",
					pszFooter = null,
					hFooterIcon = IntPtr.Zero,
					cxWidth = 0,
					cButtons = 0,
					pButtons = IntPtr.Zero,
					nDefaultButton = (int) NativeMethods.TaskDialogButtons.OK,
					cRadioButtons = 0,
					pRadioButtons = IntPtr.Zero,
					nDefaultRadioButton = 0,
					pfCallback = null,
					lpCallbackData = IntPtr.Zero,
					dwFlags = NativeMethods.TaskDialogFlags.TDF_POSITION_RELATIVE_TO_WINDOW
						| NativeMethods.TaskDialogFlags.TDF_EXPAND_FOOTER_AREA
				};
		}

		private static NativeMethods.TaskDialogButtons ConvertToTaskDialogButtons(MessageBoxButtons buttons, bool showCloseButtonInsteadOfOK)
		{
			NativeMethods.TaskDialogButtons taskDialogButtons;
			switch (buttons)
			{
				case MessageBoxButtons.OK:
					taskDialogButtons = (showCloseButtonInsteadOfOK ? NativeMethods.TaskDialogButtons.Close : NativeMethods.TaskDialogButtons.OK);
					break;
				case MessageBoxButtons.OKCancel:
					taskDialogButtons = NativeMethods.TaskDialogButtons.OK | NativeMethods.TaskDialogButtons.Cancel;
					break;
				case MessageBoxButtons.YesNoCancel:
					taskDialogButtons = NativeMethods.TaskDialogButtons.Yes | NativeMethods.TaskDialogButtons.No | NativeMethods.TaskDialogButtons.Cancel;
					break;
				case MessageBoxButtons.YesNo:
					taskDialogButtons = NativeMethods.TaskDialogButtons.Yes | NativeMethods.TaskDialogButtons.No;
					break;
				case MessageBoxButtons.RetryCancel:
					taskDialogButtons = NativeMethods.TaskDialogButtons.Retry | NativeMethods.TaskDialogButtons.Cancel;
					break;
				default:
					taskDialogButtons = NativeMethods.TaskDialogButtons.Close;
					break;
			}
			return taskDialogButtons;
		}

		private static NativeMethods.TaskDialogIcon ConvertToTaskDialogIcon(MessageBoxIcon icon)
		{
			NativeMethods.TaskDialogIcon taskDialogIcon;
			switch (icon)
			{
				case MessageBoxIcon.Information:
					taskDialogIcon = NativeMethods.TaskDialogIcon.Information;
					break;
				case MessageBoxIcon.Warning: // same value as MessageBoxIcon.Exclamation
					taskDialogIcon = NativeMethods.TaskDialogIcon.Warning;
					break;
				case MessageBoxIcon.Stop: // same value as MessageBoxIcon.Error
					taskDialogIcon = NativeMethods.TaskDialogIcon.Stop;
					break;
				default:
					taskDialogIcon = NativeMethods.TaskDialogIcon.None;
					break;
			}
			return taskDialogIcon;
		}
	}
}
