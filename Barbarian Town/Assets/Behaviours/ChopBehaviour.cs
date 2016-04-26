using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(ObjectData))]
public class ChopBehaviour : BaseBehaviour, ITickable
{
    ObjectData data;

    float cooldown = 0;

    void Awake()
    {
        data = GetComponent<ObjectData>();
    }

    public void Tick()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return;
        }

        cooldown = 1;

        Debug.Log("Chopping...");
    }
}
