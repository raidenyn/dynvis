using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DynVis.EventsLog
{
    public static partial class Log
    {
        private const string MessageBoxTitle = "DynVis";

        public static readonly bool WriteLogFile = true;

        static Log()
        {
            if (WriteLogFile)
            {
                LogToFile.Init();
            }
        }

        public static void LogError(string message)
        {
            DoLog(new Event(message, EventType.Error));
        }

        public static void LogEvent(string message)
        {
            DoLog(new Event(message));
        }

        public static void LogResult(string message, bool result)
        {
            DoLog(new Event(message, result));
        }

        private static void DoLog(Event e)
        {
            Events.Add(e);
            if (OnNewEvent != null) OnNewEvent(new EventEventArgs(e));
        }

        public static void ShowError(IWin32Window owner, string message)
        {
            MessageBox.Show(owner, message, MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            LogError(message);
        }

        public static void ShowMessage(IWin32Window owner,string message)
        {
            MessageBox.Show(owner, message, MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            LogEvent(message);
        }

        public readonly static List<Event> Events = new List<Event>();

        public delegate void EventHandler(EventEventArgs e);

        public static event EventHandler OnNewEvent;

        public new static string ToString()
        {
            var logText = new StringBuilder();
            foreach (var ev in Events)
            {
                logText.AppendLine(ev.ToString());
            }
            return logText.ToString();
        }
    }

    public class Event
    {
        private const string Error = " Ошибка";

        public readonly string Message;
        public readonly EventType EventType;
        public readonly DateTime CreateTime;
        public readonly bool? Result;

        public Event(string message, EventType eventType)
        {
            Message = message;
            EventType = eventType;
            CreateTime = DateTime.Now;
        }

        public Event(string message)
            : this(message, EventType.Notification)
        {
        }

        public Event(string message, bool result)
            : this(message, EventType.Result)
        {
            Result = result;
        }

        public override string ToString()
        {
            if (EventType == EventType.Result)
            {
                return String.Format("{0}{1}: {2,-60}  [{3}]", CreateTime, EventType == EventType.Error ? Error : "", Message, Result == null ? "" : (Result.Value ? "OK" : "FAILE"));
            }
            return String.Format("{0}{1}: {2}", CreateTime, EventType == EventType.Error ? Error : "", Message);
        }
    }

    public enum EventType
    {
        Notification,
        Error,
        Result
    }

    public class EventEventArgs: EventArgs
    {
        public EventEventArgs(Event e)
        {
            Event = e;
        }

        public Event Event;
    } 
}
