using System;
using System.Linq;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            TimeSpan time = GetTimeSpan(aTime);

            return string.Format(
                "{0}{5}" +
                "{1}{5}" +
                "{2}{5}" +
                "{3}{5}" +
                "{4}",
                time.Seconds % 2 == 1 ? "O" : "Y",
                SetupLineWith4Lamps((int)time.TotalHours / 5, "R"),
                SetupLineWith4Lamps((int)time.TotalHours % 5, "R"),
                SetupLineWith11Lamps(time.Minutes / 5),
                SetupLineWith4Lamps(time.Minutes % 5, "Y"),
                Environment.NewLine);
        }

        private static TimeSpan GetTimeSpan(string aTime)
        {
            //TimeSpan.ParseExact() is not working wiht 24:00:00
            var parts = aTime.Split(':').Select(i => int.Parse(i)).ToArray();

            return new TimeSpan(parts[0], parts[1], parts[2]);
        }

        private string SetupLineWith4Lamps(int count, string filler)
        {
            string output = "";
            for (int i = 0; i < count; i++)
            {
                output += filler;
            }

            return (output + "OOOO").Substring(0, 4);
        }

        private string SetupLineWith11Lamps(int count)
        {
            string output = "";
            for (int i = 1; i <= count; i++)
            {
                output += i % 3 == 0 ? "R" : "Y";
            }

            return (output + "OOOOOOOOOOO").Substring(0, 11);
        }
    }
}