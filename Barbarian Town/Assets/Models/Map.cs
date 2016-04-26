using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Map
{
    public int width;
    public int height;
    public Dictionary<int, int, ETile[]> canvas;

    public Map(int w, int h)
    {
        width = w;
        height = h;

        canvas = new Dictionary<int, int, ETile[]>();
        EachXY(i => canvas[i] = new ETile[5]);
    }

    public void EachXY(Action<Tuple<int, int>> action)
    {
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                action(new Tuple<int, int>(x, y));
    }
}
