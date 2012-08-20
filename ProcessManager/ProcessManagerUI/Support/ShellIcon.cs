using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ProcessManagerUI.Support
{
	public static class ShellIcon
	{
		public static Icon GetSmallIcon(string fileName)
		{
			return GetIcon(fileName, NativeMethods.SHGFI_SMALLICON);
		}

		public static Icon GetLargeIcon(string fileName)
		{
			return GetIcon(fileName, NativeMethods.SHGFI_LARGEICON);
		}

		private static Icon GetIcon(string fileName, uint flags)
		{
			NativeMethods.SHFILEINFO shinfo = new NativeMethods.SHFILEINFO();
			IntPtr hImgSmall = NativeMethods.SHGetFileInfo(fileName, 0, ref shinfo, (uint) Marshal.SizeOf(shinfo), NativeMethods.SHGFI_ICON | flags);
			Icon icon = (Icon) Icon.FromHandle(shinfo.hIcon).Clone();
			NativeMethods.DestroyIcon(shinfo.hIcon);
			return icon;
		}
	}
}
