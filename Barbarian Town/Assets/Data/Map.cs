using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Map
{
    public int width;
    public int height;
    public Dictionary<int, int, List<ObjectData>> canvas;
    public List<ITickable> tickableObjects;

    public Map(int w, int h)
    {
        width = w;
        height = h;

        canvas = new Dictionary<int, int, List<ObjectData>>();
        EachXY(i => canvas[i] = new List<ObjectData>());

        tickableObjects = new List<ITickable>();
    }

    public ObjectData AddObject(string obj, Vector2 pos, int order)
    {
        ObjectData d = Instantiator.Create(obj, pos, order);
        canvas[(int)pos.x, (int)pos.y].Add(d);

        // adds tickable components if there are any
        ITickable tickable = d.gameObject.GetComponent<ITickable>();
        if (tickable != null)
        {
            tickableObjects.Add(tickable);
        }

        return d;
    }

    public void RemoveObject(ObjectData objectData)
    {
        canvas[objectData.x, objectData.y].Remove(objectData);
        GameObject.Destroy(objectData.gameObject);
    }

    public void EachXY(Action<Tuple<int, int>> action)
    {
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                action(new Tuple<int, int>(x, y));
    }

    public bool Passable(int x, int y)
    {
        bool passable = true;
        List<ObjectData> objs = canvas[x, y];
        foreach (ObjectData obj in objs)
            if (obj != null && !obj.passable)
                passable = false;
        return passable;
    }
}
