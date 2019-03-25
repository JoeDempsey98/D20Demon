using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace DiscordDnDBot.Core.DateAndTime
{
    internal static class DateAndTimeManagement
    {
        private static string path = "Core/DateAndTime/DateAndTime.json";
        private static Dictionary<string, string> pairs;
        public static string currentDate;

        static DateAndTimeManagement()
        {
            pairs = new Dictionary<string, string>();
        }

        public static void UpdateDate()
        {
            currentDate = DateTime.Now.ToShortDateString();
        }
        public static string LoadDate()
        {
            if (File.ReadAllText(path) == "")
            {
                UpdateDate();
                return currentDate;
            }
            string data = File.ReadAllText(path);
            currentDate = JsonConvert.DeserializeObject<Dictionary<string, string>>(data).GetValueOrDefault("Date");
            return currentDate;
        }
        public static void SaveDate()
        {
            UpdateDate();
            pairs.Add("Date", currentDate);
            string json = JsonConvert.SerializeObject(pairs);
            File.WriteAllText(path, json);
        }
    }
}
