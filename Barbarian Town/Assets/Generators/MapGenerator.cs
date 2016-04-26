using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MapGenerator
{
    static System.Random random;

    public static Map CreateMap(int w, int h, string seed = null)
    {
        if (seed == null)
            seed = DateTime.Now.Ticks.ToString();
        random = new System.Random(seed.GetHashCode());

        Map map = new Map(w, h);

        // grass base
        map.EachXY(i => map.canvas[i][0] = ETile.Grass);

        Dictionary<Vector2, ETile> objs = new Dictionary<Vector2, ETile>();

        // trees
        int treeN = random.Next(w * h / 15, w * h / 10);
        while (treeN > 0)
        {
            Vector2 v = new Vector2(random.Next(0, w), random.Next(0, h));
            if (objs.ContainsKey(v))
                continue;
            
            objs[v] = ETile.Tree;
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
                {
                    objs.Remove(p);
                }
            }
        }

        // adds bonfire
        objs.Add(new Vector2(h / 2, w / 2), ETile.Bonfire);

        // draws objects
        foreach (KeyValuePair<Vector2, ETile> pair in objs)
        {
            map.canvas[(int)pair.Key.x, (int)pair.Key.y][1] = pair.Value;
        }
        
        return map;
    }
}
