namespace ProcessManager.DataObjects
{
	public interface IFileSystemEntry
	{
		string Name { get; }
		bool IsFolder { get; }

		bool Equals(string name);
	}
}