using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(ObjectData))]
public class ChopBehaviour : BaseBehaviour
{
    ObjectData data;

    void Awake()
    {
        data = GetComponent<ObjectData>();
    }

    public override void Perform(KeyCode code)
    {
        Debug.Log("Chop this!");
    }
}
