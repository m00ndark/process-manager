using System.Runtime.Serialization;
using ProcessManager.DataObjects;

namespace ProcessManager.Service.DataObjects
{
	[DataContract]
	public class DTOProcessActionResult
	{
		public DTOProcessActionResult(ProcessActionResult processActionResult)
		{
			Type = processActionResult.Type;
			ErrorMessage = processActionResult.ErrorMessage;
			Status = processActionResult.Status != null ? new DTOProcessStatus(processActionResult.Status) : null;
		}

		#region Properties

		[DataMember]
		public ActionType Type { get; private set; }

		[DataMember]
		public string ErrorMessage { get; private set; }

		[DataMember]
		public DTOProcessStatus Status { get; private set; }

		#endregion

		public ProcessActionResult FromDTO(Machine machine)
		{
			return new ProcessActionResult(Type, ErrorMessage, Status?.FromDTO(machine));
		}
	}
}
