using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProcessManagerUI.Controls
{
	[ToolboxBitmap(typeof(Panel))]
	public class BackgroundPanel : Panel
	{
		public BackgroundPanel()
		{
			BackColor = Color.FromKnownColor(KnownColor.Window);
			BorderColor = Color.FromArgb(130, 135, 144);
			DoubleBuffered = true;
		}

		[Description("Gets or sets the panel border color."), Category("Appearance")]
		public Color BorderColor { get; set; }

		protected override void OnClientSizeChanged(System.EventArgs e)
		{
			base.OnClientSizeChanged(e);
			Invalidate(new Region(new Rectangle(new Point(0, 0), ClientSize)));
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.FillRectangle(SystemBrushes.Window, 1, 1, Size.Width - 2, Size.Height - 2);
			e.Graphics.DrawRectangle(new Pen(BorderColor), 0, 0, Size.Width - 1, Size.Height - 1);
		}
	}
}
