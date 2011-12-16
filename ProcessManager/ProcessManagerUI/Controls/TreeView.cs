/*
* VISTA CONTROLS FOR .NET 2.0
* ENHANCED TREEVIEW
* 
* Written by Marco Minerva, mailto:marco.minerva@gmail.com
* 
* This code is released under the Microsoft Community License (Ms-CL).
*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProcessManagerUI.Controls
{
	[ToolboxBitmap(typeof(TreeView))]
	public class TreeView : System.Windows.Forms.TreeView
	{
		public TreeView()
		{
			base.HotTracking = true;
			base.ShowLines = false;
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style |= NativeMethods.TVS_NOHSCROLL;
				return createParams;
			}
		}

		[Browsable(false)]
		private new bool HotTracking
		{
			get { return base.HotTracking; }
			set { base.HotTracking = true; }
		}

		[Browsable(false)]
		private new bool ShowLines
		{
			get { return base.ShowLines; }
			set { base.ShowLines = false; }
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			NativeMethods.SetWindowTheme(Handle, "explorer", null);

			int style = NativeMethods.SendMessage(Handle, Convert.ToUInt32(NativeMethods.TVM_GETEXTENDEDSTYLE), 0, 0);
			style |= (NativeMethods.TVS_EX_AUTOHSCROLL | NativeMethods.TVS_EX_FADEINOUTEXPANDOS | NativeMethods.TVS_EX_DOUBLEBUFFER);
			NativeMethods.SendMessage(Handle, NativeMethods.TVM_SETEXTENDEDSTYLE, 0, style);
		}
	}
}
