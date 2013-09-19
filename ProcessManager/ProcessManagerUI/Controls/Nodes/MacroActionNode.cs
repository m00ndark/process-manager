using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManager.Utilities;
using ProcessManagerUI.Controls.Nodes.Support;
using Application = ProcessManager.DataObjects.Application;

namespace ProcessManagerUI.Controls.Nodes
{
	public enum MacroActionState
	{
		Unknown,
		Success,
		Ongoing,
		Failure
	}

	public partial class MacroActionNode : UserControl, IMacroNode
	{
		private readonly Guid _id;
		private MacroActionState _state;
		private readonly string _actionName;

		public event EventHandler CheckedChanged;
		public event EventHandler<ActionEventArgs> ActionTaken;
		public event EventHandler StateChanged;

        public MacroActionNode(IMacroAction macroAction, Guid macroID)
		{
			InitializeComponent();
			MacroAction = macroAction;
			_id = MakeID(macroID, macroAction.ID);
			_state = MacroActionState.Unknown;
	        _actionName = GetActionName(MacroAction);
	        //BackColor = Color.FromArgb(255, 192, 128);
		}

		#region Properties

        public IMacroAction MacroAction { get; private set; }

		public MacroActionState State
		{
			get { return _state; }
			set
			{
				_state = value;
				ApplyState();
				RaiseStateChangedEvent();
			}
		}

		public Guid ID { get { return _id; } }
		public CheckState CheckState { get { return checkBoxSelected.CheckState; } }

		#endregion

		#region GUI event handlers

		private void CheckBoxSelected_CheckedChanged(object sender, EventArgs e)
		{
			if (!checkBoxSelected.Checked)
				Settings.Client.M_CheckedNodes.Remove(ID);
			else if (!Settings.Client.M_CheckedNodes.Contains(ID))
				Settings.Client.M_CheckedNodes.Add(ID);

			RaiseCheckedChangedEvent();
		}

		private void LinkLabelPlay_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			State = MacroActionState.Ongoing;

			RaiseActionTakenEvent(new MacroAction(ActionType.Play, MacroAction));
		}

		#endregion

		#region Implementation of INode

		public Size LayoutNode()
		{
			labelActionName.Text = _actionName;
			Size = new Size(labelActionName.Location.X + labelActionName.Size.Width, Size.Height);
			ApplyState();
			return Size;
		}

		public void ForceWidth(int width)
		{
			Size = new Size(width, Size.Height);
		}

		public void Check(bool @checked)
		{
			checkBoxSelected.Checked = @checked;
		}

		public void TakeAction(ActionType type)
		{
			throw new InvalidOperationException("Not used by macro nodes");

			//if (type != ActionType.Play)
			//	throw new ArgumentException("Invalid action type");

			//if (checkBoxSelected.Checked)
			//	RaiseActionTakenEvent(new MacroAction(ActionType.Play, MacroAction));
		}

		#endregion

		#region Event raisers

		private void RaiseCheckedChangedEvent()
		{
			if (CheckedChanged != null)
				CheckedChanged(this, EventArgs.Empty);
		}

		private void RaiseActionTakenEvent(IAction action)
		{
			if (ActionTaken != null)
				ActionTaken(this, new ActionEventArgs(action));
		}

		private void RaiseStateChangedEvent()
		{
			if (StateChanged != null)
				StateChanged(this, EventArgs.Empty);
		}

		#endregion

		#region Helpers

        private static Guid MakeID(Guid macroID, Guid macroActionID)
		{
			return Cryptographer.CreateGUID(macroID.ToString() + macroActionID);
		}

		private static string GetActionName(IMacroAction macroAction)
		{
			Group group = null;
			Application application = null;
			switch (macroAction.Type)
			{
				case MacroActionType.Start:
				case MacroActionType.Stop:
				case MacroActionType.Restart:
					MacroProcessAction macroProcessAction = (MacroProcessAction) macroAction;
					Machine machine = Settings.Client.Machines.FirstOrDefault(x => x.ID == macroProcessAction.MachineID);
					if (machine != null && ConnectionStore.ConnectionCreated(machine))
					{
						group = ConnectionStore.Connections[machine].Configuration.Groups.FirstOrDefault(x => x.ID == macroProcessAction.GroupID);
						application = ConnectionStore.Connections[machine].Configuration.Applications.FirstOrDefault(x => x.ID == macroProcessAction.ApplicationID);
					}
					return string.Format("{0} / {1}", 
						group != null ? group.Name : macroProcessAction.GroupID.ToString(),
						application != null ? application.Name : macroProcessAction.ApplicationID.ToString());
				case MacroActionType.Distribute:
					MacroDistributionAction macroDistributionAction = (MacroDistributionAction) macroAction;
					Machine sourceMachine = Settings.Client.Machines.FirstOrDefault(x => x.ID == macroDistributionAction.SourceMachineID);
					if (sourceMachine != null && ConnectionStore.ConnectionCreated(sourceMachine))
					{
						group = ConnectionStore.Connections[sourceMachine].Configuration.Groups.FirstOrDefault(x => x.ID == macroDistributionAction.GroupID);
						application = ConnectionStore.Connections[sourceMachine].Configuration.Applications.FirstOrDefault(x => x.ID == macroDistributionAction.ApplicationID);
					}
					return string.Format("{0} / {1} / {2}",
						sourceMachine != null ? sourceMachine.HostName : macroDistributionAction.SourceMachineID.ToString(),
						group != null ? group.Name : macroDistributionAction.GroupID.ToString(),
						application != null ? application.Name : macroDistributionAction.ApplicationID.ToString());
				case MacroActionType.Wait:
					MacroWaitAction macroWaitAction = (MacroWaitAction) macroAction;
					if (!macroWaitAction.IsValid) throw new InvalidOperationException();
					switch (macroWaitAction.WaitForEvent)
					{
						case MacroActionWaitForEvent.Timeout:
							return string.Format("{0} for timeout, {1} ms", macroWaitAction.Type, macroWaitAction.TimeoutMilliseconds);
						case MacroActionWaitForEvent.PreviousActionsCompleted:
							return string.Format("{0} for previous actions completed", macroWaitAction.Type);
						default:
							throw new InvalidOperationException();
					}
				default:
					return string.Format("{0} ({1})", macroAction.Type, macroAction.ID);
			}
		}

		private void ApplyState()
		{
			switch (State)
			{
				case MacroActionState.Ongoing:
					pictureBoxStatus.Image = Properties.Resources.macro_ongoing_16;
					break;
				case MacroActionState.Success:
					pictureBoxStatus.Image = Properties.Resources.macro_success_16;
					break;
				case MacroActionState.Failure:
					pictureBoxStatus.Image = Properties.Resources.macro_failure_16;
					break;
				default:
					pictureBoxStatus.Image = Properties.Resources.macro_unknown_16;
					break;
			}
		}

		#endregion

		public bool Matches(Guid macroID, Guid macroActionID)
		{
			return Matches(MakeID(macroID, macroActionID));
		}

		public bool Matches(Guid id)
		{
			return (ID == id);
		}
	}
}
