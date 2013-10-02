using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataAccess;
using ProcessManager.DataObjects;
using ProcessManager.DataObjects.Comparers;
using ProcessManager.EventArguments;
using ProcessManager.Service.Client;
using ProcessManager.Service.DataObjects;
using ProcessManager.Utilities;
using ProcessManagerUI.Controls.Nodes;
using ProcessManagerUI.Controls.Nodes.Support;
using ProcessManagerUI.Support;
using Application = ProcessManager.DataObjects.Application;

namespace ProcessManagerUI.Forms
{
	public partial class ControlPanelForm : Form, IControlPanel, IProcessManagerEventHandler
	{
		private enum SuspensionType
		{
			TabLayout,
			NodeLayout,
			RootNodeSizeChanged
		}

		private static readonly IDictionary<ProcessGrouping, string> _processGroupingDescriptions = new Dictionary<ProcessGrouping, string>()
			{
				{ ProcessGrouping.MachineGroupApplication, "Machine > Group > Application" },
				{ ProcessGrouping.GroupMachineApplication, "Group > Machine > Application" }
			};
		private static readonly IDictionary<DistributionGrouping, string> _distributionGroupingDescriptions = new Dictionary<DistributionGrouping, string>()
			{
				{ DistributionGrouping.MachineGroupApplicationMachine, "Source Machine > Group > Application > Destination Machine" },
				{ DistributionGrouping.GroupMachineApplicationMachine, "Group > Source Machine > Application > Destination Machine" }
			};
		private DateTime _formClosedAt;
		private ConfigurationForm _configurationForm;
		private readonly List<IRootNode> _processRootNodes;
		private readonly List<ProcessApplicationNode> _processApplicationNodes;
		private readonly List<IRootNode> _distributionRootNodes;
		private readonly List<DistributionDestinationMachineNode> _distributionDestinationMachineNodes;
		private readonly List<IRootNode> _macroRootNodes;
		private readonly List<MacroActionNode> _macroActionNodes;
		private readonly UISuspension<SuspensionType> _ui;
		private readonly List<MachineConnection> _startupInitializedConnetions;

		public event EventHandler<MachineConfigurationHashEventArgs> ConfigurationChanged;
		public event EventHandler<DistributionResultEventArgs> DistributionCompleted;

		public ControlPanelForm()
		{
			InitializeComponent();
			MouseWheel += ControlPanelForm_MouseWheel;
			tabPageProcess.Tag = new TabPageData(ControlPanelTab.Process);
			tabPageProcess.Text = tabPageProcess.Tag.ToString();
			tabPageDistribution.Tag = new TabPageData(ControlPanelTab.Distribution);
			tabPageDistribution.Text = tabPageDistribution.Tag.ToString();
			tabPageMacro.Tag = new TabPageData(ControlPanelTab.Macro);
			tabPageMacro.Text = tabPageMacro.Tag.ToString();
			_formClosedAt = DateTime.MinValue;
			_configurationForm = null;
			_processRootNodes = new List<IRootNode>();
			_processApplicationNodes = new List<ProcessApplicationNode>();
			_distributionRootNodes = new List<IRootNode>();
			_distributionDestinationMachineNodes = new List<DistributionDestinationMachineNode>();
			_macroRootNodes = new List<IRootNode>();
			_macroActionNodes = new List<MacroActionNode>();
			_ui = new UISuspension<SuspensionType>();
			_startupInitializedConnetions = new List<MachineConnection>();
			MacroPlayer = new MacroPlayer(this);
			ServiceHelper.Initialize(this);
		}

		#region Properties

		private MacroPlayer MacroPlayer { get; set; }
		private TabPageData SelectedTabData { get { return (TabPageData) tabControlSection.SelectedTab.Tag; } }
		private ControlPanelTab SelectedTab { get { return SelectedTabData.ControlPanelTab; } }

		private FlowLayoutPanel CurrentFlowLayoutPanel
		{
			get
			{
				switch (SelectedTab)
				{
					case ControlPanelTab.Process:
						return flowLayoutPanelProcessApplications;
					case ControlPanelTab.Distribution:
						return flowLayoutPanelDistributionDestinations;
					case ControlPanelTab.Macro:
						return flowLayoutPanelMacros;
					default:
						return null;
				}
			}
		}

		private Panel CurrentScrollPanel
		{
			get
			{
				switch (SelectedTab)
				{
					case ControlPanelTab.Process:
						return panelScrollProcessApplications;
					case ControlPanelTab.Distribution:
						return panelScrollDistributionDestinations;
					case ControlPanelTab.Macro:
						return panelScrollMacros;
					default:
						return null;
				}
			}
		}

		private List<IRootNode> CurrentRootNodes
		{
			get
			{
				switch (SelectedTab)
				{
					case ControlPanelTab.Process:
						return _processRootNodes;
					case ControlPanelTab.Distribution:
						return _distributionRootNodes;
					case ControlPanelTab.Macro:
						return _macroRootNodes;
					default:
						return null;
				}
			}
		}

		#endregion

		#region Event raisers

		private bool RaiseConfigurationChangedEvent(Machine machine, string configurationHash)
		{
			if (ConfigurationChanged != null)
			{
				ConfigurationChanged(this, new MachineConfigurationHashEventArgs(machine, configurationHash));
				return true;
			}
			return false;
		}

		private bool RaiseDistributionCompletedEvent(DistributionResult distributionResult)
		{
			if (DistributionCompleted != null)
			{
				DistributionCompleted(this, new DistributionResultEventArgs(distributionResult));
				return true;
			}
			return false;
		}

		#endregion

		#region GUI events handlers

		#region Main form

		private void ControlPanelForm_Load(object sender, EventArgs e)
		{
			HideForm();
			ExtendGlass();
			Settings.Client.Load();

			EnableOptionStartWithWindows(Settings.Client.StartWithWindows);
			EnableOptionUserOwnsControlPanel(Settings.Client.UserOwnsControlPanel);
			EnableOptionKeepControlPanelTopMost(Settings.Client.KeepControlPanelTopMost);
			CheckOptionMinimumLogLevel(Settings.Client.LogTypeMinLevel);

			foreach (ProcessGrouping grouping in _processGroupingDescriptions.Keys)
			{
				int index = comboBoxProcessGroupBy.Items.Add(new ComboBoxItem<ProcessGrouping>(_processGroupingDescriptions[grouping], grouping));
				if (Settings.Client.P_SelectedGrouping == grouping.ToString())
					comboBoxProcessGroupBy.SelectedIndex = index;
			}
			if (comboBoxProcessGroupBy.SelectedIndex == -1)
				comboBoxProcessGroupBy.SelectedIndex = 0;

			foreach (DistributionGrouping grouping in _distributionGroupingDescriptions.Keys)
			{
				int index = comboBoxDistributionGroupBy.Items.Add(new ComboBoxItem<DistributionGrouping>(_distributionGroupingDescriptions[grouping], grouping));
				if (Settings.Client.D_SelectedGrouping == grouping.ToString())
					comboBoxDistributionGroupBy.SelectedIndex = index;
			}
			if (comboBoxDistributionGroupBy.SelectedIndex == -1)
				comboBoxDistributionGroupBy.SelectedIndex = 0;

			TabPage selectedTabPage = tabControlSection.TabPages.Cast<TabPage>().FirstOrDefault(tabPage => tabPage.Tag.ToString() == Settings.Client.CP_SelectedTab);
			if (selectedTabPage != null && tabControlSection.SelectedTab != selectedTabPage)
				tabControlSection.SelectedTab = selectedTabPage; // this will trigger DisplaySelectedTabPage()
			else
				DisplaySelectedTabPage();

			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerInitializationCompleted += ServiceConnectionHandler_ServiceHandlerInitializationCompleted;
			ProcessManagerServiceConnectionHandler.Instance.ServiceHandlerConnectionChanged += ServiceConnectionHandler_ServiceHandlerConnectionChanged;
			List<Machine> machinesToConnect = Settings.Client.Machines.Where(machine => !ConnectionStore.ConnectionCreated(machine)).ToList();
			if (machinesToConnect.Count > 0)
				_ui.Suspend(SuspensionType.TabLayout);
			foreach (MachineConnection connection in machinesToConnect.Select(machine => ConnectionStore.CreateConnection(this, machine)))
			{
				_startupInitializedConnetions.Add(connection);
				connection.ServiceHandler.Initialize();
			}
		}

		private void ControlPanelForm_Deactivate(object sender, EventArgs e)
		{
			if (!Settings.Client.UserOwnsControlPanel && Opacity == 1)
				HideForm();
		}

		private void ControlPanelForm_Resize(object sender, EventArgs e)
		{
			if (Settings.Client.UserOwnsControlPanel)
			{
				CurrentRootNodes.ForEach(node => node.ForceWidth(CurrentFlowLayoutPanel.Size.Width));
			}
		}

		private void ControlPanelForm_Enter(object sender, EventArgs e)
		{
			if (Settings.Client.UserOwnsControlPanel && Settings.Client.KeepControlPanelTopMost)
				TopMost = true;
		}

		private void ControlPanelForm_MouseWheel(object sender, MouseEventArgs e)
		{
			CurrentScrollPanel.Focus();
		}

		private void PanelGlassTop_MouseDown(object sender, MouseEventArgs e)
		{
			if (Settings.Client.UserOwnsControlPanel && e.Button == MouseButtons.Left)
			{
				NativeMethods.ReleaseCapture();
				NativeMethods.SendMessage(Handle, NativeMethods.WM_NCLBUTTONDOWN, NativeMethods.HT_CAPTION, 0);
			}
		}

		private void PictureBoxClose_MouseEnter(object sender, EventArgs e)
		{
			pictureBoxClose.Image = Properties.Resources.close_active_16_14;
		}

		private void PictureBoxClose_MouseLeave(object sender, EventArgs e)
		{
			pictureBoxClose.Image = Properties.Resources.close_16_14;
		}

		private void PictureBoxClose_Click(object sender, EventArgs e)
		{
			HideForm();
		}

		private void TabControlSection_SelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.Client.CP_SelectedTab = SelectedTab.ToString();
			Settings.Client.Save(ClientSettingsType.States);

			DisplaySelectedTabPage();
		}

		private void ComboBoxProcessGroupBy_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxProcessGroupBy.SelectedIndex == -1) return;
			if (_ui.IsSuspended(SuspensionType.NodeLayout)) return;

			ProcessGrouping grouping = ((ComboBoxItem<ProcessGrouping>) comboBoxProcessGroupBy.Items[comboBoxProcessGroupBy.SelectedIndex]).Tag;
			Settings.Client.P_SelectedGrouping = grouping.ToString();
			Settings.Client.Save(ClientSettingsType.States);

			LayoutProcessNodes();
		}

		private void ComboBoxProcessMachineFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxProcessMachineFilter.SelectedIndex == -1) return;
			if (_ui.IsSuspended(SuspensionType.NodeLayout)) return;

			Settings.Client.P_SelectedFilterMachine = ((ComboBoxItem) comboBoxProcessMachineFilter.SelectedItem).Text;
			Settings.Client.Save(ClientSettingsType.States);

			LayoutProcessNodes();
		}

		private void ComboBoxProcessGroupFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxProcessGroupFilter.SelectedIndex == -1) return;
			if (_ui.IsSuspended(SuspensionType.NodeLayout)) return;

			Settings.Client.P_SelectedFilterGroup = ((ComboBoxItem) comboBoxProcessGroupFilter.SelectedItem).Text;
			Settings.Client.Save(ClientSettingsType.States);

			LayoutProcessNodes();
		}

		private void ComboBoxProcessApplicationFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxProcessApplicationFilter.SelectedIndex == -1) return;
			if (_ui.IsSuspended(SuspensionType.NodeLayout)) return;

			Settings.Client.P_SelectedFilterApplication = ((ComboBoxItem) comboBoxProcessApplicationFilter.SelectedItem).Text;
			Settings.Client.Save(ClientSettingsType.States);

			LayoutProcessNodes();
		}

		private void LinkLabelProcessStartAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_processRootNodes.ForEach(node => node.TakeAction(ActionType.Start));
		}

		private void LinkLabelProcessStopAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_processRootNodes.ForEach(node => node.TakeAction(ActionType.Stop));
		}

		private void LinkLabelProcessRestartAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_processRootNodes.ForEach(node => node.TakeAction(ActionType.Restart));
		}

		private void LinkLabelProcessExpandAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ExpandAll(true);
		}

		private void LinkLabelProcessCollapseAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ExpandAll(false);
		}

		private void ComboBoxDistributionGroupBy_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDistributionGroupBy.SelectedIndex == -1) return;
			if (_ui.IsSuspended(SuspensionType.NodeLayout)) return;

			DistributionGrouping grouping = ((ComboBoxItem<DistributionGrouping>) comboBoxDistributionGroupBy.Items[comboBoxDistributionGroupBy.SelectedIndex]).Tag;
			Settings.Client.D_SelectedGrouping = grouping.ToString();
			Settings.Client.Save(ClientSettingsType.States);

			LayoutDistributionNodes();
		}

		private void ComboBoxDistributionSourceMachineFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDistributionSourceMachineFilter.SelectedIndex == -1) return;
			if (_ui.IsSuspended(SuspensionType.NodeLayout)) return;

			Settings.Client.D_SelectedFilterSourceMachine = ((ComboBoxItem) comboBoxDistributionSourceMachineFilter.SelectedItem).Text;
			Settings.Client.Save(ClientSettingsType.States);

			LayoutDistributionNodes();
		}

		private void ComboBoxDistributionGroupFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDistributionGroupFilter.SelectedIndex == -1) return;
			if (_ui.IsSuspended(SuspensionType.NodeLayout)) return;

			Settings.Client.D_SelectedFilterGroup = ((ComboBoxItem) comboBoxDistributionGroupFilter.SelectedItem).Text;
			Settings.Client.Save(ClientSettingsType.States);

			LayoutDistributionNodes();
		}

		private void ComboBoxDistributionApplicationFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDistributionApplicationFilter.SelectedIndex == -1) return;
			if (_ui.IsSuspended(SuspensionType.NodeLayout)) return;

			Settings.Client.D_SelectedFilterApplication = ((ComboBoxItem) comboBoxDistributionApplicationFilter.SelectedItem).Text;
			Settings.Client.Save(ClientSettingsType.States);

			LayoutDistributionNodes();
		}

		private void ComboBoxDistributionDestinationMachineFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDistributionDestinationMachineFilter.SelectedIndex == -1) return;
			if (_ui.IsSuspended(SuspensionType.NodeLayout)) return;

			Settings.Client.D_SelectedFilterDestinationMachine = ((ComboBoxItem) comboBoxDistributionDestinationMachineFilter.SelectedItem).Text;
			Settings.Client.Save(ClientSettingsType.States);

			LayoutDistributionNodes();
		}

		private void LinkLabelDistributionDistributeAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_distributionRootNodes.ForEach(node => node.TakeAction(ActionType.Distribute));
		}

		private void LinkLabelDistributionExpandAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ExpandAll(true);
		}

		private void LinkLabelDistributionCollapseAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ExpandAll(false);
		}

		private void LinkLabelMacroPlayAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			_macroRootNodes.ForEach(node => node.TakeAction(ActionType.Play));
		}

		private void LinkLabelMacroExpandAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ExpandAll(true);
		}

		private void LinkLabelMacroCollapseAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ExpandAll(false);
		}

		private void LinkLabelOpenConfiguration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenConfigurationForm();
		}

		#endregion

		#region Notify icon

		private void NotifyIcon_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			if (!Settings.Client.UserOwnsControlPanel && Opacity == 1)
				HideForm();
		}

		private void NotifyIcon_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			if (Settings.Client.UserOwnsControlPanel || Opacity == 0 && _formClosedAt.AddMilliseconds(500) < DateTime.Now)
				ShowForm();
		}

		#endregion

		#region System tray context menu

		private void ToolStripMenuItemSystemTrayConfiguration_Click(object sender, EventArgs e)
		{
			OpenConfigurationForm();
		}

		private void ToolStripMenuItemSystemTrayOptionsStartWithWindows_Click(object sender, EventArgs e)
		{
			ToggleOptionStartWithWindows();
		}

		private void ToolStripMenuItemSystemTrayOptionsUserOwnsControlPanel_Click(object sender, EventArgs e)
		{
			ToggleOptionUserOwnsControlPanel();
		}

		private void ToolStripMenuItemSystemTrayOptionsKeepControlPanelTopMost_Click(object sender, EventArgs e)
		{
			ToggleOptionKeepControlPanelTopMost();
		}

		private void ToolStripMenuItemSystemTrayOptionsMinimumLogLevelError_Click(object sender, EventArgs e)
		{
			ToggleOptionMinimumLogLevel(LogType.Error);
		}

		private void ToolStripMenuItemSystemTrayOptionsMinimumLogLevelWarning_Click(object sender, EventArgs e)
		{
			ToggleOptionMinimumLogLevel(LogType.Warning);
		}

		private void ToolStripMenuItemSystemTrayOptionsMinimumLogLevelFlow_Click(object sender, EventArgs e)
		{
			ToggleOptionMinimumLogLevel(LogType.Flow);
		}

		private void ToolStripMenuItemSystemTrayOptionsMinimumLogLevelVerbose_Click(object sender, EventArgs e)
		{
			ToggleOptionMinimumLogLevel(LogType.Verbose);
		}

		private void ToolStripMenuItemSystemTrayOptionsMinimumLogLevelDebug_Click(object sender, EventArgs e)
		{
			ToggleOptionMinimumLogLevel(LogType.Debug);
		}

		private void ToolStripMenuItemSystemTrayExit_Click(object sender, EventArgs e)
		{
			Settings.Client.Save(ClientSettingsType.States);
			Close();
			Environment.Exit(0);
		}

		#endregion

		#endregion

		#region Connection handler event handlers

		private void ServiceConnectionHandler_ServiceHandlerInitializationCompleted(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			lock (_startupInitializedConnetions)
			{
				_startupInitializedConnetions
					.Where(connection => connection.ServiceHandler == e.ServiceHandler)
					.ToList()
					.ForEach(connection => _startupInitializedConnetions.Remove(connection));
			}

			if (_startupInitializedConnetions.Count > 0)
				return;

			_ui.Resume(SuspensionType.TabLayout);

			if (InvokeRequired)
			{
				Invoke(new EventHandler<ServiceHandlerConnectionChangedEventArgs>(ServiceConnectionHandler_ServiceHandlerInitializationCompleted), sender, e);
				return;
			}

			InvalidateTabPages();
			UpdateFiltersAndLayout();
		}

		private void ServiceConnectionHandler_ServiceHandlerConnectionChanged(object sender, ServiceHandlerConnectionChangedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ServiceHandlerConnectionChangedEventArgs>(ServiceConnectionHandler_ServiceHandlerConnectionChanged), sender, e);
				return;
			}

			InvalidateTabPages();
			UpdateFiltersAndLayout();
		}

		#endregion

		#region Node event handlers

		private void RootNode_SizeChanged(object sender, EventArgs e)
		{
			if (!_ui.IsSuspended(SuspensionType.RootNodeSizeChanged))
				UpdateSize();
		}

		private void Node_CheckedChanged(object sender, EventArgs e)
		{
			if (CurrentFlowLayoutPanel.Controls.Count > 0)
			{
				int uncheckedCount = CurrentRootNodes.Count(node => node.CheckState == CheckState.Unchecked);
				EnableActionLinks(uncheckedCount != CurrentRootNodes.Count);
			}
		}

		private void Node_ActionTaken(object sender, ActionEventArgs e)
		{
			TakeAction(e.Action);
		}

		#endregion

		#region Configuration form event handlers

		private void ConfigurationForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (_configurationForm == null)
				return;

			_configurationForm.FormClosed -= ConfigurationForm_FormClosed;
			_configurationForm.ConfigurationChanged -= ConfigurationForm_ConfigurationChanged;
			_configurationForm.MacrosChanged -= ConfigurationForm_MacrosChanged;
			_configurationForm = null;
		}

		private void ConfigurationForm_ConfigurationChanged(object sender, MachinesEventArgs e)
		{
			InvalidateTabPages();
			UpdateFiltersAndLayoutAsync();
		}

		private void ConfigurationForm_MacrosChanged(object sender, EventArgs e)
		{
			InvalidateTabPages(ControlPanelTab.Macro);
			UpdateFiltersAndLayoutAsync();
		}

		#endregion

		#region Implementation of IProcessManagerEventHandler

		public void ProcessManagerServiceEventHandler_ProcessStatusesChanged(object sender, ProcessStatusesEventArgs e)
		{
			Logger.Add(LogType.Verbose, "Received ProcessStatusesChanged event: count = " + e.ProcessStatuses.Count + e.ProcessStatuses.Aggregate("", (x, y) => x + ", " + y.GroupID + " / " + y.ApplicationID));
			HandleProcessStatusesChanged(e.ProcessStatuses);
		}

		public void ProcessManagerServiceEventHandler_ConfigurationChanged(object sender, MachineConfigurationHashEventArgs e)
		{
			HandleConfigurationChanged(e.Machine, e.ConfigurationHash);
		}

		public void ProcessManagerServiceEventHandler_DistributionCompleted(object sender, DistributionResultEventArgs e)
		{
			HandleDistributionCompleted(e.DistributionResult);
		}

		#endregion

		#region Helpers

		#region Handling Process Manager Service events

		private void HandleProcessStatusesChanged(IEnumerable<ProcessStatus> processStatuses)
		{
			Task.Factory.StartNew(() => ApplyProcessStatuses(processStatuses));
		}

		private void HandleConfigurationChanged(Machine machine, string configurationHash)
		{
			Task.Factory.StartNew(() =>
				{
					if (!RaiseConfigurationChangedEvent(machine, configurationHash) && ConnectionStore.Connections[machine].Configuration.Hash != configurationHash)
						ReloadConfiguration(machine);
				});
		}

		private void HandleDistributionCompleted(DistributionResult distributionResult)
		{
			Task.Factory.StartNew(() =>
				{
					if (RaiseDistributionCompletedEvent(distributionResult))
						ApplyDistributionState(distributionResult);
				});
		}

		#endregion

		private void ShowForm()
		{
			if (Opacity == 0)
			{
				Location = new Point(Math.Min(MousePosition.X - Size.Width / 2, Screen.PrimaryScreen.WorkingArea.Width - Size.Width - 8),
					Math.Max(Screen.PrimaryScreen.WorkingArea.Top, Screen.PrimaryScreen.WorkingArea.Height - Size.Height - 8));
				Opacity = 1;
			}
			Show();
			try { NativeMethods.SetForegroundWindow(Handle); } catch { ; }

		}

		private void HideForm()
		{
			Hide();
			Opacity = 0;
			_formClosedAt = DateTime.Now;
		}

		private void ExtendGlass()
		{
			bool isGlassSupported = false;
			NativeMethods.DwmIsCompositionEnabled(ref isGlassSupported);
			if (isGlassSupported)
			{
				NativeMethods.Margins margins = new NativeMethods.Margins(0, 0, panelGlass.Height, 0);
				NativeMethods.DwmExtendFrameIntoClientArea(Handle, ref margins);
				Color keyColor = Color.FromArgb(255, 255, 0, 0);
				TransparencyKey = keyColor;
				panelGlass.BackColor = keyColor;
			}
		}

		private void ToggleOptionStartWithWindows()
		{
			Settings.Client.StartWithWindows = !Settings.Client.StartWithWindows;
			RegistryHandler.SaveClientSettings(ClientSettingsType.Options);
			RegistryHandler.SetWindowsStartupTrigger(Settings.Client.StartWithWindows ? System.Windows.Forms.Application.ExecutablePath : null);
			EnableOptionStartWithWindows(Settings.Client.StartWithWindows);
		}

		private void ToggleOptionUserOwnsControlPanel()
		{
			Settings.Client.UserOwnsControlPanel = !Settings.Client.UserOwnsControlPanel;
			RegistryHandler.SaveClientSettings(ClientSettingsType.Options);
			EnableOptionUserOwnsControlPanel(Settings.Client.UserOwnsControlPanel);
		}

		private void ToggleOptionKeepControlPanelTopMost()
		{
			Settings.Client.KeepControlPanelTopMost = !Settings.Client.KeepControlPanelTopMost;
			RegistryHandler.SaveClientSettings(ClientSettingsType.Options);
			EnableOptionKeepControlPanelTopMost(Settings.Client.KeepControlPanelTopMost);
		}

		private void ToggleOptionMinimumLogLevel(LogType logType)
		{
			Settings.Client.LogTypeMinLevel = logType;
			RegistryHandler.SaveClientSettings(ClientSettingsType.Options);
			CheckOptionMinimumLogLevel(Settings.Client.LogTypeMinLevel);
		}

		private void EnableOptionStartWithWindows(bool enable)
		{
			toolStripMenuItemSystemTrayOptionsStartWithWindows.Checked = enable;
		}

		private void EnableOptionUserOwnsControlPanel(bool enable)
		{
			UpdateSize();
			pictureBoxClose.Visible = enable;
			toolStripMenuItemSystemTrayOptionsUserOwnsControlPanel.Checked = enable;
			toolStripMenuItemSystemTrayOptionsKeepControlPanelTopMost.Enabled = enable;
		}

		private void EnableOptionKeepControlPanelTopMost(bool enable)
		{
			toolStripMenuItemSystemTrayOptionsKeepControlPanelTopMost.Checked = enable;
			TopMost = enable;
		}

		private void CheckOptionMinimumLogLevel(LogType logType)
		{
			toolStripMenuItemSystemTrayOptionsMinimumLogLevelError.Checked = logType == LogType.Error;
			toolStripMenuItemSystemTrayOptionsMinimumLogLevelWarning.Checked = logType == LogType.Warning;
			toolStripMenuItemSystemTrayOptionsMinimumLogLevelFlow.Checked = logType == LogType.Flow;
			toolStripMenuItemSystemTrayOptionsMinimumLogLevelVerbose.Checked = logType == LogType.Verbose;
			toolStripMenuItemSystemTrayOptionsMinimumLogLevelDebug.Checked = logType == LogType.Debug;
			Logger.LogTypeMinLevel = logType;
		}

		private void DisplaySelectedTabPage()
		{
			if (tabControlSection.SelectedTab == tabPageProcess)
			{
				tableLayoutPanelDistribution.Visible = false;
				tableLayoutPanelMacro.Visible = false;
				tableLayoutPanelProcess.Visible = true;
			}
			else if (tabControlSection.SelectedTab == tabPageDistribution)
			{
				tableLayoutPanelProcess.Visible = false;
				tableLayoutPanelMacro.Visible = false;
				tableLayoutPanelDistribution.Visible = true;
			}
			else if (tabControlSection.SelectedTab == tabPageMacro)
			{
				tableLayoutPanelProcess.Visible = false;
				tableLayoutPanelDistribution.Visible = false;
				tableLayoutPanelMacro.Visible = true;
			}

			if (!SelectedTabData.Invalidated)
				UpdateSize();
			else
				UpdateFiltersAndLayout();
		}

		private void InvalidateTabPages(ControlPanelTab? tab = null)
		{
			foreach (TabPageData tabPageData in tabControlSection.TabPages
				.Cast<TabPage>()
				.Select(tabPage => (TabPageData) tabPage.Tag)
				.Where(data => tab == null || data.ControlPanelTab == tab.Value))
			{
				tabPageData.Invalidate();
			}
		}

		private void OpenConfigurationForm()
		{
			if (_configurationForm != null)
			{
				try { NativeMethods.SetForegroundWindow(_configurationForm.Handle); } catch { ; }
				return;
			}

			_configurationForm = new ConfigurationForm();
			if (Settings.Client.UserOwnsControlPanel && Settings.Client.KeepControlPanelTopMost)
				_configurationForm.TopMost = true;
			_configurationForm.FormClosed += ConfigurationForm_FormClosed;
			_configurationForm.ConfigurationChanged += ConfigurationForm_ConfigurationChanged;
			_configurationForm.MacrosChanged += ConfigurationForm_MacrosChanged;
			_configurationForm.Show();
		}

		private void ReloadConfiguration(Machine machine)
		{
			try
			{
				ConnectionStore.Connections[machine].Configuration = ConnectionStore.Connections[machine].ServiceHandler.Service.GetConfiguration().FromDTO();
				InvalidateTabPages();
				UpdateFiltersAndLayout();
			}
			catch (Exception ex)
			{
				Logger.Add("Failed to retrieve new machine configuration", ex);
			}
		}

		private void UpdateFiltersAndLayoutAsync()
		{
			if (_ui.IsSuspended(SuspensionType.TabLayout))
				return;

			Task.Factory.StartNew(UpdateFiltersAndLayout);
		}

		private delegate void UpdateFiltersAndLayoutDelegate();

		private void UpdateFiltersAndLayout()
		{
			if (_ui.IsSuspended(SuspensionType.TabLayout))
				return;

			if (InvokeRequired)
			{
				Invoke(new UpdateFiltersAndLayoutDelegate(UpdateFiltersAndLayout));
				return;
			}

			UpdateProcessFilterAndLayout();
			UpdateDistributionFilterAndLayout();
			LayoutMacroNodes();
			SelectedTabData.Validate();
		}

		#region Process tab

		private void RetrieveAllProcessStatuses()
		{
			Task.Factory.StartNew(() =>
				{
					List<ProcessStatus> processStatuses = new List<ProcessStatus>();
					foreach (Machine machine in Settings.Client.Machines)
					{
						try
						{
							if (!ConnectionStore.ConnectionCreated(machine))
								throw new Exception("No connection to machine " + machine);

							processStatuses.AddRange(ConnectionStore.Connections[machine].ServiceHandler.Service
								.GetAllProcessStatuses().Select(x => x.FromDTO(machine)));
						}
						catch (Exception ex)
						{
							Logger.Add("Failed to retrieve all process statuses from machine " + machine, ex);
						}
					}
					ApplyProcessStatuses(processStatuses);
				});
		}

		private delegate void ApplyProcessStatusesDelegate(IEnumerable<ProcessStatus> processStatuses);

		private void ApplyProcessStatuses(IEnumerable<ProcessStatus> processStatuses)
		{
			if (InvokeRequired)
			{
				Invoke(new ApplyProcessStatusesDelegate(ApplyProcessStatuses), processStatuses);
				return;
			}

			lock (_processApplicationNodes)
			{
				foreach (ProcessStatus processStatus in processStatuses)
				{
					ProcessApplicationNode applicationNode = _processApplicationNodes.FirstOrDefault(node =>
						node.Matches(processStatus.Machine.ID, processStatus.GroupID, processStatus.ApplicationID));

					if (applicationNode != null)
						applicationNode.Status = processStatus.Value;
				}
			}
		}

		private void UpdateProcessFilterAndLayout()
		{
			if (SelectedTab != ControlPanelTab.Process)
				return;

			_ui.Suspend(SuspensionType.NodeLayout);
			UpdateProcessFilter();
			_ui.Resume(SuspensionType.NodeLayout);
			LayoutProcessNodes();
		}

		private delegate void UpdateProcessFilterDelegate();

		private void UpdateProcessFilter()
		{
			if (InvokeRequired)
			{
				Invoke(new UpdateProcessFilterDelegate(UpdateProcessFilter));
				return;
			}

			if (SelectedTab != ControlPanelTab.Process)
				return;

			string selectedMachineName = Settings.Client.P_SelectedFilterMachine;
			comboBoxProcessMachineFilter.Items.Clear();
			comboBoxProcessMachineFilter.Items.Add(new ComboBoxItem(string.Empty));

			foreach (Machine machine in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.Select(connection => connection.Machine)
				.Distinct(new MachineEqualityComparer())
				.OrderBy(machine => machine.HostName))
			{
				int index = comboBoxProcessMachineFilter.Items.Add(new ComboBoxItem(machine.HostName));
				if (Comparer.MachinesEqual(machine, selectedMachineName))
					comboBoxProcessMachineFilter.SelectedIndex = index;
			}

			string selectedGroupName = Settings.Client.P_SelectedFilterGroup;
			comboBoxProcessGroupFilter.Items.Clear();
			comboBoxProcessGroupFilter.Items.Add(new ComboBoxItem(string.Empty));

			foreach (Group group in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.SelectMany(connection => connection.Configuration.Groups)
				.Distinct(new GroupEqualityComparer())
				.OrderBy(group => group.Name))
			{
				int index = comboBoxProcessGroupFilter.Items.Add(new ComboBoxItem(group.Name));
				if (Comparer.GroupsEqual(group, selectedGroupName))
					comboBoxProcessGroupFilter.SelectedIndex = index;
			}

			string selectedApplicationName = Settings.Client.P_SelectedFilterApplication;
			comboBoxProcessApplicationFilter.Items.Clear();
			comboBoxProcessApplicationFilter.Items.Add(new ComboBoxItem(string.Empty));

			foreach (Application application in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.SelectMany(connection => connection.Configuration.Applications)
				.Where(application => !application.DistributionOnly)
				.Distinct(new ApplicationEqualityComparer())
				.OrderBy(application => application.Name))
			{
				int index = comboBoxProcessApplicationFilter.Items.Add(new ComboBoxItem(application.Name));
				if (Comparer.ApplicationsEqual(application, selectedApplicationName))
					comboBoxProcessApplicationFilter.SelectedIndex = index;
			}
		}

		private delegate void LayoutProcessNodesDelegate();

		private void LayoutProcessNodes()
		{
			if (_ui.IsSuspended(SuspensionType.NodeLayout))
				return;

			if (InvokeRequired)
			{
				Invoke(new LayoutProcessNodesDelegate(LayoutProcessNodes));
				return;
			}

			if (SelectedTab != ControlPanelTab.Process)
				return;

			Settings.Client.Save(ClientSettingsType.States);
			ProcessGrouping grouping = ((ComboBoxItem<ProcessGrouping>) comboBoxProcessGroupBy.SelectedItem).Tag;

			lock (_processApplicationNodes)
			{
				flowLayoutPanelProcessApplications.Controls.Clear();
				_processRootNodes.ForEach(node =>
					{
						node.SizeChanged -= RootNode_SizeChanged;
						node.CheckedChanged -= Node_CheckedChanged;
						node.ActionTaken -= Node_ActionTaken;
						node.Dispose();
					});
				//_allProcessNodes.ForEach(node => node.Dispose());
				_processRootNodes.Clear();
				_processApplicationNodes.Clear();

				var applications = ConnectionStore.Connections.Values
					.Where(connection => connection.Configuration != null)
					.Where(connection => string.IsNullOrEmpty(Settings.Client.P_SelectedFilterMachine) || connection.Machine.Equals(Settings.Client.P_SelectedFilterMachine))
					.SelectMany(connection =>
						connection.Configuration.Groups
							.Where(group => string.IsNullOrEmpty(Settings.Client.P_SelectedFilterGroup) || group.Equals(Settings.Client.P_SelectedFilterGroup))
							.SelectMany(group => connection.Configuration.Applications
								.Where(application => group.Applications.Contains(application.ID))
								.Where(application => !application.DistributionOnly)
								.Where(application => string.IsNullOrEmpty(Settings.Client.P_SelectedFilterApplication) || application.Equals(Settings.Client.P_SelectedFilterApplication))
								.Select(application => new
									{
										connection.Machine,
										Group = group,
										Application = application
									})))
					.ToList();

				switch (grouping)
				{
					case ProcessGrouping.MachineGroupApplication:
						{
							var machinesGroupsApplications = applications
								.GroupBy(a => a.Machine, (a, b) => new
									{
										Machine = a,
										Groups = b.GroupBy(c => c.Group, (c, d) => new
											{
												Group = c,
												Applications = d
											})
									}, new MachineEqualityComparer());

							_processRootNodes.AddRange(machinesGroupsApplications.OrderBy(x => x.Machine.HostName).Select(machineGroupsApplications =>
								{
									IEnumerable<ProcessGroupNode> groupNodes = machineGroupsApplications.Groups.OrderBy(x => x.Group.Name).Select(groupApplications =>
										{
											IEnumerable<ProcessApplicationNode> applicationNodes = groupApplications.Applications.OrderBy(x => x.Application.Name).Select(application =>
												new ProcessApplicationNode(application.Application, application.Group.ID, application.Machine.ID)).ToList();
											_processApplicationNodes.AddRange(applicationNodes);
											return new ProcessGroupNode(groupApplications.Group, groupApplications.Applications.First().Machine.ID, applicationNodes, grouping);
										}).ToList(); // must make ToList() to ensure ApplicationNodes only are created once
									return new ProcessMachineNode(machineGroupsApplications.Machine, null, groupNodes, grouping);
								}).ToList());
						}
						break;
					case ProcessGrouping.GroupMachineApplication:
						{
							var groupsMachinesApplications = applications
								.GroupBy(a => a.Group, (a, b) => new
									{
										Group = a,
										Machines = b.GroupBy(c => c.Machine, (c, d) => new
											{
												Machine = c,
												Applications = d
											})
									}, new GroupEqualityComparer());

							_processRootNodes.AddRange(groupsMachinesApplications.OrderBy(x => x.Group.Name).Select(groupMachinesApplications =>
								{
									IEnumerable<ProcessMachineNode> machineNodes = groupMachinesApplications.Machines.OrderBy(x => x.Machine.HostName).Select(machineApplications =>
										{
											IEnumerable<ProcessApplicationNode> applicationNodes = machineApplications.Applications.OrderBy(x => x.Application.Name).Select(application =>
												new ProcessApplicationNode(application.Application, application.Group.ID, application.Machine.ID)).ToList();
											_processApplicationNodes.AddRange(applicationNodes);
											return new ProcessMachineNode(machineApplications.Machine, machineApplications.Applications.First().Group.ID, applicationNodes, grouping);
										}).ToList(); // must make ToList() to ensure ApplicationNodes only are created once
									return new ProcessGroupNode(groupMachinesApplications.Group, null, machineNodes, grouping);
								}).ToList());
						}
						break;
				}

				_processRootNodes.ForEach(node =>
					{
						node.SizeChanged += RootNode_SizeChanged;
						node.CheckedChanged += Node_CheckedChanged;
						node.ActionTaken += Node_ActionTaken;
					});
			}

			if (_processApplicationNodes.Count > 0)
			{
				_ui.Suspend(SuspensionType.RootNodeSizeChanged);
				UpdateSize(_processRootNodes.Select(node => node.LayoutNode()).ToList());
				_processRootNodes.ForEach(node => node.ForceWidth(flowLayoutPanelProcessApplications.Size.Width));
				_ui.Resume(SuspensionType.RootNodeSizeChanged);
				flowLayoutPanelProcessApplications.Controls.AddRange(_processRootNodes.Cast<Control>().ToArray());

				Settings.Client.P_CheckedNodes.ToList()
					.Select(id => _processApplicationNodes.FirstOrDefault(node => node.Matches(id)))
					.Where(node => node != null)
					.ToList().ForEach(node => node.Check(true));

				RetrieveAllProcessStatuses();
			}
			else
				UpdateSize(null);

			labelProcessUnavailable.Visible = (_processApplicationNodes.Count == 0);
		}

		#endregion

		#region Distribution tab

		private delegate void ApplyDistributionStateDelegate(DistributionResult distributionResult);

		private void ApplyDistributionState(DistributionResult distributionResult)
		{
			if (InvokeRequired)
			{
				Invoke(new ApplyDistributionStateDelegate(ApplyDistributionState), distributionResult);
				return;
			}

			lock (_distributionDestinationMachineNodes)
			{
				DistributionDestinationMachineNode destinationMachineNode = _distributionDestinationMachineNodes.FirstOrDefault(node =>
					node.Matches(new Machine(distributionResult.SourceMachineHostName).ID, distributionResult.GroupID,
						distributionResult.ApplicationID, new Machine(distributionResult.DestinationMachineHostName).ID));

				if (destinationMachineNode != null)
					destinationMachineNode.State = (distributionResult.Result == DistributionResultValue.Success ? DistributionState.Success : DistributionState.Failure);
			}
		}

		private void UpdateDistributionFilterAndLayout()
		{
			if (SelectedTab != ControlPanelTab.Distribution)
				return;

			_ui.Suspend(SuspensionType.NodeLayout);
			UpdateDistributionFilter();
			_ui.Resume(SuspensionType.NodeLayout);
			LayoutDistributionNodes();
		}

		private delegate void UpdateDistributionFilterDelegate();

		private void UpdateDistributionFilter()
		{
			if (InvokeRequired)
			{
				Invoke(new UpdateDistributionFilterDelegate(UpdateDistributionFilter));
				return;
			}

			if (SelectedTab != ControlPanelTab.Distribution)
				return;

			string selectedSourceMachineName = Settings.Client.D_SelectedFilterSourceMachine;
			comboBoxDistributionSourceMachineFilter.Items.Clear();
			comboBoxDistributionSourceMachineFilter.Items.Add(new ComboBoxItem(string.Empty));

			foreach (Machine machine in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.Select(connection => connection.Machine)
				.Distinct(new MachineEqualityComparer())
				.OrderBy(machine => machine.HostName))
			{
				int index = comboBoxDistributionSourceMachineFilter.Items.Add(new ComboBoxItem(machine.HostName));
				if (Comparer.MachinesEqual(machine, selectedSourceMachineName))
					comboBoxDistributionSourceMachineFilter.SelectedIndex = index;
			}

			string selectedGroupName = Settings.Client.D_SelectedFilterGroup;
			comboBoxDistributionGroupFilter.Items.Clear();
			comboBoxDistributionGroupFilter.Items.Add(new ComboBoxItem(string.Empty));

			foreach (Group group in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.SelectMany(connection => connection.Configuration.Groups)
				.Distinct(new GroupEqualityComparer())
				.OrderBy(group => group.Name))
			{
				int index = comboBoxDistributionGroupFilter.Items.Add(new ComboBoxItem(group.Name));
				if (Comparer.GroupsEqual(group, selectedGroupName))
					comboBoxDistributionGroupFilter.SelectedIndex = index;
			}

			string selectedApplicationName = Settings.Client.D_SelectedFilterApplication;
			comboBoxDistributionApplicationFilter.Items.Clear();
			comboBoxDistributionApplicationFilter.Items.Add(new ComboBoxItem(string.Empty));

			foreach (Application application in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.SelectMany(connection => connection.Configuration.Applications)
				.Where(application => application.Sources.Count > 0)
				.Distinct(new ApplicationEqualityComparer())
				.OrderBy(application => application.Name))
			{
				int index = comboBoxDistributionApplicationFilter.Items.Add(new ComboBoxItem(application.Name));
				if (Comparer.ApplicationsEqual(application, selectedApplicationName))
					comboBoxDistributionApplicationFilter.SelectedIndex = index;
			}

			string selectedDestinationMachineName = Settings.Client.D_SelectedFilterDestinationMachine;
			comboBoxDistributionDestinationMachineFilter.Items.Clear();
			comboBoxDistributionDestinationMachineFilter.Items.Add(new ComboBoxItem(string.Empty));

			foreach (Machine machine in ConnectionStore.Connections.Values
				.Where(connection => connection.Configuration != null)
				.Select(connection => connection.Machine)
				.Distinct(new MachineEqualityComparer())
				.OrderBy(machine => machine.HostName))
			{
				int index = comboBoxDistributionDestinationMachineFilter.Items.Add(new ComboBoxItem(machine.HostName));
				if (Comparer.MachinesEqual(machine, selectedDestinationMachineName))
					comboBoxDistributionDestinationMachineFilter.SelectedIndex = index;
			}
		}

		private delegate void LayoutDistributionNodesDelegate();

		private void LayoutDistributionNodes()
		{
			if (_ui.IsSuspended(SuspensionType.NodeLayout))
				return;

			if (InvokeRequired)
			{
				Invoke(new LayoutDistributionNodesDelegate(LayoutDistributionNodes));
				return;
			}

			if (SelectedTab != ControlPanelTab.Distribution)
				return;

			Settings.Client.Save(ClientSettingsType.States);
			DistributionGrouping grouping = ((ComboBoxItem<DistributionGrouping>) comboBoxDistributionGroupBy.SelectedItem).Tag;

			lock (_distributionDestinationMachineNodes)
			{
				flowLayoutPanelDistributionDestinations.Controls.Clear();
				_distributionRootNodes.ForEach(node =>
					{
						node.SizeChanged -= RootNode_SizeChanged;
						node.CheckedChanged -= Node_CheckedChanged;
						node.ActionTaken -= Node_ActionTaken;
						node.Dispose();
					});
				_distributionRootNodes.Clear();
				_distributionDestinationMachineNodes.Clear();

				var destinationMachines = ConnectionStore.Connections.Values
					.Where(sourceConnection => sourceConnection.Configuration != null)
					.Where(sourceConnection => string.IsNullOrEmpty(Settings.Client.D_SelectedFilterSourceMachine) || sourceConnection.Machine.Equals(Settings.Client.D_SelectedFilterSourceMachine))
					.SelectMany(sourceConnection =>
						sourceConnection.Configuration.Groups
							.Where(group => string.IsNullOrEmpty(Settings.Client.D_SelectedFilterGroup) || group.Equals(Settings.Client.D_SelectedFilterGroup))
							.SelectMany(group => sourceConnection.Configuration.Applications
								.Where(application => group.Applications.Contains(application.ID))
								.Where(application => application.Sources.Count > 0)
								.Where(application => string.IsNullOrEmpty(Settings.Client.D_SelectedFilterApplication) || application.Equals(Settings.Client.D_SelectedFilterApplication))
								.SelectMany(application => ConnectionStore.Connections.Values
									.Where(destinationConnection => destinationConnection.Configuration != null)
									.Where(destinationConnection => !Comparer.MachinesEqual(destinationConnection.Machine, sourceConnection.Machine))
									.Where(destinationConnection => string.IsNullOrEmpty(Settings.Client.D_SelectedFilterDestinationMachine) || destinationConnection.Machine.Equals(Settings.Client.D_SelectedFilterDestinationMachine))
									.Where(destinationConnection => destinationConnection.Configuration.Groups.Any(destinationGroup => Comparer.GroupsEqual(destinationGroup, group)))
									.Select(destinationConnection => new
										{
											SourceMachine = sourceConnection.Machine,
											Group = group,
											Application = application,
											DestinationMachine = destinationConnection.Machine
										}))))
					.ToList();

				switch (grouping)
				{
					case DistributionGrouping.MachineGroupApplicationMachine:
						{
							var machinesGroupsApplicationsMachines = destinationMachines
								.GroupBy(a => a.SourceMachine, (a, b) => new
									{
										SourceMachine = a,
										Groups = b.GroupBy(c => c.Group, (c, d) => new
											{
												Group = c,
												Applications = d.GroupBy(e => e.Application, (e, f) => new
													{
														Application = e,
														DestinationMachines = f
													})
											})
									}, new MachineEqualityComparer());

							_distributionRootNodes.AddRange(machinesGroupsApplicationsMachines.OrderBy(x => x.SourceMachine.HostName).Select(machineGroupsApplicationsMachines =>
								{
									IEnumerable<DistributionGroupNode> groupNodes = machineGroupsApplicationsMachines.Groups.OrderBy(x => x.Group.Name).Select(groupApplicationsMachines =>
										{
											IEnumerable<DistributionApplicationNode> applicationNodes = groupApplicationsMachines.Applications.OrderBy(x => x.Application.Name).Select(applicationMachines =>
												{
													IEnumerable<DistributionDestinationMachineNode> destinationMachineNodes = applicationMachines.DestinationMachines.OrderBy(x => x.DestinationMachine.HostName).Select(machine =>
														new DistributionDestinationMachineNode(machine.DestinationMachine, machine.Application.ID, machine.Group.ID, machine.SourceMachine.ID)).ToList();
													_distributionDestinationMachineNodes.AddRange(destinationMachineNodes);
													return new DistributionApplicationNode(applicationMachines.Application, destinationMachineNodes, grouping);
												}).ToList(); // must make ToList() to ensure DestinationMachineNodes only are created once
											return new DistributionGroupNode(groupApplicationsMachines.Group, machineGroupsApplicationsMachines.SourceMachine.ID, applicationNodes, grouping);
										}).ToList(); // must make ToList() to ensure ApplicationNodes only are created once
									return new DistributionSourceMachineNode(machineGroupsApplicationsMachines.SourceMachine, null, groupNodes, grouping);
								}).ToList());
						}
						break;
					case DistributionGrouping.GroupMachineApplicationMachine:
						{
							var groupsMachinesApplicationsMachines = destinationMachines
								.GroupBy(a => a.Group, (a, b) => new
									{
										Group = a,
										SourceMachines = b.GroupBy(c => c.SourceMachine, (c, d) => new
											{
												SourceMachine = c,
												Applications = d.GroupBy(e => e.Application, (e, f) => new
													{
														Application = e,
														DestinationMachines = f
													})
											})
									}, new GroupEqualityComparer());

							_distributionRootNodes.AddRange(groupsMachinesApplicationsMachines.OrderBy(x => x.Group.Name).Select(groupMachinesApplicationsMachines =>
								{
									IEnumerable<DistributionSourceMachineNode> sourceMachineNodes = groupMachinesApplicationsMachines.SourceMachines.OrderBy(x => x.SourceMachine.HostName).Select(machineApplicationsMachines =>
										{
											IEnumerable<DistributionApplicationNode> applicationNodes = machineApplicationsMachines.Applications.OrderBy(x => x.Application.Name).Select(applicationMachines =>
												{
													IEnumerable<DistributionDestinationMachineNode> destinationMachineNodes = applicationMachines.DestinationMachines.OrderBy(x => x.DestinationMachine.HostName).Select(machine =>
														new DistributionDestinationMachineNode(machine.DestinationMachine, machine.Application.ID, machine.Group.ID, machine.SourceMachine.ID)).ToList();
													_distributionDestinationMachineNodes.AddRange(destinationMachineNodes);
													return new DistributionApplicationNode(applicationMachines.Application, destinationMachineNodes, grouping);
												}).ToList(); // must make ToList() to ensure DestinationMachineNodes only are created once
											return new DistributionSourceMachineNode(machineApplicationsMachines.SourceMachine, groupMachinesApplicationsMachines.Group.ID, applicationNodes, grouping);
										}).ToList(); // must make ToList() to ensure ApplicationNodes only are created once
									return new DistributionGroupNode(groupMachinesApplicationsMachines.Group, null, sourceMachineNodes, grouping);
								}).ToList());
						}
						break;
				}

				_distributionRootNodes.ForEach(node =>
					{
						node.SizeChanged += RootNode_SizeChanged;
						node.CheckedChanged += Node_CheckedChanged;
						node.ActionTaken += Node_ActionTaken;
					});
			}

			if (_distributionDestinationMachineNodes.Count > 0)
			{
				_ui.Suspend(SuspensionType.RootNodeSizeChanged);
				UpdateSize(_distributionRootNodes.Select(node => node.LayoutNode()).ToList());
				_distributionRootNodes.ForEach(node => node.ForceWidth(flowLayoutPanelDistributionDestinations.Width));
				_ui.Resume(SuspensionType.RootNodeSizeChanged);
				flowLayoutPanelDistributionDestinations.Controls.AddRange(_distributionRootNodes.Cast<Control>().ToArray());

				Settings.Client.D_CheckedNodes.ToList()
					.Select(id => _distributionDestinationMachineNodes.FirstOrDefault(node => node.Matches(id)))
					.Where(node => node != null)
					.ToList().ForEach(node => node.Check(true));
			}
			else
				UpdateSize(null);

			labelDistributionUnavailable.Visible = (_distributionDestinationMachineNodes.Count == 0);
		}

		#endregion

		#region Macro tab

		private delegate void ApplyMacroActionStateDelegate(Guid macroID, Guid macroActionID, MacroActionState state);

		public void ApplyMacroActionState(Guid macroID, Guid macroActionID, MacroActionState state)
		{
			if (InvokeRequired)
			{
				Invoke(new ApplyMacroActionStateDelegate(ApplyMacroActionState), macroID, macroActionID, state);
				return;
			}

			lock (_macroActionNodes)
			{
				MacroActionNode macroActionNode = _macroActionNodes.FirstOrDefault(node => node.Matches(macroID, macroActionID));

				if (macroActionNode != null)
					macroActionNode.State = state;
			}
		}

		private delegate void LayoutMacroNodesDelegate();

		private void LayoutMacroNodes()
		{
			if (InvokeRequired)
			{
				Invoke(new LayoutMacroNodesDelegate(LayoutMacroNodes));
				return;
			}

			if (SelectedTab != ControlPanelTab.Macro)
				return;

			Settings.Client.Save(ClientSettingsType.States);

			lock (_macroActionNodes)
			{
				flowLayoutPanelMacros.Controls.Clear();
				_macroRootNodes.ForEach(node =>
					{
						node.SizeChanged -= RootNode_SizeChanged;
						node.CheckedChanged -= Node_CheckedChanged;
						node.ActionTaken -= Node_ActionTaken;
						node.Dispose();
					});
				_macroRootNodes.Clear();
				_macroActionNodes.Clear();

				var macroBundles = Settings.Client.Macros.Select(macro => new
					{
						Macro = macro,
						GroupedActionBundles = macro.ActionBundles.SelectMany(actionBundle => actionBundle.Actions
							.Select(action => new
								{
									Action = action,
									MachineID = action.Type == MacroActionType.Wait
										? Guid.Empty
										: action.Type == MacroActionType.Distribute
											? ((MacroDistributionAction) action).DestinationMachineID
											: ((MacroProcessAction) action).MachineID
								})
							.GroupBy(a => a.MachineID, (a, b) => new
								{
									MachineID = a,
									actionBundle.Type,
									GroupedActions = b
								}))
					});

				_macroRootNodes.AddRange(macroBundles.OrderBy(x => x.Macro.Name).Select(macroBundle =>
					{
						IEnumerable<IMacroNode> levelTwoNodes = macroBundle.GroupedActionBundles.Select(groupedActionBundle =>
							{
								switch (groupedActionBundle.Type)
								{
									case MacroActionType.Start:
									case MacroActionType.Stop:
									case MacroActionType.Restart:
									case MacroActionType.Distribute:
										Dictionary<bool, List<MacroActionNode>> macroActionNodes = groupedActionBundle.GroupedActions
											.Select(groupedAction => new MacroActionNode(groupedAction.Action, macroBundle.Macro.ID))
											.GroupBy(macroActionNode => macroActionNode.IsComplete)
											.ToDictionary(x => x.Key, x => x.ToList());
										if (macroActionNodes.ContainsKey(false)) 
											macroActionNodes[false].ForEach(macroActionNode => macroActionNode.Dispose());
										if (macroActionNodes.ContainsKey(true))
										{
											_macroActionNodes.AddRange(macroActionNodes[true]);
											return (IMacroNode) new MacroMachineNode(groupedActionBundle.MachineID, groupedActionBundle.Type, macroBundle.Macro, macroActionNodes[true]);
										}
										return null;
									case MacroActionType.Wait:
										MacroActionNode waitMacroActionNode = new MacroActionNode(groupedActionBundle.GroupedActions.First().Action, macroBundle.Macro.ID);
										_macroActionNodes.Add(waitMacroActionNode);
										return (IMacroNode) waitMacroActionNode;
									default:
										throw new InvalidOperationException();
								}
							}).Where(node => node != null).ToList();
						return new MacroNode(macroBundle.Macro, levelTwoNodes);
					}).ToList());

				_macroRootNodes.ForEach(node =>
					{
						node.SizeChanged += RootNode_SizeChanged;
						node.CheckedChanged += Node_CheckedChanged;
						node.ActionTaken += Node_ActionTaken;
					});
			}

			if (_macroActionNodes.Count > 0)
			{
				UpdateSize(_macroRootNodes.Select(node => node.LayoutNode()).ToList());
				_macroRootNodes.ForEach(node => node.ForceWidth(flowLayoutPanelMacros.Size.Width));
				flowLayoutPanelMacros.Controls.AddRange(_macroRootNodes.Cast<Control>().ToArray());

				Settings.Client.M_CheckedNodes.ToList()
					.Select(id => _macroActionNodes.FirstOrDefault(node => node.Matches(id)))
					.Where(node => node != null)
					.ToList().ForEach(node => node.Check(true));
			}
			else
				UpdateSize(null);

			labelMacroUnavailable.Visible = (_macroActionNodes.Count == 0);
		}

		#endregion

		private void ExpandAll(bool expand)
		{
			_ui.Suspend(SuspensionType.RootNodeSizeChanged);
			CurrentRootNodes.ForEach(node => node.ExpandAll(expand));
			_ui.Resume(SuspensionType.RootNodeSizeChanged);
			UpdateSize();
		}

		private void UpdateSize()
		{
			UpdateSize(CurrentRootNodes.Select(node => node.Size).ToList());
		}

		private void UpdateSize(ICollection<Size> rootNodeSizes)
		{
			const int MIN_WIDTH = 465, MIN_HEIGHT = 220;
			MinimumSize = MaximumSize = new Size(0, 0);

			if (rootNodeSizes != null && rootNodeSizes.Count > 0)
			{
				int totalNodesHeight = rootNodeSizes.Sum(size => size.Height);
				int maxNodeWidth = rootNodeSizes.Max(size => size.Width);
				if (!Settings.Client.UserOwnsControlPanel)
				{
					Size = new Size(Math.Max(MIN_WIDTH, Width - CurrentScrollPanel.Width + maxNodeWidth),
						Math.Min(Height - CurrentScrollPanel.Height + totalNodesHeight, Screen.PrimaryScreen.WorkingArea.Height));
				}
				CurrentFlowLayoutPanel.Size = new Size(Math.Max(MIN_WIDTH, maxNodeWidth), totalNodesHeight);
			}
			else
				Size = new Size(MIN_WIDTH, MIN_HEIGHT);

			if (!Settings.Client.UserOwnsControlPanel)
			{
				MinimumSize = MaximumSize = Size;
				Location = new Point(Math.Min(Location.X, Screen.PrimaryScreen.WorkingArea.Width - Size.Width - 8),
					Math.Max(Screen.PrimaryScreen.WorkingArea.Top, Screen.PrimaryScreen.WorkingArea.Height - Size.Height - 8));
			}
			else
				MinimumSize = new Size(MIN_WIDTH, MIN_HEIGHT);
		}

		private void EnableActionLinks(bool enable)
		{
			switch (SelectedTab)
			{
				case ControlPanelTab.Process:
					linkLabelProcessStartAll.Enabled = enable;
					linkLabelProcessStopAll.Enabled = enable;
					linkLabelProcessRestartAll.Enabled = enable;
					break;
				case ControlPanelTab.Distribution:
					linkLabelDistributionDistributeAll.Enabled = enable;
					break;
				case ControlPanelTab.Macro:
					linkLabelMacroPlayAll.Enabled = enable;
					break;
			}
		}

		public bool TakeAction(IAction action)
		{
			try
			{
				ProcessAction processAction = action as ProcessAction;

				if (processAction != null)
				{
					if (processAction.Machine == null || processAction.Group == null || processAction.Application == null)
						throw new Exception("Incomplete ProcessAction object");

					if (!ConnectionStore.ConnectionCreated(processAction.Machine))
						throw new Exception("No connection to machine " + processAction.Machine);

					if (!ConnectionStore.ConfigurationAvailable(processAction.Machine))
						throw new Exception("No configuration available for machine " + processAction.Machine);

					Group group = ConnectionStore.Connections[processAction.Machine].Configuration.Groups.FirstOrDefault(x => Comparer.GroupsEqual(processAction.Group, x));
					Application application = ConnectionStore.Connections[processAction.Machine].Configuration.Applications.FirstOrDefault(x => Comparer.ApplicationsEqual(processAction.Application, x));

					if (group == null)
						throw new Exception("Group " + processAction.Group + " missing on machine " + processAction.Machine);

					if (application == null)
						throw new Exception("Application " + processAction.Application + " missing on machine " + processAction.Machine);

					processAction.Group = group;
					processAction.Application = application;

					return ConnectionStore.Connections[processAction.Machine].ServiceHandler.Service.TakeProcessAction(new DTOProcessAction(processAction));
				}

				DistributionAction distributionAction = action as DistributionAction;

				if (distributionAction != null)
				{
					if (distributionAction.SourceMachine == null || distributionAction.Group == null || distributionAction.Application == null || distributionAction.DestinationMachine == null)
						throw new Exception("Incomplete DistributionAction object");

					if (!ConnectionStore.ConnectionCreated(distributionAction.SourceMachine))
						throw new Exception("No connection to source machine " + distributionAction.SourceMachine);

					return ConnectionStore.Connections[distributionAction.SourceMachine].ServiceHandler.Service.TakeDistributionAction(new DTODistributionAction(distributionAction));
				}

				MacroAction macroAction = action as MacroAction;

				if (macroAction != null)
				{
					if (macroAction.Macro == null || macroAction.Actions.Count == 0)
						throw new Exception("Incomplete MacroAction object");

					MacroPlayer.Play(macroAction.Macro, macroAction.Actions);
					return true;
				}
			}
			catch (Exception ex)
			{
				Logger.Add("Failed to take action", ex);
			}

			return false;
		}

		#endregion
	}
}
