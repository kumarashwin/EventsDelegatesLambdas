using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelegatesLambdas
{
    public class Worker
    {
        public event EventHandler<WorkPerformedEventArgs> workPerformed;
        public event EventHandler workCompleted;

        public void DoWork(int hours, WorkType workType)
        {
            //Check if event has delegates or is empty
            if (workPerformed != null)
            {
                //Raise event for each hour
                for (int i = 1; i <= hours; i++)                 
                    workPerformed(this, new WorkPerformedEventArgs(i, workType));

                //Raise event once work completed
                workCompleted(this, EventArgs.Empty);
            }              
        }

        #region Explicit code to help me understand

        //Creates a delegate that can represent any method with the same signature
        //Similar structure to EventHandler, except EventHandler has the sender object parameter
        public delegate void WorkPerformedDelegate(WorkPerformedEventArgs e);

        //The method we will pass into the delegate defined above
        public static void TestWorkPerformed(WorkPerformedEventArgs e) =>
            Console.WriteLine($"From Test: Hours worked: {e.Hours}, doing: {e.WorkType}");

        //Creates an event that follows the mold of the delegate defined above
        public event WorkPerformedDelegate workPerformedEvent;

        /// <summary>
        /// More explicit definition for the same 'DoWork' method
        /// that takes a standard delegate encapsulating the
        /// WorkPerformedEventArgs class that stores the data we
        /// work with
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="workType"></param>
        public void DoWorkExplicit(int hours, WorkType workType)
        {
            //Check if event has delegates or is empty
            if (workPerformedEvent != null)
            {
                //Raise event for each hour
                for (int i = 1; i <= hours; i++)
                    workPerformedEvent(new WorkPerformedEventArgs(i, workType));
            }
        }
        #endregion
    }
}
