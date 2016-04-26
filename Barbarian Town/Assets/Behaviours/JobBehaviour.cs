using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class JobBehaviour : BaseBehaviour
{
    public Job.Type type;

    public override void Perform(KeyCode code)
    {
        Job job = new Job();
        job.target = GetComponent<ObjectData>();
        job.type = type;

        Global.jobManager.QueueJob(job);
    }
}