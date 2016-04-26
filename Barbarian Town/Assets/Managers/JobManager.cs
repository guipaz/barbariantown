using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    Dictionary<Job.Type, List<Job>> pendingJobs;
    List<Job> assignedJobs;

    void Awake()
    {
        pendingJobs = new Dictionary<Job.Type, List<Job>>();
    }

    public void QueueJob(Job j)
    {
        if (!pendingJobs.ContainsKey(j.type))
            pendingJobs[j.type] = new List<Job>();
        pendingJobs[j.type].Add(j);
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
        assignedJobs.Add(j);
    }
}

public class Job
{
    public Type type;
    public ObjectData target;
    public BarbarianData owner;

    public enum Type
    {
        ChopTree,
    }
}