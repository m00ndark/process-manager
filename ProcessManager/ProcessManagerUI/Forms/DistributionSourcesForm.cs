using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Service.Client;
using ProcessManagerUI.Utilities;
using Application = ProcessManager.DataObjects.Application;

namespace ProcessManagerUI.Forms
{
	public partial class DistributionSourcesForm : Form
	{
		#region BrowseMode enum

		private enum BrowseMode
		{
			BasedOnGroup,
			BasedOnApplication
		}

		#endregion

		#region BrowseModeWrapper class

		private class BrowseModeWrapper
		{
			public BrowseModeWrapper(BrowseMode browseMode)
			{
				BrowseMode = browseMode;
			}

			public BrowseMode BrowseMode { get; }

			public override string ToString()
			{
				switch (BrowseMode)
				{
					case BrowseMode.BasedOnGroup:
						return "Based On Group";
					case BrowseMode.BasedOnApplication:
						return "Based On Application";
				}
				throw new InvalidOperationException();
			}
		}

		#endregion

		private const string UNSPECIFIED_PATH = "<unspecified>";

		private readonly Func<IEnumerable<Group>> _getAllGroups;
		private bool _machineAvailable;
		private DistributionSource _selectedSource;
		private bool _disableStateChangedEvents;

		public DistributionSourcesForm(Machine machine, Application application, Func<IEnumerable<Group>> getAllGroups)
		{
			InitializeComponent();

			Machine = machine;
			Application = application;
			DistributionSources = new List<DistributionSource>(application.Sources.Select(source => source.Clone()));
			DistributionSourcesChanged = false;
			_getAllGroups = getAllGroups;
			_machineAvailable = ConnectionStore.ConnectionCreated(Machine);
			_selectedSource = null;
			_disableStateChangedEvents = false;
		}

		#region Properties

		public Machine Machine { get; }
		public Application Application { get; }
		public List<DistributionSource> DistributionSources { get; }
		public bool DistributionSourcesChanged { get; private set; }

		#endregion

		#region GUI event handlers

		private void DistributionSourcesForm_Load(object sender, EventArgs e)
		{
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerConnectionChanged += ServiceConnectionHandler_ServiceHandlerConnectionChanged;

			foreach (DistributionSource source in DistributionSources)
			{
				listViewSources.Items.Add(new ListViewItem(new[]
					{
						source.Path ?? UNSPECIFIED_PATH,
						source.Filter,
						source.Recursive.ToString(CultureInfo.InvariantCulture),
						source.Inclusive.ToString(CultureInfo.InvariantCulture)
					}) { Tag = source });
			}

			EnableControls();
		}

		private void DistributionSourcesForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerConnectionChanged -= ServiceConnectionHandler_ServiceHandlerConnectionChanged;
		}

		private void ListViewSources_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listViewSources.SelectedItems.Count == 0)
			{
				panelSource.Visible = false;
				_selectedSource = null;
			}
			else
			{
				_disableStateChangedEvents = true;
				_selectedSource = (DistributionSource) listViewSources.SelectedItems[0].Tag;
				DisplayPath(_selectedSource.Path);
				textBoxFilter.Text = _selectedSource.Filter;
				checkBoxRecursive.Checked = _selectedSource.Recursive;
				checkBoxInclusive.Checked = _selectedSource.Inclusive;
				_disableStateChangedEvents = false;
				EnableControls();
				panelSource.Visible = true;
			}
		}

		private void ButtonAddSource_Click(object sender, EventArgs e)
		{
			if (!_machineAvailable)
				return;

			_disableStateChangedEvents = true;
			_selectedSource = new DistributionSource();
			DistributionSources.Add(_selectedSource);
			DistributionSourcesChanged = true;
			DisplayPath(_selectedSource.Path);
			textBoxFilter.Text = _selectedSource.Filter;
			checkBoxRecursive.Checked = _selectedSource.Recursive;
			checkBoxInclusive.Checked = _selectedSource.Inclusive;
			ListViewItem item = listViewSources.Items.Add(new ListViewItem(new[]
				{
					_selectedSource.Path ?? UNSPECIFIED_PATH,
					_selectedSource.Filter,
					_selectedSource.Recursive.ToString(CultureInfo.InvariantCulture),
					_selectedSource.Inclusive.ToString(CultureInfo.InvariantCulture)
				}) { Tag = _selectedSource });
			_disableStateChangedEvents = false;
			item.Selected = true;
			panelSource.Visible = true;
			EnableControls();
			textBoxPath.Focus();
		}

		private void ButtonRemoveSource_Click(object sender, EventArgs e)
		{
			if (!_machineAvailable)
				return;

			if (listViewSources.SelectedItems.Count == 0)
				return;

			_selectedSource = (DistributionSource) listViewSources.SelectedItems[0].Tag;
			DistributionSources.Remove(_selectedSource);
			DistributionSourcesChanged = true;
			listViewSources.Items.Remove(listViewSources.SelectedItems[0]);
			_selectedSource = null;
			EnableControls();
		}

		private void TextBoxPath_TextChanged(object sender, EventArgs e)
		{
			if (_disableStateChangedEvents)
				return;

			SourceChanged();
			EnableControls();
		}

		private void TextBoxPath_Enter(object sender, EventArgs e)
		{
			if (textBoxPath.ForeColor == Color.Silver)
				DisplayPath(string.Empty);
		}

		private void TextBoxPath_Leave(object sender, EventArgs e)
		{
			UpdateSelectedSource();
		}

		private void ButtonBrowseSourcePath_Click(object sender, EventArgs e)
		{
			if (!_machineAvailable)
				return;

			if (textBoxPath.ForeColor == Color.Silver)
				Picker.ShowMenu(buttonBrowseSourcePath, Enum.GetValues(typeof(BrowseMode)).Cast<BrowseMode>().Select(x => new BrowseModeWrapper(x)),
					_getAllGroups(), ContextMenu_BrowseSourcePath_GroupClicked);
			else
				Picker.ShowMenu(buttonBrowseSourcePath, _getAllGroups(), group => ContextMenu_BrowseSourcePath_GroupClicked(null, group));
		}

		private void TextBoxFilter_TextChanged(object sender, EventArgs e)
		{
			if (_disableStateChangedEvents)
				return;

			SourceChanged();
			EnableControls();
		}

		private void TextBoxFilter_Leave(object sender, EventArgs e)
		{
			UpdateSelectedSource();
		}

		private void CheckBoxRecursive_CheckedChanged(object sender, EventArgs e)
		{
			if (_disableStateChangedEvents)
				return;

			UpdateSelectedSource();
		}

		private void CheckBoxInclusive_CheckedChanged(object sender, EventArgs e)
		{
			if (_disableStateChangedEvents)
				return;

			UpdateSelectedSource();
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			UpdateSelectedSource();

			if (DistributionSourcesChanged && !ValidateSources())
				return;

			DialogResult = DialogResult.OK;
			Close();
		}

		#endregion

		#region Picker event handlers

		private void ContextMenu_BrowseSourcePath_GroupClicked(BrowseModeWrapper browseModeWrapper, Group group)
		{
			string sourcePath = (textBoxPath.ForeColor == Color.Silver && browseModeWrapper != null
				? (browseModeWrapper.BrowseMode == BrowseMode.BasedOnGroup
					? group.Path
					: Path.Combine(group.Path, Application.RelativePath.Trim(Path.DirectorySeparatorChar)))
				: Path.Combine(group.Path, textBoxPath.Text.Trim(Path.DirectorySeparatorChar)));
			FileSystemBrowserForm fileSystemBrowser = new FileSystemBrowserForm(Machine)
				{
					Description = "Select a source path for the distribution...",
					SelectedPath = sourcePath,
					BrowserMode = FileSystemBrowserForm.Mode.Folder | FileSystemBrowserForm.Mode.File
				};

			if (fileSystemBrowser.ShowDialog(this) != DialogResult.OK)
				return;

			if (!fileSystemBrowser.SelectedPath.StartsWith(@group.Path, StringComparison.CurrentCultureIgnoreCase))
				Messenger.ShowError("Invalid source path", $"The selected source path must start with the selected group's path; {@group.Path}");
			else
			{
				DisplayPath(fileSystemBrowser.SelectedPath.Substring(@group.Path.TrimEnd(Path.DirectorySeparatorChar).Length));
				UpdateSelectedSource();
			}
		}

		#endregion

		#region Service handler event handlers

		private void ServiceConnectionHandler_ServiceHandlerConnectionChanged(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ServiceHandlerConnectionChangedEventArgs>(ServiceConnectionHandler_ServiceHandlerConnectionChanged), sender, e);
				return;
			}

			Machine machine = ConnectionStore.Connections.Values.Where(x => x.ServiceHandler == e.ServiceHandler).Select(x => x.Machine).FirstOrDefault();
			if (machine != null && Machine == machine)
			{
				if (e.Status == ProcessManagerServiceHandlerStatus.Disconnected)
				{
					_machineAvailable = false;
					EnableControls(false);
					Messenger.ShowWarning("Connection lost", "The connection to the selected machine was lost.");
				}
				else if (e.Status == ProcessManagerServiceHandlerStatus.Connected)
				{
					_machineAvailable = true;
					EnableControls();
				}
			}
		}

		#endregion

		#region Helpers

		private void EnableControls(bool enable = true)
		{
			listViewSources.Enabled = (_machineAvailable && enable);
			buttonAddSource.Enabled = (_machineAvailable && enable);
			buttonRemoveSource.Enabled = (_machineAvailable && enable);
			backgroundPanelSource.Enabled = (_machineAvailable && enable);
			buttonOK.Enabled = (_machineAvailable && enable);
		}

		private void UpdateSelectedSource()
		{
			if (_selectedSource != null)
			{
				if (SourceChanged())
				{
					_selectedSource.Path = (textBoxPath.ForeColor == Color.Silver || string.IsNullOrEmpty(textBoxPath.Text) ? null : textBoxPath.Text);
					_selectedSource.Filter = textBoxFilter.Text;
					_selectedSource.Recursive = checkBoxRecursive.Checked;
					_selectedSource.Inclusive = checkBoxInclusive.Checked;
					ListViewItem item = listViewSources.Items.Cast<ListViewItem>().First(x => x.Tag == _selectedSource);
					item.SubItems[0].Text = _selectedSource.Path ?? UNSPECIFIED_PATH;
					item.SubItems[1].Text = _selectedSource.Filter;
					item.SubItems[2].Text = _selectedSource.Recursive.ToString(CultureInfo.InvariantCulture);
					item.SubItems[3].Text = _selectedSource.Inclusive.ToString(CultureInfo.InvariantCulture);
					listViewSources.Sort();
				}
				DisplayPath(_selectedSource.Path);
				EnableControls();
			}
		}

		private bool SourceChanged()
		{
			bool sourceChanged = false;
			if (_selectedSource != null)
			{
				sourceChanged = (_selectedSource.Path != (textBoxPath.ForeColor == Color.Silver ? null : textBoxPath.Text)
					|| _selectedSource.Filter != textBoxFilter.Text
					|| _selectedSource.Recursive != checkBoxRecursive.Checked
					|| _selectedSource.Inclusive != checkBoxInclusive.Checked);
				DistributionSourcesChanged |= sourceChanged;
			}
			return sourceChanged;
		}

		private bool ValidateSources()
		{
			IDictionary<DistributionSource, List<string>> invalidSources = DistributionSources
				.Select(source => new
					{
						Source = source,
						Messages = DistributionSourceIsValid(source).ToList()
					})
				.Where(x => x.Messages.Count > 0)
				.ToDictionary(x => x.Source, x => x.Messages);
			if (invalidSources.Count > 0)
			{
				int invalidSourceCount = invalidSources.Keys.Count;
				Messenger.ShowError($"Distribution source{(invalidSourceCount == 1 ? string.Empty : "s")} invalid",
					"One or more distribution source property invalid. See details for more information.",
					invalidSources.Aggregate(string.Empty, (x, y) => x + Environment.NewLine + Environment.NewLine
						+ y.Value.Select(z => new { Source = y.Key, Message = z })
							.Aggregate(string.Empty, (a, b) => a + Environment.NewLine + Machine + " > " + Application + ": " + b.Message).Trim()).Trim());
				return false;
			}
			return true;
		}

		private static IEnumerable<string> DistributionSourceIsValid(DistributionSource source)
		{
			if (string.IsNullOrEmpty(source.Path))
				yield return "Source path missing";
			else if (!Path.IsPathRooted(source.Path))
				yield return "Source path must be rooted";
		}

		private void DisplayPath(string path)
		{
			textBoxPath.ForeColor = (path == null ? Color.Silver : Color.FromKnownColor(KnownColor.WindowText));
			textBoxPath.Text = (path ?? UNSPECIFIED_PATH);
		}

		#endregion
	}
}
