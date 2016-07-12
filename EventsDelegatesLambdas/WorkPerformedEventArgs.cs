using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelegatesLambdas
{
    public enum WorkType
    {
        GoToMeetings,
        Golf,
        GenerateReports
    }

    public class WorkPerformedEventArgs : EventArgs
    {
        public WorkPerformedEventArgs(int hours, WorkType workType)
        {
            Hours = hours;
            WorkType = workType;
        }

        public int Hours { get; set; }
        public WorkType WorkType { get; set; }
    }
}
