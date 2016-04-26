using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    public Map map { get; private set; }
    
    public void Create(int w, int h, string seed = null)
    {
        if (seed == null)
            seed = DateTime.Now.Ticks.ToString();
        Global.random = new System.Random(seed.GetHashCode());

        map = new Map(w, h);

        // grass base
        map.EachXY(i => map.AddObject("ground/grass", new Vector2(i.Item1, i.Item2), 0));

        Dictionary<Vector2, ObjectData> objs = new Dictionary<Vector2, ObjectData>();

        // trees
        int treeN = Global.random.Next(w * h / 15, w * h / 10);
        while (treeN > 0)
        {
            Vector2 v = new Vector2(Global.random.Next(0, w), Global.random.Next(0, h));
            if (objs.ContainsKey(v))
                continue;

            objs[v] = map.AddObject("resource/tree", v, 1);
            treeN--;
        }

        // clear the 5x5 middle
        int yStart = h / 2 - 3;
        int xStart = w / 2 - 3;
        for (int y = yStart; y < yStart + 7; y++)
        {
            for (int x = xStart; x < xStart + 7; x++)
            {
                Vector2 p = new Vector2(x, y);
                if (objs.ContainsKey(p))
                    map.RemoveObject(objs[p]);
            }
        }

        // adds bonfire
        Vector2 pos = new Vector2(w / 2, h / 2);
        map.AddObject("building/bonfire", pos, 1);

        // spawns initial barbarians
        int spawned = 0;
        while (spawned < 5)
        {
            Vector2 p = new Vector2(Global.random.Next(w / 2 - 3, w / 2 + 4), Global.random.Next(h / 2 - 3, h / 2 + 4));
            if (!map.Passable((int)p.x, (int)p.y))
                continue;

            map.AddObject("character/barbarian", p, 1);
            spawned++;
        }
    }

    /// <summary>
    /// Returns the topmost object of the selected coordinate
    /// </summary>
    /// <param name="pos">position in the canvas</param>
    /// <returns></returns>
    public ObjectData GetObject(Vector2 pos)
    {
        if (pos.x < 0 || pos.x >= map.width || pos.y < 0 || pos.y >= map.height)
            return null;
        
        List<ObjectData> objs = map.canvas[(int)pos.x, (int)pos.y];
        if (objs.Count > 0)
            return objs[objs.Count - 1];
        return null;
    }

    public void MoveObject(ObjectData data, Vector2 pos)
    {
        if (!map.Passable((int)pos.x, (int)pos.y))
            return;

        map.canvas[data.x, data.y].Remove(data);
        map.canvas[(int)pos.x, (int)pos.y].Add(data);

        data.gameObject.transform.position = pos;
    }
}
