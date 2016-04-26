using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    public Dictionary<Job.Type, List<Job>> pendingJobs;
    public List<Job> assignedJobs;

    void Awake()
    {
        pendingJobs = new Dictionary<Job.Type, List<Job>>();
        assignedJobs = new List<Job>();
    }

    public void QueueJob(Job j)
    {
        if (!pendingJobs.ContainsKey(j.type))
            pendingJobs[j.type] = new List<Job>();

        // if there's another job for this object, removes it
        Job.Type removeType = Job.Type.Chop;
        Job removeJob = null;
        bool removeAssigned = false;

        foreach (List<Job> q in pendingJobs.Values)
        {
            foreach (Job qj in q)
            {
                if (qj.target == j.target)
                {
                    removeType = qj.type;
                    removeJob = qj;
                    break;
                }
            }

            if (removeJob != null)
                break;
        }

        // searches for the assigned too
        if (removeJob == null)
        {
            foreach (Job q in assignedJobs)
            {
                if (q.target == j.target)
                {
                    removeType = q.type;
                    removeJob = q;
                    removeAssigned = true;
                    break;
                }
            }
        }
        
        if (removeJob != null)
        {
            Debug.Log("Removing previous job");

            if (removeAssigned)
            {
                assignedJobs.Remove(removeJob);
                UnassignJob(removeJob);
            } else
            {
                pendingJobs[removeType].Remove(removeJob);
            }
        }
        
        pendingJobs[j.type].Add(j);
        Debug.Log(string.Format("Job queued: {0}", j.type.ToString()));
    }

    public List<Job> GetPending(Job.Type allowedJob)
    {
        if (!pendingJobs.ContainsKey(allowedJob))
            return null;
        return pendingJobs[allowedJob];
    }

    public void AssignJob(Job j, BarbarianData owner)
    {
        if (!pendingJobs.ContainsKey(j.type) ||
            !pendingJobs[j.type].Contains(j) ||
            j.owner != null ||
            assignedJobs.Contains(j) ||
            owner == null)
        {
            Debug.LogError("Couldn't assign job, something's wrong");
            return;
        }
        
        pendingJobs[j.type].Remove(j);
        j.owner = owner;
        owner.currentJob = j;
        assignedJobs.Add(j);
    }

    public void UnassignJob(Job j)
    {
        j.owner.currentJob = null;
    }
}

public class Job
{
    public Type type;
    public ObjectData target;
    public BarbarianData owner;
    
    public Job() { }
    public Job(Type type, ObjectData target, BarbarianData owner)
    {
        this.type = type;
        this.target = target;
        this.owner = owner;
    }

    public enum Type
    {
        Idle,
        Wander,
        Chop,
    }
}