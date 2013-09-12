using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ProcessManager;
using ProcessManager.DataObjects;
using ProcessManager.EventArguments;
using ProcessManagerUI.Controls.Nodes;

namespace ProcessManagerUI
{
	public class MacroPlayer
	{
		#region MacroPlaybackContainer class

		private class MacroPlaybackContainer
		{
			public MacroPlaybackContainer(Macro macro, List<PlayableMacroAction> playableMacroActions)
			{
				Macro = macro;
				PlayableMacroActions = playableMacroActions;
			}

			public Macro Macro { get; private set; }
			public List<PlayableMacroAction> PlayableMacroActions { get; private set; }
		}

		#endregion

		#region PlayableMacroAction class

		private class PlayableMacroAction
		{
			public PlayableMacroAction(Macro macro, IMacroAction macroAction, IAction action = null)
			{
				Macro = macro;
				MacroAction = macroAction;
				Action = action;
				MacroActionState = MacroActionState.Unknown;
			}

			public Macro Macro { get; private set; }
			public IMacroAction MacroAction { get; private set; }
			public IAction Action { get; private set; }
			public MacroActionState MacroActionState { get; set; }
		}

		#endregion

		private readonly IDictionary<Guid, MacroPlaybackContainer> _macroPlaybackContainers;

		public MacroPlayer(IControlPanel controlPanel)
		{
			ControlPanel = controlPanel;
			_macroPlaybackContainers = new Dictionary<Guid, MacroPlaybackContainer>();
			ControlPanel.DistributionCompleted += ProcessManagerServiceEventHandler_DistributionCompleted;
		}

		#region Properties

		private IControlPanel ControlPanel { get; set; }

		#endregion

		#region Event handlers

		private void ProcessManagerServiceEventHandler_DistributionCompleted(object sender, DistributionResultEventArgs e)
		{
			Machine sourceMachine = new Machine(e.DistributionResult.SourceMachineHostName);
			Machine destinationMachine = new Machine(e.DistributionResult.DestinationMachineHostName);
			List<PlayableMacroAction> matchingPlayableMacroActions = new List<PlayableMacroAction>();
			lock (_macroPlaybackContainers)
			{
				matchingPlayableMacroActions.AddRange(_macroPlaybackContainers.Values
					.SelectMany(container => container.PlayableMacroActions
						.Where(playableMacroAction => playableMacroAction.MacroAction.Type == MacroActionType.Distribute)
						.Select(playableMacroAction => new
							{
								MacroAction = (MacroDistributionAction) playableMacroAction.MacroAction,
								PlayableMacroAction = playableMacroAction
							})
						.Where(x => x.MacroAction.SourceMachineID == sourceMachine.ID
							&& x.MacroAction.GroupID == e.DistributionResult.GroupID
							&& x.MacroAction.ApplicationID == e.DistributionResult.ApplicationID
							&& x.MacroAction.DestinationMachineID == destinationMachine.ID)
						.Select(x => x.PlayableMacroAction)));

				matchingPlayableMacroActions.ForEach(playableMacroAction => playableMacroAction.MacroActionState = Convert(e.DistributionResult.Result));
			}
			matchingPlayableMacroActions.ForEach(playableMacroAction =>
				ControlPanel.ApplyMacroActionState(playableMacroAction.Macro.ID, playableMacroAction.MacroAction.ID, playableMacroAction.MacroActionState));
		}

		#endregion

		public void Play(Macro macro, List<IMacroAction> macroActions)
		{
			new Thread(() => PlayThread(macro, macroActions)).Start();
		}

		private void PlayThread(Macro macro, List<IMacroAction> macroActions)
		{
			lock (_macroPlaybackContainers)
			{
				if (_macroPlaybackContainers.ContainsKey(macro.ID))
					return;

				_macroPlaybackContainers.Add(macro.ID, null);
			}

			macroActions.ForEach(macroAction => ControlPanel.ApplyMacroActionState(macro.ID, macroAction.ID, MacroActionState.Unknown));

			List<PlayableMacroAction> playableMacroActions = macroActions.Select(macroAction =>
				{
					if (macroAction.Type == MacroActionType.Wait)
						return new PlayableMacroAction(macro, macroAction);

					IAction action = CreateAction(macroAction);
					return action != null ? new PlayableMacroAction(macro, macroAction, action) : null;
				}).Where(x => x != null).ToList();

			MacroPlaybackContainer container = new MacroPlaybackContainer(macro, playableMacroActions);

			lock (_macroPlaybackContainers)
				_macroPlaybackContainers[macro.ID] = container;

			container.PlayableMacroActions.ForEach(PlayMacroAction);

			lock (_macroPlaybackContainers)
				_macroPlaybackContainers.Remove(macro.ID);
		}

		private void PlayMacroAction(PlayableMacroAction playableMacroAction)
		{
			ControlPanel.ApplyMacroActionState(playableMacroAction.Macro.ID, playableMacroAction.MacroAction.ID, MacroActionState.Ongoing);
			if (playableMacroAction.MacroAction.Type == MacroActionType.Wait)
			{
				MacroWaitAction macroWaitAction = (MacroWaitAction) playableMacroAction.MacroAction;
				switch (macroWaitAction.WaitForEvent)
				{
					case MacroActionWaitForEvent.Timeout:
						{
							Thread.Sleep(macroWaitAction.TimeoutMilliseconds);
						}
						break;
					case MacroActionWaitForEvent.PreviousActionCompleted:
						{
							List<PlayableMacroAction> previousPlayableMacroActions = new List<PlayableMacroAction>();
							MacroPlaybackContainer container = _macroPlaybackContainers[playableMacroAction.Macro.ID];
							foreach (PlayableMacroAction previousPlayableMacroAction in container.PlayableMacroActions.TakeWhile(x => x.MacroAction.ID != playableMacroAction.MacroAction.ID))
							{
								if (previousPlayableMacroAction.MacroAction.Type == MacroActionType.Wait)
									previousPlayableMacroActions.Clear();
								else
									previousPlayableMacroActions.Add(previousPlayableMacroAction);
							}
							bool anyOngoing = true;
							while (anyOngoing)
							{
								lock (_macroPlaybackContainers)
								{
									anyOngoing = previousPlayableMacroActions.Aggregate(false, (current, previousPlayableMacroAction) =>
										current | (previousPlayableMacroAction.MacroActionState == MacroActionState.Ongoing));
								}
								if (anyOngoing)
									Thread.Sleep(100);
							}
						}
						break;
				}
				ControlPanel.ApplyMacroActionState(playableMacroAction.Macro.ID, playableMacroAction.MacroAction.ID, MacroActionState.Success);
			}
			else
			{
				bool actionSuccess = ControlPanel.TakeAction(playableMacroAction.Action);
				lock (_macroPlaybackContainers)
				{
					playableMacroAction.MacroActionState = !actionSuccess
						? MacroActionState.Failure
						: playableMacroAction.MacroAction.Type == MacroActionType.Start || playableMacroAction.MacroAction.Type == MacroActionType.Stop || playableMacroAction.MacroAction.Type == MacroActionType.Restart
							? MacroActionState.Success
							: MacroActionState.Ongoing;
				}
				ControlPanel.ApplyMacroActionState(playableMacroAction.Macro.ID, playableMacroAction.MacroAction.ID, playableMacroAction.MacroActionState);
			}
		}

		private static IAction CreateAction(IMacroAction macroAction)
		{
			switch (macroAction.Type)
			{
				case MacroActionType.Start:
				case MacroActionType.Stop:
				case MacroActionType.Restart:
					{
						MacroProcessAction macroProcessAction = (MacroProcessAction) macroAction;
						Machine machine = Settings.Client.Machines.FirstOrDefault(x => x.ID == macroProcessAction.MachineID);
						if (machine != null && ConnectionStore.ConnectionCreated(machine))
						{
							Group group = ConnectionStore.Connections[machine].Configuration.Groups.FirstOrDefault(x => x.ID == macroProcessAction.GroupID);
							Application application = ConnectionStore.Connections[machine].Configuration.Applications.FirstOrDefault(x => x.ID == macroProcessAction.ApplicationID);
							return new ProcessAction(Convert(macroAction.Type), machine, @group, application);
						}
						return null;
					}
				case MacroActionType.Distribute:
					{
						MacroDistributionAction macroDistributionAction = (MacroDistributionAction) macroAction;
						Machine sourceMachine = Settings.Client.Machines.FirstOrDefault(x => x.ID == macroDistributionAction.SourceMachineID);
						Machine destinationMachine = Settings.Client.Machines.FirstOrDefault(x => x.ID == macroDistributionAction.DestinationMachineID);
						if (sourceMachine != null && ConnectionStore.ConnectionCreated(sourceMachine))
						{
							Group group = ConnectionStore.Connections[sourceMachine].Configuration.Groups.FirstOrDefault(x => x.ID == macroDistributionAction.GroupID);
							Application application = ConnectionStore.Connections[sourceMachine].Configuration.Applications.FirstOrDefault(x => x.ID == macroDistributionAction.ApplicationID);
							return new DistributionAction(Convert(macroAction.Type), sourceMachine, group, application, destinationMachine);
						}
						return null;
					}
				default:
					return null;
			}
		}

		//private static List<List<IMacroAction>> ChunkMacroActions(IEnumerable<IMacroAction> macroActions)
		//{
		//	int chunkNo = 0;
		//	List<List<IMacroAction>> chunkedMacroActions = new List<List<IMacroAction>>();
		//	foreach (IMacroAction macroAction in macroActions)
		//	{
		//		if (chunkedMacroActions[chunkNo] == null)
		//			chunkedMacroActions[chunkNo] = new List<IMacroAction>();

		//		chunkedMacroActions[chunkNo].Add(macroAction);

		//		if (macroAction.Type == MacroActionType.Wait)
		//			chunkNo++;
		//	}
		//	return chunkedMacroActions;
		//}

		private static ActionType Convert(MacroActionType macroActionType)
		{
			switch (macroActionType)
			{
				case MacroActionType.Start:
					return ActionType.Start;
				case MacroActionType.Stop:
					return ActionType.Stop;
				case MacroActionType.Restart:
					return ActionType.Restart;
				case MacroActionType.Distribute:
					return ActionType.Distribute;
				default:
					throw new ArgumentException("No matching ActionType available");
			}
		}

		private static MacroActionState Convert(DistributionResultValue distributionResultValue)
		{
			switch (distributionResultValue)
			{
				case DistributionResultValue.Success:
					return MacroActionState.Success;
				case DistributionResultValue.Failure:
					return MacroActionState.Failure;
				default:
					throw new ArgumentException("No matching MacroActionState available");
			}
		}
	}
}
