using System.Collections.Generic;
using System.Linq;
using ProcessManager.DataObjects.Comparers;

namespace ProcessManager.DataObjects
{
	public class MachineList : List<Machine>
	{
		public new void Add(Machine item)
		{
			base.Add(item);
			List<Machine> machines = this.OrderBy(machine => machine.HostName).ToList();
			Clear();
			AddRange(machines);
		}

		public new void Sort()
		{
			Sort(new MachineEqualityComparer());
		}
	}
}
