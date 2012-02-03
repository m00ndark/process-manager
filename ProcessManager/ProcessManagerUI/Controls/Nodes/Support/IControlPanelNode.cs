﻿using System;
using System.Drawing;
using System.Windows.Forms;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;

namespace ProcessManagerUI.Controls.Nodes.Support
{
	public interface IControlPanelNode
	{
		Guid ID { get; }
		Size Size { get; }
		CheckState CheckState { get; }

		event EventHandler CheckedChanged;
		event EventHandler<ApplicationActionEventArgs> ActionTaken;

		void Dispose();
		Size LayoutNode();
		void ForceWidth(int width);
		void Check(bool @checked);
		void TakeAction(ApplicationActionType type);
	}
}
