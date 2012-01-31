namespace ProcessManagerUI.Support
{
	public class ComboBoxItem<T>
	{
		public ComboBoxItem(string text) : this(text, default(T)) {}

		public ComboBoxItem(string text, T tag)
		{
			Text = text;
			Tag = tag;
		}

		#region Properties

		public string Text { get; private set; }
		public T Tag { get; private set; }

		#endregion

		public override string ToString()
		{
			return Text;
		}
	}
}
