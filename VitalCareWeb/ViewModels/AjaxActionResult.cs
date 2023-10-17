namespace VitalCareWeb.ViewModels
{
	public class AjaxActionResult
	{
		/// <summary>
		/// whether the action was successful or not 
		/// </summary>
		public bool IsSuccessful { get; set; }

		/// <summary>
		/// message to show (type would depend on IsSuccessful - eg: toastr["success"] or toastr["error"]) 
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// if a js event needs to be triggered - the event name
		/// </summary>
		public string TriggerEventName { get; set; }

		/// <summary>
		/// data for js event (TriggerEventName) in json format
		/// </summary>
		public string TriggerEventData { get; set; }

		/// <summary>
		/// if a modal needs to be hidden - id of the modal
		/// </summary>
		public string HideModalId { get; set; }

		/// <summary>
		/// reload page (with block UI)
		/// </summary>
		public bool IsReloadPage { get; set; }

		public AjaxActionResult(bool isSuccessful, string message)
		{
			IsSuccessful = isSuccessful;
			Message = message;
		}

		public AjaxActionResult(bool isSuccessful, string message, string hideModalId = "", bool isReloadPage = false)
		{
			IsSuccessful = isSuccessful;
			Message = message;
			HideModalId = hideModalId;
			IsReloadPage = isReloadPage;
		}

		public AjaxActionResult(bool isSuccessful, string message, string hideModalId = "", bool isReloadPage = false, string triggerEventName = "", string triggerEventData = "")
		{
			IsSuccessful = isSuccessful;
			Message = message;
			HideModalId = hideModalId;
			TriggerEventName = triggerEventName;
			TriggerEventData = triggerEventData;
			IsReloadPage = isReloadPage;
		}
	}
}
