using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(BarbarianData))]
public class BarbarianBehaviour : BaseBehaviour, ITickable
{
    BarbarianData data;

    void Awake()
    {
        data = GetComponent<BarbarianData>();

        foreach (Job.Type job in data.allowedJobs)
        {
            switch (job)
            {
                case Job.Type.Chop:
                    gameObject.AddComponent<ChopBehaviour>();
                    break;
            }
        }
    }

    public void Tick()
    {
        if (data.currentJob == null || data.currentJob.type == Job.Type.Idle)
            FindJob();

        switch (data.currentJob.type)
        {
            case Job.Type.Chop:
                ChopBehaviour chop = GetComponent<ChopBehaviour>();
                if (chop != null)
                    chop.Tick();
                break;
        }
    }

    public override void Perform(KeyCode code)
    {
        switch (code)
        {
            case KeyCode.X:
                Debug.Log(string.Format("Current job: {0}", data.currentJob != null ? data.currentJob.type.ToString() : "null"));
                break;
        }
    }

    void FindJob()
    {
        foreach (Job.Type allowedJob in data.allowedJobs)
        {
            List<Job> jobs = Global.jobManager.GetPending(allowedJob);
            if (jobs != null && jobs.Count > 0)
            {
                Global.jobManager.AssignJob(jobs[0], data);
                Debug.Log("Assign!");
                break;
            }
        }

        // still no job, just be idle or wander
        if (data.currentJob == null)
        {
            bool wander = Global.random.Next(0, 4) == 3; // 25%
            if (wander)
                data.currentJob = new Job(Job.Type.Wander, null, data);
            else
                data.currentJob = new Job(Job.Type.Idle, null, data);
        } 
    }
}
