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
		}

		[Description("Gets or sets the panel border color."), Category("Appearance")]
		public Color BorderColor { get; set; }

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.DrawRectangle(
				new Pen(BorderColor),
				e.ClipRectangle.Left,
				e.ClipRectangle.Top,
				e.ClipRectangle.Width - 1,
				e.ClipRectangle.Height - 1);
		}
	}
}
