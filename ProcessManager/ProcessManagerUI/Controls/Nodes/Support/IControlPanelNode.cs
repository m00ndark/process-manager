using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProcessManagerUI.Controls.Nodes.Support
{
	public interface IControlPanelNode
	{
		Guid ID { get; }
		Size Size { get; }
		CheckState CheckState { get; }

		event EventHandler CheckedChanged;

		void Dispose();
		Size LayoutNode();
		void ForceWidth(int width);
		void Check(bool @checked);
		void Start();
		void Stop();
		void Restart();
	}
}
