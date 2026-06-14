using System;

namespace KarateMVVMApp.Models.Options
{
    public class ResetTimeOption
    {
        public string Display { get; }
        public TimeSpan Value { get; }

        public ResetTimeOption(string display, TimeSpan value)
        {
            Display = display;
            Value = value;
        }
    }
}
