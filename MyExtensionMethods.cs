using System;

namespace netcore2_websocket_server
{
    public static class MyExtensionMethods
    {
        public static long DatetimeToEpochTime(this DateTime obj)
        {
            var sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(obj - sTime).TotalSeconds;
        }

        public static DateTime EpochTimeToDatetime(this long obj)
        {
            var sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return sTime.AddSeconds(obj);
        }

        public static string Serialize(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        public static T Deserialize<T>(this string obj)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(obj);
        }
    }
}