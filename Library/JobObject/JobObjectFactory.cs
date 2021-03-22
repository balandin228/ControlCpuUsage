using System;
using System.Collections.Generic;
using System.Text;
using Fs.Processes.JobObjects;

namespace Library.JobObject
{
    public static class JobObjectFactory
    {
        public static Fs.Processes.JobObjects.JobObject CreateJob(decimal cpuLimit, RateControlInterval interval)
        {
            var cpuRateLimit = new CpuRateLimit(cpuLimit, true);
            var jobLimits = new JobLimits()
            {
                CpuRate = cpuRateLimit
            };
            return new Fs.Processes.JobObjects.JobObject(jobLimits, null);
        }
    }
}
