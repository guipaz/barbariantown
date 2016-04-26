using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BarbarianBehaviour : BaseBehaviour, ITickable
{
    ObjectData data;
    
    float moveCooldown = 0;

    void Awake()
    {
        data = GetComponent<ObjectData>();
    }

    public void Tick()
    {
        if (moveCooldown > 0)
        {
            moveCooldown -= Time.deltaTime;
            return;
        }

        int h = Global.random.Next(-1, 2);
        int v = Global.random.Next(-1, 2);
        Vector2 pos = transform.position + new Vector3(h, v, 0);
        Global.mapManager.MoveObject(data, pos);

        moveCooldown = Global.random.Next(1, 4);
    }
}
