namespace ProcessManagerUI.Support
{
	public class ComboBoxItem<T>
	{
		public ComboBoxItem(string text) : this(text, default(T)) {}

		public ComboBoxItem(T tag) : this(null, tag) {}

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
			return Text ?? Tag.ToString();
		}
	}

	public class ComboBoxItem : ComboBoxItem<object>
	{
		public ComboBoxItem(string text) : base(text) {}
		public ComboBoxItem(object tag) : base(tag) {}
		public ComboBoxItem(string text, object tag) : base(text, tag) {}
	}
}
