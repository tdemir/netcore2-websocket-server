using System;

namespace netcore2_websocket_server.Model.WebSocket
{
    public class WebSocketDataModel<T>
    {
        public WebSocketDataModel()
        {
            ProcessName = "";
            CurrentDatetime = DateTime.UtcNow;
            HasError = false;
        }
        public string ProcessName { get; set; }
        public T Data { get; set; }

        public long CurrentDateUnixTime { get; set; }

        public DateTime CurrentDatetime
        {
            get
            {
                return CurrentDateUnixTime.EpochTimeToDatetime();
            }
            set
            {
                CurrentDateUnixTime = value.DatetimeToEpochTime();
            }
        }

        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }
}