namespace Trellendar.WebSite.Modules._Shared
{
    public class AjaxResponse
    {
        public bool Success { get; set; }

        public dynamic Data { get; set; }

        public string ErrorMessage { get; set; }
    }
}