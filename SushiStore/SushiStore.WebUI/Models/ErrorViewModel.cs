namespace SushiStore.WebUI.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

namespace SushiStore
{
    public class WebUI
    {
    }
}