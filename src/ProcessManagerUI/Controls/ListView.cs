/*
* VISTA CONTROLS FOR .NET 2.0
* ENHANCED LISTVIEW
* 
* Written by Marco Minerva, mailto:marco.minerva@gmail.com
* 
* This code is released under the Microsoft Community License (Ms-CL).
*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ProcessManagerUI.Support;

namespace ProcessManagerUI.Controls
{
	[ToolboxBitmap(typeof(ListView))]
	public class ListView : System.Windows.Forms.ListView
	{
		private Boolean _flag = false;

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case 15:
					//Paint event
					if (!_flag)
					{
						//1-time run needed
						NativeMethods.SetWindowTheme(Handle, "explorer", null); //Explorer style
						if (ShowColumnLines)
							NativeMethods.SendMessage(Handle, NativeMethods.LVM_SETEXTENDEDLISTVIEWSTYLE, NativeMethods.LVS_EX_DOUBLEBUFFER, NativeMethods.LVS_EX_DOUBLEBUFFER); //Blue selection, keeps other extended styles
						_flag = true;
					}
					break;
			}
			base.WndProc(ref m);
		}

		[Description("Gets or sets whether the control renders column lines."), Category("Appearance"), DefaultValue(false)]
		public bool ShowColumnLines { get; set; }
	}
}
