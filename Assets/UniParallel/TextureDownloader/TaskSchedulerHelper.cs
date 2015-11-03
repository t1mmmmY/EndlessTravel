using System;
using UniParallelGeneric;

public static class TaskSchedulerHelper
{
    private static PTaskScheduler mTaskScheduler = PTaskScheduler.Create();

    public static PTaskScheduler GetTaskScheduler()
    {
        return mTaskScheduler;
    }
}
