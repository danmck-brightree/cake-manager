namespace CakeManager.Shared
{
    public class CakeMarkResult
    {
        public enum StatusType { Okay, ExpiredData, LimitReached, Exception }

        public StatusType Status { get; set; } = StatusType.Okay;

        public bool Success { get; set; }

        public string GetStatusMessage(string defaultMessage)
        {
            var message = string.Empty;

            switch (Status)
            {
                case StatusType.ExpiredData:
                    message = "The cake mark data you were viewing had expired. Please try again.";
                    break;
                case StatusType.LimitReached:
                    message = "You have reached the cake mark limit.";
                    break;
                case StatusType.Exception:
                    message = "The system is down.";
                    break;
                default:
                    message = defaultMessage;
                    break;
            }

            return message;
        }
    }
}
