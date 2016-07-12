using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelegatesLambdas
{
    //public delegate int WorkPerformedHandler(object sender, WorkPerformedEventArgs e);

    public class Worker
    {
        public event EventHandler<WorkPerformedEventArgs> workPerformed;
        public event EventHandler workCompleted;

        public Worker()
        {
            //workPerformed += (add WorkPerformedEventArgs delegates that encapsulate methods);
        }

        public void DoWork(int hours, WorkType workType)
        {
            for (int i = 0; i < hours; i++)
            {
                OnWorkPerformed(i+1, workType);
                //Raise event for each hour
            }
            OnWorkCompleted();
            //Raise event for work completed
           
        }

        protected virtual void OnWorkPerformed(int hours, WorkType workType)
        {
            if (workPerformed != null)
                workPerformed(this,
                    new WorkPerformedEventArgs(hours, workType));
        }

        protected virtual void OnWorkCompleted()
        {
            if (workCompleted != null)
                workCompleted(this, EventArgs.Empty);
        }

    }
}
